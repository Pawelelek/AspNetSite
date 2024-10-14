using Go1Bet.Core.Constants;
using Go1Bet.Core.Context;
using Go1Bet.Infrastructure.DTO_s.Balance;
using Go1Bet.Infrastructure.DTO_s.Category;
using Go1Bet.Infrastructure.DTO_s.User;
using Go1Bet.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go1Bet.Infrastructure.Services
{
    public class BalanceService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public BalanceService(AppDbContext context, UserManager<AppUser> userManager) 
        { 
            _context = context;
            _userManager = userManager;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            try
            {
                var balances = await _context.Balances

                    .Select(b => new BalanceItemDTO
                    {
                        Id = b.Id,
                        UserId = b.UserId,
                        DateCreated = b.DateCreated.ToString(),
                        Money = b.Money.ToString(),
                        Reviewed = b.Reviewed,
                        countTransactions = b.TransactionHistory.Where(t => t.BalanceId == b.Id).Count()
                    }).ToListAsync();

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
            var balances = await _context.Balances
                    .Where(u => u.UserId == id)
                    .Select(b => new BalanceItemDTO
                    { 
                        Id = b.Id,
                        UserId = b.UserId,
                        DateCreated = b.DateCreated.ToString(),
                        Money = b.Money.ToString(),
                        Reviewed = b.Reviewed,
                        countTransactions = b.TransactionHistory.Where(t => t.BalanceId == b.Id).Count(),
                        Transactions = b.TransactionHistory
                                       .Where(t => t.BalanceId == b.Id)
                                       .Select(t => new TransactionItemDTO
                                       {
                                           Id = t.Id,
                                           DateCreated = t.DateCreated.ToString(),
                                           Value = t.Value.ToString(),
                                           TransactionType = t.TransactionType.ToString(),
                                           BalanceId = t.BalanceId,
                                           Description = t.Description
                                       }).ToList()
                    }).ToListAsync();
            return new ServiceResponse
            {
                Success = true,
                Payload = balances
            };
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var balances = await _context.Balances
                    .Where(b => b.Id == id)
                    .Select(b => new BalanceItemDTO
                    {
                        Id = b.Id,
                        UserId = b.UserId,
                        DateCreated = b.DateCreated.ToString(),
                        Money = b.Money.ToString(),
                        Reviewed = b.Reviewed,
                        countTransactions = b.TransactionHistory.Where(t => t.BalanceId == b.Id).Count(),
                        Transactions = b.TransactionHistory
                                       .Where(t => t.BalanceId == b.Id)
                                       .Select(t => new TransactionItemDTO
                                       {
                                           Id = t.Id,
                                           DateCreated = t.DateCreated.ToString(),
                                           Value = t.Value.ToString(),
                                           TransactionType = t.TransactionType.ToString(),
                                           BalanceId = t.BalanceId,
                                           Description = t.Description
                                       }).ToList()
                    }).ToListAsync();

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
            if(model.Money == default) { model.Money = 0; }
            var balance = new BalanceEntity() { Money = model.Money, UserId = model.UserId };
            await _context.Balances.AddAsync(balance);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Message = "Balance has been created.",
                Success = true,
            };
        }
        public async Task<ServiceResponse> DepositAsync(BalanceInteractionDTO model)
        {
            var depositExits = await _context.Transactions.Where(t => t.BalanceId == model.BalanceId && t.TransactionType == TransactionType.Deposit).AnyAsync();
            double money = model.Money;
            if(!depositExits)
            {
                
                model.BonusMoney = model.Money > 3000 ? 3000 : model.Money;
            }
            BalanceInteraction(model.BalanceId, money, model.Discount, model.BonusMoney, TransactionType.Deposit);
            return new ServiceResponse
            {
                Message = "The money was credited.",
                Success = true,
            };
        }
        public void BalanceInteraction(string balanceId, double money, int discount, double bonusMoney, TransactionType transactionType, string description = "System")
        {
            //With async - not working!
            var balance =  _context.Balances.Where(b => b.Id == balanceId).FirstOrDefault();
            double tempMoney = money;
            if (discount > 0 && transactionType == TransactionType.Deposit)
            {
                tempMoney = money + ((money / 100) * discount);
            }
            double sumMoney = tempMoney + bonusMoney;
            if (transactionType == TransactionType.Withdrawal && -money > balance.Money)
            {
                sumMoney = 0;
            }
            balance.Money += sumMoney;
            var transaction = new TransactionEntity() { BalanceId = balance.Id, DateCreated = DateTime.UtcNow, TransactionType = transactionType, Value = sumMoney, Description = description, Discount =  discount};
            _context.Transactions.Add(transaction);

            _context.Balances.Update(balance);
            _context.SaveChanges();
        }
        public async Task<ServiceResponse> WithdrawalAsync(BalanceInteractionDTO model)
        {
            BalanceInteraction(model.BalanceId, -model.Money, 0, 0, TransactionType.Withdrawal);
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
