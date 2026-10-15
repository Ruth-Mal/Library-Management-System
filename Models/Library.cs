namespace LibraryManagementSystem.Models;

public class Library : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}