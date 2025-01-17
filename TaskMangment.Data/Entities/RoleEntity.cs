namespace TaskMangment.Data.Entities;

public class RoleEntity
{
    public string Name { get; set; } = null!;
    public List<UserEntity> Users { get; set; } = new List<UserEntity>();   
}