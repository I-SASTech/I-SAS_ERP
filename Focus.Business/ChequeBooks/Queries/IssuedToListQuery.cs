using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ChequeBooks.Queries
{
 public   class IssuedToListQuery : IRequest<List<IssuedToLookUpModel>>
    {

        public class Handler : IRequestHandler<IssuedToListQuery, List<IssuedToLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<IssuedToListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<IssuedToLookUpModel>> Handle(IssuedToListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var chequebook = await _context.Contacts.Where(x => x.IsActive).AsNoTracking()
                       .ToListAsync();
                    var employees = await _context.EmployeeRegistrations.Where(x=>!x.IsActive).AsNoTracking()
                       .ToListAsync();
                    var isserLsit = await _context.Issueds.AsNoTracking()
                     .ToListAsync();
                    var chequebookList = new List<IssuedToLookUpModel>();

                    if (isserLsit != null)
                    {
                        foreach (var item in isserLsit)
                        {
                            chequebookList.Add(new IssuedToLookUpModel()
                            {

                                Id = item.Id,
                                Name= item.Name,
                                AccountId=item.AccountId

                            });
                        }

                    } 
                    if (chequebook != null)
                    {
                        foreach (var item in chequebook)
                        {
                            chequebookList.Add(new IssuedToLookUpModel()
                            {

                                Id = item.Id,
                                Name = item.EnglishName == "" || item.EnglishName == null ? item.ArabicName : item.EnglishName,
                                NameArabic = item.ArabicName,
                                AccountId=item.AccountId

                            });
                        }

                    }
                    if(employees!=null)
                    {
                        foreach (var item in employees)
                        {
                            chequebookList.Add(new IssuedToLookUpModel()
                            {

                                Id = item.Id,
                                Name = item.EnglishName=="" || item.EnglishName == null ? item.ArabicName: item.EnglishName,
                                NameArabic = item.ArabicName,
                                AccountId=item.PayableAccountId
                            });
                        }
                    }
                    return chequebookList;

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
