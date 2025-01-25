using System;

namespace TaskMangment.Data.Entities;

public class LoginEntity
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
