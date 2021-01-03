using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Domain.StoreContext.Services;
using BaltaStore.Domain.StoreContext.ValueObjects;
using BaltaStore.Shared.Commands;
using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler : Notifiable, ICommandHandler<CreateCustomerCommand>, ICommandHandler<AddAddressCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;

        public CustomerHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateCustomerCommand command)
        {
            //Verifica se CPF ja existe base
            if (_customerRepository.CheckDocument(command.Document))
            {
                AddNotification("Document", "Este CPF ja está em uso");
            }

            //Verificar se email ja existe na base
            if (_customerRepository.CheckDocument(command.Email))
            {
                AddNotification("Document", "Este E-mail já está em uso");
            }

            //Criar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            //Criar a entidade
            var customer = new Customer(name, document, email, command.Phone);


            //Validar entidades e VOs
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if (Invalid)
                return new CommandResult(
                        false,
                        "Por favor corrija os campos abaixo",
                        Notifications
                       );

            //Persistir o cliente
            _customerRepository.Save(customer);

            //Enviar email de boas vindas
            _emailService.Send(email.Address, "jmarcos15vnz@gmail.com", "Bem Vindo", "Seja Bem Vindo ao Balta Store");

            //Retornar o resultado para tela
            CommandResult commandResult = new CommandResult(
                true,
                "Bem vindo ao BaltaStore",
                new
                {
                    Id = customer.Id,
                    Name = name.ToString(),
                    Email = email.Address
                });

            return commandResult;
        }

        public ICommandResult Handle(AddAddressCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
