using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task<Member> AddAsync(Member member);
    Task<Member?> UpdateAsync(int id, Member member);
    Task<bool> DeleteAsync(int id);
}