using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseItauDigitalAssetsBank.Domain.Entities;

namespace CaseItauDigitalAssetsBank.Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAllAsync(CancellationToken ct = default);
        Task<Cliente?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(Cliente cliente, CancellationToken ct = default);
        Task UpdateAsync(Cliente cliente, CancellationToken ct = default);
        Task DeleteAsync(Cliente cliente, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);

        Task<bool> TryWithdrawAsync(int id, decimal amount, CancellationToken ct = default);
        Task<bool> TryDepositAsync(int id, decimal amount, CancellationToken ct = default);

    }
}
