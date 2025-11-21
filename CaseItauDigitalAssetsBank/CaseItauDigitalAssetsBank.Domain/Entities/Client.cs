using CaseItauDigitalAssetsBank.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItauDigitalAssetsBank.Domain.Entities
{
    public sealed class Client : Account
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public Client(string name, string email)
        {
            DomainExceptionValidation.When(Id < 0, "Id inválido.");
            ValidateDomain(name, email);
            Id = Id;
        }

        public void Update(string name, string email)
        {
            ValidateDomain(name, email);
        }

        private void ValidateDomain(string name, string email)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome inválido. O nome é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Email inválido. O email é obrigatório");

            Name = name;

            Email = email;

            Saldo = Saldo;

        }
    }
}
