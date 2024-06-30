using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.InquiryRequest.Commands.AddUpdateInquiry;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleDetails;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleList;
using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryDetails
{
    public class GetInquiryDetailQuery : IRequest<InquiryDetailLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetInquiryDetailQuery, InquiryDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            public readonly IMediator _Mediator;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryDetailQuery> logger,
                IMapper mapper,
                IMediator mediator,
                UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _Mediator = mediator;
                _userManager = userManager;
            }
            public async Task<InquiryDetailLookUpModel> Handle(GetInquiryDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var inquiry = await _context.Inquiries.FindAsync(request.Id);
                    var module = await _Mediator.Send(new GetInquiryModuleListQuery
                    {
                        isActive = true
                    }, cancellationToken);

                    if (inquiry == null)
                        throw new NotFoundException(nameof(inquiry), request.Id);
                    var attachments = _context.Attachments.Where(x => x.DocumentId == request.Id).ToList();
                    var attachmentList = new List<InquiryAttachmentLookUpModel>();
                    foreach (var attachment in attachments.Where(x=>x.ModuleId == null))
                    {
                        attachmentList.Add(new InquiryAttachmentLookUpModel()
                        {
                            Date = attachment.Date,
                            Name = attachment.Name,
                            NameArabic = attachment.NameArabic,
                            Path = attachment.Path,
                            Description = attachment.Description,
                            FileName = attachment.FileName,
                            DocumentId = attachment.DocumentId
                        });
                    }

                    var itemList = new List<InquiryItemLookUpModel>();
                    foreach (var item in inquiry.InquiryItems)
                    {
                        itemList.Add(new InquiryItemLookUpModel()
                        {
                            ItemId = item.ItemId,
                            InquiryId = item.InquiryId,
                        });
                    }

                    //Inquiry Status List
                    var statusList = new List<InquiryStatusLookUpModel>();
                    foreach (var item in inquiry.InquiryStatus)
                    {
                        statusList.Add(new InquiryStatusLookUpModel()
                        {
                            DateTime = item.DateTime.ToString("d"),
                            Reason = item.Reason,
                            InquiryStatusDynamicId = item.InquiryStatusDynamicId,
                            Status = item.InquiryStatusDynamic.Name,
                            InquiryId = item.InquiryId,
                        });
                    }

                    //Get data from InquiryModuleQuestion and arrange it
                    var inquiryModuleLookUpModelList = new List<InquiryModuleLookUpModel>();
                    var moduleQuestionLookUplList = new List<ModuleQuestionLookUp>();



                    foreach (var moduleLookUp in module.Results.InquiryModuleLookUp)
                    {
                        foreach (var question in inquiry.InquiryModuleQuestions)
                        {
                            if (!string.IsNullOrEmpty(question.Question))
                            {
                                var ques = moduleLookUp.ModuleQuestionLookUps.FirstOrDefault(x =>
                                    x.InquiryModuleId == question.InquiryModuleId && x.Question == question.Question );
                                var answerList = new List<ModuleAnswerLookUp>();
                                if (!string.IsNullOrEmpty(question.Answer)) answerList.Add(new ModuleAnswerLookUp() { Id = 0, Answer = question.Answer, Selected = true});
                                if (!string.IsNullOrEmpty(question.Answer1)) answerList.Add(new ModuleAnswerLookUp() { Id = 1, Answer = question.Answer1, Selected = true });
                                if (!string.IsNullOrEmpty(question.Answer2)) answerList.Add(new ModuleAnswerLookUp() { Id = 2, Answer = question.Answer2, Selected = true });
                                if (!string.IsNullOrEmpty(question.Answer3)) answerList.Add(new ModuleAnswerLookUp() { Id = 3, Answer = question.Answer3, Selected = true });
                                if (!string.IsNullOrEmpty(question.Answer4)) answerList.Add(new ModuleAnswerLookUp() { Id = 4, Answer = question.Answer4, Selected = true });
                                if (!string.IsNullOrEmpty(question.Answer5)) answerList.Add(new ModuleAnswerLookUp() { Id = 5, Answer = question.Answer5, Selected = true });
                                if (!string.IsNullOrEmpty(question.Answer6)) answerList.Add(new ModuleAnswerLookUp() { Id = 6, Answer = question.Answer6, Selected = true });
                                if (!string.IsNullOrEmpty(question.Answer7)) answerList.Add(new ModuleAnswerLookUp() { Id = 7, Answer = question.Answer7, Selected = true });
                                if (!string.IsNullOrEmpty(question.Answer8)) answerList.Add(new ModuleAnswerLookUp() { Id = 8, Answer = question.Answer8, Selected = true });
                                if (!string.IsNullOrEmpty(question.Answer9)) answerList.Add(new ModuleAnswerLookUp() { Id = 9, Answer = question.Answer9, Selected = true });
                                if(answerList.Count > 0 && ques != null) ques.AnswerLookUps = answerList;
                            }
                        }

                        //ModuleAttachments
                        


                        var withoutQuestion = inquiry.InquiryModuleQuestions.FirstOrDefault(x => string.IsNullOrEmpty(x.Question)&& string.IsNullOrEmpty(x.Answer) && x.InquiryModuleId == moduleLookUp.Id);
                        if (withoutQuestion != null)
                        {
                            inquiryModuleLookUpModelList.Add(new InquiryModuleLookUpModel()
                            {
                                Id = moduleLookUp.Id,
                                Code = moduleLookUp.Code,
                                Description = withoutQuestion.Description,
                                Label = moduleLookUp.Label,
                                IsActive = moduleLookUp.IsActive,
                                Name = moduleLookUp.Name,
                                RowNumber = moduleLookUp.RowNumber,
                                AttachmentCompulsory = moduleLookUp.AttachmentCompulsory,
                                Compulsory = moduleLookUp.Compulsory,
                                ModuleQuestionLookUps = moduleLookUp.ModuleQuestionLookUps,
                                Attachments = attachments.Where(x => x.ModuleId == moduleLookUp.Id && x.DocumentId == inquiry.Id).ToList()
                            });
                        }
                        else
                        {
                            inquiryModuleLookUpModelList.Add(new InquiryModuleLookUpModel()
                            {
                                Id = moduleLookUp.Id,
                                Code = moduleLookUp.Code,
                                Description = moduleLookUp.Description,
                                Label = moduleLookUp.Label,
                                IsActive = moduleLookUp.IsActive,
                                Name = moduleLookUp.Name,
                                RowNumber = moduleLookUp.RowNumber,
                                ModuleQuestionLookUps = moduleLookUp.ModuleQuestionLookUps,
                                Attachments = attachments.Where(x => x.ModuleId == moduleLookUp.Id && x.DocumentId == inquiry.Id).ToList()
                            });
                        }

                        


                       
                    }

                    var countInquiryOfSelectedCustomer = _context.Inquiries.Count(x => x.CustomerId == inquiry.CustomerId);
                    var inquiryLookUp = new InquiryDetailLookUpModel()
                    {
                        Id = inquiry.Id,
                        InquiryNumber = inquiry.InquiryNumber,
                        DateTime = inquiry.DateTime,
                        DueDateTime = inquiry.DueDateTime,
                        Reference = inquiry.Reference,
                        Description = inquiry.Description,
                        TermAndCondition = inquiry.TermAndCondition,
                        UserMessage = inquiry.UserMessage,
                        IsTerm = inquiry.IsTerm,
                        InquiryStatus = inquiry.InquiryStatus.LastOrDefault()?.InquiryStatusDynamic.Name,
                        CustomerId = inquiry.CustomerId,
                        ReceiverId = inquiry.ReceiverId,
                        ReceiverName = _userManager.Users.FirstOrDefault(x => x.Id == inquiry.ReceiverId.ToString())?.UserName,
                        HandlerId = inquiry.HandlerId,
                        HandlerName = _userManager.Users.FirstOrDefault(x => x.Id == inquiry.HandlerId.ToString())?.UserName,
                        MediaTypeId = inquiry.MediaTypeId,
                        MediaType = inquiry.MediaType.Name,
                        CustomerName = string.IsNullOrEmpty(inquiry.Contact.EnglishName) ? inquiry.Contact.ArabicName : inquiry.Contact.EnglishName,
                        InquiryType = inquiry.InquiryType.Name,
                        InquiryProcessId = inquiry.InquiryProcessId,
                        InquiryTypeId = inquiry.InquiryTypeId,
                        InquiryPriorityId = inquiry.InquiryPriorityId,
                        ReferedBy = inquiry.ReferedBy,
                        AttachmentList = attachmentList,
                        InquiryItems = itemList,
                        InquiryModuleLookUp = inquiryModuleLookUpModelList,
                        TimeSpan = GetRelativeDate(inquiry.DateTime),
                        TotalInquiry = countInquiryOfSelectedCustomer,
                        InquiryStatusLookUp = statusList
                    };
                    return inquiryLookUp;
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
        public static string GetRelativeDate(DateTime commentDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - commentDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
    }
}
