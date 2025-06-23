namespace Interface {

    public interface IFacade
    {
        string ImportUsers(string? path);
        string GetUser(string ID);
        (bool, string) AddUser(string? ID, string? Name, string? Email, string? PhoneNumber, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? Address, string? AccountCreation, string? PayMethod, string? AccountStatus);
        (bool, string) UpdateUser(string ID, string? Name, string? Email, string? PhoneNumber, string? BirthDate, string? Sex, string? Passport, string? CountryCode, string? Address, string? AccountCreation, string? PayMethod, string? AccountStatus);
        string DeleteUser(string ID);

    }
}