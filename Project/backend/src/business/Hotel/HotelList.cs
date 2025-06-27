using System.Text.Json;
using System.Text.RegularExpressions;

namespace Business {
    public class HotelList : Hotel {
        public string Name { set; get; }
        public string CountryCode { set; get; }
        public bool IsActive { set; get; }

        public HotelList(int ID, string Name, string CountryCode, bool IsActive) : base(ID) {
            this.Name = Name;
            this.CountryCode = CountryCode;
            this.IsActive = IsActive;
        }

    }
}