namespace LibraryManagementSystem.Models;

public class Member : BaseEntity
{
    public string MembershipNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}