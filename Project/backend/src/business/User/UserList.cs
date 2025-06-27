using System.Text.Json;
using System.Text.RegularExpressions;

namespace Business {
    public class UserList : User {
        public string Name { set; get; }
        public string CountryCode { set; get; }
        public bool IsActive { set; get; }

        public UserList(string ID, string Name, string CountryCode, bool IsActive) : base(ID) {
            this.Name = Name;
            this.CountryCode = CountryCode;
            this.IsActive = IsActive;
        }

    }
}