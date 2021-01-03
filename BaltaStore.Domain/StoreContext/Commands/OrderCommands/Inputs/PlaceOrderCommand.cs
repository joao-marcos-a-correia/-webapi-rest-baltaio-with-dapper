using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Commands.OrderCommands.Inputs
{
    public class PlaceOrderCommand : Notifiable, ICommand
    {
        public PlaceOrderCommand()
        {
            OrderItems = new List<OrderItemCommand>();
        }
        public Guid Customer { get; set; }
        public IList<OrderItemCommand> OrderItems { get; set; }

        public bool Valid()
        {
            AddNotifications(
                new ValidationContract()
                    .HasLen(Customer.ToString(), 36, "Customer", "Idenficador do cliente inválido")
                    .IsGreaterThan(OrderItems.Count, 0, "OrderItems", "Nenhum item do pedido foi encontrado")
             );

            return base.IsValid;
        }
    }

    public class OrderItemCommand
    {
        public Guid Product { get; set; }
        public decimal Quantity { get; set; }
    }
}
