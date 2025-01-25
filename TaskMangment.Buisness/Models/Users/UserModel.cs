namespace TaskMangment.Buisness.Models.Users;

public readonly struct UserModel
{
    public UserModel(string username, string password, string role)
    {
        Username = username;
        Password = password;
        Role = role;
    }

    public readonly string Username;
    public readonly string Password;
    public readonly string Role;
}