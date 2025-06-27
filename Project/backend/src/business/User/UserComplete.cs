using System.Text.Json;
using System.Text.RegularExpressions;

namespace Business {

    public class UserComplete : User {

        public string Name { set; get; }
        public string Email { set; get; }
        public string BirthDate { set; get; }
        public int Age { set; get; }
        public short Sex { set; get; }
        public string CountryCode { set; get; }
        public string Passport { set; get; }
        public string AccountCreation { set; get; }
        public bool IsActive { set; get; }

        public UserComplete(string ID, string Name, string Email, string BirthDate, short Sex, int Age, string CountryCode, string Passport, bool IsActive, string AccountCreation) : base(ID){
            this.Name = Name;
            this.Email = Email;
            this.BirthDate = BirthDate.Replace("-","/");
            this.Sex = Sex;
            this.Age = Age;
            this.CountryCode = CountryCode;
            this.Passport = Passport;
            this.IsActive = IsActive;
            this.AccountCreation = AccountCreation.Replace("-","/");
        }

        /// <summary>
        /// Make a JSON string using a User
        /// </summary>
        /// <returns></returns>
        public string ToJSONString() {
            return JsonSerializer.Serialize(new {
                    id = this.ID,
                    name = this.Name,
                    email = this.Email,
                    birth_date = this.BirthDate,
                    age = this.Age,
                    country_code = this.CountryCode,
                    passport = this.Passport,
                    active = this.IsActive,
                    account_creation = this.AccountCreation
                });
        }

    }
}