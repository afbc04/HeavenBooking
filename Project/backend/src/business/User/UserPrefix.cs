using System.Text.Json;
using System.Text.RegularExpressions;

namespace Business {
    public class UserPrefix : User {
        public string Name { set; get; }

        public UserPrefix(string ID, string Name) : base(ID) {
            this.Name = Name;
        }

    }
}