using System;
using Domain.Entities.Entidades;
using Xunit;

namespace Domain.Entities.Tests
{
    public class PedidoInternacionalTests
    {
        [Fact]
        public void Processar_DeveEmitirCommercialInvoiceComValorCorreto()
        {
            var pedido = new PedidoInternacional();
            var recibo = pedido.Processar();
            Assert.Contains("Commercial Invoice", recibo);
            Assert.Contains("500", recibo);
        }

        [Fact]
        public void CalcularSubtotal_DeveRetornarValorEsperado()
        {
            var pedido = new PedidoInternacional();
            var subtotal = typeof(PedidoInternacional)
                .GetMethod("CalcularSubtotal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .Invoke(pedido, null);
            Assert.Equal(500m, (decimal)subtotal!);
        }

        [Fact]
        public void Processar_ComFreteEPromocao_DeveAplicarCorretamente()
        {
            var pedido = new PedidoInternacional(frete: v => v + 200, promocao: v => v * 0.8m);
            var recibo = pedido.Processar();
            Assert.Contains("560", recibo); // 500 + 200 = 700 * 0.8 = 560
        }

        [Fact]
        public void Validar_DeveExecutarSemErros()
        {
            var pedido = new PedidoInternacional();
            var method = typeof(PedidoInternacional)
                .GetMethod("Validar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            var exception = Record.Exception(() => method.Invoke(pedido, null));
            Assert.Null(exception);
        }

        [Fact]
        public void EmitirRecibo_DeveRetornarStringFormatada()
        {
            var pedido = new PedidoInternacional();
            var method = typeof(PedidoInternacional)
                .GetMethod("EmitirRecibo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            var recibo = (string)method.Invoke(pedido, new object[] { 999.99m })!;
            Assert.Contains("Commercial Invoice emitida", recibo);
        }
    }

}
