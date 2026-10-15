using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.Implementations;

public class MemberService : IMemberService
{
    private readonly LibraryDbContext _context;

    public MemberService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Member>> GetAllAsync() =>
        await _context.Members.ToListAsync();

    public async Task<Member?> GetByIdAsync(int id) =>
        await _context.Members.FindAsync(id);

    public async Task<Member> AddAsync(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task<Member?> UpdateAsync(int id, Member member)
    {
        var existing = await _context.Members.FindAsync(id);
        if (existing == null) return null;

        existing.FullName = member.FullName;
        existing.Email = member.Email;
        existing.Phone = member.Phone;
        existing.MembershipNumber = member.MembershipNumber;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null) return false;

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        return true;
    }
}