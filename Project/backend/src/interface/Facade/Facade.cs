using Interface;
using Business;
using DataBase;
using System.Text.Json;

namespace Interface {

    public class Facade : IFacade {

        private IUserManager _users;
        private ReaderWriterLockSlim rwLock = new();

        /// <summary>
        /// Write lock of Facade
        /// </summary>
        private class FacadeWriteLock : IDisposable {
            private readonly ReaderWriterLockSlim _lock;

            public FacadeWriteLock(ReaderWriterLockSlim Lock) {
                _lock = Lock;
                _lock.EnterWriteLock();
            }

            public void Dispose() {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Read lock of Facade
        /// </summary>
        private class FacadeReadLock : IDisposable {
            private readonly ReaderWriterLockSlim _lock;

            public FacadeReadLock(ReaderWriterLockSlim Lock) {
                _lock = Lock;
                _lock.EnterReadLock();
            }

            public void Dispose() {
                _lock.ExitReadLock();
            }
        }

        public Facade() {

            if (DAO.InitDataBase() == false)
                throw new FacadeException("Database not loaded");

            this._users = new UserManager();
        }

        /// <summary>
        /// Gets the information of an User
        /// </summary>
        /// <param name="ID">ID of User</param>
        /// <returns></returns>
        public RouterPacket GetUser(string ID) {
            
            using var readLock = new FacadeReadLock(this.rwLock);

            if (this._users.ContainsUser(ID) == false)
                return new RouterPacket(404);

            UserComplete user = this._users.GetUser(ID);
            return new RouterPacket(200,user.ToJSONString());

        }

        /// <summary>
        /// Adds a new User into the system
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="Email"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="BirthDate"></param>
        /// <param name="Sex"></param>
        /// <param name="Passport"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Address"></param>
        /// <param name="AccountCreation"></param>
        /// <param name="PayMethod"></param>
        /// <param name="AccountStatus"></param>
        /// <returns></returns>
        public RouterPacket AddUser(string? ID, string? Name, string? Email, string? PhoneNumber, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? Address, string? AccountCreation, string? PayMethod, string? AccountStatus) {
            
            using var writeLock = new FacadeWriteLock(this.rwLock);

            var required_fields = new (string field, string? value)[] {
                ("id",ID), ("name",Name), ("email",Email), ("phone_number",PhoneNumber), ("birth_date",BirthDate), 
                ("sex",Sex), ("passport",Passport), ("country_code",CountryCode), ("address",Address), 
                ("account_creation",AccountCreation), ("pay_method",PayMethod), ("account_status",AccountStatus)
            };

            foreach (var (field,value) in required_fields)
                if (value == null)
                    return new RouterPacket(400,JsonSerializer.Serialize(new {
                        added = false, 
                        error_message = $"{field}-not-exists"
                        }));

            (bool wasAdded, string? errorMessage,UserComplete? user_added) = this._users.AddUser(ID!, Name!, Email!, PhoneNumber!, BirthDate!, Sex!, Passport!, CountryCode!, Address!, AccountCreation!, PayMethod!, AccountStatus!);

            if (wasAdded == true)
                return new RouterPacket(201,JsonSerializer.Serialize(new {
                    added = true,
                    user = user_added!.ToJSONString()
                }));

            else
                return new RouterPacket(422,JsonSerializer.Serialize(new {
                    added = false,
                    error_message = errorMessage
                }));

        }

        /// <summary>
        /// Deletes a User identified by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public RouterPacket DeleteUser(string ID) {

            using var writeLock = new FacadeWriteLock(this.rwLock);

            if (this._users.ContainsUser(ID) == false)
                return new RouterPacket(404);
            

            (bool wasRemoved, UserComplete user_removed) = this._users.DeleteUser(ID);
            return new RouterPacket(wasRemoved ? 200 : 202, JsonSerializer.Serialize(new {
                removed = wasRemoved,
                user = user_removed.ToJSONString()}));
            
        }

        /// <summary>
        /// Updates a User identified by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="Email"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="BirthDate"></param>
        /// <param name="Sex"></param>
        /// <param name="Passport"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Address"></param>
        /// <param name="AccountCreation"></param>
        /// <param name="PayMethod"></param>
        /// <param name="AccountStatus"></param>
        /// <returns></returns>
        public RouterPacket UpdateUser(string ID, string? Name, string? Email, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? AccountStatus) {
            
            using var writeLock = new FacadeWriteLock(this.rwLock);

            if (this._users.ContainsUser(ID) == false)
                return new RouterPacket(404);

            (bool wasUpdated, string? errorMessage, UserComplete user_updated) = this._users.UpdateUser(ID,Name,Email,BirthDate,Sex,Passport,CountryCode,AccountStatus);

            if (wasUpdated == true)
                return new RouterPacket(200,JsonSerializer.Serialize(new {
                    updated = true,
                    user = user_updated.ToJSONString()
                }));

            else
                return new RouterPacket(422,JsonSerializer.Serialize(new {
                    updated = false,
                    error_message = errorMessage
                }));

        }

        /// <summary>
        /// Gives a list of Users
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public RouterPacket GetUsers(int? page) {

            int user_page = page == null ? 0 : (int) page;
            IList<UserList> users = this._users.GetUsers(user_page,30);

            IList<object> users_in_json = [];

            foreach (UserList user in users) {

                users_in_json.Add(new {
                    id = user.ID,
                    name = user.Name,
                    country_code = user.CountryCode,
                    is_active = user.IsActive
                });

            }

            return new RouterPacket(200,JsonSerializer.Serialize(users_in_json));

        }

        /// <summary>
        /// Gives a list of Users which name has the given prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public RouterPacket GetUsersPrefix(string prefix, int? page) {

            int user_page = page == null ? 0 : (int) page;
            IList<UserPrefix> users = this._users.GetUsersPrefix(prefix,user_page,30);

            IList<object> users_in_json = [];

            foreach (UserPrefix user in users) {

                users_in_json.Add(new {
                    id = user.ID,
                    name = user.Name,
                });

            }

            return new RouterPacket(200,JsonSerializer.Serialize(users_in_json));

        }

        /// <summary>
        /// Gives metrics about what happened in a certain date
        /// </summary>
        /// <returns></returns>
        public RouterPacket GetMetrics(int? year, int? month) {

            int date_wanted = 0 + (year != null ? 1 : 0) + (year != null && month != null ? 1 : 0);
            IDictionary<int,Metrics> dates = new Dictionary<int,Metrics>();

            var users = date_wanted switch {
                1 => this._users.GetMetricsMonths((int) year!),
                2 => this._users.GetMetricsDays((int) year!, (int) month!),
                _ => this._users.GetMetricsYears()
            };

            foreach ( (int date, int quantity) in users) {

                if (dates.ContainsKey(date) == false)
                    dates[date] = new Metrics();

                dates[date].UsersCreated = quantity;

            }

            IList<object> metrics_json = [];
            string date_type = date_wanted switch {
                1 => "month",
                2 => "day",
                _ => "year"
            };

            foreach (var metric in dates.OrderBy(key_pair => key_pair.Key))
                metrics_json.Add(new Dictionary<string,int>{
                    {date_type, metric.Key},
                    {"users", metric.Value.UsersCreated},
                    {"reservations", metric.Value.ReservationsBooked},
                    {"flights", metric.Value.FlightsDepartured},
                    {"passengers", metric.Value.Passengers},
                    {"unique_passengers", metric.Value.UniquePassengers}
                });

            return new RouterPacket(200,JsonSerializer.Serialize(metrics_json));

        }

        /// <summary>
        /// Import users expecified in a CSV file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public RouterPacket ImportUsers(string? path) {

            using var writeLock = new FacadeWriteLock(this.rwLock);

            if (path == null)
                return new RouterPacket(422,JsonSerializer.Serialize(new {
                        imported = false,
                        error_message = "path-not-exists"
                    }));

                
            (bool was_imported, string? error_message, int? how_many_users_imported, int? how_many_users_failed) = this._users.ImportFile(path);

            if (was_imported == true)
                return new RouterPacket(201,JsonSerializer.Serialize(new {
                    imported = true,
                    users_imported = how_many_users_imported,
                    users_not_imported = how_many_users_failed
                }));

            else
                return new RouterPacket(422,JsonSerializer.Serialize(new {
                    imported = false,
                    error_message = error_message
                }));

        }

    }
}