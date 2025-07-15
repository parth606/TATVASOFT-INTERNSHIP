using Microsoft.EntityFrameworkCore;
using Mission.Entities.context;
using Mission.Entities.Entities;
using Mission.Entities.Migrations;
using Mission.Entities.Models.CommonModel;
using Mission.Entities.Models.MissionsModels;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        private readonly MissionDbContext _dbContext = dbContext;

        public List<DropDownResponseModel> GetMissionThemeList()
        {
            List<DropDownResponseModel> missionthemeList = _dbContext.MissionTheme
                .Where(mt => mt.Status == "active" && !mt.IsDeleted)
                .Select(mt => new DropDownResponseModel(mt.Id, mt.ThemeName))
                .ToList();

            return missionthemeList;
        }

        public List<DropDownResponseModel> GetMissionSkillList()
        {
            List<DropDownResponseModel> missionskillList = _dbContext.MissionSkill
                .Where(mt => mt.Status == "active" && !mt.IsDeleted)
                .Select(mt => new DropDownResponseModel(mt.Id, mt.SkillName))
                .ToList();

            return missionskillList;
        }


        public List<MissionResponseModel> ClientMissionList(int userId)
        {
            var missions = _dbContext.Missions
                            .Where(x => !x.IsDeleted)
                            .OrderBy(x => x.StartDate)
                            .Select(m => new MissionResponseModel()
                            {
                                Id = m.Id,
                                MissionTitle = m.MissionTitle,
                                MissionDescription = m.MissionDescription,
                                MissionOrganisationName = m.MissionOrganisationName,
                                MissionOrganisationDetail = m.MissionOrganisationDetail,
                                CountryId = m.CountryId,
                                CityId = m.CityId,
                                StartDate = m.StartDate,
                                EndDate = m.EndDate,
                                MissionType = m.MissionType,
                                TotalSheets = m.TotalSheets ?? 0,
                                MissionThemeId = m.MissionThemeId,
                                MissionSkillId = m.MissionSkillId,
                                MissionImages = m.MissionImages,
                                CountryName = m.Country.CountryName,
                                CityName = m.City.CityName,
                                MissionThemeName = m.MissionTheme.ThemeName,
                                MissionSkillName = string.Join(",", _dbContext.MissionSkill
                                    .Where(ms => m.MissionSkillId.Contains(ms.Id.ToString()))
                                    .Select(ms => ms.SkillName)
                                    .ToList()),
                                MissionApplyStatus = _dbContext.MissionApplications.Any(ma => ma.UserId == userId && ma.MissionId == m.Id) ? "Applied" : "Apply",
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
                    TotalSheets = x.TotalSheets ?? 0
                }).ToList();
            return missions;
        }

        public string AddMission(AddMissionRequestModel model)
        {

            var exists = _dbContext.Missions.Any(x => x.MissionTitle.ToLower() == model.MissionTitle.ToLower()
                                                        && x.CityId == model.CityId
                                                        && x.StartDate == model.StartDate
                                                        && x.EndDate == model.EndDate && !x.IsDeleted);
            if (exists)
            {
                throw new Exception("Mission already exist");
            }

            var mission = new Missions()
            {
                MissionTitle = model.MissionTitle,
                MissionDescription = model.MissionDescription,
                MissionOrganisationName = model.MissionOrganisationName,
                MissionOrganisationDetail = model.MissionOrganisationDetail,
                CountryId = model.CountryId,
                CityId = model.CityId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                MissionType = model.MissionType,
                TotalSheets = model.TotalSheets,
                MissionThemeId = model.MissionThemeId,
                MissionSkillId = model.MissionSkillId,
                MissionImages = model.MissionImages,
                MissionDocuments = model.MissionDocuments,
                MissionAvailability = model.MissionAvailability,
                MissionVideoUrl = model.MissionVideoUrl,
                CreatedDate = DateTime.UtcNow,
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
                    TotalSheets = x.TotalSheets ?? 0
                }).FirstOrDefault() ?? throw new Exception("Mission not found");
        }

        public string DeleteMissionById(int missionId)
        {
            var mission = _dbContext.Missions.Where(x => x.Id == missionId && !x.IsDeleted).ExecuteUpdate(x => x.SetProperty(p => p.IsDeleted, true));
            return "Mission deleted successfully";
        }

        public string ApplyMission(ApplyMissionRequestModel request)
        {

            var mission = _dbContext.Missions.Where(x => x.Id == request.MissionId && !x.IsDeleted).FirstOrDefault();

            if (mission == null) { throw new Exception("Mission Not Found"); }

            if (mission.TotalSheets == 0) { throw new Exception("Mission housefull"); }

            if (mission.TotalSheets < request.Sheets) { throw new Exception("Not available seats"); }

            var missionApplication = new MissionApplication()
            {
                MissionId = request.MissionId,
                UserId = request.UserId,
                AppliedDate = request.AppliedDate,
                Status = request.Status,
                Sheet = request.Sheets,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            _dbContext.MissionApplications.Add(missionApplication);
            _dbContext.SaveChanges();

            mission.TotalSheets -= request.Sheets;

            _dbContext.Missions.Update(mission);
            _dbContext.SaveChanges();

            return "Mission applied successfully";

        }

        public string ApproveMission(int id)
        {
            var exists = _dbContext.MissionApplications.Any(x => x.Id == id);

            if (!exists) { throw new Exception("Mission application not exist"); }

            var updateCount = _dbContext.MissionApplications.Where(x => x.Id == id).ExecuteUpdate(m => m.SetProperty(property => property.Status, true));

            return updateCount > 0 ? "Mission approved" : "Mission is not approved";
        }


        public List<MissionApplicationResponseModel> MissionApplicationList()
        {
            return _dbContext.MissionApplications
                .Where(m => !m.IsDeleted && !m.Mission.IsDeleted && !m.User.IsDeleted)
                .Select(m => new MissionApplicationResponseModel()
                {
                    Id = m.Id,
                    AppliedDate = m.AppliedDate,
                    MissionId = m.MissionId,
                    MissionTheme = m.Mission.MissionTheme.ThemeName,
                    MissionTitle = m.Mission.MissionTitle,
                    Sheets = m.Sheet,
                    Status = m.Status,
                    UserId = m.UserId ,
                    UserName = $"{m.User.FirstName} {m.User.LastName}",
                }).ToList();
        }

        public string DeleteMissionApplication(int id)
        {
            var mission = _dbContext.MissionApplications.Where(x => x.Id == id && !x.IsDeleted).ExecuteUpdate(x => x.SetProperty(p => p.IsDeleted, true));
            return "Mission application deleted successfully";
        }
    }
}
