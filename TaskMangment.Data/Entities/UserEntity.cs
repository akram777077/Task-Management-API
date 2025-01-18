using System.ComponentModel.DataAnnotations;

namespace TaskMangment.Data.Entities;

public class UserEntity
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public required string RoleName { get; set; }
    public  RoleEntity? Role { get; set; }
    public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}