using System.Text.Json;
using System.Text.RegularExpressions;

namespace Business {

    public class HotelComplete : Hotel {

        public string Name { set; get; }
        public string BirthDate { set; get; }
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
        public string AccountCreation { set; get; }
        public bool IsActive { set; get; }

        public HotelComplete(int ID, string Name, string BirthDate, short Sex, int Age, string CountryCode, string Passport, int ReservationsQuantity, int RefundedReservationsQuantity, int FlightsQuantity, int FlightsArrivedQuantity, int FlightsLostQuantity, double TotalSpent, bool IsActive, string AccountCreation) : base(ID){
            this.Name = Name;
            this.BirthDate = BirthDate.Replace("-","/");
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
                    birth_date = this.BirthDate,
                    age = this.Age,
                    country_code = this.CountryCode,
                    passport = this.Passport,
                    reservations = this.ReservationsQuantity,
                    reservations_refunded = this.RefundedReservationsQuantity,
                    flights = this.FlightsQuantity,
                    flights_arrived = this.FlightsArrivedQuantity,
                    flights_lost = this.FlightsLostQuantity,
                    total_spent = this.TotalSpent,
                    active = this.IsActive,
                    account_creation = this.AccountCreation
                });
        }

    }
}