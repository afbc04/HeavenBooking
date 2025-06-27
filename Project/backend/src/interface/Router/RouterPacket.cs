using Interface;

namespace Interface {

    public class RouterPacket {

        public int StatusCode { set; get; }
        public string? JSON { set; get; }

        public RouterPacket(int status_code, string JSON) {
            this.StatusCode = status_code;
            this.JSON = JSON;
        }

        public RouterPacket(int status_code) {
            this.StatusCode = status_code;
            this.JSON = null;
        }

    }


}