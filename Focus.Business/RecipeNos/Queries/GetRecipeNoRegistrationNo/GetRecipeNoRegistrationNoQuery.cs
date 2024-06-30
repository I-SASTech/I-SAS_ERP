using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.RecipeNos.Queries.GetRecipeNoRegistrationNo
{
    public class GetRecipeNoRegistrationNoQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetRecipeNoRegistrationNoQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetRecipeNoRegistrationNoQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<string> Handle(GetRecipeNoRegistrationNoQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var recipeNo = await _context.RecipeNos
                        .OrderBy(x => x.RegistrationNo)
                        .LastOrDefaultAsync(cancellationToken);

                    if (recipeNo != null)
                    {
                        if (string.IsNullOrEmpty(recipeNo.RegistrationNo))
                        {
                            return GenerateCodeFirstTime();
                        }

                        return GenerateNewCode(recipeNo.RegistrationNo);
                    }

                    return GenerateCodeFirstTime();
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }

            public string GenerateCodeFirstTime()
            {
                return "RN-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "RN-" +autoNo.ToString(format);
                return newCode;
            }
        }
    }
}

