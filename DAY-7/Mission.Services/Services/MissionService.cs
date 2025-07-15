﻿using Mission.Entities.Models.MissionsModels;
using Mission.Repositories.IRepositories;
using Mission.Services.IServices;

namespace Mission.Services.Services
{
    public class MissionService(IMissionRepository missionRepository) : IMissionService
    {
        private readonly IMissionRepository _missionRepository = missionRepository;

        public List<MissionResponseModel> GetMissionList()
        {
            return _missionRepository.MissionList();
        }

        public MissionResponseModel GetMissionById(int missionId)
        {
            return _missionRepository.GetMissionById(missionId);
        }


        public string DeleteMission(int missionId)
        {
            return _missionRepository.DeleteMissionById(missionId);
        }


        public string AddMission(AddMissionRequestModel request)
        {
            return _missionRepository.AddMission(request);
        }

        public List<MissionResponseModel> ClientMissionList(int userId)
        {
            return _missionRepository.ClientMissionList(userId);
        }
    }
}
