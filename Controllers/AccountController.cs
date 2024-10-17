using PokerAppAPI.Models;
using PokerAppAPI.Resources;
using PokerAppAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace PokerAppAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    //private readonly PokerDb _context;
    private readonly IAccountService _service;

    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
        var accounts = await _service.GetAccountsASync();
        return Ok(accounts); 
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccount(int id)
    {
        var account = await _service.GetAccountByIdASync(id);

        if (account == null)
        {
            return NotFound();
        }

        return Ok(account);
    }

    [HttpPost]
    public async Task<ActionResult<Account>> PostAccount(Account account)
    {
        ServiceResult<Account> result = await _service.AddAccountASync(account);

        if (result.ErrorCode == ServiceErrorCodes.None)
        {
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Account>> PutAccount(int id, Account newAccount)
    {
        ServiceResult<Account> result = await _service.UpdateAccountASync(id, newAccount);

        if (result.ErrorCode != ServiceErrorCodes.None)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Account>> DeleteAccount(int id)
    {
        ServiceResult<Account> result = await _service.DeleteAccountASync(id);

        if (result.ErrorCode != ServiceErrorCodes.None)
        {
            return NotFound();
        }

        return Ok();
    }
}


