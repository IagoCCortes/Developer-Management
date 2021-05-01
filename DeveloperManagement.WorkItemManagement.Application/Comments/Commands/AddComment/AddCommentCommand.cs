﻿using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Comments.Commands.AddComment
{
    public class AddCommentCommand : IRequest
    {
        public Guid WorkItemId { get; set; }
        public string Comment { get; set; }
    }
    
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _userService;

        public AddCommentCommandHandler(IUnitOfWork uow, ICurrentUserService userService)
        {
            _uow = uow;
            _userService = userService;
        }
        
        public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}