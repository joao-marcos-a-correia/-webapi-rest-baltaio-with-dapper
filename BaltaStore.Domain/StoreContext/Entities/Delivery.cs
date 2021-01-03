using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Shared.Entities;
using System;
using System.Collections.Generic;

namespace BaltaStore.Domain.StoreContext.Entities
{
    public class Delivery : Entity
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            CreateDate = DateTime.Now;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Status = EDeliveryStatus.WAITING;

        }
        public DateTime CreateDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public EDeliveryStatus Status { get; private set; }

        public void Ship()
        {
            Status = EDeliveryStatus.SHIPPED;
        }

        public void Cancel()
        {
            //Se entrega ja realizada, não permitir cancelar
            if (Status != EDeliveryStatus.DELIVERED)
            {
                Status = EDeliveryStatus.CANCELLED;
            }
        }
    }
}
