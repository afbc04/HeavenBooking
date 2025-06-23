using Business;
using DataBase;

namespace Business
{

    public class UserManager : IUserManager
    {

        private DAO _users;
        public int UsersQuantity { set; get; }
        public int ActiveUsersQuantity { set; get; }
        private ISet<string> _ids;

        public UserManager()
        {

            this._users = DAO.GetInstance();
            this._ids = new HashSet<string>();
            UpdateStatisticsOfManager();

        }

        private void UpdateStatisticsOfManager()
        {
            var dao = this._users;
            this._ids = dao.GetUsersIDs();

            foreach (string s in this._ids)
            {
                if (s == "Andre")
                    Console.WriteLine("FOUND IT");
            }

            Console.WriteLine("END");

            this.UsersQuantity = dao.GetUsersCount();
            this.ActiveUsersQuantity = dao.GetUsersActiveCount();
        }

        public User? GetUser(string ID)
        {

            Console.WriteLine($"ID GET {ID}");

            if (this._ids.Contains(ID) == false)
                return null;

            return this._users.GetUserByID(ID);

        }

        public (bool, string?) AddUser(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus)
        {

            if (this._ids.Contains(ID))
                return (false, "ID is taken");

            (bool isUserValid, string? validationError) = User.ValidateUser(ID, Name, Email, PhoneNumber, BirthDate, Sex, Passport, CountryCode, Address, AccountCreation, PayMethod, AccountStatus);

            if (isUserValid == false)
                return (false, validationError);

            if (this._users.AddUser(ID, Name, Email, PhoneNumber, BirthDate, Sex, Passport, CountryCode, Address, AccountCreation, PayMethod, AccountStatus)) {
                this._ids.Add(ID);
                return (true, null);
            }
            else
                return (false, "Something went wrong adding user to Database");

        }

        public bool DeleteUser(string ID)
        {
            if (this._users.DeleteUser(ID))
            {
                this._ids.Remove(ID);
                return true;
            }
            else
                return false;
        }


        public (bool, string, int, int) ImportFile(string csv_path)
        {

            bool valid = false;
            int usersValid = 0;
            int usersFailed = 0;
            string errorMessage = "File does not exists";

            if (File.Exists(csv_path))
            {

                StreamWriter writer_valid = new("datasets/users.csv");
                StreamWriter writer_error = new("datasets/users_error.csv");

                try
                {

                    StreamReader reader = new(csv_path);

                    string? line = null;

                    Dictionary<string, int> header = new();

                    if ((line = reader.ReadLine()) != null)
                    {

                        string[] header_fields = line.Split(";");

                        for (int i = 0; i < header_fields.Length; i++)
                        {

                            header[header_fields[i]] = i;

                        }


                        if (header_fields.Length > 0)
                        {

                            while ((line = reader.ReadLine()) != null)
                            {

                                string[] fields = line.Split(";");

                                string user_ID = fields[header["id"]];
                                string user_Name = fields[header["name"]];
                                string user_Email = fields[header["email"]];
                                string user_PhoneNumber = fields[header["phone_number"]];
                                string user_BirthDate = fields[header["birth_date"]];
                                string user_Sex = fields[header["sex"]];
                                string user_Passport = fields[header["passport"]];
                                string user_CountryCode = fields[header["country_code"]];
                                string user_Address = fields[header["address"]];
                                string user_AccountCreation = fields[header["account_creation"]];
                                string user_PayMethod = fields[header["pay_method"]];
                                string user_AccountStatus = fields[header["account_status"]];

                                (bool userValid, string? errorUserMessage) = User.ValidateUser(user_ID, user_Name, user_Email, user_PhoneNumber, user_BirthDate, user_Sex, user_Passport, user_CountryCode, user_Address, user_AccountCreation, user_PayMethod, user_AccountStatus);

                                if (this._ids.Contains(user_ID) == false)
                                {

                                    if (userValid == true)
                                    {

                                        Console.WriteLine(user_ID);

                                        this._ids.Add(user_ID);
                                        writer_valid.WriteLine(User.ToCSVString(user_ID, user_Name, user_Email, user_PhoneNumber, user_BirthDate, user_Sex, user_Passport, user_CountryCode, user_Address, user_AccountCreation, user_PayMethod, user_AccountStatus));
                                        usersValid++;
                                    }
                                    else
                                    {
                                        usersFailed++;
                                        writer_error.WriteLine($"{errorUserMessage};{line}");
                                    }

                                }
                                else
                                {
                                    usersFailed++;
                                    writer_error.WriteLine($"ID duplicated;{line}");
                                }

                            }

                            if (usersValid > 0)
                            {

                                if (this._users.ImportUsers())
                                {
                                    valid = true;
                                    UpdateStatisticsOfManager();
                                }
                                else
                                {
                                    valid = false;
                                    errorMessage = "Something went wrong adding users to Database";
                                }

                            }
                            else
                            {
                                valid = true;
                            }

                        }
                        else
                        {
                            errorMessage = "Header not found";
                        }

                    }
                    else
                    {
                        errorMessage = "File empty";
                    }

                    reader.Close();
                    writer_error.Close();
                    writer_valid.Close();

                }
                catch (Exception ex)
                {
                    valid = false;
                    errorMessage = ex.Message;
                }

            }

            return (valid, errorMessage, usersValid, usersFailed);

        }

    }

}