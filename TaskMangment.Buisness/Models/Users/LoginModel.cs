using System;

namespace TaskMangment.Buisness.Models.Users;

public readonly struct LoginModel
{
    public LoginModel(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public readonly string Username;
    public readonly string Password;
}
