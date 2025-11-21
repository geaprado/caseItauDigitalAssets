using Moq;
using CaseItauDigitalAssetsBank.Application.Interfaces;
using CaseItauDigitalAssetsBank.Application.Services;
namespace CaseItauDigitalAssetsBank.Test

{
    [TestClass]
    public class ClienteServiceTests
    {
        [TestMethod]
        public async Task Depositar_DeveRetornarTrue_QuandoSucesso()
        {
            var repo = new Mock<IClienteRepository>();
            repo.Setup(r => r.TryDepositAsync(1, 100m, default))
                .ReturnsAsync(true);

            var service = new ClienteService(repo.Object);

            var ok = await service.DepositarAsync(1, 100m);

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public async Task Sacar_DeveRetornarFalse_QuandoSaldoInsuficiente()
        {
            var repo = new Mock<IClienteRepository>();
            repo.Setup(r => r.TryWithdrawAsync(1, 100m, default))
                .ReturnsAsync(false);

            var service = new ClienteService(repo.Object);

            var ok = await service.SacarAsync(1, 100m);

            Assert.IsFalse(ok);
        }
    }
}
