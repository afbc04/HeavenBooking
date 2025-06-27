namespace Business {

    public interface IUserManager {

        bool ContainsUser(string ID);
        (bool, string, int, int) ImportFile(string csv_path);
        UserComplete GetUser(string ID);
        (bool, string?,UserComplete?) AddUser(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus);
        (bool, string?, UserComplete) UpdateUser(string ID, string? Name,string? Email, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? AccountStatus);
        (bool,UserComplete) DeleteUser(string ID);
        IList<UserList> GetUsers(int page, int limit);
        IList<UserPrefix> GetUsersPrefix(string prefix, int page, int limit);
        IDictionary<int,int> GetMetricsYears();
        IDictionary<int,int> GetMetricsMonths(int year);
        IDictionary<int,int> GetMetricsDays(int year, int month);

    }

}