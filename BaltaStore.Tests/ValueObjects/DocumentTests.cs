using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaltaStore.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        private Document _validDocument;
        private Document _invalidDocument;

        public DocumentTests()
        {
            _validDocument = new Document("70449596010");
            _invalidDocument = new Document("12345678910");
        }

        [TestMethod]
        public void ShouldReturnNotficationWhenDocumentIsNotValid()
        {
            Assert.AreEqual(false, _invalidDocument.IsValid);
        }

        [TestMethod]
        public void ShouldNotReturnNotficationWhenDocumentIsValid()
        {
            Assert.AreEqual(true, _validDocument.IsValid);
        }
    }
}
