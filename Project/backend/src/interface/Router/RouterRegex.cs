using Interface;
using System.Text.RegularExpressions;

namespace Interface
{

    public class RouterRegex
    {

        public static readonly Regex UsersById = new(@"^/users/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex Users = new(@"^/users($|\?)", RegexOptions.Compiled);
        public static readonly Regex ReservationsById = new(@"^/reservations/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex Reservations = new(@"^/reservations($|\?)", RegexOptions.Compiled);
        public static readonly Regex FlightsById = new(@"^/flights/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex Flights = new(@"^/flights($|\?)", RegexOptions.Compiled);
        public static readonly Regex HotelsById = new(@"^/hotels/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex Hotels = new(@"^/hotels($|\?)", RegexOptions.Compiled);
        public static readonly Regex AirportsById = new(@"^/airports/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex Airports = new(@"^/airports($|\?)", RegexOptions.Compiled);
        public static readonly Regex ReservationsOfUser = new(@"^/reservations-of-user/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex FlightsOfUser = new(@"^/flights-of-user/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex FlightsAndReservationsOfUser = new(@"^/flights-and-reservations-of-user/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex RatingHotel = new(@"^/rating-hotel/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex AllRatingsHotel = new(@"^/all-ratings-hotel/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex ReservationsOfHotel = new(@"^/reservations-of-hotel/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex FlightsOfAirport = new(@"^/flights-of-airport/.+/\d{4}-\d{2}-\d{2}_\d{2}:\d{2}:\d{2}/\d{4}-\d{2}-\d{2}_\d{2}:\d{2}:\d{2}($|\?)", RegexOptions.Compiled);
        public static readonly Regex TopFlightsAirport = new(@"^/top-flights-airport($|\?)", RegexOptions.Compiled);
        public static readonly Regex TopPassengersAirportYear = new(@"^/top-passengers-airport-year/\d{1,}($|\?)", RegexOptions.Compiled);
        public static readonly Regex TopDelayAirport = new(@"^/top-delay-airport($|\?)", RegexOptions.Compiled);
        public static readonly Regex TopRatingHotels = new(@"^/top-rating-hotels", RegexOptions.Compiled);
        public static readonly Regex TopReservationsHotels = new(@"^/top-reservations-hotels($|\?)", RegexOptions.Compiled);
        public static readonly Regex TopRevenueHotels = new(@"^/top-revenue-hotels($|\?)", RegexOptions.Compiled);
        public static readonly Regex RevenueHotel = new(@"^/revenue-hotel/.+/\d{4}-\d{2}-\d{2}/\d{4}-\d{2}-\d{2}($|\?)", RegexOptions.Compiled);
        public static readonly Regex PrefixUsers = new(@"^/prefix-users/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex MetricsAll = new(@"^/metrics-all($|\?)", RegexOptions.Compiled);
        public static readonly Regex MetricsYear = new(@"^/metrics-year/\d{1,}($|\?)", RegexOptions.Compiled);
        public static readonly Regex MetricsMonth = new(@"^/metrics-month/\d{1,}/\d{2}($|\?)", RegexOptions.Compiled);
        public static readonly Regex GlobalInformation = new(@"^/global-information($|\?)", RegexOptions.Compiled);
        public static readonly Regex RevenueAllTimeHotel = new(@"^/revenue-all-time-hotel/.+($|\?)", RegexOptions.Compiled);
        public static readonly Regex TopPassengersAirport = new(@"^/top-passengers-airport($|\?)", RegexOptions.Compiled);
        public static readonly Regex UsersBatch = new(@"^/users-batch($|\?)", RegexOptions.Compiled);
        public static readonly Regex ReservationsBatch = new(@"^/reservations-batch($|\?)", RegexOptions.Compiled);
        public static readonly Regex HotelsBatch = new(@"^/hotels-batch($|\?)", RegexOptions.Compiled);
        public static readonly Regex FlightsBatch = new(@"^/flights-batch($|\?)", RegexOptions.Compiled);
        public static readonly Regex AirportsBatch = new(@"^/airports-batch($|\?)", RegexOptions.Compiled);
        public static readonly Regex PassengersBatch = new(@"^/passengers-batch($|\?)", RegexOptions.Compiled);

    }

}