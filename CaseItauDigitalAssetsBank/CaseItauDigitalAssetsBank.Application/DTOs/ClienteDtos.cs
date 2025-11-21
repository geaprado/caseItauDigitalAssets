using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItauDigitalAssetsBank.Application.DTOs
{
    public record ClienteCreateDto(string Nome, string Email);
    public record ClienteDto(int Id, string Nome, string Email, decimal Saldo);
    public record OperacaoDto(decimal Valor);
}
