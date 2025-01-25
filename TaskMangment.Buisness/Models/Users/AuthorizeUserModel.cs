using System;

namespace TaskMangment.Buisness.Models.Users;

public readonly struct AuthorizeUserModel
{
    public AuthorizeUserModel(string username, string role)
    {
        Username = username;
        Role = role;
    }
    public readonly string Username;
    public readonly string Role;
}
