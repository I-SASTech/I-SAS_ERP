using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleDetails;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.InquiryRequestModule.Commands.AddUpdateInquiryModule
{
    public class AddUpdateInquiryModuleCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public InquiryModuleDetailsLookUpModel ModuleDetails { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateInquiryModuleCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateInquiryModuleCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateInquiryModuleCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.ModuleDetails.Id != Guid.Empty)
                    {
                        var module = await Context.InquiryModules.FindAsync(request.ModuleDetails.Id);
                        if (module == null)
                            throw new NotFoundException("Inquiry Module Name", request.ModuleDetails.Name);

                        var isModuleNameExist = await Context.InquiryModules.AnyAsync(x => x.Name == request.ModuleDetails.Name, cancellationToken: cancellationToken);
                        if (module.Name != request.ModuleDetails.Name && isModuleNameExist)
                            throw new ObjectAlreadyExistsException("Inquiry Module " + request.ModuleDetails.Name + " Already Exist");


                        module.Name = request.ModuleDetails.Name;
                        module.Label = request.ModuleDetails.Label;
                        module.Description = request.ModuleDetails.Description;
                        module.IsActive = request.ModuleDetails.IsActive;
                        module.Compulsory = request.ModuleDetails.Compulsory;
                        module.AttachmentCompulsory = request.ModuleDetails.AttachmentCompulsory;
                        if(module.ModuleQuestions != null)
                            Context.ModuleQuestions.RemoveRange(module.ModuleQuestions);
                        var questionListUpdate = new List<ModuleQuestion>();
                        if (request.ModuleDetails.ModuleQuestionLookUps != null)
                        {
                            foreach (var q in request.ModuleDetails.ModuleQuestionLookUps)
                            {
                                var question = new ModuleQuestion
                                {
                                    Question = q.Question,
                                    InquiryModuleId = module.Id
                                };
                                foreach (var ans in q.AnswerLookUps)
                                {
                                    if (ans.Id == 0) question.Answer = ans.Answer;
                                    if (ans.Id == 1) question.Answer1 = ans.Answer;
                                    if (ans.Id == 2) question.Answer2 = ans.Answer;
                                    if (ans.Id == 3) question.Answer3 = ans.Answer;
                                    if (ans.Id == 4) question.Answer4 = ans.Answer;
                                    if (ans.Id == 5) question.Answer5 = ans.Answer;
                                    if (ans.Id == 6) question.Answer6 = ans.Answer;
                                    if (ans.Id == 7) question.Answer7 = ans.Answer;
                                    if (ans.Id == 8) question.Answer8 = ans.Answer;
                                    if (ans.Id == 9) question.Answer9 = ans.Answer;
                                }
                                questionListUpdate.Add(question);
                            }
                            await Context.ModuleQuestions.AddRangeAsync(questionListUpdate, cancellationToken);
                            //Save changes to database
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        return module.Id;
                    }


                    var isModuleExist = await Context.InquiryModules.AnyAsync(x => x.Name == request.ModuleDetails.Name || x.Code ==request.ModuleDetails.Code, cancellationToken: cancellationToken);
                    if (isModuleExist)
                        throw new ObjectAlreadyExistsException("Inquiry Module " + request.ModuleDetails.Name + " Already Exist");
                    var rowNumber = Convert.ToInt32(request.ModuleDetails.Code.Split("-")[1]);
                    //New brand Added
                    var inq = new InquiryModule()
                    {
                        Name = request.ModuleDetails.Name,
                        Label = request.ModuleDetails.Label,
                        Description = request.ModuleDetails.Description,
                        Code = request.ModuleDetails.Code,
                        IsActive = request.ModuleDetails.IsActive,
                        Compulsory = request.ModuleDetails.Compulsory,
                        AttachmentCompulsory = request.ModuleDetails.AttachmentCompulsory,
                        RowNumber = rowNumber
                    };
                    //Add Department to database
                    await Context.InquiryModules.AddAsync(inq, cancellationToken);

                    //Add Question to inquiry Look
                    var questionList = new List<ModuleQuestion>();
                    if (request.ModuleDetails.ModuleQuestionLookUps != null)
                    {
                        foreach (var q in request.ModuleDetails.ModuleQuestionLookUps)
                        {
                            var question = new ModuleQuestion
                            {
                                Question = q.Question,
                                InquiryModuleId = inq.Id
                            };
                            foreach (var ans in q.AnswerLookUps)
                            {
                                if (ans.Id == 0) question.Answer = ans.Answer;
                                if (ans.Id == 1) question.Answer1 = ans.Answer;
                                if (ans.Id == 2) question.Answer2 = ans.Answer;
                                if (ans.Id == 3) question.Answer3 = ans.Answer;
                                if (ans.Id == 4) question.Answer4 = ans.Answer;
                                if (ans.Id == 5) question.Answer5 = ans.Answer;
                                if (ans.Id == 6) question.Answer6 = ans.Answer;
                                if (ans.Id == 7) question.Answer7 = ans.Answer;
                                if (ans.Id == 8) question.Answer8 = ans.Answer;
                                if (ans.Id == 9) question.Answer9 = ans.Answer;
                            }
                            questionList.Add(question);
                        }
                        await Context.ModuleQuestions.AddRangeAsync(questionList, cancellationToken);
                        //Save changes to database
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    return inq.Id;

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to module your request please contact support");
                }
            }
        }
    }
}
