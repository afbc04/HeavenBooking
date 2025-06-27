using System.Text.Json;
using System.Text.RegularExpressions;

namespace Business {

    public class Hotel {
        public int ID { set; get; }

        public Hotel(int ID) {
            this.ID = ID;
        }

        /// <summary>
        /// Converts data into a CSV format to be imported in mySQL
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
        public static string ToCSVString(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus) {

            bool is_user_active = false;
            if (Regex.IsMatch(AccountStatus, "^active",RegexOptions.IgnoreCase) == true)
                is_user_active = true;

            Console.WriteLine($"{AccountStatus} : {is_user_active}");

            short sex = 2;
            if (Regex.IsMatch(Sex, "F", RegexOptions.IgnoreCase) == true) sex = 1;
            if (Regex.IsMatch(Sex, "M", RegexOptions.IgnoreCase) == true) sex = 0;

            return $"{ID};{Name};{BirthDate.Replace("/","-")};{sex};{Passport};{CountryCode};{AccountCreation.Split(" ")[0].Replace("/","-")};{(is_user_active == true ? 1 : 0)};0;0;0;0;0;0";
        }

        /// <summary>
        /// Validates the data of an user
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
        public static (bool, string?) ValidateUser(bool NewEntry,string ID, string Name, string? Email, string? PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string? Address, string AccountCreation, string? PayMethod, string AccountStatus) {

            var validations = new (bool only_to_new_entries,string? field, string regex, string error_message, bool letter_case_doesnt_matter)[] {
                (true, ID, ".+","id",false),
                (false, Name, ".+","name",false),
                (true, Email, @"^.+@.+\..{2,}$","email",false),
                (true, PhoneNumber, ".+","phone_number",false),
                (false, BirthDate, @"^\d{4}/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])$","birth_date",false),
                (false, Sex, "^(F|M|)$","sex",true),
                (false, Passport, ".+","passport",false),
                (false, CountryCode, @"^[A-Z]{2}$","country_code",false),
                (true, Address, ".+","address",false),
                (true, AccountCreation, @"^\d{4}/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1]) ([0-1][0-9]|2[0-3])(:[0-5][0-9]){2}$","account_creation",false),
                (true, PayMethod, ".+","pay_method",false),
                (false, AccountStatus, "^(active|inactive)$","account_status",true)
            };

            foreach (var (only_to_new_entries, field, regex, error_message, letter_case_doesnt_matter) in validations)

                if ((NewEntry == true || (NewEntry == false && only_to_new_entries == false)) && 
                    Regex.IsMatch(field!,regex,letter_case_doesnt_matter == true ? RegexOptions.IgnoreCase : RegexOptions.None) == false)
                    return (false,$"{error_message}-not-valid");

            if (DateTime.Parse(BirthDate).CompareTo(DateTime.Parse(AccountCreation)) > 0)
                return (false,"birth-date-after-account-creation");

            return (true, null);

        }

    }
}