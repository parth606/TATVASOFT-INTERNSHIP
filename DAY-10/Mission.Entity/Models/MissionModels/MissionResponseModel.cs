﻿using System;

namespace Mission.Entity.Models.MissionModels
{
    public class MissionResponseModel
    {
        public int Id { get; set; }

        public string MissionTitle { get; set; }

        public string MissionDescription { get; set; }

        public string MissionOrganisationName { get; set; }

        public string MissionOrganisationDetail { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string MissionType { get; set; }

        public int? TotalSheets { get; set; }

        public DateTime? RegistrationDeadLine { get; set; }

        public int MissionThemeId { get; set; }

        public string MissionImages { get; set; }

        public string MissionDocuments { get; set; }

        public string MissionAvailability { get; set; }

        public string MissionVideoUrl { get; set; }

        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string MissionThemeName { get; set; }
    }
}
