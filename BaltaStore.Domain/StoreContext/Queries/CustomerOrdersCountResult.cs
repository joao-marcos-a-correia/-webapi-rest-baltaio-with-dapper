using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Queries
{
    public class CustomerOrdersCountResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document{ get; set; }
        public int Orders { get; set; }
    }
}
