using Go1Bet.Core.Context;
using Go1Bet.Core.DTO_s.Balance;
using Go1Bet.Core.DTO_s.Category;
using Go1Bet.Core.DTO_s.User;
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
        public async Task<ServiceResponse> GetAllAsync()
        {
            try
            {
                var balances = _context.Balances

                    .Select(b => new BalanceItemDTO
                    {
                        Id = b.Id,
                        UserId = b.UserId,
                        DateCreated = b.DateCreated.ToString(),
                        Money = b.Money,
                        Reviewed = b.Reviewed,

                        countTransactions = b.TransactionHistory.Where(t => t.BalanceId == b.Id).Count()
                    }).ToList();

                return new ServiceResponse
                {
                    Success = true,
                    Payload = balances
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ServiceResponse> GetByUserIdAsync(string id)
        {
            var balances = _context.Balances
                    .Where(u => u.UserId == id)
                    .Select(b => new BalanceItemDTO
                    { 
                        Id = b.Id,
                        UserId = b.UserId,
                        DateCreated = b.DateCreated.ToString(),
                        Money = b.Money,
                        Reviewed = b.Reviewed,
                        countTransactions = b.TransactionHistory.Where(t => t.BalanceId == b.Id).Count(),
                        Transactions = b.TransactionHistory
                                       .Where(t => t.BalanceId == b.Id)
                                       .Select(t => new TransactionItemDTO
                                       {
                                           Id = t.Id,
                                           DateCreated = t.DateCreated.ToString(),
                                           Value = t.Value,
                                           TransactionType = t.TransactionType.ToString(),
                                           BalanceId = t.BalanceId
                                       }).ToList()
                    }).ToList();
            return new ServiceResponse
            {
                Success = true,
                Payload = balances
            };
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var balances = _context.Balances
                    .Where(b => b.Id == id)
                    .Select(b => new BalanceItemDTO
                    {
                        Id = b.Id,
                        UserId = b.UserId,
                        DateCreated = b.DateCreated.ToString(),
                        Money = b.Money,
                        Reviewed = b.Reviewed,
                        countTransactions = b.TransactionHistory.Where(t => t.BalanceId == b.Id).Count(),
                        Transactions = b.TransactionHistory
                                       .Where(t => t.BalanceId == b.Id)
                                       .Select(t => new TransactionItemDTO
                                       {
                                           Id = t.Id,
                                           DateCreated = t.DateCreated.ToString(),
                                           Value = t.Value,
                                           TransactionType = t.TransactionType.ToString(),
                                           BalanceId = t.BalanceId
                                       }).ToList()
                    }).ToList();

            if (balances != null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Payload = balances[0]
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Balance was not found"
            };
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
            if (Double.Parse(model.Money) > Double.Parse(balance.Money)) 
            {
                return new ServiceResponse
                {
                    Message = "Don't have enough money.",
                    Success = true,
                };
            }
            double tmp = Double.Parse(balance.Money) - Double.Parse(model.Money);
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
