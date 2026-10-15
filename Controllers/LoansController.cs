using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _loanService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var loan = await _loanService.GetByIdAsync(id);
        return loan == null ? NotFound() : Ok(loan);
    }

    [HttpPost("borrow")]
    public async Task<IActionResult> Borrow(BorrowBookDto dto)
    {
        var loan = await _loanService.BorrowBookAsync(dto.BookId, dto.MemberId, dto.LoanDays);
        return loan == null ? BadRequest("Book unavailable or invalid member/book.") : Ok(loan);
    }

    [HttpPost("return")]
    public async Task<IActionResult> Return(ReturnBookDto dto)
    {
        var loan = await _loanService.ReturnBookAsync(dto.LoanId);
        return loan == null ? BadRequest("Invalid loan or already returned.") : Ok(loan);
    }
}