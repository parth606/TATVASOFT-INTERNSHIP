using Mission.Entities.Models.MissionsModels;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        List<MissionResponseModel> MissionList();
        string AddMission(AddMissionRequestModel request);
        MissionResponseModel GetMissionById(int missionId);
        string DeleteMissionById(int missionId);

        List<MissionResponseModel> ClientMissionList(int userId);
    }
}
