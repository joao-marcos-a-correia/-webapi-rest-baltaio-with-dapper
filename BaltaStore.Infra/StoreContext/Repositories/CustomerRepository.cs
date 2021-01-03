﻿using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Infra.StoreContext.DataContexts;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BaltaStore.Domain.StoreContext.Queries;

namespace BaltaStore.Infra.StoreContext.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BaltaDataContext _context;

        public CustomerRepository(BaltaDataContext context)
        {
            _context = context;
        }
        public bool CheckDocument(string document)
        {
            return
                _context.Connection
                .Query<bool>("spCheckDocument",
                new { Document = document },
                commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public bool CheckEmail(string email)
        {
            return
                _context.Connection
                .Query<bool>("spCheckEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<ListCustomerQueryResult> Get()
        {
            return
                _context.Connection
                .Query<ListCustomerQueryResult>(
                    "SELECT " +
                    "[Id]," +
                    "CONCAT(FirstName, ' ', LastName) as Name," +
                    "[Document]," +
                    "[Email] " +
                    "FROM [Customer]");
        }

        public GetCustomerQueryResult Get(Guid Id)
        {
            return
                _context.Connection
                .Query<GetCustomerQueryResult>(
                    "SELECT " +
                    "[Id]," +
                    "CONCAT(FirstName, ' ', LastName) as Name," +
                    "[Document]," +
                    "[Email] " +
                    "FROM [Customer] WHERE Id = @Id",
                new { Id })
                .FirstOrDefault();
        }

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document)
        {
            return
                _context.Connection
                .Query<CustomerOrdersCountResult>(
                    "spGetCustomerOrdersCount",
                new { Document = document },
                commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<ListCustomerOrderQueryResult> GetOrders(Guid Id)
        {
            return
                _context.Connection
                .Query<ListCustomerOrderQueryResult>(
                    "",
                new { Id },
                commandType: CommandType.StoredProcedure);
        }

        public void Save(Customer customer)
        {
            _context.Connection.Execute(
                "spCreateCustomer",
                new
                {
                    Id = customer.Id,
                    FirstName = customer.Name.FirstName,
                    Lastname = customer.Name.LastName,
                    Document = customer.Document.Number,
                    Email = customer.Email.Address,
                    Phone = customer.Phone
                }, commandType: CommandType.StoredProcedure);

            foreach (var address in customer.Addresses)
            {
                _context.Connection.Execute(
                    "spCreateAddress",
                    new
                    {
                        Id = address.Id,
                        CustomerId = customer.Id,
                        Number = address.Number,
                        Complement = address.Complement,
                        District = address.District,
                        City = address.City,
                        State = address.State,
                        Country = address.Country,
                        ZipCode = address.ZipCode,
                        Type = address.Type
                    }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
