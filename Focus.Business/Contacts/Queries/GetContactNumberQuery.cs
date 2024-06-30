using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Contacts.Queries
{
   public class GetContactNumberQuery : IRequest<ContactRegistrationLookUpModel>
    {
        public bool isCustomer { get; set; }
        public string CashCustomer { get; set; }
        public bool isCashCustomer { get; set; }

        public class Handler : IRequestHandler<GetContactNumberQuery, ContactRegistrationLookUpModel>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetContactNumberQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<ContactRegistrationLookUpModel> Handle(GetContactNumberQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var contact = new ContactRegistrationLookUpModel();

                    var autoCode = await Context.Contacts.OrderBy(x => x.Code).Where(x => x.IsCustomer == request.isCustomer && !x.IsCashCustomer)
                        .Select(x=>new {
                            Code=x.Code
                    }).LastOrDefaultAsync(cancellationToken);
                    if(request.isCashCustomer)
                    {
                        var cashCustomerCode = await Context.Contacts.OrderBy(x => x.Code).Where(x => x.IsCustomer == request.isCustomer && x.IsCashCustomer).Select(x => new {
                            Code = x.Code
                        }).LastOrDefaultAsync(cancellationToken);

                        if(cashCustomerCode!=null)
                        {
                            if (string.IsNullOrEmpty(cashCustomerCode.Code))
                            {
                                if (request.isCustomer && request.isCashCustomer)
                                {
                                    contact.CashCustomer = "WC-00001";


                                }
                                else
                                {
                                    contact.CashCustomer = "WS-00001";

                                }





                            }

                            string fetchNo = cashCustomerCode.Code.Substring(3);
                            Int32 autoNo = Convert.ToInt32((fetchNo));
                            var format = "00000";
                            autoNo++;
                            string prefix;
                            if (!request.isCustomer)
                            {
                                prefix = "WS-";
                            }
                            else
                            {
                                prefix = "WC-";
                            }
                            var newCode = prefix + autoNo.ToString(format);

                            contact.CashCustomer = newCode;
                        }
                        else
                        {
                            if (request.isCustomer)
                            {
                                contact.CashCustomer = "WC-00001";
                            }
                            else
                            {
                                contact.CashCustomer = "WS-00001";
                            }
                           
                        }
                       

                    }



                    if (autoCode != null)
                    {

                        if (string.IsNullOrEmpty(autoCode.Code))
                        {
                            if (request.isCustomer)
                            {
                                contact.Contact = "SU-00001";

                                
                            }
                            else
                            {
                                contact.Contact = "CU-00001";
                                //return new ContactRegistrationLookUpModel()
                                //{
                                   
                                //    CashCustomer= cashCustomerCode==null? "WC-00001":GenerateNewCode(cashCustomerCode.Code, request.isCustomer, cashCustomerCode.IsCashCustomer),
                                //};
                            }
                            return contact;
                        }
                        else
                        {
                            string fetchNo = autoCode.Code.Substring(3);
                            Int32 autoNo = Convert.ToInt32((fetchNo));
                            var format = "00000";
                            autoNo++;
                            string prefix;
                            if (!request.isCustomer) 
                            {
                                prefix = "SU-";
                            }
                            else
                            {
                                prefix = "CU-";
                            }
                            var newCode = prefix + autoNo.ToString(format);
                            contact.Contact = newCode;
                        }
                       




                        return contact;


                    }
                    if (!request.isCustomer)
                    {
                        contact.Contact = "SU-00001";


                    }
                    else
                    {
                        contact.Contact = "CU-00001";
                      
                    }
                    return contact;

                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            public string GenerateCodeFirstTime(bool customer)
            {
                if (!customer)
                {
                    return "SU-00001";
                }
                return "CU-00001";
            }

            public string GenerateNewCode(string soNumber, bool customer,bool isCashCustomer)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                string prefix;
                if (isCashCustomer) {
                    prefix = "WC-";
                }
                else
                {
                    if (!customer)
                    {
                        prefix = "SU-";
                    }
                    else
                    {
                        prefix = "CU-";
                    }
                }
                
                var newCode = prefix + autoNo.ToString(format);
                return newCode;
            }
           


        }
    }
}