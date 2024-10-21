using PokerAppAPI.Resources;
using PokerAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace PokerAppAPI.Services;

/*
 *      Interface and Context class for handling account creation and management
 */

public enum ServiceErrorCodes
{
    None = 0,
    AccountDoesNotExist,
    CreateAccountFailure
}

//Generic service result class for returning service object and error codes 
public class ServiceResult<T>
{
    public T? Result { get; set;  }
    public ServiceErrorCodes ErrorCode;

    public static ServiceResult<T> Success(T result)
    {
        return new ServiceResult<T> { Result = result, ErrorCode = ServiceErrorCodes.None };
    }

    public static ServiceResult<T> Failuire(ServiceErrorCodes errorCode)
    {
        return new ServiceResult<T> { ErrorCode = errorCode };
    }
}

public interface IAccountService
{
    //define some required methods
    public Task<ServiceResult<IEnumerable<Account>>> GetAccountsASync();
    public Task<ServiceResult<Account>> GetAccountByIdASync(int id);
    public Task<ServiceResult<Account>> AddAccountASync(Account account);
    public Task<ServiceResult<Account>> UpdateAccountASync(int id, Account account);
    public Task<ServiceResult<Account>> DeleteAccountASync(int id);
}

public class AccountService : IAccountService
{
    private readonly PokerDb _context;

    public AccountService(PokerDb context)
    {
        _context = context;
    }

    public async Task<ServiceResult<IEnumerable<Account>>> GetAccountsASync()
    {
        var accounts = await _context.Users.ToListAsync();
        return ServiceResult<IEnumerable<Account>>.Success(accounts);
    }

    public async Task<ServiceResult<Account>> GetAccountByIdASync(int id)
    {
        var account = await _context.Users.FindAsync(id);

        if (account == null)
        {
            return ServiceResult<Account>.Failuire(ServiceErrorCodes.AccountDoesNotExist);
        }

        return ServiceResult<Account>.Success(account);
    }

    public async Task<ServiceResult<Account>> AddAccountASync(Account account)
    {
        //check username exists

        _context.Users.Add(account);

        await _context.SaveChangesAsync();

        return ServiceResult<Account>.Success(account);
    }

    public async Task<ServiceResult<Account>> UpdateAccountASync(int id, Account account)
    {
        var accountToUpdate = await _context.Users.FindAsync(id);

        if (accountToUpdate == null)
        {
            return ServiceResult<Account>.Failuire(ServiceErrorCodes.AccountDoesNotExist);
        }

        accountToUpdate.UserName = account.UserName;
        accountToUpdate.Chips = account.Chips;

        await _context.SaveChangesAsync();

        return ServiceResult<Account>.Success(account);
    }

    public async Task<ServiceResult<Account>> DeleteAccountASync(int id)
    {
        var accountToDelete = await _context.Users.FindAsync(id);

        if (accountToDelete == null)
        {
            return ServiceResult<Account>.Failuire(ServiceErrorCodes.AccountDoesNotExist);
        }

        _context.Users.Remove(accountToDelete);

        await _context.SaveChangesAsync();

        return ServiceResult<Account>.Success(accountToDelete);
    }
}