using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.Implementations;

public class LoanService : ILoanService
{
    private readonly LibraryDbContext _context;

    public LoanService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Loan>> GetAllAsync() =>
        await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .ToListAsync();

    public async Task<Loan?> GetByIdAsync(int id) =>
        await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .FirstOrDefaultAsync(l => l.Id == id);

    public async Task<Loan?> BorrowBookAsync(int bookId, int memberId, int loanDays)
    {
        var book = await _context.Books.FindAsync(bookId);
        var member = await _context.Members.FindAsync(memberId);

        if (book == null || member == null || book.AvailableCopies <= 0)
            return null;

        book.AvailableCopies--;

        var loan = new Loan
        {
            BookId = bookId,
            MemberId = memberId,
            BorrowedAt = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(loanDays),
            Status = "Borrowed"
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return loan;
    }

    public async Task<Loan?> ReturnBookAsync(int loanId)
    {
        var loan = await _context.Loans.FindAsync(loanId);
        if (loan == null || loan.ReturnedAt != null)
            return null;

        var book = await _context.Books.FindAsync(loan.BookId);
        if (book == null) return null;

        book.AvailableCopies++;
        loan.ReturnedAt = DateTime.UtcNow;
        loan.Status = "Returned";

        await _context.SaveChangesAsync();
        return loan;
    }
}