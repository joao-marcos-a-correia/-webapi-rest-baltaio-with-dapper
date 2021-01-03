﻿using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(
                new ValidationContract()
                    .Requires()
                    .IsEmail(Address, "Email", "O Email é Inválido")
            );      
        }
        public string Address { get; private set; }

    public override string ToString()
    {
        return Address;
    }
}
}
