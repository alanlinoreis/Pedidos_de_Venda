using System;
using Domain.Entities.Entidades;
using Xunit;

namespace Domain.Entities.Tests
{
    public class PedidoTests
    {
        [Fact]
        public void Processar_DeveGerarReciboComValorPadrao()
        {
            var pedido = new Pedido();
            var recibo = pedido.Processar();
            Assert.Contains("Recibo", recibo);
        }

        [Fact]
        public void CalcularTotal_DeveRetornarValorBaseQuandoSemEstrategias()
        {
            var pedido = new Pedido();
            var total = typeof(Pedido)
                .GetMethod("CalcularTotal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .Invoke(pedido, null);
            Assert.Equal(100m, (decimal)total!);
        }

        [Fact]
        public void CalcularTotal_DeveAplicarFreteCorretamente()
        {
            var pedido = new Pedido(frete: v => v + 50);
            var total = typeof(Pedido)
                .GetMethod("CalcularTotal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .Invoke(pedido, null);
            Assert.Equal(150m, (decimal)total!);
        }

        [Fact]
        public void CalcularTotal_DeveAplicarPromocaoCorretamente()
        {
            var pedido = new Pedido(promocao: v => v * 0.9m);
            var total = typeof(Pedido)
                .GetMethod("CalcularTotal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .Invoke(pedido, null);
            Assert.Equal(90m, (decimal)total!);
        }

        [Fact]
        public void Processar_DeveRetornarReciboComValorFinalCorreto()
        {
            var pedido = new Pedido(frete: v => v + 20, promocao: v => v - 10);
            var recibo = pedido.Processar();
            Assert.Contains("Recibo", recibo);
            Assert.Contains("110", recibo); // 100 + 20 - 10
        }
    }
}
