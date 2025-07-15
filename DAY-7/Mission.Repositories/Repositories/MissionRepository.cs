using Microsoft.EntityFrameworkCore;
using Mission.Entities.context;
using Mission.Entities.Entities;
using Mission.Entities.Models.MissionsModels;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        private readonly MissionDbContext _dbContext = dbContext;

        public List<MissionResponseModel> ClientMissionList(int userId)
        {
            var missions = _dbContext.Missions
                            .Where(x => !x.IsDeleted)
                            .OrderBy(x => x.StartDate)
                            .Select(x => new MissionResponseModel()
                            {
                                Id = x.Id,
                                CityId = x.CityId,
                                CityName = x.City.CityName,
                                CountryId = x.CountryId,
                                CountryName = x.Country.CountryName,
                                EndDate = x.EndDate,
                                MissionDescription = x.MissionDescription,
                                MissionImages = x.MissionImages,
                                MissionOrganisationDetail = x.MissionOrganisationDetail,
                                MissionOrganisationName = x.MissionOrganisationName,
                                MissionSkillId = x.MissionSkillId,
                                MissionThemeId = x.MissionThemeId,
                                MissionThemeName = x.MissionTheme.ThemeName,
                                MissionTitle = x.MissionTitle,
                                MissionType = x.MissionType,
                                StartDate = x.StartDate,
                                TotalSheets = x.TotalSheets
                            }).ToList();
            return missions;
        }

        public List<MissionResponseModel> MissionList()
        {
            var missions = _dbContext.Missions.Where(x => !x.IsDeleted)
                .Select(x => new MissionResponseModel()
                {
                    Id = x.Id,
                    CityId = x.CityId,
                    CityName = x.City.CityName,
                    CountryId = x.CountryId,
                    CountryName = x.Country.CountryName,
                    EndDate = x.EndDate,
                    MissionDescription = x.MissionDescription,
                    MissionImages = x.MissionImages,
                    MissionOrganisationDetail = x.MissionOrganisationDetail,
                    MissionOrganisationName = x.MissionOrganisationName,
                    MissionSkillId = x.MissionSkillId,
                    MissionThemeId = x.MissionThemeId,
                    MissionThemeName = x.MissionTheme.ThemeName,
                    MissionTitle = x.MissionTitle,
                    MissionType = x.MissionType,
                    StartDate = x.StartDate,
                    TotalSheets = x.TotalSheets
                }).ToList();
            return missions;
        }

        public string AddMission(AddMissionRequestModel request)
        {

            var exists = _dbContext.Missions.Any(x => x.MissionTitle.ToLower() == request.MissionTitle.ToLower()
                                                        && x.CityId == request.CityId
                                                        && x.StartDate.Date == request.StartDate.Date
                                                        && x.EndDate.Date == request.EndDate.Date && !x.IsDeleted);
            if (exists)
            {
                throw new Exception("Mission already exist");
            }

            var mission = new Missions()
            {
                MissionDescription = request.MissionDescription,
                MissionImages = request.MissionImages,
                CityId = request.CityId,
                CountryId = request.CountryId,
                MissionOrganisationDetail = request.MissionOrganisationDetail,
                MissionOrganisationName = request.MissionOrganisationName,
                MissionSkillId = request.MissionSkillId,
                MissionThemeId = request.MissionThemeId,
                MissionTitle = request.MissionTitle,
                MissionType = request.MissionType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedDate = DateTime.UtcNow,
                TotalSheets = request.TotalSheets,
                IsDeleted = false
            };

            _dbContext.Missions.Add(mission);
            _dbContext.SaveChanges();
            return "Mission Save Successfully";
        }

        public MissionResponseModel GetMissionById(int missionId)
        {
            return _dbContext.Missions
                .Where(x => x.Id == missionId && !x.IsDeleted)
                .Select(x => new MissionResponseModel
                {
                    Id = x.Id,
                    CityId = x.CityId,
                    CityName = x.City.CityName,
                    CountryId = x.CountryId,
                    CountryName = x.Country.CountryName,
                    EndDate = x.EndDate,
                    MissionDescription = x.MissionDescription,
                    MissionImages = x.MissionImages,
                    MissionOrganisationDetail = x.MissionOrganisationDetail,
                    MissionOrganisationName = x.MissionOrganisationName,
                    MissionSkillId = x.MissionSkillId,
                    MissionThemeId = x.MissionThemeId,
                    MissionThemeName = x.MissionTheme.ThemeName,
                    MissionTitle = x.MissionTitle,
                    MissionType = x.MissionType,
                    StartDate = x.StartDate,
                    TotalSheets = x.TotalSheets
                }).FirstOrDefault() ?? throw new Exception("Mission not found");
        }

        public string DeleteMissionById(int missionId)
        {
            var mission = _dbContext.Missions.Where(x => x.Id == missionId && !x.IsDeleted).ExecuteUpdate(x => x.SetProperty(p => p.IsDeleted, true));
            return "Mission deleted successfully";
        }
    }
}
