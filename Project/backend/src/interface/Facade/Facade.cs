using Interface;
using Business;
using DataBase;

namespace Interface
{

    public class Facade : IFacade
    {

        private IUserManager _users;
        private ReaderWriterLockSlim rwLock = new();

        public Facade()
        {

            if (DAO.InitDataBase() == false)
                throw new FacadeDataBaseException();

            this._users = new UserManager();
        }

        public string ImportUsers(string? path)
        {

            rwLock.EnterWriteLock();
            try
            {

                if (path is not null)
                {
                    (bool wasImported, string? errorMessage, int? usersImported, int? usersFailed) = this._users.ImportFile(path);

                    if (wasImported == true)
                        return $@"{{
                                    ""imported"" : ""true"",
                                    ""success"" : {usersImported},
                                    ""failed"" : {usersFailed}
                                  }}";
                    else
                        return $@"{{
                                    ""imported"" : ""false"",
                                    ""error_message"" : ""{errorMessage}"" 
                                  }}";

                }
                else
                    return "{\"imported\" : \"false\", \"error_message\" : \"Path does not exist\"}";

            }
            finally { rwLock.ExitWriteLock(); }

        }

        public string GetUser(string ID)
        {
            rwLock.EnterReadLock();
            try
            {

                User? user = this._users.GetUser(ID);
                if (user == null)
                    throw new RouterException(404);
                else
                    return $@"{{
                                ""id"" : ""{user.ID}"",
                                ""name"" : ""{user.Name}"",
                                ""age"" : {user.Age},
                                ""country_code"" : ""{user.CountryCode}"",
                                ""passport"" : ""{user.Passport}"",
                                ""reservations"" : {user.ReservationsQuantity},
                                ""reservations_refunded"" : {user.RefundedReservationsQuantity},
                                ""flights"" : {user.FlightsQuantity},
                                ""flights_arrived"" : {user.FlightsArrivedQuantity},
                                ""flights_lost"" : {user.FlightsLostQuantity},
                                ""total_spent"" : {user.TotalSpent},
                                ""active"" : {(user.IsActive ? "true" : "false")}
                              }}";


            }
            finally { rwLock.ExitReadLock(); }

        }

        public (bool, string) AddUser(string? ID, string? Name, string? Email, string? PhoneNumber, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? Address, string? AccountCreation, string? PayMethod, string? AccountStatus)
        {
            rwLock.EnterWriteLock();
            try
            {

                if (ID == null) return (false, @$"{{""added"" : false, ""error_message"" : ""ID does not exists""}}");
                if (Name == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Name does not exists""}}");
                if (Email == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Email does not exists""}}");
                if (PhoneNumber == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Phone Number does not exists""}}");
                if (BirthDate == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Birth Date does not exists""}}");
                if (Sex == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Sex does not exists""}}");
                if (Passport == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Passport does not exists""}}");
                if (CountryCode == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Country Code does not exists""}}");
                if (Address == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Address does not exists""}}");
                if (AccountCreation == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Account Creation Date does not exists""}}");
                if (PayMethod == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Pay Method does not exists""}}");
                if (AccountStatus == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Account Status does not exists""}}");

                (bool wasAdded, string? errorMessage) = this._users.AddUser(ID, Name, Email, PhoneNumber, BirthDate, Sex, Passport, CountryCode, Address, AccountCreation, PayMethod, AccountStatus);

                if (wasAdded == true)
                    return (true, $@"{{
                                ""added"" : ""true"",
                                ""user_id"" : {ID},
                              }}");
                else
                    return (false, $@"{{
                                ""added"" : ""false"",
                                ""error_message"" : ""{errorMessage}"" 
                              }}");

            }
            finally { rwLock.ExitWriteLock(); }
        }

        public (bool, string) UpdateUser(string ID, string? Name, string? Email, string? PhoneNumber, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? Address, string? AccountCreation, string? PayMethod, string? AccountStatus)
        {
            rwLock.EnterWriteLock();
            try
            {

                User? current_user = this._users.GetUser(ID);

                if (current_user == null)
                    throw new RouterException(404);

                if (Name == null) Name = current_user.Name;
                if (BirthDate == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Birth Date does not exists""}}");
                if (Sex == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Sex does not exists""}}");
                if (Passport == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Passport does not exists""}}");
                if (CountryCode == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Country Code does not exists""}}");
                if (Address == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Address does not exists""}}");
                if (AccountCreation == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Account Creation Date does not exists""}}");
                if (PayMethod == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Pay Method does not exists""}}");
                if (AccountStatus == null) return (false, @$"{{""added"" : false, ""error_message"" : ""Account Status does not exists""}}");

                //(bool wasAdded, string? errorMessage) = this._users.AddUser(ID,Name,Email,PhoneNumber,BirthDate,Sex,Passport,CountryCode,Address,AccountCreation,PayMethod,AccountStatus);

                //if (wasAdded == true)
                //    return (true,$@"{{
                //                ""added"" : ""true"",
                //                ""user_id"" : {ID},
                //              }}");
                //else
                //    return (false,$@"{{
                //                ""added"" : ""false"",
                //                ""error_message"" : ""{errorMessage}"" 
                //              }}");
                return (true, ":D");

            }
            finally { rwLock.ExitWriteLock(); }
        }
        
        public string DeleteUser(string ID)
        {
            rwLock.EnterWriteLock();
            try
            {

                User? current_user = this._users.GetUser(ID);

                if (current_user == null)
                    throw new RouterException(404);

                bool wasRemoved = this._users.DeleteUser(ID);

                if (wasRemoved == true)
                    return $@"{{""removed"" : ""true""}}";
                else
                    return $@"{{""removed"" : ""false""}}";

            }
            finally { rwLock.ExitWriteLock(); }
        }


    }
}