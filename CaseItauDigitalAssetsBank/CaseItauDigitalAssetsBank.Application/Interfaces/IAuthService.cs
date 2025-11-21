using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItauDigitalAssetsBank.Application.Interfaces
{
    public interface IAuthService
    {
        bool ValidateCredentials(string username, string password);
        string GenerateToken(string username, IEnumerable<string>? roles = null);
    }
}
