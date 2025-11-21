using CaseItauDigitalAssetsBank.Application.Interfaces;
using CaseItauDigitalAssetsBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItauDigitalAssetsBank.Application.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _repo;
        private readonly IUnitOfWork? _uow;

        public ClienteService(IClienteRepository repo, IUnitOfWork? uow = null)
        {
            _repo = repo;
            _uow = uow;
        }

        public Task<List<Cliente>> GetAllAsync(CancellationToken ct = default) => _repo.GetAllAsync(ct);
        public Task<Cliente?> GetByIdAsync(int id, CancellationToken ct = default) => _repo.GetByIdAsync(id, ct);

        public async Task<Cliente> CreateAsync(string nome, string email, CancellationToken ct = default)
        {
            var c = new Cliente { Nome = nome, Email = email, Saldo = 0m };
            await _repo.AddAsync(c, ct);
            await _repo.SaveChangesAsync(ct);
            return c;
        }

        public async Task<bool> UpdateAsync(int id, string nome, string email, CancellationToken ct = default)
        {
            var c = await _repo.GetByIdAsync(id, ct);
            if (c == null) return false;
            c.Nome = nome;
            c.Email = email;
            await _repo.UpdateAsync(c, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var c = await _repo.GetByIdAsync(id, ct);
            if (c == null) return false;
            await _repo.DeleteAsync(c, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DepositarAsync(int id, decimal valor, CancellationToken ct = default)
        {
            if (valor <= 0) return false;
            return await _repo.TryDepositAsync(id, valor, ct);
        }

        public async Task<bool> SacarAsync(int id, decimal valor, CancellationToken ct = default)
        {
            if (valor <= 0) return false;
            return await _repo.TryWithdrawAsync(id, valor, ct);
        }
    }
}
