namespace Business {

    public class Metrics {

        public int UsersCreated { set; get; }
        public int ReservationsBooked { set; get; }
        public int FlightsDepartured { set; get; }
        public int Passengers { set; get; }
        public int UniquePassengers { set; get; }

        public Metrics() {
            this.UsersCreated = 0;
            this.ReservationsBooked = 0;
            this.FlightsDepartured = 0;
            this.Passengers = 0;
            this.UniquePassengers = 0;
        }

    }

}