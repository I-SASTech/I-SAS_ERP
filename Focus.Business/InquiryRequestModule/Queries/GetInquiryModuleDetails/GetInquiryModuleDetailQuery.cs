using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleDetails
{
    public class GetInquiryModuleDetailQuery : IRequest<InquiryModuleDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetInquiryModuleDetailQuery, InquiryModuleDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryModuleDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<InquiryModuleDetailsLookUpModel> Handle(GetInquiryModuleDetailQuery request, CancellationToken cancellationToken)
            {
                var module = await _context.InquiryModules.FindAsync(request.Id);

                try
                {
                    if (module != null)
                    {
                        var questionList = new List<ModuleQuestionLookUp>();
                        if (module.ModuleQuestions != null)
                        {
                            foreach (var question in module.ModuleQuestions)
                            {
                                var answerList = new List<ModuleAnswerLookUp>();
                                if(!string.IsNullOrEmpty(question.Answer)) answerList.Add(new ModuleAnswerLookUp() { Id = 0, Answer = question.Answer });
                                if(!string.IsNullOrEmpty(question.Answer1)) answerList.Add(new ModuleAnswerLookUp() { Id = 1, Answer = question.Answer1 });
                                if(!string.IsNullOrEmpty(question.Answer2)) answerList.Add(new ModuleAnswerLookUp() { Id = 2, Answer = question.Answer2});
                                if(!string.IsNullOrEmpty(question.Answer3)) answerList.Add(new ModuleAnswerLookUp() { Id = 3, Answer = question.Answer3 });
                                if(!string.IsNullOrEmpty(question.Answer4)) answerList.Add(new ModuleAnswerLookUp() { Id = 4, Answer = question.Answer4 });
                                if(!string.IsNullOrEmpty(question.Answer5)) answerList.Add(new ModuleAnswerLookUp() { Id = 5, Answer = question.Answer5 });
                                if(!string.IsNullOrEmpty(question.Answer6)) answerList.Add(new ModuleAnswerLookUp() { Id = 6, Answer = question.Answer6 });
                                if(!string.IsNullOrEmpty(question.Answer7)) answerList.Add(new ModuleAnswerLookUp() { Id = 7, Answer = question.Answer7 });
                                if(!string.IsNullOrEmpty(question.Answer8)) answerList.Add(new ModuleAnswerLookUp() { Id = 8, Answer = question.Answer8 });
                                if(!string.IsNullOrEmpty(question.Answer9)) answerList.Add(new ModuleAnswerLookUp() { Id = 9, Answer = question.Answer9 });
                                questionList.Add(new ModuleQuestionLookUp
                                {
                                    Id = question.Id,
                                    Question = question.Question,
                                    InquiryModuleId = question.InquiryModuleId,
                                    AnswerLookUps = answerList
                                });
                            }
                        }
                        return new InquiryModuleDetailsLookUpModel()
                        {
                            Id = module.Id,
                            IsActive = module.IsActive,
                            Compulsory = module.Compulsory,
                            AttachmentCompulsory = module.AttachmentCompulsory,
                            Description = module.Description,
                            Code = module.Code,
                            Label = module.Label,
                            Name = module.Name,
                            ModuleQuestionLookUps = questionList

                        };
                    }
                    throw new NotFoundException(nameof(module), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to module your request please contact support");
                }
            }
        }
    }
}
