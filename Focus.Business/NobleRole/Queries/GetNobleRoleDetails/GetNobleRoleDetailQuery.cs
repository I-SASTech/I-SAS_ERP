using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.NobleRole.Queries.GetNobleRoleDetails
{
    public class GetNobleRoleDetailQuery : IRequest<NobleRoleDetailsLookUpModel>
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public class Handler : IRequestHandler<GetNobleRoleDetailQuery, NobleRoleDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetNobleRoleDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<NobleRoleDetailsLookUpModel> Handle(GetNobleRoleDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    NobleRoles nobleRoles = null;
                    if (request.CompanyId.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        nobleRoles = await _context.NobleRoles.FindAsync(request.Id);

                        if (nobleRoles != null)
                        {
                            return NobleRoleDetailsLookUpModel.Create(nobleRoles);
                        }
                    }
                    else
                    {
                        _context.DisableTenantFilter = true;
                        nobleRoles = await _context.NobleRoles.FirstOrDefaultAsync(x =>
                            EF.Property<Guid>(x, "CompanyId") == request.CompanyId, cancellationToken: cancellationToken);

                        if (nobleRoles != null)
                        {
                            return NobleRoleDetailsLookUpModel.Create(nobleRoles);
                        }
                    }

                    throw new NotFoundException(nameof(nobleRoles), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
