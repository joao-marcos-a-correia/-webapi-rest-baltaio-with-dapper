using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Product _mouse;
        private Product _keyboard;
        private Product _chair;
        private Product _monitor;
        private Customer _customer;
        private Order _order;

        public OrderTests()
        {
            var name = new Name("Joao", "Marcos");
            var document = new Document("70449596010");
            var email = new Email("joao@gmail.com");
            _customer = new Customer(name, document, email, "12345678");
            _order = new Order(_customer);
            _mouse = new Product("Mouse Gamer", "Mouse Gamer", "mouse.jpg", 100M, 10);
            _keyboard = new Product("Teclado", "Teclado", "teclado.jpg", 100M, 10);
            _chair = new Product("Cadeira", "Cadeira", "cadeira.jpg", 100M, 10);
            _monitor = new Product("Monitor", "Monitor", "monitor.jpg", 100M, 10);
        }
        //Place an order
        [TestMethod]
        public void ShouldCreateOrderWhenValid()
        {
            Assert.AreEqual(true, _order.IsValid);
        }

        //OrderStatus after created must be Created
        [TestMethod]
        public void StatusShouldBeCreatedWhenOrderCreated()
        {
            Assert.AreEqual(EOrderStatus.CREATED, _order.Status);
        }

        //After add a new item, amount must change
        [TestMethod]
        public void ShouldReturnTwoWhenAddedTwoValidItems()
        {
            _order.AddItem(_monitor, 5);
            _order.AddItem(_mouse, 5);
            Assert.AreEqual(2, _order.Items.Count);
        }

        //After add a new item, product stock must be subtracted
        [TestMethod]
        public void ShouldReturnFiveWhenPurchasedFiveItem()
        {
            _order.AddItem(_mouse, 5);
            Assert.AreEqual(_mouse.QuantityOnHand, 5);
        }

        //After confirm order, this must be a number
        [TestMethod]
        public void ShouldReturnAnNumberWhenOrderPlaced()
        {
            _order.Place();
            Assert.AreNotEqual("", _order.Number);
        }

        //After pay an order, OrderStatus must be Pay
        [TestMethod]
        public void ShouldReturnPaidWhenOrderPaid()
        {
            _order.Pay();
            Assert.AreEqual(EOrderStatus.PAID, _order.Status);
        }

        //Which more than 10 prouducts, should exists more than 2 deliveries
        [TestMethod]
        public void ShouldReturnTwoShippingsWhenPurchasedTenProducts()
        {
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);

            _order.Ship();

            Assert.AreEqual(2, _order.Deliveries.Count);
        }

        //After cancel an order, OrderStatus must be Cancel
        [TestMethod]
        public void StatusShouldBeCancelWhenOrderCanceled()
        {
            _order.Cancel();
            Assert.AreEqual(EOrderStatus.CANCELLED, _order.Status);
        }

        //After cancel an order, pending deliveries must be Cancel
        [TestMethod]
        public void ShouldCancelShippingWhenOrderCancelled()
        {
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);
            _order.AddItem(_mouse, 5);

            _order.Ship();
            _order.Cancel();

            foreach (var x in _order.Deliveries)
            {
                Assert.AreEqual(EDeliveryStatus.CANCELLED, x.Status);
            }

        }
    }
}
