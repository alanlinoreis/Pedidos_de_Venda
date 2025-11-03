using System;
using Domain.Entities.Entidades;
using Xunit;

namespace Domain.Entities.Tests
{
    public class PedidoNacionalTests
    {
        [Fact]
        public void Processar_DeveEmitirNFeComValorCorreto()
        {
            var pedido = new PedidoNacional();
            var recibo = pedido.Processar();
            Assert.Contains("NF-e", recibo);
            Assert.Contains("200", recibo);
        }

        [Fact]
        public void CalcularSubtotal_DeveRetornarValorEsperado()
        {
            var pedido = new PedidoNacional();
            var subtotal = typeof(PedidoNacional)
                .GetMethod("CalcularSubtotal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .Invoke(pedido, null);
            Assert.Equal(200m, (decimal)subtotal!);
        }

        [Fact]
        public void Processar_ComFreteEPromocao_DeveAlterarValorFinal()
        {
            var pedido = new PedidoNacional(frete: v => v + 100, promocao: v => v * 0.5m);
            var recibo = pedido.Processar();
            Assert.Contains("150", recibo); // 200 + 100 = 300 * 0.5 = 150
        }

        [Fact]
        public void Validar_DeveExecutarSemErros()
        {
            var pedido = new PedidoNacional();
            var method = typeof(PedidoNacional)
                .GetMethod("Validar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            var exception = Record.Exception(() => method.Invoke(pedido, null));
            Assert.Null(exception);
        }

        [Fact]
        public void EmitirRecibo_DeveRetornarStringFormatada()
        {
            var pedido = new PedidoNacional();
            var method = typeof(PedidoNacional)
                .GetMethod("EmitirRecibo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            var recibo = (string)method.Invoke(pedido, new object[] { 250m })!;
            Assert.Equal("NF-e emitida no valor de R$ 250,00", recibo.Replace(".", ","));
        }
    }

}
