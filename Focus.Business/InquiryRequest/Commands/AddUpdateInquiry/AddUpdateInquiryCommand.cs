using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InquiryRequest.Queries.GetInquiryDetails;
using Focus.Domain.Interface;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.InquiryRequest.Commands.AddUpdateInquiry
{
    public class AddUpdateInquiryCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public InquiryDetailLookUpModel InquiryDetail { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateInquiryCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateInquiryCommand> logger, IUserHttpContextProvider contextProvider, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _contextProvider = contextProvider;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdateInquiryCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.InquiryDetail.Id == Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var data = _context.Inquiries.FirstOrDefault(x =>
                            x.InquiryNumber == request.InquiryDetail.InquiryNumber);
                        if (data != null)
                            throw new ObjectAlreadyExistsException("Inquiry number already exist");

                        var unAssignedId = _context.InquiryStatusDynamics.FirstOrDefault(x => x.Name == "UnAssigned");
                        ////Check Media Type Already exist
                        //var mediaTypeExist = await Context.MediaTypes.FirstOrDefaultAsync(x =>
                        //    x.Name.ToLower() == request.InquiryDetail.MediaType.ToLower(), cancellationToken: cancellationToken);
                        //Guid id = Guid.Empty;
                        //if (mediaTypeExist == null)
                        //{
                        //    var mediaType = new MediaType()
                        //    {
                        //        Name = request.InquiryDetail.MediaType
                        //    };
                        //    await Context.MediaTypes.AddAsync(mediaType, cancellationToken);
                        //    id = mediaType.Id;
                        //}
                        
                        var inquiry = new Inquiry()
                        {
                            CustomerId = request.InquiryDetail.CustomerId,
                            DateTime = request.InquiryDetail.DateTime,
                            Description = request.InquiryDetail.Description,
                            DueDateTime = request.InquiryDetail.DueDateTime,
                            HandlerId = request.InquiryDetail.HandlerId,
                            InquiryNumber = request.InquiryDetail.InquiryNumber,
                           // InquiryStatus = (InquiryStatus)Enum.Parse(typeof(InquiryStatus), request.InquiryDetail.InquiryStatus),
                           //InquiryStatusDynamicId = unAssignedId.Id,
                            Reference = request.InquiryDetail.Reference,
                            TermAndCondition = request.InquiryDetail.TermAndCondition,
                            UserMessage = request.InquiryDetail.UserMessage,
                            IsTerm = request.InquiryDetail.IsTerm,
                            ReceiverId = request.InquiryDetail.ReceiverId,
                            MediaTypeId = request.InquiryDetail.MediaTypeId,
                            //InquiryModuleId = request.InquiryDetail.InquiryModuleId,
                            InquiryTypeId = request.InquiryDetail.InquiryTypeId,
                            InquiryProcessId = request.InquiryDetail.InquiryProcessId,
                            InquiryPriorityId = request.InquiryDetail.InquiryPriorityId,
                            ReferedBy = request.InquiryDetail.ReferedBy,
                            CreatedBy = _contextProvider.GetUserId(),
                            BranchId = request.InquiryDetail.BranchId
                        };
                        await _context.Inquiries.AddAsync(inquiry, cancellationToken);

                        //Add Data to inquiry Items
                        var inquiryItemList = new List<InquiryItem>();
                        foreach (var inquiryItem in request.InquiryDetail.InquiryItems)
                        {
                            inquiryItemList.Add(new InquiryItem()
                            {
                                InquiryId = inquiry.Id,
                                ItemId = inquiryItem.ItemId
                            });
                        }
                        // Add Data to Inquiry Status Table

                        var inquiryStatus = new InquiryStatus()
                        {
                            DateTime = DateTime.Now,
                            InquiryStatusDynamicId = unAssignedId.Id,
                            InquiryId = inquiry.Id
                        };
                        await _context.InquiryStatuses.AddAsync(inquiryStatus, cancellationToken);

                        //End Inquiry Status Command
                        var inquiryModuleQuestion = new List<InquiryModuleQuestion>();
                        var attachmentList = new List<Attachment>();
                        foreach (var moduleLookUp in request.InquiryDetail.InquiryModuleLookUp)
                        {
                            if (moduleLookUp.ModuleQuestionLookUps.Count > 0)
                            {
                                foreach (var question in moduleLookUp.ModuleQuestionLookUps)
                                {
                                    var ques = new InquiryModuleQuestion()
                                    {
                                        Question = question.Question,
                                        Description = moduleLookUp.Description,
                                        InquiryId = inquiry.Id,
                                        InquiryModuleId = moduleLookUp.Id
                                    };
                                    foreach (var ans in question.AnswerLookUps)
                                    {
                                        if (ans.Id == 0 && ans.Selected) ques.Answer = ans.Answer;
                                        if (ans.Id == 1 && ans.Selected) ques.Answer1 = ans.Answer;
                                        if (ans.Id == 2 && ans.Selected) ques.Answer2 = ans.Answer;
                                        if (ans.Id == 3 && ans.Selected) ques.Answer3 = ans.Answer;
                                        if (ans.Id == 4 && ans.Selected) ques.Answer4 = ans.Answer;
                                        if (ans.Id == 5 && ans.Selected) ques.Answer5 = ans.Answer;
                                        if (ans.Id == 6 && ans.Selected) ques.Answer6 = ans.Answer;
                                        if (ans.Id == 7 && ans.Selected) ques.Answer7 = ans.Answer;
                                        if (ans.Id == 8 && ans.Selected) ques.Answer8 = ans.Answer;
                                        if (ans.Id == 9 && ans.Selected) ques.Answer9 = ans.Answer;
                                    }
                                    inquiryModuleQuestion.Add(ques);
                                }
                            }
                            else
                            {
                                inquiryModuleQuestion.Add(new InquiryModuleQuestion()
                                {
                                    Description = moduleLookUp.Description,
                                    InquiryId = inquiry.Id,
                                    InquiryModuleId = moduleLookUp.Id
                                });
                            }

                            foreach (var attach in moduleLookUp.Attachments)
                            {
                                attachmentList.Add(new Attachment()
                                {
                                    Date = attach.Date,
                                    DocumentId = inquiry.Id,
                                    ModuleId = moduleLookUp.Id,
                                    Description = attach.Description,
                                    FileName = attach.FileName,
                                    Path = attach.Path
                                });
                            }
                        }
                        if (inquiryModuleQuestion.Count > 0)
                        {
                            await _context.InquiryModuleQuestions.AddRangeAsync(inquiryModuleQuestion, cancellationToken);
                        }
                        if (inquiryItemList.Count > 0)
                        {
                            await _context.InquiryItems.AddRangeAsync(inquiryItemList, cancellationToken);
                        }
                        

                        //Add Attachment List
                        foreach (var item in request.InquiryDetail.AttachmentList)
                        {
                            attachmentList.Add(new Attachment()
                            {
                                Date = item.Date,
                                DocumentId = inquiry.Id,
                                Description = item.Description,
                                FileName = item.FileName,
                                Path = item.Path
                            });
                            //Add Attachments to database
                           

                        }
                        if (attachmentList.Count > 0)
                        {
                            await _context.Attachments.AddRangeAsync(attachmentList, cancellationToken);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);


                        await _context.SaveChangesAsync(cancellationToken);
                        return inquiry.Id;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var inquiry = _context.Inquiries.FirstOrDefault(x =>
                            x.Id == request.InquiryDetail.Id);
                        if (inquiry == null)
                            throw new ObjectAlreadyExistsException("Selected Inquiry not exist");

                        ////Check Media Type Already exist
                        //var mediaTypeExist = await Context.MediaTypes.FirstOrDefaultAsync(x =>
                        //    x.Name.ToLower() == request.InquiryDetail.MediaType.ToLower(), cancellationToken: cancellationToken);
                        //Guid id = Guid.Empty;
                        //if (mediaTypeExist == null)
                        //{
                        //    var mediaType = new MediaType()
                        //    {
                        //        Name = request.InquiryDetail.MediaType
                        //    };
                        //    await Context.MediaTypes.AddAsync(mediaType, cancellationToken);
                        //    id = mediaType.Id;
                        //}


                        inquiry.CustomerId = request.InquiryDetail.CustomerId;
                        inquiry.DateTime = request.InquiryDetail.DateTime;
                        inquiry.Description = request.InquiryDetail.Description;
                        inquiry.DueDateTime = request.InquiryDetail.DueDateTime;
                        inquiry.HandlerId = request.InquiryDetail.HandlerId;
                        inquiry.InquiryNumber = request.InquiryDetail.InquiryNumber;
                        inquiry.Reference = request.InquiryDetail.Reference;
                        inquiry.TermAndCondition = request.InquiryDetail.TermAndCondition;
                        inquiry.UserMessage = request.InquiryDetail.UserMessage;
                        inquiry.IsTerm = request.InquiryDetail.IsTerm;
                        inquiry.ReceiverId = request.InquiryDetail.ReceiverId;
                        inquiry.MediaTypeId = request.InquiryDetail.MediaTypeId;
                        //inquiry.InquiryModuleId = request.InquiryDetail.InquiryModuleId;
                        inquiry.InquiryProcessId = request.InquiryDetail.InquiryProcessId;
                        inquiry.InquiryTypeId = request.InquiryDetail.InquiryTypeId;
                        inquiry.InquiryPriorityId = request.InquiryDetail.InquiryPriorityId;
                        inquiry.ReferedBy = request.InquiryDetail.ReferedBy;
                        
                        _context.InquiryItems.RemoveRange(inquiry.InquiryItems);
                        var inquiryItemList = new List<InquiryItem>();
                        foreach (var inquiryItem in request.InquiryDetail.InquiryItems)
                        {
                            inquiryItemList.Add(new InquiryItem()
                            {
                                InquiryId = inquiry.Id,
                                ItemId = inquiryItem.ItemId
                            });
                        }

                        if (inquiryItemList.Count > 0)
                        {
                            await _context.InquiryItems.AddRangeAsync(inquiryItemList, cancellationToken);
                        }
                        _context.InquiryModuleQuestions.RemoveRange(inquiry.InquiryModuleQuestions);
                        var inquiryModuleQuestion = new List<InquiryModuleQuestion>();
                        var attachmentList = new List<Attachment>();
                        foreach (var moduleLookUp in request.InquiryDetail.InquiryModuleLookUp)
                        {
                            if (moduleLookUp.ModuleQuestionLookUps.Count > 0)
                            {
                                foreach (var question in moduleLookUp.ModuleQuestionLookUps)
                                {
                                    var ques = new InquiryModuleQuestion()
                                    {
                                        Question = question.Question,
                                        Description = moduleLookUp.Description,
                                        InquiryId = inquiry.Id,
                                        InquiryModuleId = moduleLookUp.Id
                                    };
                                    foreach (var ans in question.AnswerLookUps)
                                    {
                                        if (ans.Id == 0 && ans.Selected) ques.Answer = ans.Answer;
                                        if (ans.Id == 1 && ans.Selected) ques.Answer1 = ans.Answer;
                                        if (ans.Id == 2 && ans.Selected) ques.Answer2 = ans.Answer;
                                        if (ans.Id == 3 && ans.Selected) ques.Answer3 = ans.Answer;
                                        if (ans.Id == 4 && ans.Selected) ques.Answer4 = ans.Answer;
                                        if (ans.Id == 5 && ans.Selected) ques.Answer5 = ans.Answer;
                                        if (ans.Id == 6 && ans.Selected) ques.Answer6 = ans.Answer;
                                        if (ans.Id == 7 && ans.Selected) ques.Answer7 = ans.Answer;
                                        if (ans.Id == 8 && ans.Selected) ques.Answer8 = ans.Answer;
                                        if (ans.Id == 9 && ans.Selected) ques.Answer9 = ans.Answer;
                                    }
                                    inquiryModuleQuestion.Add(ques);
                                   
                                }
                            }
                            else
                            {
                                inquiryModuleQuestion.Add(new InquiryModuleQuestion()
                                {
                                    Description = moduleLookUp.Description,
                                    InquiryId = inquiry.Id,
                                    InquiryModuleId = moduleLookUp.Id
                                });
                            }

                            if (moduleLookUp.Attachments!= null)
                            {
                                foreach (var attach in moduleLookUp.Attachments)
                                {
                                    attachmentList.Add(new Attachment()
                                    {
                                        Date = attach.Date,
                                        DocumentId = inquiry.Id,
                                        ModuleId = moduleLookUp.Id,
                                        Description = attach.Description,
                                        FileName = attach.FileName,
                                        Path = attach.Path
                                    });
                                }
                            }
                        }
                        if (inquiryModuleQuestion.Count > 0)
                        {
                            await _context.InquiryModuleQuestions.AddRangeAsync(inquiryModuleQuestion, cancellationToken);
                        }
                        _context.Attachments.RemoveRange(_context.Attachments.Where(x=>x.DocumentId == request.InquiryDetail.Id));
                        //Add Attachment List
                        foreach (var item in request.InquiryDetail.AttachmentList)
                        {
                            attachmentList.Add(new Attachment()
                            {
                                Date = item.Date,
                                DocumentId = inquiry.Id,
                                Description = item.Description,
                                FileName = item.FileName,
                                Path = item.Path
                            });
                            //Add Attachments to database
                            //await Context.Attachments.AddAsync(attachment, cancellationToken);

                        }

                        if (attachmentList.Count > 0)
                        {
                            await _context.Attachments.AddRangeAsync(attachmentList, cancellationToken);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);


                        await _context.SaveChangesAsync(cancellationToken);
                        return inquiry.Id;
                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
