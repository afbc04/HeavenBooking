using Business;
using DataBase;

namespace Business
{

    public class HotelManager : IHotelManager {

        private DAO _users;
        public int UsersQuantity { set; get; }
        public int ActiveUsersQuantity { set; get; }
        private ISet<string> _ids;

        public HotelManager() {

            this._users = DAO.GetInstance();
            this._ids = new HashSet<string>();
            UpdateStatisticsOfManager();

        }


        private void UpdateStatisticsOfManager()
        {
            var dao = this._users;
            this._ids = dao.GetUsersIDs();
            this.UsersQuantity = dao.GetUsersCount();
            this.ActiveUsersQuantity = dao.GetUsersActiveCount();
        }

        /// <summary>
        /// Informs if a User exists
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool ContainsUser(string ID) {
            return this._ids.Contains(ID);
        }

        /// <summary>
        /// Gets the User identifies by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public UserComplete GetUser(string ID) {
            return this._users.GetUserByID(ID);
        }

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <returns>Returns if user was added, and if not, gives the reason via string</returns>
        /*
        public (bool, string?, UserComplete?) AddUser(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus) {

            if (this._ids.Contains(ID))
                return (false, "id-duplicated",null);

            (bool isUserValid, string? validationError) = User.ValidateUser(true,ID, Name, Email, PhoneNumber, BirthDate, Sex, Passport, CountryCode, Address, AccountCreation, PayMethod, AccountStatus);

            if (isUserValid == false)
                return (false, validationError,null);

            if (this._users.PutUser(ID, Name, Email, PhoneNumber, BirthDate, Sex, Passport, CountryCode, Address, AccountCreation, PayMethod, AccountStatus)) {

                this._ids.Add(ID);
                UserComplete user_added = this._users.GetUserByID(ID);
                return (true, null,user_added);

            }
            else
                return (false, "database-error",null);

        }*/

        /// <summary>
        /// Deletes an user identified by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public (bool,UserComplete) DeleteUser(string ID) {

            UserComplete user_to_be_deleted = this._users.GetUserByID(ID);
            bool was_user_deleted = this._users.DeleteUser(ID);

            if (was_user_deleted)
                this._ids.Remove(ID);
                
            return (was_user_deleted,user_to_be_deleted);

        }

        /// <summary>
        /// Updates the information of an user identified by ID using the new information
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="BirthDate"></param>
        /// <param name="Sex"></param>
        /// <param name="Passport"></param>
        /// <param name="CountryCode"></param>
        /// <param name="PayMethod"></param>
        /// <param name="AccountStatus"></param>
        /// <returns></returns>
        /// 
        /*
        public (bool, string?, UserComplete) UpdateUser(string ID, string? Name, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? AccountStatus) {

            UserComplete user = this._users.GetUserByID(ID);

            string OldSex = user.Sex switch {
                0 => "M",
                1 => "F",
                _ => "",
            };

            string NewName = Name == null ? user.Name : Name;
            string NewBirthDate = BirthDate == null ? user.BirthDate : BirthDate;
            string NewSex = Sex == null ? OldSex : Sex;
            string NewPassport = Passport == null ? user.Passport : Passport;
            string NewCountryCode = CountryCode == null ? user.CountryCode : CountryCode;
            string NewAccountStatus = AccountStatus == null ? (user.IsActive ? "active" : "inactive") : AccountStatus;

            (bool user_valid, string? error_message) = User.ValidateUser(false,ID,NewName,null,null,NewBirthDate,NewSex,NewPassport,NewCountryCode,null,user.AccountCreation,null,NewAccountStatus);

            if (user_valid == false)
                return (false, error_message,user);

            if (this._users.PutUser(ID, NewName, "", "", NewBirthDate, NewSex, NewPassport, NewCountryCode, "", user.AccountCreation, "", NewAccountStatus)) {

                UserComplete user_updated = this._users.GetUserByID(ID);
                return (true, null,user_updated);

            }
            else
                return (false, "database-error",user);

        }*/

        /// <summary>
        /// Gives a list of existing users
        /// </summary>
        /// <param name="page">Page of the Users List</param>
        /// <param name="limit">Limit of Users</param>
        /// <returns></returns>
        public IList<UserList> GetUsers(int page, int limit) {
            return this._users.GetUsers(page,limit);
        }

        /// <summary>
        /// Imports all users in a CSV file indicated by csv_path
        /// </summary>
        /// <param name="csv_path"></param>
        /// <returns></returns>
        public (bool, string, int, int) ImportFile(string csv_path) {

            bool was_imported = false;
            int users_valid = 0;
            int users_invalid = 0;
            string error_message = "file-not-exists";

            if (File.Exists(csv_path) == false)
                return (false,error_message,0,0);


            StreamWriter writer_of_valid_users = new("datasets/users.csv");
            StreamWriter writer_of_invalid_users = new("datasets/users_error.csv");

            try {

                StreamReader reader = new(csv_path);

                string? line = null;
                Dictionary<string, int> csv_header = new();

                if ((line = reader.ReadLine()) != null) {

                    string[] header_fields = line.Split(";");

                    for (int i = 0; i < header_fields.Length; i++)
                        csv_header[header_fields[i]] = i;

                }

                if (csv_header.Count > 0) {
                
                    while ((line = reader.ReadLine()) != null) {

                        string[] fields = line.Split(";");

                        string user_ID = fields[csv_header["id"]];
                        string user_Name = fields[csv_header["name"]];
                        string user_Email = fields[csv_header["email"]];
                        string user_PhoneNumber = fields[csv_header["phone_number"]];
                        string user_BirthDate = fields[csv_header["birth_date"]];
                        string user_Sex = fields[csv_header["sex"]];
                        string user_Passport = fields[csv_header["passport"]];
                        string user_CountryCode = fields[csv_header["country_code"]];
                        string user_Address = fields[csv_header["address"]];
                        string user_AccountCreation = fields[csv_header["account_creation"]];
                        string user_PayMethod = fields[csv_header["pay_method"]];
                        string user_AccountStatus = fields[csv_header["account_status"]];

                        (bool is_user_valid, string? user_invalid_message) = User.ValidateUser(true,user_ID, user_Name, user_Email, user_PhoneNumber, user_BirthDate, user_Sex, user_Passport, user_CountryCode, user_Address, user_AccountCreation, user_PayMethod, user_AccountStatus);
                        bool is_user_id_taken = this._ids.Contains(user_ID);

                        if (is_user_id_taken == false && is_user_valid == true) {

                            this._ids.Add(user_ID);
                            //writer_of_valid_users.WriteLine(User.ToCSVString(user_ID, user_Name, user_Email, user_PhoneNumber, user_BirthDate, user_Sex, user_Passport, user_CountryCode, user_Address, user_AccountCreation, user_PayMethod, user_AccountStatus));
                            users_valid++;

                        }
                        else {

                            users_invalid++;
                            writer_of_invalid_users.WriteLine($"{(is_user_id_taken == true ? "id-duplicate" : user_invalid_message)};{line}");

                        }

                    }          

                    was_imported = true;

                    if (users_valid > 0) {

                        //if (this._users.ImportUsers()) {
                        if (true) {
                            was_imported = true;
                            UpdateStatisticsOfManager();
                        }
                        else {
                            was_imported = false;
                            error_message = "database-error";
                        }

                    }

                }
                else
                    error_message = "header-not-exists";
                    

                reader.Close();
                writer_of_invalid_users.Close();
                writer_of_valid_users.Close();

            }
            catch (Exception) {
                was_imported = false;
                error_message = "reader-or-writer-failed";
            }

            return (was_imported, error_message, users_valid, users_invalid);

        }

    }

}