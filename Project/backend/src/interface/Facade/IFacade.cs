namespace Interface {

    public interface IFacade
    {
        RouterPacket ImportUsers(string? path);
        RouterPacket GetUser(string ID);
        RouterPacket AddUser(string? ID, string? Name, string? Email, string? PhoneNumber, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? Address, string? AccountCreation, string? PayMethod, string? AccountStatus);
        RouterPacket UpdateUser(string ID, string? Name, string? Email, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? AccountStatus);
        RouterPacket DeleteUser(string ID);
        RouterPacket GetUsers(int? page);
        RouterPacket GetUsersPrefix(string prefix, int? page);
        RouterPacket GetMetrics(int? year, int? month);

    }
}