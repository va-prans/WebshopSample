﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Webshop.Application.Common;
using Webshop.Application.Interfaces;

namespace Webshop.Application.Account.Commands.Create
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Domain.Entities.Account>
    {
        private readonly IWebshopContext _context;

        public CreateAccountCommandHandler(
            IWebshopContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Account
            {
                OwnerId = request.OwnerId,
                Name = request.Name,                
            };

            _context.Accounts.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
