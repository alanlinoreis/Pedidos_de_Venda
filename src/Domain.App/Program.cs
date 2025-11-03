using System;
using Domain.Entities.Entidades;

namespace Domain.App
{
    class Program
    {
        // Teste LSP: aceita qualquer Pedido e chama Processar()
        static void ProcessarPedido(Pedido pedido)
        {
            Console.WriteLine(pedido.Processar());
            Console.WriteLine();
        }

        static void Main()
        {
            // Estratégias de Frete
            Func<decimal, decimal> freteFixo = v => v + 50m;
            Func<decimal, decimal> fretePercentual = v => v * 1.10m;

            // Estratégias de Promoção
            Func<decimal, decimal> semPromocao = v => v;
            Func<decimal, decimal> cupom10 = v => v * 0.90m;

            Console.WriteLine("=== Teste de LSP ===");
            Pedido p1 = new PedidoNacional(freteFixo, cupom10);
            Pedido p2 = new PedidoInternacional(fretePercentual, semPromocao);

            // Teste LSP (sem downcast, funciona igual)
            ProcessarPedido(p1);
            ProcessarPedido(p2);

            Console.WriteLine("=== Teste de Composição (troca de estratégias) ===");

            // Troca dinâmica dos delegates — sem criar novas subclasses
            var pedidoComOutroFrete = new PedidoNacional(fretePercentual, cupom10);
            ProcessarPedido(pedidoComOutroFrete);

            var pedidoSemPromocao = new PedidoInternacional(freteFixo, semPromocao);
            ProcessarPedido(pedidoSemPromocao);
        }
    }
}
