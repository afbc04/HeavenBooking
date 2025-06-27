using System.Text.Json;
using System.Text.RegularExpressions;

namespace Business {

    public class UserBatch {

        public string ID { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string BirthDate { set; get; }
        public short Sex { set; get; }
        public string CountryCode { set; get; }
        public string AccountCreation { set; get; }
        public string Passport { set; get; }
        public bool AccountStatus { set; get; }

        public UserBatch(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus) {
            this.ID = ID;
            this.Name = Name;
            this.Email = Email;
            this.BirthDate = BirthDate.Replace("/","-");

            short sex = 2;

            if (Regex.IsMatch(Sex,"^F",RegexOptions.IgnoreCase)) sex = 1;
            if (Regex.IsMatch(Sex,"^M",RegexOptions.IgnoreCase)) sex = 0;

            bool status = true;

            if (Regex.IsMatch(AccountStatus,"^inactive",RegexOptions.IgnoreCase)) status = false;

            this.Sex = sex;
            this.CountryCode = CountryCode;
            this.Passport = Passport;
            this.AccountStatus = status;
            this.AccountCreation = AccountCreation.Split(" ")[0].Replace("/","-");
        }

    }
}