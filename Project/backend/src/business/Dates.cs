namespace Business {

    public class Dates {

        public static string DateToString(DateTime date) {
            return $"{date.Year:D4}/{date.Month:D2}/{date.Day:D2}";
        }

        public static string DateTimeToString(DateTime date) {
            return $"{date.Year:D4}/{date.Month:D2}/{date.Day:D2} {date.Hour:D2}:{date.Minute:D2}:{date.Second:D2}";
        }

    }

}