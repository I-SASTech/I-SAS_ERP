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
using Focus.Business.Common;
using Focus.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Brands.Queries.GetBrandList;
using Focus.Business.Exceptions;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleDetails;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleList
{
    public class GetInquiryModuleListQuery : PagedRequest, IRequest<PagedResult<InquiryModuleListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetInquiryModuleListQuery, PagedResult<InquiryModuleListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            public readonly IMediator _Mediator;
            private string _code;
            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryModuleListQuery> logger,
                IMapper mapper,
                IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _Mediator = mediator;
            }
            public async Task<PagedResult<InquiryModuleListModel>> Handle(GetInquiryModuleListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<InquiryModule> query;
                    query = _context.InquiryModules.AsQueryable();
                    if (query.Count() < 2)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var isModuleExist = await _context.InquiryModules.AnyAsync(x => x.Name == "Hardware" && x.Name == "Software", cancellationToken: cancellationToken);
                        if (!isModuleExist)
                        {
                            var autoCode = await _Mediator.Send(new GetInquiryModuleCodeQuery(), cancellationToken);
                            //New brand Added
                            var inq = new InquiryModule()
                            {
                                Name = "Hardware",
                                Label = "Hardware",
                                Code = autoCode,
                                IsActive = true,
                                Compulsory = true

                            };
                            //Add Department to database
                            await _context.InquiryModules.AddAsync(inq, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);
                            var secondCode = await _Mediator.Send(new GetInquiryModuleCodeQuery(), cancellationToken);
                            //New brand Added
                            var inq1 = new InquiryModule()
                            {
                                Name = "Software",
                                Label = "Software",
                                Code = secondCode,
                                IsActive = true,
                                Compulsory = true
                            };
                            //Add Department to database
                            await _context.InquiryModules.AddAsync(inq1, cancellationToken);

                            await _Mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                        }
                    }
                    if (request.isActive)
                    {



                        query = query.OrderBy(x => x.RowNumber);
                        var inquiryModuleList = new List<InquiryModuleLookUpModel>();
                        foreach (var q in query)
                        {
                            var questionList = new List<ModuleQuestionLookUp>();
                            if (q.ModuleQuestions != null)
                            {
                                foreach (var question in q.ModuleQuestions)
                                {
                                    var answerList = new List<ModuleAnswerLookUp>();
                                    if (!string.IsNullOrEmpty(question.Answer)) answerList.Add(new ModuleAnswerLookUp() { Id = 0, Answer = question.Answer });
                                    if (!string.IsNullOrEmpty(question.Answer1)) answerList.Add(new ModuleAnswerLookUp() { Id = 1, Answer = question.Answer1 });
                                    if (!string.IsNullOrEmpty(question.Answer2)) answerList.Add(new ModuleAnswerLookUp() { Id = 2, Answer = question.Answer2 });
                                    if (!string.IsNullOrEmpty(question.Answer3)) answerList.Add(new ModuleAnswerLookUp() { Id = 3, Answer = question.Answer3 });
                                    if (!string.IsNullOrEmpty(question.Answer4)) answerList.Add(new ModuleAnswerLookUp() { Id = 4, Answer = question.Answer4 });
                                    if (!string.IsNullOrEmpty(question.Answer5)) answerList.Add(new ModuleAnswerLookUp() { Id = 5, Answer = question.Answer5 });
                                    if (!string.IsNullOrEmpty(question.Answer6)) answerList.Add(new ModuleAnswerLookUp() { Id = 6, Answer = question.Answer6 });
                                    if (!string.IsNullOrEmpty(question.Answer7)) answerList.Add(new ModuleAnswerLookUp() { Id = 7, Answer = question.Answer7 });
                                    if (!string.IsNullOrEmpty(question.Answer8)) answerList.Add(new ModuleAnswerLookUp() { Id = 8, Answer = question.Answer8 });
                                    if (!string.IsNullOrEmpty(question.Answer9)) answerList.Add(new ModuleAnswerLookUp() { Id = 9, Answer = question.Answer9 });
                                    answerList.Add(new ModuleAnswerLookUp() { Id = 10, Answer = "Other" });
                                    questionList.Add(new ModuleQuestionLookUp
                                    {
                                        Id = question.Id,
                                        Question = question.Question,
                                        InquiryModuleId = question.InquiryModuleId,
                                        AnswerLookUps = answerList
                                    });
                                }
                            }
                            inquiryModuleList.Add(new InquiryModuleLookUpModel()
                            {
                                Id = q.Id,
                                Code = q.Code,
                                Name = q.Name,
                                Label = q.Label,
                                Description = q.Description,
                                IsActive = q.IsActive,
                                Compulsory = q.Compulsory,
                                AttachmentCompulsory = q.AttachmentCompulsory,
                                RowNumber = q.RowNumber,
                                IsEnable = false,
                                ModuleQuestionLookUps = questionList,
                                Attachments = new List<Attachment>()
                            });
                        }

                        return new PagedResult<InquiryModuleListModel>
                        {


                            Results = new InquiryModuleListModel
                            {
                                InquiryModuleLookUp = inquiryModuleList
                            }
                        };

                    }
                    else
                    {
                       

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;



                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.Name.ToLower().Contains(searchTerm.ToLower()));

                        }
                        query = query.OrderBy(x => x.RowNumber);

                        var inquiryModuleList = new List<InquiryModuleLookUpModel>();
                        foreach (var q in query)
                        {
                            inquiryModuleList.Add(new InquiryModuleLookUpModel()
                            {
                                Id = q.Id,
                                Code = q.Code,
                                Name = q.Name,
                                Label = q.Label,
                                Description = q.Description,
                                IsActive = q.IsActive,
                                RowNumber = q.RowNumber
                            });
                        }

                       

                        return new PagedResult<InquiryModuleListModel>
                        {


                            Results = new InquiryModuleListModel
                            {
                                InquiryModuleLookUp = inquiryModuleList
                            }
                        };

                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
