using Go1Bet.Core.Context;
using Go1Bet.Core.DTO_s.Balance;
using Go1Bet.Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Core.Services
{
    public class BalanceService
    {
        private readonly AppDbContext _context;
        public BalanceService(AppDbContext context) 
        { 
            _context = context;
        }
        public async Task<ServiceResponse> CreateAsync(BalanceCreateDTO model)
        {
            if(model.Money == null) { model.Money = "0"; }
            var balance = new BalanceEntity() { Money = model.Money, UserId = model.UserId };
            await _context.Balances.AddAsync(balance);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "User has been created.",
                Success = true,
            };
        }
        public async Task<ServiceResponse> DepositAsync(BalanceDepositDTO model)
        {
            var balance = await _context.Balances.Where(b => b.Id == model.BalanceId).FirstOrDefaultAsync();

            int tmp = (int.Parse(balance.Money) + int.Parse(model.Money));
            balance.Money = tmp.ToString();
            var transaction = new TransactionEntity() { BalanceId = balance.Id, DateCreated = DateTime.UtcNow, TransactionType = Constants.TransactionType.Deposit, Value = model.Money };
            await _context.Transactions.AddAsync(transaction);

            _context.Balances.Update(balance);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "The money was credited.",
                Success = true,
            };
        }
        public async Task<ServiceResponse> WithdrawalAsync(BalanceWithdrawalDTO model)
        {
            var balance = await _context.Balances.Where(b => b.Id == model.BalanceId).FirstOrDefaultAsync();
            int tmp = (int.Parse(balance.Money) - int.Parse(model.Money));
            balance.Money = tmp.ToString();

            var transaction = new TransactionEntity() { BalanceId = balance.Id, DateCreated = DateTime.UtcNow, TransactionType = Constants.TransactionType.Withdrawal, Value = model.Money };
            await _context.Transactions.AddAsync(transaction);

            _context.Balances.Update(balance);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "The money was withdrawn.",
                Success = true,
            };
        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var balance = await _context.Balances.Where(b => b.Id == id).FirstOrDefaultAsync();
            foreach(var transaction in await _context.Transactions.Where(b=> b.BalanceId == id).ToListAsync())
            {
                _context.Transactions.Remove(transaction);
            }
             

            _context.Balances.Remove(balance);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "The balance was deleted.",
                Success = true,
            };
        }
    }
}
