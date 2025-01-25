using System;

namespace TaskMangment.Data.Entities;

public class AuthorizeUserEntity
{
    public required string Username { get; set; }
    public required string Role { get; set; }
}
