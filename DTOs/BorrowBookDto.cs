namespace LibraryManagementSystem.DTOs;

public class BorrowBookDto
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public int LoanDays { get; set; } = 14;
}