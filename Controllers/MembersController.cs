using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _memberService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var member = await _memberService.GetByIdAsync(id);
        return member == null ? NotFound() : Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberDto dto)
    {
        var member = new Member
        {
            MembershipNumber = dto.MembershipNumber,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone
        };

        var created = await _memberService.AddAsync(member);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Member member)
    {
        var updated = await _memberService.UpdateAsync(id, member);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _memberService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}