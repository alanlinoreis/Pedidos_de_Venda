using System;

namespace Domain.Entities.Entidades
{
    // Classe base (não abstrata)
    public class Pedido
    {
        // Delegates de composição (estratégias)
        private readonly Func<decimal, decimal> _frete;
        private readonly Func<decimal, decimal> _promocao;

        // Construtor com injeção opcional das estratégias
        public Pedido(
            Func<decimal, decimal>? frete = null,
            Func<decimal, decimal>? promocao = null)
        {
            _frete = frete ?? (v => v);         // padrão: sem alteração
            _promocao = promocao ?? (v => v);   // padrão: sem desconto
        }

        // Método público: orquestra o ritual fixo
        public string Processar()
        {
            Validar();
            decimal total = CalcularTotal();
            return EmitirRecibo(total);
        }

        // Ganchos protegidos (podem ser sobrescritos)
        protected virtual void Validar() { }

        protected virtual decimal CalcularSubtotal() => 100m; // regra padrão

        protected virtual string EmitirRecibo(decimal total)
            => $"Recibo: {total:C}";

        // Parte comum: aplica políticas plugáveis
        protected decimal CalcularTotal()
        {
            decimal subtotal = CalcularSubtotal();
            decimal comFrete = _frete(subtotal);
            decimal final = _promocao(comFrete);
            return final;
        }
    }
}
