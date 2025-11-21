using CaseItauDigitalAssetsBank.Application.Interfaces;
using CaseItauDigitalAssetsBank.Domain.Entities;
using CaseItauDigitalAssetsBank.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItauDigitalAssetsBank.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _ctx;
        public ClienteRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Cliente cliente, CancellationToken ct = default) => await _ctx.Clientes.AddAsync(cliente, ct);
        public async Task DeleteAsync(Cliente cliente, CancellationToken ct = default) { _ctx.Clientes.Remove(cliente); await Task.CompletedTask; }
        public async Task<List<Cliente>> GetAllAsync(CancellationToken ct = default) => await _ctx.Clientes.AsNoTracking().ToListAsync(ct);
        public async Task<Cliente?> GetByIdAsync(int id, CancellationToken ct = default) => await _ctx.Clientes.FirstOrDefaultAsync(c => c.Id == id, ct);
        public async Task UpdateAsync(Cliente cliente, CancellationToken ct = default) { _ctx.Clientes.Update(cliente); await Task.CompletedTask; }
        public async Task SaveChangesAsync(CancellationToken ct = default) => await _ctx.SaveChangesAsync(ct);

        public async Task<bool> TryWithdrawAsync(int id, decimal amount, CancellationToken ct = default)
        {
            var sql = @"UPDATE Clientes SET Saldo = Saldo - @p0 WHERE Id = @p1 AND Saldo >= @p0";
            var affected = await _ctx.Database.ExecuteSqlRawAsync(sql, new object[] { amount, id }, ct);
            return affected > 0;
        }

        public async Task<bool> TryDepositAsync(int id, decimal amount, CancellationToken ct = default)
        {
            var sql = @"UPDATE Clientes SET Saldo = Saldo + @p0 WHERE Id = @p1";
            var affected = await _ctx.Database.ExecuteSqlRawAsync(sql, new object[] { amount, id }, ct);
            return affected > 0;
        }

    }

}
