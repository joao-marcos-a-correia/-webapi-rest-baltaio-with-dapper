using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
    }
}
