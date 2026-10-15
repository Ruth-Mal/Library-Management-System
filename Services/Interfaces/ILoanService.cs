using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services.Interfaces;

public interface ILoanService
{
    Task<IEnumerable<Loan>> GetAllAsync();
    Task<Loan?> GetByIdAsync(int id);
    Task<Loan?> BorrowBookAsync(int bookId, int memberId, int loanDays);
    Task<Loan?> ReturnBookAsync(int loanId);
}