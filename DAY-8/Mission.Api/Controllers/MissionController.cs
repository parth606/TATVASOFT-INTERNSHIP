using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities.Entities;
using Mission.Entities.Models;
using Mission.Entities.Models.MissionsModels;
using Mission.Services.IServices;

namespace Mission.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController(IMissionService missionService, IWebHostEnvironment hostingEnvironment) : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
        private readonly IMissionService _missionService = missionService;
        ResponseResult result = new ResponseResult();

        [HttpGet]
        [Route("MissionList")]
        public ResponseResult MissionList()
        {
            try
            {
                result.Data = _missionService.GetMissionList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionDetailById/{id}")]
        public ResponseResult MissionDetailsById(int id)
        {
            try
            {
                result.Data = _missionService.GetMissionById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("AddMission")]
        public ResponseResult AddMission(AddMissionRequestModel request)
        {
            try
            {
                if(request.EndDate.Date < request.StartDate.Date) { throw new Exception("Selected end date must be greater then start date"); };

                result.Data = _missionService.AddMission(request);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpDelete]
        [Route("DeleteMission/{id}")]
        public ResponseResult DeleteMission(int id)
        {
            try
            {
                result.Data = _missionService.DeleteMission(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

      

        [HttpGet]
        [Route("GetMissionThemeList")]
        [Authorize]
        public ResponseResult GetMissionThemeList()
        {
            try
            {
                result.Data = _missionService.GetMissionThemeList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("GetMissionSkillList")]
        [Authorize]
        public ResponseResult GetMissionSkillList()
        {
            try
            {
                result.Data = _missionService.GetMissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("MissionApplicationApprove")]
        public ResponseResult MissionApplicationApprove(MissionApplication application)
        {
            try
            {
                result.Data = _missionService.ApproveMission(application.Id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionApplicationList")]
        public ResponseResult MissionApplicationList()
        {
            try
            {
                result.Data = _missionService.MissionApplicationList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }


        [HttpPost]
        [Route("MissionApplicationDelete")]
        public ResponseResult MissionApplicationDelete(MissionApplication application)
        {
            try
            {
                result.Data = _missionService.DeleteMissionApplication(application.Id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
