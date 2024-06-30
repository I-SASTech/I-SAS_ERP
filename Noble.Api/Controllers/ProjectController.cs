
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

using Focus.Business.InquiryComments.Commands.AddUpdateInquiryComment;
using Focus.Business.InquiryComments.Commands.DeleteInquiryComment;
using Focus.Business.InquiryComments.Queries.GetInquiryCommentDetails;
using Focus.Business.InquiryComments.Queries.GetInquiryCommentList;
using Focus.Business.InquiryEmails.Commands.AddUpdateInquiryEmail;
using Focus.Business.InquiryEmails.Commands.DeleteInquiryEmail;
using Focus.Business.InquiryEmails.Queries.GetInquiryEmailDetails;
using Focus.Business.InquiryEmails.Queries.GetInquiryEmailList;
using Focus.Business.InquiryRequest.Commands.AddUpdateInquiry;
using Focus.Business.InquiryRequest.Queries.GetInquiryDetails;
using Focus.Business.InquiryRequest.Queries.GetInquiryList;
using Focus.Business.InquiryRequestModule.Commands.AddUpdateInquiryModule;
using Focus.Business.InquiryRequestModule.Commands.DeleteInquiryModule;
using Focus.Business.InquiryRequestModule.Commands.UpdateInquiryModelRow;
using Focus.Business.InquiryRequestModule.Queries;
using Focus.Business.InquiryRequestProcess.Commands.AddUpdateInquiryProcess;
using Focus.Business.InquiryRequestProcess.Commands.DeleteInquiryProcess;
using Focus.Business.InquiryRequestProcess.Queries;
using Focus.Business.InquiryRequestProcess.Queries.GetInquiryProcessDetails;
using Focus.Business.InquiryRequestProcess.Queries.GetInquiryProcessList;
using Focus.Business.Interface;
using Noble.Api.Models;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleDetails;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleList;
using Focus.Business.InquiryRequestType.Commands.AddUpdateInquiryType;
using Focus.Business.InquiryRequestType.Commands.DeleteInquiryType;
using Focus.Business.InquiryRequestType.Queries;
using Focus.Business.InquiryRequestType.Queries.GetInquiryTypeDetails;
using Focus.Business.InquiryRequestType.Queries.GetInquiryTypeList;
using Focus.Business.InquiryMeetings.Queries.GetInquiryMeetingDetails;
using Focus.Business.InquiryMeetings.Commands.DeleteInquiryMeeting;
using Focus.Business.InquiryMeetings.Queries.GetInquiryMeetingList;
using Focus.Business.InquiryMeetings.Commands.AddUpdateInquiryMeeting;
using Focus.Business.InquiryRequest.Queries.InquiryMediaType;
using Focus.Business.InquiryRequest.Queries.InquiryPriority;
using Focus.Business.InquiryRequest.Queries.InquiryStatus;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        public readonly IApplicationDbContext Context;
        //private readonly IUserHttpContextProvider _contextProvider;
        //private readonly UserManager<ApplicationUser> _userManager;
        public ProjectController(IApplicationDbContext context)
        {
            Context = context;
            //_contextProvider = contextProvider;
            //_userManager = userManager;
        }

        #region Inquiry
        //Save Media Type for Inquiry
        [Route("api/Project/SaveMediaType")]
        [HttpGet("SaveMediaType")]
        [Roles("CanAddInquiry", "CanEditInquiry")]
        public async Task<IActionResult> SaveMediaType(string name)
        {
            var mediaType = await Mediator.Send(new AddInquiryMediaType()
            {
                MediaType = new InquiryMediaTypeLookUp()
                {
                    Name = name
                }
            });

            return Ok(mediaType);
        }
        //Get Media Type for Inquiry
        [Route("api/Project/GetMediaType")]
        [HttpGet("GetMediaType")]
        [Roles("CanAddInquiry", "CanEditInquiry")]
        public async Task<IActionResult> GetMediaType()
        {
            var mediaType = await Mediator.Send(new GetInquiryMediaTypeQuery());
            return Ok(mediaType);
        }
        
        //Save Status for Inquiry
        [Route("api/Project/SaveStatus")]
        [HttpGet("SaveStatus")]
        [Roles("CanAddInquiry", "CanEditInquiry")]
        public async Task<IActionResult> SaveStatus(string name)
        {
            var mediaType = await Mediator.Send(new AddInquiryStatus()
            {
                Status = new InquiryStatusLookUp()
                {
                    Name = name
                }
            });

            return Ok(mediaType);
        }
        //Get Media Type for Inquiry
        [Route("api/Project/GetStatus")]
        [HttpGet("GetStatus")]
        [Roles("CanAddInquiry", "CanEditInquiry")]
        public async Task<IActionResult> GetStatus()
        {
            var mediaType = await Mediator.Send(new GetInquiryStatusQuery());
            return Ok(mediaType);
        }
        
        //Save Priority for Inquiry
        [Route("api/Project/SavePriority")]
        [HttpGet("SavePriority")]
        [Roles("CanAddInquiry", "CanEditInquiry")]
        public async Task<IActionResult> SavePriority(string name)
        {
            var mediaType = await Mediator.Send(new AddInquiryPriority()
            {
                Priority = new InquiryPriorityLookUp()
                {
                    Name = name
                }
            });

            return Ok(mediaType);
        }
        //Get Media Type for Inquiry
        [Route("api/Project/GetPriority")]
        [HttpGet("GetPriority")]
        [Roles("CanAddInquiry", "CanEditInquiry")]
        public async Task<IActionResult> GetPriority()
        {
            var mediaType = await Mediator.Send(new GetInquiryPriorityQuery());
            return Ok(mediaType);
        }
        //Get Auto generated code for Inquiry
        [Route("api/Project/InquiryAutoGenerateNo")]
        [HttpGet("InquiryAutoGenerateNo")]
        [Roles("CanAddInquiry")]
        public async Task<IActionResult> InquiryAutoGenerateNo(Guid? branchId)
        {
            var autoNo = await Mediator.Send(new GetInquiryCodeQuery()
            {
                BranchId= branchId
            });
            

            return Ok(new
            {
                autoNumber = autoNo
            });
        }

        //Save Data Inquiry
        [Route("api/Project/SaveInquiry")]
        [HttpPost("SaveInquiry")]
        [Roles("CanAddInquiry", "CanEditInquiry" )]
        public async Task<IActionResult> SaveInquiry([FromBody] InquiryDetailLookUpModel inquiryDetail)
        {
            var result = await Mediator.Send(new AddUpdateInquiryCommand()
            {
                InquiryDetail = inquiryDetail
            });

            if (inquiryDetail.Id == Guid.Empty)
            {
                return Ok(new {message="Data added Successfully", result=result, isAdd=true});
            }
            return Ok(new { message = "Data updated Successfully", result = result, isAdd = false });

        }

        //Get Inquiry List
        [Route("api/Project/GetInquiryList")]
        [HttpGet("GetInquiryList")]
        [Roles("CanViewInquiry")]
        public async Task<IActionResult> GetInquiryList(Guid? processId, int? pageNumber, Guid? branchId)
        {
            var result = await Mediator.Send(new GetInquiryListQuery()
            {
                ProcessId = processId,
                PageNumber = pageNumber ?? 1,
                BranchId = branchId,
            });
            

            return Ok(result);
        }

        [Route("api/Project/GetInquiryDetail")]
        [HttpGet("GetInquiryDetail")]
        [Roles("CanViewInquiry")]
        public async Task<IActionResult> GetInquiryDetail(Guid id)
        {
            var inquiryDetail = await Mediator.Send(new GetInquiryDetailQuery()
            {
                Id = id
            });
            return Ok(inquiryDetail);

        }
        [Route("api/Project/CheckCustomerAlreadyInquiry")]
        [HttpGet("CheckCustomerAlreadyInquiry")]
        [Roles("CanAddInquiry", "CanEditInquiry")]
        public async Task<IActionResult> CheckCustomerAlreadyInquiry(Guid id)
        {
            var data = await Mediator.Send(new CheckCustomerAlreadyInquiry()
            {
                Id = id
            });
            return Ok(data);

        }

        [Route("api/Project/InquiryDetailForList")]
        [HttpGet("InquiryDetailForList")]
        [Roles("CanViewInquiry")]
        public async Task<IActionResult> InquiryDetailForList(Guid id)
        {
            var data = await Mediator.Send(new InquiryDetailForList()
            {
                Id = id
            });
            return Ok(data);

        }
        [Route("api/Project/UpdateInquiryStatus")]
        [HttpPost("UpdateInquiryStatus")]
        [Roles("CanReplyInquiryDashboard")]
        public async Task<IActionResult> UpdateInquiryStatus([FromBody] InquiryStatusLookUpModel lookUp)
        {
            var data = await Mediator.Send(new UpdateInquiryStatus()
            {
                LookUp = lookUp
            });
            return Ok(data);

        }

        #endregion

        #region InquiryProcess

        [Route("api/Project/InquiryProcessCode")]
        [HttpGet("InquiryProcessCode")]
        [Roles("CanAddInquiryProcess")]
        public async Task<IActionResult> InquiryProcessCode()
        {
            var autoNo = await Mediator.Send(new GetInquiryProcessCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Project/SaveInquiryProcess")]
        [HttpPost("SaveInquiryProcess")]
        [Roles("CanAddInquiryProcess", "CanEditInquiryProcess")]
        public async Task<IActionResult> SaveInquiryProcess([FromBody] InquiryProcessDetailsLookUpModel processVm)
        {
            Guid id;
            id = await Mediator.Send(new AddUpdateInquiryProcessCommand()
            {
                ProcessDetails = processVm
            });
            if (id != Guid.Empty)
            {
                var process = await Mediator.Send(new GetInquiryProcessDetailQuery()
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, inquiryProcess = process });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Project/InquiryProcessList")]
        [HttpGet("InquiryProcessList")]
        [Roles("CanViewInquiryProcess", "CanAddInquiry", "CanEditInquiry")]


        public async Task<IActionResult> InquiryProcessList(bool isActive,string searchTerm, int? pageNumber)
        {
            var process = await Mediator.Send(new GetInquiryProcessListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(process);
        }

        [Route("api/Project/InquiryProcessDelete")]
        [HttpGet("InquiryProcessDelete")]
        [Roles("CanViewInquiryProcess")]
        public async Task<IActionResult> InquiryProcessDelete(Guid id)
        {
            var processId = await Mediator.Send(new DeleteInquiryProcessCommand
            {
                Id = id
            });
            return Ok(processId);

        }
        [Route("api/Project/InquiryProcessDetail")]
        [HttpGet("InquiryProcessDetail")]
        [Roles("CanEditInquiryProcess")]
        public async Task<IActionResult> InquiryProcessDetail(Guid id)
        {
            var processDetail = await Mediator.Send(new GetInquiryProcessDetailQuery()
            {
                Id = id
            });
            return Ok(processDetail);

        }
        #endregion

        #region InquiryModule

        [Route("api/Project/InquiryModuleCode")]
        [HttpGet("InquiryModuleCode")]
        [Roles("CanAddInquirySetup")]
        public async Task<IActionResult> InquiryModuleCode()
        {
            var autoNo = await Mediator.Send(new GetInquiryModuleCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Project/SaveInquiryModule")]
        [HttpPost("SaveInquiryModule")]
        [Roles("CanAddInquirySetup", "CanEditInquirySetup")]
        public async Task<IActionResult> SaveInquiryModule([FromBody] InquiryModuleDetailsLookUpModel moduleVm)
        {
            Guid id;
            id = await Mediator.Send(new AddUpdateInquiryModuleCommand()
            {
                ModuleDetails = moduleVm
            });
            if (id != Guid.Empty)
            {
                var module = await Mediator.Send(new GetInquiryModuleDetailQuery()
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, inquiryModule = module });
            }

            return Ok(new { IsSuccess = false });



        }

        [Route("api/Project/UpdateModuleList")]
        [HttpPost("UpdateModuleList")]
        [Roles("CanViewInquirySetup")]
        public async Task<IActionResult> UpdateModuleList([FromBody] List<InquiryModuleLookUpModel> moduleList)
        {
            Guid id;
            id = await Mediator.Send(new UpdateInquiryModelRowCommand()
            {
                ModuleList = moduleList
            });
           

            return Ok(new { IsSuccess = true });



        }
        [Route("api/Project/InquiryModuleList")]
        [HttpGet("InquiryModuleList")]
        [Roles("CanViewInquirySetup", "CanAddInquiry", "CanEditInquiry")]


        public async Task<IActionResult> InquiryModuleList(bool isActive,string searchTerm)
        {
            var module = await Mediator.Send(new GetInquiryModuleListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
            });
            return Ok(module);
        }

        [Route("api/Project/InquiryModuleDelete")]
        [HttpGet("InquiryModuleDelete")]
        [Roles("CanViewInquirySetup")]
        public async Task<IActionResult> InquiryModuleDelete(Guid id)
        {
            var moduleId = await Mediator.Send(new DeleteInquiryModuleCommand
            {
                Id = id
            });
            return Ok(moduleId);

        }
        [Route("api/Project/InquiryModuleDetail")]
        [HttpGet("InquiryModuleDetail")]
        [Roles("CanEditInquirySetup")]
        public async Task<IActionResult> InquiryModuleDetail(Guid id)
        {
            var moduleDetail = await Mediator.Send(new GetInquiryModuleDetailQuery()
            {
                Id = id
            });
            return Ok(moduleDetail);

        }
        #endregion

        #region InquiryType

        [Route("api/Project/InquiryTypeCode")]
        [HttpGet("InquiryTypeCode")]
        [Roles("CanAddInquiryType")]
        public async Task<IActionResult> InquiryTypeCode()
        {
            var autoNo = await Mediator.Send(new GetInquiryTypeCodeQuery());

            return Ok(autoNo);
        }

        [Route("api/Project/SaveInquiryType")]
        [HttpPost("SaveInquiryType")]
        [Roles("CanAddInquiryType", "CanEditInquiryType")]
        public async Task<IActionResult> SaveInquiryType([FromBody] InquiryTypeDetailsLookUpModel processVm)
        {
            Guid id;
            id = await Mediator.Send(new AddUpdateInquiryTypeCommand()
            {
                TypeDetails = processVm
            });
            if (id != Guid.Empty)
            {
                var process = await Mediator.Send(new GetInquiryTypeDetailQuery()
                {
                    Id = id
                });
                return Ok(new { IsSuccess = true, inquiryType = process });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Project/InquiryTypeList")]
        [HttpGet("InquiryTypeList")]
        [Roles("CanViewInquiryType", "CanAddInquiry", "CanEditInquiry")]


        public async Task<IActionResult> InquiryTypeList(bool isActive, string searchTerm, int? pageNumber)
        {
            var process = await Mediator.Send(new GetInquiryTypeListQuery
            {
                isActive = isActive,
                SearchTerm = searchTerm,
                PageNumber = pageNumber ?? 1,
            });
            return Ok(process);
        }

        [Route("api/Project/InquiryTypeDelete")]
        [HttpGet("InquiryTypeDelete")]
        [Roles("CanViewInquiryType")]
        public async Task<IActionResult> InquiryTypeDelete(Guid id)
        {
            var processId = await Mediator.Send(new DeleteInquiryTypeCommand
            {
                Id = id
            });
            return Ok(processId);

        }
        [Route("api/Project/InquiryTypeDetail")]
        [HttpGet("InquiryTypeDetail")]
        [Roles("CanEditInquiryType")]
        public async Task<IActionResult> InquiryTypeDetail(Guid id)
        {
            var processDetail = await Mediator.Send(new GetInquiryTypeDetailQuery()
            {
                Id = id
            });
            return Ok(processDetail);

        }
        #endregion


        #region InquiryComment

        

        [Route("api/Project/SaveInquiryComment")]
        [HttpPost("SaveInquiryComment")]
        [Roles("CanReplyInquiryDashboard")]
        public async Task<IActionResult> SaveInquiryComment([FromBody] InquiryCommentDetailsLookUpModel commentDetail)
        {
            var id = await Mediator.Send(new AddUpdateInquiryCommentCommand
            {
                CommentDetails = commentDetail



            });
            
           
            if (id != Guid.Empty)
            {
                var inquiryComment = await Mediator.Send(new GetInquiryCommentListQuery()
                {
                    InquiryId = commentDetail.InquiryId
                });
                return Ok(new { IsSuccess = true, inquiryCommentList = inquiryComment });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Project/InquiryCommentList")]
        [HttpGet("InquiryCommentList")]
        [Roles("CanViewInquiryDashboard", "CanReplyInquiryDashboard")]
        public async Task<IActionResult> InquiryCommentList(Guid inquiryId)
        {
            var commentList = await Mediator.Send(new GetInquiryCommentListQuery()
            {
                InquiryId = inquiryId
            });
            return Ok(commentList);
        }
        [Route("api/Project/InquiryCommentDelete")]
        [HttpGet("InquiryCommentDelete")]
        [Roles( "CanReplyInquiryDashboard")]
        public async Task<IActionResult> InquiryCommentDelete(Guid id)
        {
            var commentId = await Mediator.Send(new DeleteInquiryCommentCommand
            {
                Id = id
            });
            return Ok(commentId);

        }
        [Route("api/Project/InquiryCommentDetail")]
        [HttpGet("InquiryCommentDetail")]
        [Roles("CanViewInquiryDashboard", "CanReplyInquiryDashboard")]
        public async Task<IActionResult> InquiryCommentDetail(Guid id)
        {
            var comment = await Mediator.Send(new GetInquiryCommentDetailQuery()
            {
                Id = id
            });
            return Ok(comment);

        }
        #endregion


        #region InquiryMeetings



        [Route("api/Project/SaveInquiryMeeting")]
        [HttpPost("SaveInquiryMeeting")]
        [Roles("CanReplyInquiryDashboard")]
        public async Task<IActionResult> SaveInquiryMeeting([FromBody] InquiryMeetingDetailsLookUpModel meetingDetail)
        {
            var id = await Mediator.Send(new AddUpdateInquiryMeetingCommand
            {
                MeetingDetails = meetingDetail



            });


            if (id != Guid.Empty)
            {
                var inquiryMeeting = await Mediator.Send(new GetInquiryMeetingListQuery()
                {
                    InquiryId = meetingDetail.InquiryId
                });
                return Ok(new { IsSuccess = true, inquiryMeetingList = inquiryMeeting });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Project/InquiryMeetingList")]
        [HttpGet("InquiryMeetingList")]
        [Roles("CanViewInquiryDashboard", "CanReplyInquiryDashboard")]
        public async Task<IActionResult> InquiryMeetingList(Guid inquiryId)
        {
            var commentList = await Mediator.Send(new GetInquiryMeetingListQuery()
            {
                InquiryId = inquiryId
            });
            return Ok(commentList);
        }
        [Route("api/Project/InquiryMeetingDelete")]
        [HttpGet("InquiryMeetingDelete")]
        [Roles("CanReplyInquiryDashboard")]
        public async Task<IActionResult> InquiryMeetingDelete(Guid id)
        {
            var commentId = await Mediator.Send(new DeleteInquiryMeetingCommand
            {
                Id = id
            });
            return Ok(commentId);

        }
        [Route("api/Project/InquiryMeetingDetail")]
        [HttpGet("InquiryMeetingDetail")]
        [Roles("CanViewInquiryDashboard", "CanReplyInquiryDashboard")]
        public async Task<IActionResult> InquiryMeetingDetail(Guid id)
        {
            var comment = await Mediator.Send(new GetInquiryMeetingDetailQuery()
            {
                Id = id
            });
            return Ok(comment);

        }
        #endregion

        #region InquiryEmails



        [Route("api/Project/SaveInquiryEmail")]
        [HttpPost("SaveInquiryEmail")]
        [Roles( "CanReplyInquiryDashboard")]
        public async Task<IActionResult> SaveInquiryEmail([FromBody] InquiryEmailDetailsLookUpModel meetingDetail)
        {
            var id = await Mediator.Send(new AddUpdateInquiryEmailCommand
            {
                EmailDetails = meetingDetail



            });


            if (id != Guid.Empty)
            {
                var inquiryEmail = await Mediator.Send(new GetInquiryEmailListQuery()
                {
                    InquiryId = meetingDetail.InquiryId
                });
                return Ok(new { IsSuccess = true, inquiryEmailList = inquiryEmail });
            }

            return Ok(new { IsSuccess = false });



        }
        [Route("api/Project/InquiryEmailList")]
        [HttpGet("InquiryEmailList")]
        [Roles("CanViewInquiryDashboard", "CanReplyInquiryDashboard")]
        public async Task<IActionResult> InquiryEmailList(Guid inquiryId)
        {
            var commentList = await Mediator.Send(new GetInquiryEmailListQuery()
            {
                InquiryId = inquiryId
            });
            return Ok(commentList);
        }
        [Route("api/Project/InquiryEmailDelete")]
        [HttpGet("InquiryEmailDelete")]
        [Roles( "CanReplyInquiryDashboard")]
        public async Task<IActionResult> InquiryEmailDelete(Guid id)
        {
            var commentId = await Mediator.Send(new DeleteInquiryEmailCommand
            {
                Id = id
            });
            return Ok(commentId);

        }
        [Route("api/Project/InquiryEmailDetail")]
        [HttpGet("InquiryEmailDetail")]
        [Roles("CanViewInquiryDashboard", "CanReplyInquiryDashboard")]
        public async Task<IActionResult> InquiryEmailDetail(Guid id)
        {
            var comment = await Mediator.Send(new GetInquiryEmailDetailQuery()
            {
                Id = id
            });
            return Ok(comment);

        }
        #endregion
    }

}
