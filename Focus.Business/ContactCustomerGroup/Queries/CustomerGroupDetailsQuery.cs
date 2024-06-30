using Focus.Business.ContactCustomerGroup.Model;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.ProductGroups.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.ContactCustomerGroup.Queries
{
    public class CustomerGroupDetailsQuery : IRequest<CustomerGroupLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<CustomerGroupDetailsQuery, CustomerGroupLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<CustomerGroupDetailsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<CustomerGroupLookupModel> Handle(CustomerGroupDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = await _context.CustomerGroups
                        .Select(productGroup => new CustomerGroupLookupModel()
                        {
                            Id = productGroup.Id,
                            Name = productGroup.Name,
                            NameArabic = productGroup.NameArabic,
                            Description = productGroup.Description,
                            Code = productGroup.Code,
                            Status = productGroup.Status,
                            ContactsList = productGroup.Contacts.Select(x => new ContactListLookupModel
                            {
                                ContactId = x.Id,
                                Code = x.Code,
                                Name = x.CustomerDisplayName,

                            }).ToList()
                        }).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);



                        return customer;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }

    }
}
