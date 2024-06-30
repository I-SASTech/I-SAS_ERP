using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Focus.Business.Terminals.Queries.GetTerminalDetails
{
    public class GetTerminalDetailQuery : IRequest<TerminalDetailsLookUpModel>
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        

        public class Handler : IRequestHandler<GetTerminalDetailQuery, TerminalDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            private readonly IHostingEnvironment _hostingEnvironment;
            public Handler(IApplicationDbContext context,
                ILogger<GetTerminalDetailQuery> logger,
                IMapper mapper, IHostingEnvironment hostingEnvironment)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _hostingEnvironment = hostingEnvironment;
            }
            public async Task<TerminalDetailsLookUpModel> Handle(GetTerminalDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var terminal = await _context.Terminals.FindAsync(request.Id);
                    if (request.CompanyId != null)
                    {
                        _context.DisableTenantFilter = true;
                        terminal = await _context.Terminals.FindAsync(request.Id);
                        if (string.IsNullOrEmpty(terminal.CompanyNameEnglish) && string.IsNullOrEmpty(terminal.CompanyNameArabic))
                        {
                            var companyList = _context.Companies.ToList();
                            var businessData = companyList.FirstOrDefault(x =>
                                x.Id == companyList.FirstOrDefault(y => y.Id == request.CompanyId)?.BusinessParentId);
                            var companyData = companyList.FirstOrDefault(x =>
                                x.Id == companyList.FirstOrDefault(y => y.Id == request.CompanyId)?.ClientParentId);
                            terminal.CompanyNameEnglish = companyData.NameEnglish;
                            terminal.CompanyNameArabic = companyData.NameArabic;
                            terminal.BusinessNameEnglish = businessData.NameEnglish;
                            terminal.BusinessNameArabic = businessData.NameArabic;
                            terminal.BusinessLogo = companyList.FirstOrDefault(y => y.Id == request.CompanyId)?.LogoPath;
                        }
                    }

                    if (terminal != null)
                    {
                        var lookUpModel =  TerminalDetailsLookUpModel.Create(terminal);
                        lookUpModel.BusinessLogo = await GetBaseImage(lookUpModel.BusinessLogo);
                        return lookUpModel;
                    }
                    throw new NotFoundException(nameof(terminal), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
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
