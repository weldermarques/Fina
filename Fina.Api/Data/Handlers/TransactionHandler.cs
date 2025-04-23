using Fina.Core.Common;
using Fina.Core.Enums;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Data.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{

    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            if (request is { Type: EnumTransactionType.Withdrawal, Ammount: >= 0})
                request.Ammount *= -1;

            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Ammount = request.Ammount,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Title = request.Title,
                Type = request.Type,
            };

            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new Response<Transaction?>(null, 500, "Erro ao criar transação");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(s 
                    => s.Id == request.Id && 
                       s.UserId == request.UserId);
            
            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");
            
            transaction.CategoryId = request.CategoryId;
            transaction.Ammount = request.Ammount;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new Response<Transaction?>(null, 500, "Erro ao criar transação");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .FirstOrDefaultAsync(s 
                    => s.Id == request.Id && 
                       s.UserId == request.UserId);
            
            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");
            

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transação removida com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new Response<Transaction?>(null, 500, "Erro ao remover transação");
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(s 
                    => s.Id == request.Id && 
                       s.UserId == request.UserId);

            return transaction is null ? 
                new Response<Transaction?>(null, 404, "Transação não encontrada") : 
                new Response<Transaction?>(transaction);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new Response<Transaction?>(null, 500, "Erro ao buscar Transação");
        }
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();

            var query = context
                .Transactions
                .AsNoTracking()
                .Where(s =>
                    s.PaidOrReceivedAt >= request.StartDate &&
                    s.PaidOrReceivedAt <= request.EndDate &&
                    s.UserId == request.UserId)
                .OrderBy(s => s.PaidOrReceivedAt);
            
            var transactions = await query
                .Skip((request.PageNumber -1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();

            return new PagedResponse<List<Transaction>?>(
                transactions, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return new PagedResponse<List<Transaction>?>(null, 500, "Erro ao buscar Transações");
        }
    }
}
