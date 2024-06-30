using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Departments
{
  public  class GetDepartmentCodeQuery : IRequest<string>
  {

      public class Handler : IRequestHandler<GetDepartmentCodeQuery, string>
      {
          public readonly IApplicationDbContext Context;
          public readonly ILogger Logger;

          public Handler(IApplicationDbContext context, ILogger<GetDepartmentCodeQuery> logger)
          {
              Context = context;
              Logger = logger;
          }

          public async Task<string> Handle(GetDepartmentCodeQuery request, CancellationToken cancellationToken)
          {
              var autoCode = await Context.Departments.OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
              if (autoCode != null)
              {
                  if (string.IsNullOrEmpty(autoCode.Code))
                  {
                      return GenerateCodeFirstTime();
                  }
                  return GenerateNewCode(autoCode.Code);
              }

              return GenerateCodeFirstTime();
          }

          public string GenerateCodeFirstTime()
          {
              return "DM-00001";
          }

          public string GenerateNewCode(string soNumber)
          {
              string fetchNo = soNumber.Substring(3);
              Int32 autoNo = Convert.ToInt32((fetchNo));
              var format = "DM-00000";
              autoNo++;
              var newCode = autoNo.ToString(format);
              return newCode;
          }
      }
  }
}
