using System.Text.RegularExpressions;

namespace Business
{
    public class User
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public int Age { set; get; }
        public short Sex { set; get; }
        public string CountryCode { set; get; }
        public string Passport { set; get; }
        public int ReservationsQuantity { set; get; }
        public int RefundedReservationsQuantity { set; get; }
        public int FlightsQuantity { set; get; }
        public int FlightsArrivedQuantity { set; get; }
        public int FlightsLostQuantity { set; get; }
        public double TotalSpent { set; get; }
        public bool IsActive { set; get; }

        public User(string ID, string Name, short Sex, int Age, string CountryCode, string Passport, int ReservationsQuantity, int RefundedReservationsQuantity, int FlightsQuantity, int FlightsArrivedQuantity, int FlightsLostQuantity, double TotalSpent, bool IsActive)
        {
            this.ID = ID;
            this.Name = Name;
            this.Sex = Sex;
            this.Age = Age;
            this.CountryCode = CountryCode;
            this.Passport = Passport;
            this.ReservationsQuantity = ReservationsQuantity;
            this.RefundedReservationsQuantity = RefundedReservationsQuantity;
            this.FlightsQuantity = FlightsQuantity;
            this.FlightsArrivedQuantity = FlightsArrivedQuantity;
            this.FlightsLostQuantity = FlightsLostQuantity;
            this.TotalSpent = TotalSpent;
            this.IsActive = IsActive;
        }

        public static string ToCSVString(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus)
        {

            bool is_user_active = false;
            if (Regex.IsMatch(AccountStatus, "active",RegexOptions.IgnoreCase) == true)
                is_user_active = true;

            short sex = 2;
            if (Regex.IsMatch(Sex, "F", RegexOptions.IgnoreCase) == true) sex = 1;
            if (Regex.IsMatch(Sex, "M", RegexOptions.IgnoreCase) == true) sex = 0;

            return $"{ID};{Name};{BirthDate.Replace("/","-")};{sex};{Passport};{CountryCode};{AccountCreation.Split(" ")[0].Replace("/","-")};{(is_user_active ? 1 : 0)};0;0;0;0;0;0";
        }

        public static (bool, string?) ValidateUser(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus)
        {

            if (Regex.IsMatch(ID, ".+") == false)
                return (false, "ID does not exists");
            if (Regex.IsMatch(Name, ".+") == false)
                return (false, "Name does not exists");
            if (Regex.IsMatch(Email, @"^.+@.+\..{2,}$") == false)
                return (false, "Email is not valid");
            if (Regex.IsMatch(PhoneNumber, @".+") == false)
                return (false, "Phone Number does not exists");
            if (Regex.IsMatch(BirthDate, @"^\d{4}/\d{2}/\d{2}$") == false)
                return (false, "Birth Date is not valid");
            if (Regex.IsMatch(Sex, "^(F|M|A)$",RegexOptions.IgnoreCase) == false)
                return (false, "Sex is not valid");
            if (Regex.IsMatch(Passport, ".+") == false)
                return (false, "Passport does not exists");
            if (Regex.IsMatch(CountryCode, @"^[A-Z]{2}$") == false)
                return (false, "Country Code is not valid");
            if (Regex.IsMatch(Address, ".+") == false)
                return (false, "Address does not exists");
            if (Regex.IsMatch(AccountCreation, @"^\d{4}/\d{2}/\d{2} \d{2}:\d{2}:\d{2}$") == false)
                return (false, "Account Creation is not valid");
            if (Regex.IsMatch(PayMethod, ".+") == false)
                return (false, "Pay Method does not exists");
            if (Regex.IsMatch(AccountStatus, "^(active|inactive)$",RegexOptions.IgnoreCase) == false)
                return (false, "Account Status is not valid");

            return (true, null);

        }

    }
}