using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Shared.Entities;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaltaStore.Domain.StoreContext.Entities
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _items;
        private readonly IList<Delivery> _deliveries;

        public Order(Customer customer)
        {
            Customer = customer;
            CreateDate = DateTime.Now;
            Status = EOrderStatus.CREATED;
            _items = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

        public void AddItem(Product product, decimal quantity)
        {
            if (quantity > product.QuantityOnHand)
            {
                AddNotification("OrderItem", $"O produto {product.Title} não tem quantidade em estoque - Quantidade: {product.QuantityOnHand}");
            }

            var item = new OrderItem(product, quantity);
            _items.Add(item);
        }

        // To Place an Order
        public void Place()
        {
            // Gera o numero do Pedido
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            // Validar
            if (_items.Count == 0)
                AddNotification("Order", "Este pedido não possui itens");
        }

        // Pagar Um Pedido
        public void Pay()
        {
            Status = EOrderStatus.PAID;
        }

        // Enviar um Pedido
        public void Ship()
        {
            // A cada 5 produtos é uma entrega
            var count = 1;
            var deliveries = new List<Delivery>();

            //quebra as entregas
            foreach (var item in _items)
            {
                if (count == 5)
                {
                    count = 1;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }

                count++;
            }

            if (deliveries == null && deliveries.Count == 0)
            {
                deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
            }

            //Envia as entregas
            deliveries.ForEach(x => x.Ship());

            //Adiciona as entregas ao pedido
            deliveries.ForEach(x => _deliveries.Add(x));

        }

        // Cancelar um pedido
        public void Cancel()
        {
            Status = EOrderStatus.CANCELLED;
            _deliveries.ToList().ForEach(x => x.Cancel());
        }
    }
}
