using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Queries
{
    public class GetCustomerQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
    }
}
