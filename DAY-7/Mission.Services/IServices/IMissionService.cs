using Mission.Entities.Models.MissionsModels;

namespace Mission.Services.IServices
{
    public interface IMissionService
    {
        List<MissionResponseModel> GetMissionList();
        MissionResponseModel GetMissionById(int missionId);
        string DeleteMission(int missionId);
        string AddMission(AddMissionRequestModel request);
        List<MissionResponseModel> ClientMissionList(int userId)
    }
}
