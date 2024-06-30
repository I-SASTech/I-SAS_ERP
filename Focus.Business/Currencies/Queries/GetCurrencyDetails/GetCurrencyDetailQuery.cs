using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Focus.Business.Currencies.Queries.GetCurrencyDetails
{
    public class GetCurrencyDetailQuery : IRequest<CurrencyDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetCurrencyDetailQuery, CurrencyDetailsLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IHostingEnvironment _hostingEnvironment;
            public Handler(IApplicationDbContext context,
                ILogger<GetCurrencyDetailQuery> logger, IHostingEnvironment hostingEnvironment)
            {
                _context = context;
                _logger = logger;
                _hostingEnvironment = hostingEnvironment;
            }
            public async Task<CurrencyDetailsLookUpModel> Handle(GetCurrencyDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var currency = await _context.Currencies.FindAsync(request.Id);
                     
                    if (currency != null)
                    {
                        var currencyModel = CurrencyDetailsLookUpModel.Create(currency);
                        currencyModel.Image = await GetBaseImage(currencyModel.Image);
                        return currencyModel;
                    }
                    throw new NotFoundException(nameof(currency), request.Id);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw;
                }
            }
            public async Task<string> GetBaseImage(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    return filePath;

                var imageExist = File.Exists(_hostingEnvironment.WebRootPath + filePath);

                if (!imageExist)
                    return "";

                var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }
    }
}
