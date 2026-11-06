using System.Collections.Generic;
using ColossalFramework.Globalization;
using UnityEngine;

namespace FavoriteCims.Utils
{
	public class FavCimsLang : MonoBehaviour
	{
        public static string GameLanguage;

        public static Dictionary<string, string> it = [];

        public static Dictionary<string, string> en = [];

        public static Dictionary<string, string> nl = [];

        public static Dictionary<string, string> rus = [];

        public static string Text(string index)
		{
			if (GameLanguage == null || GameLanguage != Locale.Get("LANGUAGE_ENGLISH"))
			{
				try
				{
					GameLanguage = Locale.Get("LANGUAGE_ENGLISH");
					if (GameLanguage == "(ITALIAN)")
					{
						it = new Dictionary<string, string>
                        {
                            // ============================================================
                            // Main navigation
                            // ============================================================

                            ["MainViewListText"] = "View ",
                            ["MainButtonTooltip"] = "Open Favorite Cims",

                            ["MainCitizensTabText"] = "Citizens",
                            ["MainCitizensTabTooltip"] = "View favorite citizens",

                            ["MainBuildingsTabText"] = "Buildings",
                            ["MainBuildingsTabTooltip"] = "View favorite buildings",

                            ["MainStatisticsTabText"] = "Stats",
                            ["MainStatisticsTabTooltip"] = "View information and statistics",

                            // ============================================================
                            // Favorite actions
                            // ============================================================

                            ["FavoriteAddTooltip"] = "Add to favorites",
                            ["FavoriteRemoveTooltip"] = "Remove from favorites",

                            // ============================================================
                            // Citizen table columns
                            // ============================================================

                            ["CitizenStatusColumnTitle"] = "Status",
                            ["CitizenStatusColumnTooltip"] = "Citizen happiness",

                            ["CitizenNameColumnTitle"] = "Citizen name",
                            ["CitizenNameColumnTooltip"] = "Citizen name",

                            ["CitizenLifeStageColumnTitle"] = "Life stage",
                            ["CitizenLifeStageColumnTooltip"] = "Citizen life stage",

                            ["CitizenAgeColumnTitle"] = "Age",
                            ["CitizenAgeColumnTooltip"] = "Citizen age",

                            ["CitizenEducationColumnTitle"] = "Education",
                            ["CitizenEducationColumnTooltip"] = "Citizen education level",

                            ["CitizenHomeColumnTitle"] = "Home",
                            ["CitizenHomeColumnTooltip"] = "Citizen home",

                            ["CitizenOccupationColumnTitle"] = "Occupation",
                            ["CitizenOccupationColumnTooltip"] = "Workplace, school, or occupation",

                            ["CitizenActivityColumnTitle"] = "Activity",
                            ["CitizenActivityColumnTooltip"] = "What the citizen is currently doing",

                            ["CitizenRemoveColumnTitle"] = "Remove",
                            ["CitizenRemoveColumnTooltip"] = "Remove this citizen from favorites",

                            // ============================================================
                            // Citizen gender
                            // ============================================================

                            ["CitizenGenderLabelMale"] = "Male",
                            ["CitizenGenderLabelFemale"] = "Female",

                            // ============================================================
                            // Citizen life stages
                            // Keep gender suffixes for languages that require them.
                            // ============================================================

                            ["CitizenLifeStageChildMale"] = "Child",
                            ["CitizenLifeStageTeenMale"] = "Teen",
                            ["CitizenLifeStageYoungAdultMale"] = "Young adult",
                            ["CitizenLifeStageAdultMale"] = "Adult",
                            ["CitizenLifeStageSeniorMale"] = "Senior",
                            ["CitizenLifeStageDeceasedMale"] = "Deceased",

                            ["CitizenLifeStageChildFemale"] = "Child",
                            ["CitizenLifeStageTeenFemale"] = "Teen",
                            ["CitizenLifeStageYoungAdultFemale"] = "Young adult",
                            ["CitizenLifeStageAdultFemale"] = "Adult",
                            ["CitizenLifeStageSeniorFemale"] = "Senior",
                            ["CitizenLifeStageDeceasedFemale"] = "Deceased",

                            // ============================================================
                            // Citizen education
                            // ============================================================

                            ["CitizenEducationUneducatedMale"] = "Uneducated",
                            ["CitizenEducationEducatedMale"] = "Educated",
                            ["CitizenEducationWellEducatedMale"] = "Well educated",
                            ["CitizenEducationHighlyEducatedMale"] = "Highly educated",

                            ["CitizenEducationUneducatedFemale"] = "Uneducated",
                            ["CitizenEducationEducatedFemale"] = "Educated",
                            ["CitizenEducationWellEducatedFemale"] = "Well educated",
                            ["CitizenEducationHighlyEducatedFemale"] = "Highly educated",

                            // ============================================================
                            // Citizen life status
                            // ============================================================

                            ["CitizenAliveStatus"] = "Citizen is alive",
                            ["CitizenDeceasedStatus"] = "Citizen is deceased",
                            ["CitizenLeftCityStatus"] = "Citizen has left the city",

                            ["CitizenDeathDateLabel"] = "Date of death: ",
                            ["CitizenDeathTimeLabel"] = "Time of death: ",

                            // ============================================================
                            // Date and time formats
                            // Date order and clock format are independent settings.
                            // Quoted separators preserve the displayed punctuation.
                            // ============================================================

                            ["DateMonthFirstFormat"] = "MM'/'dd'/'yyyy",
                            ["DateDayFirstFormat"] = "dd'/'MM'/'yyyy",
                            ["DateYearFirstFormat"] = "yyyy'-'MM'-'dd",

                            ["Time12HourFormat"] = "hh':'mm tt",
                            ["Time24HourFormat"] = "HH':'mm",

                            ["DateFormatSettingLabel"] = "Date format",
                            ["DateFormatSettingTooltip"] = "Choose the order of the month, day, and year",

                            ["DateMonthFirstOptionText"] = "Month / Day / Year",
                            ["DateDayFirstOptionText"] = "Day / Month / Year",
                            ["DateYearFirstOptionText"] = "Year / Month / Day",

                            ["TimeFormatSettingLabel"] = "Time format",
                            ["TimeFormatSettingTooltip"] = "Choose a 12-hour or 24-hour clock",

                            ["Time12HourOptionText"] = "12-hour clock",
                            ["Time24HourOptionText"] = "24-hour clock",

                            // ============================================================
                            // Citizen happiness
                            // ============================================================

                            ["CitizenHappinessVeryUnhappyStatus"] = "Very unhappy",
                            ["CitizenHappinessUnhappyStatus"] = "Unhappy",
                            ["CitizenHappinessHappyStatus"] = "Happy",
                            ["CitizenHappinessVeryHappyStatus"] = "Very happy",
                            ["CitizenHappinessExtremelyHappyStatus"] = "Extremely happy",

                            // ============================================================
                            // Citizen home
                            // ============================================================

                            ["CitizenHomelessStatus"] = "Homeless",
                            ["CitizenHomelessTooltip"] = "Citizen has no home",

                            ["CitizenHomeOutsideCityStatus"] = "Outside the city",
                            ["CitizenHomeOutsideCityTooltip"] = "Citizen lives outside the city",

                            ["CitizenHomeCemeteryStatus"] = "Cemetery",

                            // ============================================================
                            // Citizen occupation
                            // ============================================================

                            ["CitizenRetiredStatus"] = "Retired",
                            ["CitizenRetiredTooltip"] = "Citizen is retired",

                            ["CitizenUnemployedStatus"] = "Unemployed",
                            ["CitizenUnemployedTooltip"] = "Citizen is unemployed",

                            ["CitizenGovernmentWorkplaceTooltip"] = "Government building",
                            ["CitizenElectricityWorkplaceTooltip"] = "Electricity facility",
                            ["CitizenWaterWorkplaceTooltip"] = "Water facility",
                            ["CitizenGenericWorkplaceTooltip"] = "Citizen works here",

                            ["CitizenWalkingTooltip"] = "Travelling on foot",
                            ["CitizenTouristTooltip"] = "This citizen is visiting the city",

                            // ============================================================
                            // Citizen details and family
                            // ============================================================

                            ["CitizenDetailsTitle"] = "Citizen details",
                            ["CitizenDetailsUnavailableTooltip"] = "No details window is available. Close an existing window and try again.",

                            ["CitizenStatusLabel"] = "Status: ",
                            ["CitizenHomeLabel"] = "Home: ",

                            ["CitizenFamilyTitle"] = "Family",
                            ["CitizenNoPartnerStatus"] = "No partner",
                            ["CitizenNoChildrenStatus"] = "No children",
                            ["CitizenNoHouseholdStatus"] = "No household details available",

                            ["CitizenSwitchTooltip"] = "Right-click to view this citizen",

                            // ============================================================
                            // Districts
                            // ============================================================

                            ["CitizenCurrentDistrictLabel"] = "Current district: ",
                            ["CitizenCurrentDistrictTooltip"] = "Citizen's current district",

                            ["DistrictLabel"] = "District: ",
                            ["DistrictNoneText"] = "None",

                            // ============================================================
                            // Citizen wealth and wellbeing
                            // ============================================================

                            ["CitizenWealthLowStatusMale"] = "Poor",
                            ["CitizenWealthMediumStatusMale"] = "Well-off",
                            ["CitizenWealthHighStatusMale"] = "Rich",

                            ["CitizenWealthLowStatusFemale"] = "Poor",
                            ["CitizenWealthMediumStatusFemale"] = "Well-off",
                            ["CitizenWealthHighStatusFemale"] = "Rich",

                            ["CitizenWellbeingLabel"] = "Wellbeing: ",
                            ["CitizenHappinessLabel"] = "Happiness: ",
                            ["CitizenWealthLabel"] = "Wealth",

                            // ============================================================
                            // Citizen health
                            // ============================================================

                            ["CitizenHealthVerySickStatusMale"] = "Very sick",
                            ["CitizenHealthSickStatusMale"] = "Sick",
                            ["CitizenHealthPoorStatusMale"] = "Poor health",
                            ["CitizenHealthHealthyStatusMale"] = "Healthy",
                            ["CitizenHealthVeryHealthyStatusMale"] = "Very healthy",
                            ["CitizenHealthExcellentStatusMale"] = "Excellent health",

                            ["CitizenHealthVerySickStatusFemale"] = "Very sick",
                            ["CitizenHealthSickStatusFemale"] = "Sick",
                            ["CitizenHealthPoorStatusFemale"] = "Poor health",
                            ["CitizenHealthHealthyStatusFemale"] = "Healthy",
                            ["CitizenHealthVeryHealthyStatusFemale"] = "Very healthy",
                            ["CitizenHealthExcellentStatusFemale"] = "Excellent health",

                            // ============================================================
                            // Death and funeral status
                            // ============================================================

                            ["CitizenWaitingForHearseStatus"] = "Waiting for a hearse",
                            ["CitizenInHearseStatus"] = "Being transported by hearse",
                            ["CitizenFuneralStatus"] = "Funeral service",
                            ["CitizenBuriedStatus"] = "Buried",

                            // ============================================================
                            // Vehicles and passengers
                            // ============================================================

                            ["VehicleViewPassengersText"] = "View passengers",
                            ["VehicleNoPassengersStatus"] = "No passengers",

                            ["VehiclePassengersTitle"] = "Passengers on board",
                            ["VehicleDriverTitle"] = "Driver",
                            ["VehiclePassengerTooltip"] = "Passenger",
                            ["VehiclePassengersButtonTooltip"] = "View passengers list",

                            // ============================================================
                            // Building occupant panel actions and titles
                            // ============================================================

                            ["BuildingViewResidentsText"] = "View residents",
                            ["BuildingResidentsTitle"] = "Residents",

                            ["BuildingViewOccupantsText"] = "View building occupants",
                            ["BuildingOccupantsTitle"] = "Building occupants",

                            ["BuildingHotelOccupantsTitle"] = "Workers, hotel guests, and visitors",

                            ["BuildingNoOccupantsStatus"] = "No listed occupants",
                            ["BuildingOccupantTotalLabel"] = "Total: ",

                            // ============================================================
                            // Registered resident sections
                            // Number is appended by the caller.
                            // ============================================================

                            ["BuildingHouseholdTitle"] = "Household ",
                            ["BuildingResidentsTooltip"] = "Registered residents",

                            // ============================================================
                            // Registered worker sections
                            // Presence and roster membership have separate empty states.
                            // ============================================================

                            ["BuildingWorkersTitle"] = "Workers",
                            ["BuildingWorkersPresentTitle"] = "At workplace",
                            ["BuildingWorkersAwayTitle"] = "Away from workplace",
                            ["BuildingWorkersUnknownTitle"] = "Presence unavailable",

                            ["BuildingWorkersTooltip"] = "Registered workers",
                            ["BuildingWorkersPresentTooltip"] = "Registered workers currently present at this building",
                            ["BuildingWorkersAwayTooltip"] = "Registered workers who are elsewhere or travelling here",
                            ["BuildingWorkersUnknownTooltip"] = "Registered workers whose current presence could not be determined",

                            ["BuildingNoWorkersStatus"] = "No registered workers",
                            ["BuildingNoWorkersPresentStatus"] = "No workers on-site",

                            // ============================================================
                            // Enrolled student sections
                            // ============================================================

                            ["BuildingStudentsTitle"] = "Students",
                            ["BuildingStudentsPresentTitle"] = "At school",
                            ["BuildingStudentsAwayTitle"] = "Away from school",
                            ["BuildingStudentsUnknownTitle"] = "Presence unavailable",

                            ["BuildingStudentsTooltip"] = "Enrolled students",
                            ["BuildingStudentsPresentTooltip"] = "Enrolled students currently present at this school",
                            ["BuildingStudentsAwayTooltip"] = "Enrolled students who are elsewhere or travelling here",
                            ["BuildingStudentsUnknownTooltip"] = "Enrolled students whose current presence could not be determined",

                            ["BuildingNoStudentsStatus"] = "No enrolled students",
                            ["BuildingNoStudentsPresentStatus"] = "No students on-site",

                            // ============================================================
                            // Ordinary visitor sections
                            // ============================================================

                            ["BuildingVisitorsTitle"] = "Visitors",
                            ["BuildingVisitorsTooltip"] = "Visitors currently inside or travelling to this building",

                            ["BuildingNoVisitorsStatus"] = "No visitors",
                            ["BuildingNoVisitorsPresentStatus"] = "No visitors inside",

                            // ============================================================
                            // Registered hotel guest sections
                            // Number is appended by the caller.
                            // ============================================================

                            ["BuildingHotelGuestsTitle"] = "Hotel guests",
                            ["BuildingHotelRoomTitle"] = "Hotel room ",
                            ["BuildingHotelGuestsTooltip"] = "Registered hotel guests",

                            ["BuildingNoHotelGuestsStatus"] = "No registered hotel guests",
                            ["BuildingNoHotelGuestsPresentStatus"] = "No hotel guests inside",

                            // ============================================================
                            // Building presence indicators
                            // ============================================================

                            ["BuildingPresencePresentTooltip"] = "Present at this building",
                            ["BuildingPresenceHeadingHereTooltip"] = "Travelling to this building",
                            ["BuildingPresenceAwayTooltip"] = "Away from this building",
                            ["BuildingPresenceUnknownTooltip"] = "Presence could not be determined",

                            ["BuildingResidentPresentTooltip"] = "At home",
                            ["BuildingResidentHeadingHereTooltip"] = "Travelling home",
                            ["BuildingResidentAwayTooltip"] = "Away from home",

                            ["BuildingWorkerPresentTooltip"] = "At workplace",
                            ["BuildingWorkerHeadingHereTooltip"] = "Travelling to work",
                            ["BuildingWorkerAwayTooltip"] = "Away from workplace",

                            ["BuildingStudentPresentTooltip"] = "At school",
                            ["BuildingStudentHeadingHereTooltip"] = "Travelling to school",
                            ["BuildingStudentAwayTooltip"] = "Away from school",

                            ["BuildingHotelGuestPresentTooltip"] = "At hotel",
                            ["BuildingHotelGuestHeadingHereTooltip"] = "Travelling to hotel",
                            ["BuildingHotelGuestAwayTooltip"] = "Away from hotel",

                            // ============================================================
                            // Expand/collapse actions
                            // ============================================================

                            ["BuildingSectionExpandTooltip"] = "Expand this section",
                            ["BuildingSectionCollapseTooltip"] = "Collapse this section",

                            // ============================================================
                            // Building-specific occupant sections
                            // ============================================================

                            ["BuildingSchoolStudentsTitle"] = "Students at school",
                            ["BuildingUniversityStudentsTitle"] = "Students at university",
                            ["BuildingPatientsTitle"] = "Patients",
                            ["BuildingDetaineesTitle"] = "Detainees",

                            ["BuildingNoDetaineesPresentStatus"] = "No detainees inside",
                            ["BuildingNoPatientsPresentStatus"] = "No patients inside",

                            // ============================================================
                            // Building-specific panel titles
                            // ============================================================

                            ["BuildingPoliceOccupantsTitle"] = "Police staff and detainees",
                            ["BuildingPrisonOccupantsTitle"] = "Prison staff and prisoners",

                            ["BuildingSchoolOccupantsTitle"] = "School staff and students",
                            ["BuildingUniversityOccupantsTitle"] = "University staff and students",

                            ["BuildingMedicalOccupantsTitle"] = "Medical staff and patients",
                            ["BuildingMedicalUniversityOccupantsTitle"] = "Medical staff, patients, and students",

                            ["BuildingHelicopterOccupantsTitle"] = "Pilots and workers",
                            ["BuildingFireStationOccupantsTitle"] = "Firefighters and workers",
                            ["BuildingRescueOccupantsTitle"] = "Rescue workers",
                            ["BuildingShelterOccupantsTitle"] = "Shelter occupants and workers",

                            // ============================================================
                            // Arrest and detention status
                            // ============================================================

                            ["CitizenArrestedStatus"] = "Citizen is under arrest",
                            ["CitizenTransportedToPrisonStatus"] = "Being transported to prison",
                            ["CitizenDetainedAtLabel"] = "Held at ",

                            // ============================================================
                            // Main window controls
                            // ============================================================

                            ["MainWindowResizeTooltip"] = "Drag to resize the window",
                            ["MainWindowResetSizeText"] = "Reset size",
                            ["MainWindowResetSizeTooltip"] = "Restore the default window size",

                            // ============================================================
                            // Building categories: residential
                            // ============================================================

                            ["BuildingTypeResidentialLowTitle"] = "Low-density residential",
                            ["BuildingTypeResidentialHighTitle"] = "High-density residential",

                            ["BuildingTypeResidentialLowEcoTitle"] = "Low-density eco residential",
                            ["BuildingTypeResidentialHighEcoTitle"] = "High-density eco residential",

                            ["BuildingTypeResidentialWallToWallTitle"] = "Wall-to-wall residential",

                            // ============================================================
                            // Building categories: commercial
                            // ============================================================

                            ["BuildingTypeCommercialLowTitle"] = "Low-density commercial",
                            ["BuildingTypeCommercialHighTitle"] = "High-density commercial",

                            ["BuildingTypeCommercialLeisureTitle"] = "Leisure commercial",
                            ["BuildingTypeCommercialTourismTitle"] = "Tourism commercial",
                            ["BuildingTypeCommercialEcoTitle"] = "Eco commercial",
                            ["BuildingTypeCommercialWallToWallTitle"] = "Wall-to-wall commercial",

                            // ============================================================
                            // Building category tooltips
                            // ============================================================

                            ["BuildingTypeCommercialEcoTooltip"] = "Eco commercial building",
                            ["BuildingTypeCommercialLeisureTooltip"] = "Leisure commercial building",
                            ["BuildingTypeCommercialTourismTooltip"] = "Tourism commercial building",
                            ["BuildingTypeCommercialWallToWallTooltip"] = "Wall-to-wall commercial building",

                            // ============================================================
                            // Building categories: other
                            // ============================================================

                            ["BuildingTypeIndustrialTitle"] = "Industrial",
                            ["BuildingTypeOfficeTitle"] = "Office"
                        };
						return it[index];
					}
					if (GameLanguage == "(DUTCH)")
					{
						nl = new Dictionary<string, string>
                        {
                            // ============================================================
                            // Main navigation
                            // ============================================================

                            ["MainViewListText"] = "View ",
                            ["MainButtonTooltip"] = "Open Favorite Cims",

                            ["MainCitizensTabText"] = "Citizens",
                            ["MainCitizensTabTooltip"] = "View favorite citizens",

                            ["MainBuildingsTabText"] = "Buildings",
                            ["MainBuildingsTabTooltip"] = "View favorite buildings",

                            ["MainStatisticsTabText"] = "Stats",
                            ["MainStatisticsTabTooltip"] = "View information and statistics",

                            // ============================================================
                            // Favorite actions
                            // ============================================================

                            ["FavoriteAddTooltip"] = "Add to favorites",
                            ["FavoriteRemoveTooltip"] = "Remove from favorites",

                            // ============================================================
                            // Citizen table columns
                            // ============================================================

                            ["CitizenStatusColumnTitle"] = "Status",
                            ["CitizenStatusColumnTooltip"] = "Citizen happiness",

                            ["CitizenNameColumnTitle"] = "Citizen name",
                            ["CitizenNameColumnTooltip"] = "Citizen name",

                            ["CitizenLifeStageColumnTitle"] = "Life stage",
                            ["CitizenLifeStageColumnTooltip"] = "Citizen life stage",

                            ["CitizenAgeColumnTitle"] = "Age",
                            ["CitizenAgeColumnTooltip"] = "Citizen age",

                            ["CitizenEducationColumnTitle"] = "Education",
                            ["CitizenEducationColumnTooltip"] = "Citizen education level",

                            ["CitizenHomeColumnTitle"] = "Home",
                            ["CitizenHomeColumnTooltip"] = "Citizen home",

                            ["CitizenOccupationColumnTitle"] = "Occupation",
                            ["CitizenOccupationColumnTooltip"] = "Workplace, school, or occupation",

                            ["CitizenActivityColumnTitle"] = "Activity",
                            ["CitizenActivityColumnTooltip"] = "What the citizen is currently doing",

                            ["CitizenRemoveColumnTitle"] = "Remove",
                            ["CitizenRemoveColumnTooltip"] = "Remove this citizen from favorites",

                            // ============================================================
                            // Citizen gender
                            // ============================================================

                            ["CitizenGenderLabelMale"] = "Male",
                            ["CitizenGenderLabelFemale"] = "Female",

                            // ============================================================
                            // Citizen life stages
                            // Keep gender suffixes for languages that require them.
                            // ============================================================

                            ["CitizenLifeStageChildMale"] = "Child",
                            ["CitizenLifeStageTeenMale"] = "Teen",
                            ["CitizenLifeStageYoungAdultMale"] = "Young adult",
                            ["CitizenLifeStageAdultMale"] = "Adult",
                            ["CitizenLifeStageSeniorMale"] = "Senior",
                            ["CitizenLifeStageDeceasedMale"] = "Deceased",

                            ["CitizenLifeStageChildFemale"] = "Child",
                            ["CitizenLifeStageTeenFemale"] = "Teen",
                            ["CitizenLifeStageYoungAdultFemale"] = "Young adult",
                            ["CitizenLifeStageAdultFemale"] = "Adult",
                            ["CitizenLifeStageSeniorFemale"] = "Senior",
                            ["CitizenLifeStageDeceasedFemale"] = "Deceased",

                            // ============================================================
                            // Citizen education
                            // ============================================================

                            ["CitizenEducationUneducatedMale"] = "Uneducated",
                            ["CitizenEducationEducatedMale"] = "Educated",
                            ["CitizenEducationWellEducatedMale"] = "Well educated",
                            ["CitizenEducationHighlyEducatedMale"] = "Highly educated",

                            ["CitizenEducationUneducatedFemale"] = "Uneducated",
                            ["CitizenEducationEducatedFemale"] = "Educated",
                            ["CitizenEducationWellEducatedFemale"] = "Well educated",
                            ["CitizenEducationHighlyEducatedFemale"] = "Highly educated",

                            // ============================================================
                            // Citizen life status
                            // ============================================================

                            ["CitizenAliveStatus"] = "Citizen is alive",
                            ["CitizenDeceasedStatus"] = "Citizen is deceased",
                            ["CitizenLeftCityStatus"] = "Citizen has left the city",

                            ["CitizenDeathDateLabel"] = "Date of death: ",
                            ["CitizenDeathTimeLabel"] = "Time of death: ",

                            // ============================================================
                            // Date and time formats
                            // Date order and clock format are independent settings.
                            // Quoted separators preserve the displayed punctuation.
                            // ============================================================

                            ["DateMonthFirstFormat"] = "MM'/'dd'/'yyyy",
                            ["DateDayFirstFormat"] = "dd'/'MM'/'yyyy",
                            ["DateYearFirstFormat"] = "yyyy'-'MM'-'dd",

                            ["Time12HourFormat"] = "hh':'mm tt",
                            ["Time24HourFormat"] = "HH':'mm",

                            ["DateFormatSettingLabel"] = "Date format",
                            ["DateFormatSettingTooltip"] = "Choose the order of the month, day, and year",

                            ["DateMonthFirstOptionText"] = "Month / Day / Year",
                            ["DateDayFirstOptionText"] = "Day / Month / Year",
                            ["DateYearFirstOptionText"] = "Year / Month / Day",

                            ["TimeFormatSettingLabel"] = "Time format",
                            ["TimeFormatSettingTooltip"] = "Choose a 12-hour or 24-hour clock",

                            ["Time12HourOptionText"] = "12-hour clock",
                            ["Time24HourOptionText"] = "24-hour clock",

                            // ============================================================
                            // Citizen happiness
                            // ============================================================

                            ["CitizenHappinessVeryUnhappyStatus"] = "Very unhappy",
                            ["CitizenHappinessUnhappyStatus"] = "Unhappy",
                            ["CitizenHappinessHappyStatus"] = "Happy",
                            ["CitizenHappinessVeryHappyStatus"] = "Very happy",
                            ["CitizenHappinessExtremelyHappyStatus"] = "Extremely happy",

                            // ============================================================
                            // Citizen home
                            // ============================================================

                            ["CitizenHomelessStatus"] = "Homeless",
                            ["CitizenHomelessTooltip"] = "Citizen has no home",

                            ["CitizenHomeOutsideCityStatus"] = "Outside the city",
                            ["CitizenHomeOutsideCityTooltip"] = "Citizen lives outside the city",

                            ["CitizenHomeCemeteryStatus"] = "Cemetery",

                            // ============================================================
                            // Citizen occupation
                            // ============================================================

                            ["CitizenRetiredStatus"] = "Retired",
                            ["CitizenRetiredTooltip"] = "Citizen is retired",

                            ["CitizenUnemployedStatus"] = "Unemployed",
                            ["CitizenUnemployedTooltip"] = "Citizen is unemployed",

                            ["CitizenGovernmentWorkplaceTooltip"] = "Government building",
                            ["CitizenElectricityWorkplaceTooltip"] = "Electricity facility",
                            ["CitizenWaterWorkplaceTooltip"] = "Water facility",
                            ["CitizenGenericWorkplaceTooltip"] = "Citizen works here",

                            ["CitizenWalkingTooltip"] = "Travelling on foot",
                            ["CitizenTouristTooltip"] = "This citizen is visiting the city",

                            // ============================================================
                            // Citizen details and family
                            // ============================================================

                            ["CitizenDetailsTitle"] = "Citizen details",
                            ["CitizenDetailsUnavailableTooltip"] = "No details window is available. Close an existing window and try again.",

                            ["CitizenStatusLabel"] = "Status: ",
                            ["CitizenHomeLabel"] = "Home: ",

                            ["CitizenFamilyTitle"] = "Family",
                            ["CitizenNoPartnerStatus"] = "No partner",
                            ["CitizenNoChildrenStatus"] = "No children",
                            ["CitizenNoHouseholdStatus"] = "No household details available",

                            ["CitizenSwitchTooltip"] = "Right-click to view this citizen",

                            // ============================================================
                            // Districts
                            // ============================================================

                            ["CitizenCurrentDistrictLabel"] = "Current district: ",
                            ["CitizenCurrentDistrictTooltip"] = "Citizen's current district",

                            ["DistrictLabel"] = "District: ",
                            ["DistrictNoneText"] = "None",

                            // ============================================================
                            // Citizen wealth and wellbeing
                            // ============================================================

                            ["CitizenWealthLowStatusMale"] = "Poor",
                            ["CitizenWealthMediumStatusMale"] = "Well-off",
                            ["CitizenWealthHighStatusMale"] = "Rich",

                            ["CitizenWealthLowStatusFemale"] = "Poor",
                            ["CitizenWealthMediumStatusFemale"] = "Well-off",
                            ["CitizenWealthHighStatusFemale"] = "Rich",

                            ["CitizenWellbeingLabel"] = "Wellbeing: ",
                            ["CitizenHappinessLabel"] = "Happiness: ",
                            ["CitizenWealthLabel"] = "Wealth",

                            // ============================================================
                            // Citizen health
                            // ============================================================

                            ["CitizenHealthVerySickStatusMale"] = "Very sick",
                            ["CitizenHealthSickStatusMale"] = "Sick",
                            ["CitizenHealthPoorStatusMale"] = "Poor health",
                            ["CitizenHealthHealthyStatusMale"] = "Healthy",
                            ["CitizenHealthVeryHealthyStatusMale"] = "Very healthy",
                            ["CitizenHealthExcellentStatusMale"] = "Excellent health",

                            ["CitizenHealthVerySickStatusFemale"] = "Very sick",
                            ["CitizenHealthSickStatusFemale"] = "Sick",
                            ["CitizenHealthPoorStatusFemale"] = "Poor health",
                            ["CitizenHealthHealthyStatusFemale"] = "Healthy",
                            ["CitizenHealthVeryHealthyStatusFemale"] = "Very healthy",
                            ["CitizenHealthExcellentStatusFemale"] = "Excellent health",

                            // ============================================================
                            // Death and funeral status
                            // ============================================================

                            ["CitizenWaitingForHearseStatus"] = "Waiting for a hearse",
                            ["CitizenInHearseStatus"] = "Being transported by hearse",
                            ["CitizenFuneralStatus"] = "Funeral service",
                            ["CitizenBuriedStatus"] = "Buried",

                            // ============================================================
                            // Vehicles and passengers
                            // ============================================================

                            ["VehicleViewPassengersText"] = "View passengers",
                            ["VehicleNoPassengersStatus"] = "No passengers",

                            ["VehiclePassengersTitle"] = "Passengers on board",
                            ["VehicleDriverTitle"] = "Driver",
                            ["VehiclePassengerTooltip"] = "Passenger",
                            ["VehiclePassengersButtonTooltip"] = "View passengers list",

                            // ============================================================
                            // Building occupant panel actions and titles
                            // ============================================================

                            ["BuildingViewResidentsText"] = "View residents",
                            ["BuildingResidentsTitle"] = "Residents",

                            ["BuildingViewOccupantsText"] = "View building occupants",
                            ["BuildingOccupantsTitle"] = "Building occupants",

                            ["BuildingHotelOccupantsTitle"] = "Workers, hotel guests, and visitors",

                            ["BuildingNoOccupantsStatus"] = "No listed occupants",
                            ["BuildingOccupantTotalLabel"] = "Total: ",

                            // ============================================================
                            // Registered resident sections
                            // Number is appended by the caller.
                            // ============================================================

                            ["BuildingHouseholdTitle"] = "Household ",
                            ["BuildingResidentsTooltip"] = "Registered residents",

                            // ============================================================
                            // Registered worker sections
                            // Presence and roster membership have separate empty states.
                            // ============================================================

                            ["BuildingWorkersTitle"] = "Workers",
                            ["BuildingWorkersPresentTitle"] = "At workplace",
                            ["BuildingWorkersAwayTitle"] = "Away from workplace",
                            ["BuildingWorkersUnknownTitle"] = "Presence unavailable",

                            ["BuildingWorkersTooltip"] = "Registered workers",
                            ["BuildingWorkersPresentTooltip"] = "Registered workers currently present at this building",
                            ["BuildingWorkersAwayTooltip"] = "Registered workers who are elsewhere or travelling here",
                            ["BuildingWorkersUnknownTooltip"] = "Registered workers whose current presence could not be determined",

                            ["BuildingNoWorkersStatus"] = "No registered workers",
                            ["BuildingNoWorkersPresentStatus"] = "No workers on-site",

                            // ============================================================
                            // Enrolled student sections
                            // ============================================================

                            ["BuildingStudentsTitle"] = "Students",
                            ["BuildingStudentsPresentTitle"] = "At school",
                            ["BuildingStudentsAwayTitle"] = "Away from school",
                            ["BuildingStudentsUnknownTitle"] = "Presence unavailable",

                            ["BuildingStudentsTooltip"] = "Enrolled students",
                            ["BuildingStudentsPresentTooltip"] = "Enrolled students currently present at this school",
                            ["BuildingStudentsAwayTooltip"] = "Enrolled students who are elsewhere or travelling here",
                            ["BuildingStudentsUnknownTooltip"] = "Enrolled students whose current presence could not be determined",

                            ["BuildingNoStudentsStatus"] = "No enrolled students",
                            ["BuildingNoStudentsPresentStatus"] = "No students on-site",

                            // ============================================================
                            // Ordinary visitor sections
                            // ============================================================

                            ["BuildingVisitorsTitle"] = "Visitors",
                            ["BuildingVisitorsTooltip"] = "Visitors currently inside or travelling to this building",

                            ["BuildingNoVisitorsStatus"] = "No visitors",
                            ["BuildingNoVisitorsPresentStatus"] = "No visitors inside",

                            // ============================================================
                            // Registered hotel guest sections
                            // Number is appended by the caller.
                            // ============================================================

                            ["BuildingHotelGuestsTitle"] = "Hotel guests",
                            ["BuildingHotelRoomTitle"] = "Hotel room ",
                            ["BuildingHotelGuestsTooltip"] = "Registered hotel guests",

                            ["BuildingNoHotelGuestsStatus"] = "No registered hotel guests",
                            ["BuildingNoHotelGuestsPresentStatus"] = "No hotel guests inside",

                            // ============================================================
                            // Building presence indicators
                            // ============================================================

                            ["BuildingPresencePresentTooltip"] = "Present at this building",
                            ["BuildingPresenceHeadingHereTooltip"] = "Travelling to this building",
                            ["BuildingPresenceAwayTooltip"] = "Away from this building",
                            ["BuildingPresenceUnknownTooltip"] = "Presence could not be determined",

                            ["BuildingResidentPresentTooltip"] = "At home",
                            ["BuildingResidentHeadingHereTooltip"] = "Travelling home",
                            ["BuildingResidentAwayTooltip"] = "Away from home",

                            ["BuildingWorkerPresentTooltip"] = "At workplace",
                            ["BuildingWorkerHeadingHereTooltip"] = "Travelling to work",
                            ["BuildingWorkerAwayTooltip"] = "Away from workplace",

                            ["BuildingStudentPresentTooltip"] = "At school",
                            ["BuildingStudentHeadingHereTooltip"] = "Travelling to school",
                            ["BuildingStudentAwayTooltip"] = "Away from school",

                            ["BuildingHotelGuestPresentTooltip"] = "At hotel",
                            ["BuildingHotelGuestHeadingHereTooltip"] = "Travelling to hotel",
                            ["BuildingHotelGuestAwayTooltip"] = "Away from hotel",

                            // ============================================================
                            // Expand/collapse actions
                            // ============================================================

                            ["BuildingSectionExpandTooltip"] = "Expand this section",
                            ["BuildingSectionCollapseTooltip"] = "Collapse this section",

                            // ============================================================
                            // Building-specific occupant sections
                            // ============================================================

                            ["BuildingSchoolStudentsTitle"] = "Students at school",
                            ["BuildingUniversityStudentsTitle"] = "Students at university",
                            ["BuildingPatientsTitle"] = "Patients",
                            ["BuildingDetaineesTitle"] = "Detainees",

                            ["BuildingNoDetaineesPresentStatus"] = "No detainees inside",
                            ["BuildingNoPatientsPresentStatus"] = "No patients inside",

                            // ============================================================
                            // Building-specific panel titles
                            // ============================================================

                            ["BuildingPoliceOccupantsTitle"] = "Police staff and detainees",
                            ["BuildingPrisonOccupantsTitle"] = "Prison staff and prisoners",

                            ["BuildingSchoolOccupantsTitle"] = "School staff and students",
                            ["BuildingUniversityOccupantsTitle"] = "University staff and students",

                            ["BuildingMedicalOccupantsTitle"] = "Medical staff and patients",
                            ["BuildingMedicalUniversityOccupantsTitle"] = "Medical staff, patients, and students",

                            ["BuildingHelicopterOccupantsTitle"] = "Pilots and workers",
                            ["BuildingFireStationOccupantsTitle"] = "Firefighters and workers",
                            ["BuildingRescueOccupantsTitle"] = "Rescue workers",
                            ["BuildingShelterOccupantsTitle"] = "Shelter occupants and workers",

                            // ============================================================
                            // Arrest and detention status
                            // ============================================================

                            ["CitizenArrestedStatus"] = "Citizen is under arrest",
                            ["CitizenTransportedToPrisonStatus"] = "Being transported to prison",
                            ["CitizenDetainedAtLabel"] = "Held at ",

                            // ============================================================
                            // Main window controls
                            // ============================================================

                            ["MainWindowResizeTooltip"] = "Drag to resize the window",
                            ["MainWindowResetSizeText"] = "Reset size",
                            ["MainWindowResetSizeTooltip"] = "Restore the default window size",

                            // ============================================================
                            // Building categories: residential
                            // ============================================================

                            ["BuildingTypeResidentialLowTitle"] = "Low-density residential",
                            ["BuildingTypeResidentialHighTitle"] = "High-density residential",

                            ["BuildingTypeResidentialLowEcoTitle"] = "Low-density eco residential",
                            ["BuildingTypeResidentialHighEcoTitle"] = "High-density eco residential",

                            ["BuildingTypeResidentialWallToWallTitle"] = "Wall-to-wall residential",

                            // ============================================================
                            // Building categories: commercial
                            // ============================================================

                            ["BuildingTypeCommercialLowTitle"] = "Low-density commercial",
                            ["BuildingTypeCommercialHighTitle"] = "High-density commercial",

                            ["BuildingTypeCommercialLeisureTitle"] = "Leisure commercial",
                            ["BuildingTypeCommercialTourismTitle"] = "Tourism commercial",
                            ["BuildingTypeCommercialEcoTitle"] = "Eco commercial",
                            ["BuildingTypeCommercialWallToWallTitle"] = "Wall-to-wall commercial",

                            // ============================================================
                            // Building category tooltips
                            // ============================================================

                            ["BuildingTypeCommercialEcoTooltip"] = "Eco commercial building",
                            ["BuildingTypeCommercialLeisureTooltip"] = "Leisure commercial building",
                            ["BuildingTypeCommercialTourismTooltip"] = "Tourism commercial building",
                            ["BuildingTypeCommercialWallToWallTooltip"] = "Wall-to-wall commercial building",

                            // ============================================================
                            // Building categories: other
                            // ============================================================

                            ["BuildingTypeIndustrialTitle"] = "Industrial",
                            ["BuildingTypeOfficeTitle"] = "Office"
                        };
						return nl[index];
					}
					if (GameLanguage == "(RUSSIAN)")
					{
						rus = new Dictionary<string, string>
                        {
                            // ============================================================
                            // Main navigation
                            // ============================================================

                            ["MainViewListText"] = "View ",
                            ["MainButtonTooltip"] = "Open Favorite Cims",

                            ["MainCitizensTabText"] = "Citizens",
                            ["MainCitizensTabTooltip"] = "View favorite citizens",

                            ["MainBuildingsTabText"] = "Buildings",
                            ["MainBuildingsTabTooltip"] = "View favorite buildings",

                            ["MainStatisticsTabText"] = "Stats",
                            ["MainStatisticsTabTooltip"] = "View information and statistics",

                            // ============================================================
                            // Favorite actions
                            // ============================================================

                            ["FavoriteAddTooltip"] = "Add to favorites",
                            ["FavoriteRemoveTooltip"] = "Remove from favorites",

                            // ============================================================
                            // Citizen table columns
                            // ============================================================

                            ["CitizenStatusColumnTitle"] = "Status",
                            ["CitizenStatusColumnTooltip"] = "Citizen happiness",

                            ["CitizenNameColumnTitle"] = "Citizen name",
                            ["CitizenNameColumnTooltip"] = "Citizen name",

                            ["CitizenLifeStageColumnTitle"] = "Life stage",
                            ["CitizenLifeStageColumnTooltip"] = "Citizen life stage",

                            ["CitizenAgeColumnTitle"] = "Age",
                            ["CitizenAgeColumnTooltip"] = "Citizen age",

                            ["CitizenEducationColumnTitle"] = "Education",
                            ["CitizenEducationColumnTooltip"] = "Citizen education level",

                            ["CitizenHomeColumnTitle"] = "Home",
                            ["CitizenHomeColumnTooltip"] = "Citizen home",

                            ["CitizenOccupationColumnTitle"] = "Occupation",
                            ["CitizenOccupationColumnTooltip"] = "Workplace, school, or occupation",

                            ["CitizenActivityColumnTitle"] = "Activity",
                            ["CitizenActivityColumnTooltip"] = "What the citizen is currently doing",

                            ["CitizenRemoveColumnTitle"] = "Remove",
                            ["CitizenRemoveColumnTooltip"] = "Remove this citizen from favorites",

                            // ============================================================
                            // Citizen gender
                            // ============================================================

                            ["CitizenGenderLabelMale"] = "Male",
                            ["CitizenGenderLabelFemale"] = "Female",

                            // ============================================================
                            // Citizen life stages
                            // Keep gender suffixes for languages that require them.
                            // ============================================================

                            ["CitizenLifeStageChildMale"] = "Child",
                            ["CitizenLifeStageTeenMale"] = "Teen",
                            ["CitizenLifeStageYoungAdultMale"] = "Young adult",
                            ["CitizenLifeStageAdultMale"] = "Adult",
                            ["CitizenLifeStageSeniorMale"] = "Senior",
                            ["CitizenLifeStageDeceasedMale"] = "Deceased",

                            ["CitizenLifeStageChildFemale"] = "Child",
                            ["CitizenLifeStageTeenFemale"] = "Teen",
                            ["CitizenLifeStageYoungAdultFemale"] = "Young adult",
                            ["CitizenLifeStageAdultFemale"] = "Adult",
                            ["CitizenLifeStageSeniorFemale"] = "Senior",
                            ["CitizenLifeStageDeceasedFemale"] = "Deceased",

                            // ============================================================
                            // Citizen education
                            // ============================================================

                            ["CitizenEducationUneducatedMale"] = "Uneducated",
                            ["CitizenEducationEducatedMale"] = "Educated",
                            ["CitizenEducationWellEducatedMale"] = "Well educated",
                            ["CitizenEducationHighlyEducatedMale"] = "Highly educated",

                            ["CitizenEducationUneducatedFemale"] = "Uneducated",
                            ["CitizenEducationEducatedFemale"] = "Educated",
                            ["CitizenEducationWellEducatedFemale"] = "Well educated",
                            ["CitizenEducationHighlyEducatedFemale"] = "Highly educated",

                            // ============================================================
                            // Citizen life status
                            // ============================================================

                            ["CitizenAliveStatus"] = "Citizen is alive",
                            ["CitizenDeceasedStatus"] = "Citizen is deceased",
                            ["CitizenLeftCityStatus"] = "Citizen has left the city",

                            ["CitizenDeathDateLabel"] = "Date of death: ",
                            ["CitizenDeathTimeLabel"] = "Time of death: ",

                            // ============================================================
                            // Date and time formats
                            // Date order and clock format are independent settings.
                            // Quoted separators preserve the displayed punctuation.
                            // ============================================================

                            ["DateMonthFirstFormat"] = "MM'/'dd'/'yyyy",
                            ["DateDayFirstFormat"] = "dd'/'MM'/'yyyy",
                            ["DateYearFirstFormat"] = "yyyy'-'MM'-'dd",

                            ["Time12HourFormat"] = "hh':'mm tt",
                            ["Time24HourFormat"] = "HH':'mm",

                            ["DateFormatSettingLabel"] = "Date format",
                            ["DateFormatSettingTooltip"] = "Choose the order of the month, day, and year",

                            ["DateMonthFirstOptionText"] = "Month / Day / Year",
                            ["DateDayFirstOptionText"] = "Day / Month / Year",
                            ["DateYearFirstOptionText"] = "Year / Month / Day",

                            ["TimeFormatSettingLabel"] = "Time format",
                            ["TimeFormatSettingTooltip"] = "Choose a 12-hour or 24-hour clock",

                            ["Time12HourOptionText"] = "12-hour clock",
                            ["Time24HourOptionText"] = "24-hour clock",

                            // ============================================================
                            // Citizen happiness
                            // ============================================================

                            ["CitizenHappinessVeryUnhappyStatus"] = "Very unhappy",
                            ["CitizenHappinessUnhappyStatus"] = "Unhappy",
                            ["CitizenHappinessHappyStatus"] = "Happy",
                            ["CitizenHappinessVeryHappyStatus"] = "Very happy",
                            ["CitizenHappinessExtremelyHappyStatus"] = "Extremely happy",

                            // ============================================================
                            // Citizen home
                            // ============================================================

                            ["CitizenHomelessStatus"] = "Homeless",
                            ["CitizenHomelessTooltip"] = "Citizen has no home",

                            ["CitizenHomeOutsideCityStatus"] = "Outside the city",
                            ["CitizenHomeOutsideCityTooltip"] = "Citizen lives outside the city",

                            ["CitizenHomeCemeteryStatus"] = "Cemetery",

                            // ============================================================
                            // Citizen occupation
                            // ============================================================

                            ["CitizenRetiredStatus"] = "Retired",
                            ["CitizenRetiredTooltip"] = "Citizen is retired",

                            ["CitizenUnemployedStatus"] = "Unemployed",
                            ["CitizenUnemployedTooltip"] = "Citizen is unemployed",

                            ["CitizenGovernmentWorkplaceTooltip"] = "Government building",
                            ["CitizenElectricityWorkplaceTooltip"] = "Electricity facility",
                            ["CitizenWaterWorkplaceTooltip"] = "Water facility",
                            ["CitizenGenericWorkplaceTooltip"] = "Citizen works here",

                            ["CitizenWalkingTooltip"] = "Travelling on foot",
                            ["CitizenTouristTooltip"] = "This citizen is visiting the city",

                            // ============================================================
                            // Citizen details and family
                            // ============================================================

                            ["CitizenDetailsTitle"] = "Citizen details",
                            ["CitizenDetailsUnavailableTooltip"] = "No details window is available. Close an existing window and try again.",

                            ["CitizenStatusLabel"] = "Status: ",
                            ["CitizenHomeLabel"] = "Home: ",

                            ["CitizenFamilyTitle"] = "Family",
                            ["CitizenNoPartnerStatus"] = "No partner",
                            ["CitizenNoChildrenStatus"] = "No children",
                            ["CitizenNoHouseholdStatus"] = "No household details available",

                            ["CitizenSwitchTooltip"] = "Right-click to view this citizen",

                            // ============================================================
                            // Districts
                            // ============================================================

                            ["CitizenCurrentDistrictLabel"] = "Current district: ",
                            ["CitizenCurrentDistrictTooltip"] = "Citizen's current district",

                            ["DistrictLabel"] = "District: ",
                            ["DistrictNoneText"] = "None",

                            // ============================================================
                            // Citizen wealth and wellbeing
                            // ============================================================

                            ["CitizenWealthLowStatusMale"] = "Poor",
                            ["CitizenWealthMediumStatusMale"] = "Well-off",
                            ["CitizenWealthHighStatusMale"] = "Rich",

                            ["CitizenWealthLowStatusFemale"] = "Poor",
                            ["CitizenWealthMediumStatusFemale"] = "Well-off",
                            ["CitizenWealthHighStatusFemale"] = "Rich",

                            ["CitizenWellbeingLabel"] = "Wellbeing: ",
                            ["CitizenHappinessLabel"] = "Happiness: ",
                            ["CitizenWealthLabel"] = "Wealth",

                            // ============================================================
                            // Citizen health
                            // ============================================================

                            ["CitizenHealthVerySickStatusMale"] = "Very sick",
                            ["CitizenHealthSickStatusMale"] = "Sick",
                            ["CitizenHealthPoorStatusMale"] = "Poor health",
                            ["CitizenHealthHealthyStatusMale"] = "Healthy",
                            ["CitizenHealthVeryHealthyStatusMale"] = "Very healthy",
                            ["CitizenHealthExcellentStatusMale"] = "Excellent health",

                            ["CitizenHealthVerySickStatusFemale"] = "Very sick",
                            ["CitizenHealthSickStatusFemale"] = "Sick",
                            ["CitizenHealthPoorStatusFemale"] = "Poor health",
                            ["CitizenHealthHealthyStatusFemale"] = "Healthy",
                            ["CitizenHealthVeryHealthyStatusFemale"] = "Very healthy",
                            ["CitizenHealthExcellentStatusFemale"] = "Excellent health",

                            // ============================================================
                            // Death and funeral status
                            // ============================================================

                            ["CitizenWaitingForHearseStatus"] = "Waiting for a hearse",
                            ["CitizenInHearseStatus"] = "Being transported by hearse",
                            ["CitizenFuneralStatus"] = "Funeral service",
                            ["CitizenBuriedStatus"] = "Buried",

                            // ============================================================
                            // Vehicles and passengers
                            // ============================================================

                            ["VehicleViewPassengersText"] = "View passengers",
                            ["VehicleNoPassengersStatus"] = "No passengers",

                            ["VehiclePassengersTitle"] = "Passengers on board",
                            ["VehicleDriverTitle"] = "Driver",
                            ["VehiclePassengerTooltip"] = "Passenger",
                            ["VehiclePassengersButtonTooltip"] = "View passengers list",

                            // ============================================================
                            // Building occupant panel actions and titles
                            // ============================================================

                            ["BuildingViewResidentsText"] = "View residents",
                            ["BuildingResidentsTitle"] = "Residents",

                            ["BuildingViewOccupantsText"] = "View building occupants",
                            ["BuildingOccupantsTitle"] = "Building occupants",

                            ["BuildingHotelOccupantsTitle"] = "Workers, hotel guests, and visitors",

                            ["BuildingNoOccupantsStatus"] = "No listed occupants",
                            ["BuildingOccupantTotalLabel"] = "Total: ",

                            // ============================================================
                            // Registered resident sections
                            // Number is appended by the caller.
                            // ============================================================

                            ["BuildingHouseholdTitle"] = "Household ",
                            ["BuildingResidentsTooltip"] = "Registered residents",

                            // ============================================================
                            // Registered worker sections
                            // Presence and roster membership have separate empty states.
                            // ============================================================

                            ["BuildingWorkersTitle"] = "Workers",
                            ["BuildingWorkersPresentTitle"] = "At workplace",
                            ["BuildingWorkersAwayTitle"] = "Away from workplace",
                            ["BuildingWorkersUnknownTitle"] = "Presence unavailable",

                            ["BuildingWorkersTooltip"] = "Registered workers",
                            ["BuildingWorkersPresentTooltip"] = "Registered workers currently present at this building",
                            ["BuildingWorkersAwayTooltip"] = "Registered workers who are elsewhere or travelling here",
                            ["BuildingWorkersUnknownTooltip"] = "Registered workers whose current presence could not be determined",

                            ["BuildingNoWorkersStatus"] = "No registered workers",
                            ["BuildingNoWorkersPresentStatus"] = "No workers on-site",

                            // ============================================================
                            // Enrolled student sections
                            // ============================================================

                            ["BuildingStudentsTitle"] = "Students",
                            ["BuildingStudentsPresentTitle"] = "At school",
                            ["BuildingStudentsAwayTitle"] = "Away from school",
                            ["BuildingStudentsUnknownTitle"] = "Presence unavailable",

                            ["BuildingStudentsTooltip"] = "Enrolled students",
                            ["BuildingStudentsPresentTooltip"] = "Enrolled students currently present at this school",
                            ["BuildingStudentsAwayTooltip"] = "Enrolled students who are elsewhere or travelling here",
                            ["BuildingStudentsUnknownTooltip"] = "Enrolled students whose current presence could not be determined",

                            ["BuildingNoStudentsStatus"] = "No enrolled students",
                            ["BuildingNoStudentsPresentStatus"] = "No students on-site",

                            // ============================================================
                            // Ordinary visitor sections
                            // ============================================================

                            ["BuildingVisitorsTitle"] = "Visitors",
                            ["BuildingVisitorsTooltip"] = "Visitors currently inside or travelling to this building",

                            ["BuildingNoVisitorsStatus"] = "No visitors",
                            ["BuildingNoVisitorsPresentStatus"] = "No visitors inside",

                            // ============================================================
                            // Registered hotel guest sections
                            // Number is appended by the caller.
                            // ============================================================

                            ["BuildingHotelGuestsTitle"] = "Hotel guests",
                            ["BuildingHotelRoomTitle"] = "Hotel room ",
                            ["BuildingHotelGuestsTooltip"] = "Registered hotel guests",

                            ["BuildingNoHotelGuestsStatus"] = "No registered hotel guests",
                            ["BuildingNoHotelGuestsPresentStatus"] = "No hotel guests inside",

                            // ============================================================
                            // Building presence indicators
                            // ============================================================

                            ["BuildingPresencePresentTooltip"] = "Present at this building",
                            ["BuildingPresenceHeadingHereTooltip"] = "Travelling to this building",
                            ["BuildingPresenceAwayTooltip"] = "Away from this building",
                            ["BuildingPresenceUnknownTooltip"] = "Presence could not be determined",

                            ["BuildingResidentPresentTooltip"] = "At home",
                            ["BuildingResidentHeadingHereTooltip"] = "Travelling home",
                            ["BuildingResidentAwayTooltip"] = "Away from home",

                            ["BuildingWorkerPresentTooltip"] = "At workplace",
                            ["BuildingWorkerHeadingHereTooltip"] = "Travelling to work",
                            ["BuildingWorkerAwayTooltip"] = "Away from workplace",

                            ["BuildingStudentPresentTooltip"] = "At school",
                            ["BuildingStudentHeadingHereTooltip"] = "Travelling to school",
                            ["BuildingStudentAwayTooltip"] = "Away from school",

                            ["BuildingHotelGuestPresentTooltip"] = "At hotel",
                            ["BuildingHotelGuestHeadingHereTooltip"] = "Travelling to hotel",
                            ["BuildingHotelGuestAwayTooltip"] = "Away from hotel",

                            // ============================================================
                            // Expand/collapse actions
                            // ============================================================

                            ["BuildingSectionExpandTooltip"] = "Expand this section",
                            ["BuildingSectionCollapseTooltip"] = "Collapse this section",

                            // ============================================================
                            // Building-specific occupant sections
                            // ============================================================

                            ["BuildingSchoolStudentsTitle"] = "Students at school",
                            ["BuildingUniversityStudentsTitle"] = "Students at university",
                            ["BuildingPatientsTitle"] = "Patients",
                            ["BuildingDetaineesTitle"] = "Detainees",

                            ["BuildingNoDetaineesPresentStatus"] = "No detainees inside",
                            ["BuildingNoPatientsPresentStatus"] = "No patients inside",

                            // ============================================================
                            // Building-specific panel titles
                            // ============================================================

                            ["BuildingPoliceOccupantsTitle"] = "Police staff and detainees",
                            ["BuildingPrisonOccupantsTitle"] = "Prison staff and prisoners",

                            ["BuildingSchoolOccupantsTitle"] = "School staff and students",
                            ["BuildingUniversityOccupantsTitle"] = "University staff and students",

                            ["BuildingMedicalOccupantsTitle"] = "Medical staff and patients",
                            ["BuildingMedicalUniversityOccupantsTitle"] = "Medical staff, patients, and students",

                            ["BuildingHelicopterOccupantsTitle"] = "Pilots and workers",
                            ["BuildingFireStationOccupantsTitle"] = "Firefighters and workers",
                            ["BuildingRescueOccupantsTitle"] = "Rescue workers",
                            ["BuildingShelterOccupantsTitle"] = "Shelter occupants and workers",

                            // ============================================================
                            // Arrest and detention status
                            // ============================================================

                            ["CitizenArrestedStatus"] = "Citizen is under arrest",
                            ["CitizenTransportedToPrisonStatus"] = "Being transported to prison",
                            ["CitizenDetainedAtLabel"] = "Held at ",

                            // ============================================================
                            // Main window controls
                            // ============================================================

                            ["MainWindowResizeTooltip"] = "Drag to resize the window",
                            ["MainWindowResetSizeText"] = "Reset size",
                            ["MainWindowResetSizeTooltip"] = "Restore the default window size",

                            // ============================================================
                            // Building categories: residential
                            // ============================================================

                            ["BuildingTypeResidentialLowTitle"] = "Low-density residential",
                            ["BuildingTypeResidentialHighTitle"] = "High-density residential",

                            ["BuildingTypeResidentialLowEcoTitle"] = "Low-density eco residential",
                            ["BuildingTypeResidentialHighEcoTitle"] = "High-density eco residential",

                            ["BuildingTypeResidentialWallToWallTitle"] = "Wall-to-wall residential",

                            // ============================================================
                            // Building categories: commercial
                            // ============================================================

                            ["BuildingTypeCommercialLowTitle"] = "Low-density commercial",
                            ["BuildingTypeCommercialHighTitle"] = "High-density commercial",

                            ["BuildingTypeCommercialLeisureTitle"] = "Leisure commercial",
                            ["BuildingTypeCommercialTourismTitle"] = "Tourism commercial",
                            ["BuildingTypeCommercialEcoTitle"] = "Eco commercial",
                            ["BuildingTypeCommercialWallToWallTitle"] = "Wall-to-wall commercial",

                            // ============================================================
                            // Building category tooltips
                            // ============================================================

                            ["BuildingTypeCommercialEcoTooltip"] = "Eco commercial building",
                            ["BuildingTypeCommercialLeisureTooltip"] = "Leisure commercial building",
                            ["BuildingTypeCommercialTourismTooltip"] = "Tourism commercial building",
                            ["BuildingTypeCommercialWallToWallTooltip"] = "Wall-to-wall commercial building",

                            // ============================================================
                            // Building categories: other
                            // ============================================================

                            ["BuildingTypeIndustrialTitle"] = "Industrial",
                            ["BuildingTypeOfficeTitle"] = "Office"
                        };
						return rus[index];
					}
					en = new Dictionary<string, string>
                    {
                        // ============================================================
                        // Main navigation
                        // ============================================================

                        ["MainViewListText"] = "View ",
                        ["MainButtonTooltip"] = "Open Favorite Cims",

                        ["MainCitizensTabText"] = "Citizens",
                        ["MainCitizensTabTooltip"] = "View favorite citizens",

                        ["MainBuildingsTabText"] = "Buildings",
                        ["MainBuildingsTabTooltip"] = "View favorite buildings",

                        ["MainStatisticsTabText"] = "Stats",
                        ["MainStatisticsTabTooltip"] = "View information and statistics",

                        // ============================================================
                        // Favorite actions
                        // ============================================================

                        ["FavoriteAddTooltip"] = "Add to favorites",
                        ["FavoriteRemoveTooltip"] = "Remove from favorites",

                        // ============================================================
                        // Citizen table columns
                        // ============================================================

                        ["CitizenStatusColumnTitle"] = "Status",
                        ["CitizenStatusColumnTooltip"] = "Citizen happiness",

                        ["CitizenNameColumnTitle"] = "Citizen name",
                        ["CitizenNameColumnTooltip"] = "Citizen name",

                        ["CitizenLifeStageColumnTitle"] = "Life stage",
                        ["CitizenLifeStageColumnTooltip"] = "Citizen life stage",

                        ["CitizenAgeColumnTitle"] = "Age",
                        ["CitizenAgeColumnTooltip"] = "Citizen age",

                        ["CitizenEducationColumnTitle"] = "Education",
                        ["CitizenEducationColumnTooltip"] = "Citizen education level",

                        ["CitizenHomeColumnTitle"] = "Home",
                        ["CitizenHomeColumnTooltip"] = "Citizen home",

                        ["CitizenOccupationColumnTitle"] = "Occupation",
                        ["CitizenOccupationColumnTooltip"] = "Workplace, school, or occupation",

                        ["CitizenActivityColumnTitle"] = "Activity",
                        ["CitizenActivityColumnTooltip"] = "What the citizen is currently doing",

                        ["CitizenRemoveColumnTitle"] = "Remove",
                        ["CitizenRemoveColumnTooltip"] = "Remove this citizen from favorites",

                        // ============================================================
                        // Citizen gender
                        // ============================================================

                        ["CitizenGenderLabelMale"] = "Male",
                        ["CitizenGenderLabelFemale"] = "Female",

                        // ============================================================
                        // Citizen life stages
                        // Keep gender suffixes for languages that require them.
                        // ============================================================

                        ["CitizenLifeStageChildMale"] = "Child",
                        ["CitizenLifeStageTeenMale"] = "Teen",
                        ["CitizenLifeStageYoungAdultMale"] = "Young adult",
                        ["CitizenLifeStageAdultMale"] = "Adult",
                        ["CitizenLifeStageSeniorMale"] = "Senior",
                        ["CitizenLifeStageDeceasedMale"] = "Deceased",

                        ["CitizenLifeStageChildFemale"] = "Child",
                        ["CitizenLifeStageTeenFemale"] = "Teen",
                        ["CitizenLifeStageYoungAdultFemale"] = "Young adult",
                        ["CitizenLifeStageAdultFemale"] = "Adult",
                        ["CitizenLifeStageSeniorFemale"] = "Senior",
                        ["CitizenLifeStageDeceasedFemale"] = "Deceased",

                        // ============================================================
                        // Citizen education
                        // ============================================================

                        ["CitizenEducationUneducatedMale"] = "Uneducated",
                        ["CitizenEducationEducatedMale"] = "Educated",
                        ["CitizenEducationWellEducatedMale"] = "Well educated",
                        ["CitizenEducationHighlyEducatedMale"] = "Highly educated",

                        ["CitizenEducationUneducatedFemale"] = "Uneducated",
                        ["CitizenEducationEducatedFemale"] = "Educated",
                        ["CitizenEducationWellEducatedFemale"] = "Well educated",
                        ["CitizenEducationHighlyEducatedFemale"] = "Highly educated",

                        // ============================================================
                        // Citizen life status
                        // ============================================================

                        ["CitizenAliveStatus"] = "Citizen is alive",
                        ["CitizenDeceasedStatus"] = "Citizen is deceased",
                        ["CitizenLeftCityStatus"] = "Citizen has left the city",

                        ["CitizenDeathDateLabel"] = "Date of death: ",
                        ["CitizenDeathTimeLabel"] = "Time of death: ",

                        // ============================================================
                        // Date and time formats
                        // Date order and clock format are independent settings.
                        // Quoted separators preserve the displayed punctuation.
                        // ============================================================

                        ["DateMonthFirstFormat"] = "MM'/'dd'/'yyyy",
                        ["DateDayFirstFormat"] = "dd'/'MM'/'yyyy",
                        ["DateYearFirstFormat"] = "yyyy'-'MM'-'dd",

                        ["Time12HourFormat"] = "hh':'mm tt",
                        ["Time24HourFormat"] = "HH':'mm",

                        ["DateFormatSettingLabel"] = "Date format",
                        ["DateFormatSettingTooltip"] = "Choose the order of the month, day, and year",

                        ["DateMonthFirstOptionText"] = "Month / Day / Year",
                        ["DateDayFirstOptionText"] = "Day / Month / Year",
                        ["DateYearFirstOptionText"] = "Year / Month / Day",

                        ["TimeFormatSettingLabel"] = "Time format",
                        ["TimeFormatSettingTooltip"] = "Choose a 12-hour or 24-hour clock",

                        ["Time12HourOptionText"] = "12-hour clock",
                        ["Time24HourOptionText"] = "24-hour clock",

                        // ============================================================
                        // Citizen happiness
                        // ============================================================

                        ["CitizenHappinessVeryUnhappyStatus"] = "Very unhappy",
                        ["CitizenHappinessUnhappyStatus"] = "Unhappy",
                        ["CitizenHappinessHappyStatus"] = "Happy",
                        ["CitizenHappinessVeryHappyStatus"] = "Very happy",
                        ["CitizenHappinessExtremelyHappyStatus"] = "Extremely happy",

                        // ============================================================
                        // Citizen home
                        // ============================================================

                        ["CitizenHomelessStatus"] = "Homeless",
                        ["CitizenHomelessTooltip"] = "Citizen has no home",

                        ["CitizenHomeOutsideCityStatus"] = "Outside the city",
                        ["CitizenHomeOutsideCityTooltip"] = "Citizen lives outside the city",

                        ["CitizenHomeCemeteryStatus"] = "Cemetery",

                        // ============================================================
                        // Citizen occupation
                        // ============================================================

                        ["CitizenRetiredStatus"] = "Retired",
                        ["CitizenRetiredTooltip"] = "Citizen is retired",

                        ["CitizenUnemployedStatus"] = "Unemployed",
                        ["CitizenUnemployedTooltip"] = "Citizen is unemployed",

                        ["CitizenGovernmentWorkplaceTooltip"] = "Government building",
                        ["CitizenElectricityWorkplaceTooltip"] = "Electricity facility",
                        ["CitizenWaterWorkplaceTooltip"] = "Water facility",
                        ["CitizenGenericWorkplaceTooltip"] = "Citizen works here",

                        ["CitizenWalkingTooltip"] = "Travelling on foot",
                        ["CitizenTouristTooltip"] = "This citizen is visiting the city",

                        // ============================================================
                        // Citizen details and family
                        // ============================================================

                        ["CitizenDetailsTitle"] = "Citizen details",
                        ["CitizenDetailsUnavailableTooltip"] = "No details window is available. Close an existing window and try again.",

                        ["CitizenStatusLabel"] = "Status: ",
                        ["CitizenHomeLabel"] = "Home: ",

                        ["CitizenFamilyTitle"] = "Family",
                        ["CitizenNoPartnerStatus"] = "No partner",
                        ["CitizenNoChildrenStatus"] = "No children",
                        ["CitizenNoHouseholdStatus"] = "No household details available",

                        ["CitizenSwitchTooltip"] = "Right-click to view this citizen",

                        // ============================================================
                        // Districts
                        // ============================================================

                        ["CitizenCurrentDistrictLabel"] = "Current district: ",
                        ["CitizenCurrentDistrictTooltip"] = "Citizen's current district",

                        ["DistrictLabel"] = "District: ",
                        ["DistrictNoneText"] = "None",

                        // ============================================================
                        // Citizen wealth and wellbeing
                        // ============================================================

                        ["CitizenWealthLowStatusMale"] = "Poor",
                        ["CitizenWealthMediumStatusMale"] = "Well-off",
                        ["CitizenWealthHighStatusMale"] = "Rich",

                        ["CitizenWealthLowStatusFemale"] = "Poor",
                        ["CitizenWealthMediumStatusFemale"] = "Well-off",
                        ["CitizenWealthHighStatusFemale"] = "Rich",

                        ["CitizenWellbeingLabel"] = "Wellbeing: ",
                        ["CitizenHappinessLabel"] = "Happiness: ",
                        ["CitizenWealthLabel"] = "Wealth",

                        // ============================================================
                        // Citizen health
                        // ============================================================

                        ["CitizenHealthVerySickStatusMale"] = "Very sick",
                        ["CitizenHealthSickStatusMale"] = "Sick",
                        ["CitizenHealthPoorStatusMale"] = "Poor health",
                        ["CitizenHealthHealthyStatusMale"] = "Healthy",
                        ["CitizenHealthVeryHealthyStatusMale"] = "Very healthy",
                        ["CitizenHealthExcellentStatusMale"] = "Excellent health",

                        ["CitizenHealthVerySickStatusFemale"] = "Very sick",
                        ["CitizenHealthSickStatusFemale"] = "Sick",
                        ["CitizenHealthPoorStatusFemale"] = "Poor health",
                        ["CitizenHealthHealthyStatusFemale"] = "Healthy",
                        ["CitizenHealthVeryHealthyStatusFemale"] = "Very healthy",
                        ["CitizenHealthExcellentStatusFemale"] = "Excellent health",

                        // ============================================================
                        // Death and funeral status
                        // ============================================================

                        ["CitizenWaitingForHearseStatus"] = "Waiting for a hearse",
                        ["CitizenInHearseStatus"] = "Being transported by hearse",
                        ["CitizenFuneralStatus"] = "Funeral service",
                        ["CitizenBuriedStatus"] = "Buried",

                        // ============================================================
                        // Vehicles and passengers
                        // ============================================================

                        ["VehicleViewPassengersText"] = "View passengers",
                        ["VehicleNoPassengersStatus"] = "No passengers",

                        ["VehiclePassengersTitle"] = "Passengers on board",
                        ["VehicleDriverTitle"] = "Driver",
                        ["VehiclePassengerTooltip"] = "Passenger",
                        ["VehiclePassengersButtonTooltip"] = "View passengers list",

                        // ============================================================
                        // Building occupant panel actions and titles
                        // ============================================================

                        ["BuildingViewResidentsText"] = "View residents",
                        ["BuildingResidentsTitle"] = "Residents",

                        ["BuildingViewOccupantsText"] = "View building occupants",
                        ["BuildingOccupantsTitle"] = "Building occupants",

                        ["BuildingHotelOccupantsTitle"] = "Workers, hotel guests, and visitors",

                        ["BuildingNoOccupantsStatus"] = "No listed occupants",
                        ["BuildingOccupantTotalLabel"] = "Total: ",

                        // ============================================================
                        // Registered resident sections
                        // Number is appended by the caller.
                        // ============================================================

                        ["BuildingHouseholdTitle"] = "Household ",
                        ["BuildingResidentsTooltip"] = "Registered residents",

                        // ============================================================
                        // Registered worker sections
                        // Presence and roster membership have separate empty states.
                        // ============================================================

                        ["BuildingWorkersTitle"] = "Workers",
                        ["BuildingWorkersPresentTitle"] = "At workplace",
                        ["BuildingWorkersAwayTitle"] = "Away from workplace",
                        ["BuildingWorkersUnknownTitle"] = "Presence unavailable",

                        ["BuildingWorkersTooltip"] = "Registered workers",
                        ["BuildingWorkersPresentTooltip"] = "Registered workers currently present at this building",
                        ["BuildingWorkersAwayTooltip"] = "Registered workers who are elsewhere or travelling here",
                        ["BuildingWorkersUnknownTooltip"] = "Registered workers whose current presence could not be determined",

                        ["BuildingNoWorkersStatus"] = "No registered workers",
                        ["BuildingNoWorkersPresentStatus"] = "No workers on-site",

                        // ============================================================
                        // Enrolled student sections
                        // ============================================================

                        ["BuildingStudentsTitle"] = "Students",
                        ["BuildingStudentsPresentTitle"] = "At school",
                        ["BuildingStudentsAwayTitle"] = "Away from school",
                        ["BuildingStudentsUnknownTitle"] = "Presence unavailable",

                        ["BuildingStudentsTooltip"] = "Enrolled students",
                        ["BuildingStudentsPresentTooltip"] = "Enrolled students currently present at this school",
                        ["BuildingStudentsAwayTooltip"] = "Enrolled students who are elsewhere or travelling here",
                        ["BuildingStudentsUnknownTooltip"] = "Enrolled students whose current presence could not be determined",

                        ["BuildingNoStudentsStatus"] = "No enrolled students",
                        ["BuildingNoStudentsPresentStatus"] = "No students on-site",

                        // ============================================================
                        // Ordinary visitor sections
                        // ============================================================

                        ["BuildingVisitorsTitle"] = "Visitors",
                        ["BuildingVisitorsTooltip"] = "Visitors currently inside or travelling to this building",

                        ["BuildingNoVisitorsStatus"] = "No visitors",
                        ["BuildingNoVisitorsPresentStatus"] = "No visitors inside",

                        // ============================================================
                        // Registered hotel guest sections
                        // Number is appended by the caller.
                        // ============================================================

                        ["BuildingHotelGuestsTitle"] = "Hotel guests",
                        ["BuildingHotelRoomTitle"] = "Hotel room ",
                        ["BuildingHotelGuestsTooltip"] = "Registered hotel guests",

                        ["BuildingNoHotelGuestsStatus"] = "No registered hotel guests",
                        ["BuildingNoHotelGuestsPresentStatus"] = "No hotel guests inside",

                        // ============================================================
                        // Building presence indicators
                        // ============================================================

                        ["BuildingPresencePresentTooltip"] = "Present at this building",
                        ["BuildingPresenceHeadingHereTooltip"] = "Travelling to this building",
                        ["BuildingPresenceAwayTooltip"] = "Away from this building",
                        ["BuildingPresenceUnknownTooltip"] = "Presence could not be determined",

                        ["BuildingResidentPresentTooltip"] = "At home",
                        ["BuildingResidentHeadingHereTooltip"] = "Travelling home",
                        ["BuildingResidentAwayTooltip"] = "Away from home",

                        ["BuildingWorkerPresentTooltip"] = "At workplace",
                        ["BuildingWorkerHeadingHereTooltip"] = "Travelling to work",
                        ["BuildingWorkerAwayTooltip"] = "Away from workplace",

                        ["BuildingStudentPresentTooltip"] = "At school",
                        ["BuildingStudentHeadingHereTooltip"] = "Travelling to school",
                        ["BuildingStudentAwayTooltip"] = "Away from school",

                        ["BuildingHotelGuestPresentTooltip"] = "At hotel",
                        ["BuildingHotelGuestHeadingHereTooltip"] = "Travelling to hotel",
                        ["BuildingHotelGuestAwayTooltip"] = "Away from hotel",

                        // ============================================================
                        // Expand/collapse actions
                        // ============================================================

                        ["BuildingSectionExpandTooltip"] = "Expand this section",
                        ["BuildingSectionCollapseTooltip"] = "Collapse this section",

                        // ============================================================
                        // Building-specific occupant sections
                        // ============================================================

                        ["BuildingSchoolStudentsTitle"] = "Students at school",
                        ["BuildingUniversityStudentsTitle"] = "Students at university",
                        ["BuildingPatientsTitle"] = "Patients",
                        ["BuildingDetaineesTitle"] = "Detainees",

                        ["BuildingNoDetaineesPresentStatus"] = "No detainees inside",
                        ["BuildingNoPatientsPresentStatus"] = "No patients inside",

                        // ============================================================
                        // Building-specific panel titles
                        // ============================================================

                        ["BuildingPoliceOccupantsTitle"] = "Police staff and detainees",
                        ["BuildingPrisonOccupantsTitle"] = "Prison staff and prisoners",

                        ["BuildingSchoolOccupantsTitle"] = "School staff and students",
                        ["BuildingUniversityOccupantsTitle"] = "University staff and students",

                        ["BuildingMedicalOccupantsTitle"] = "Medical staff and patients",
                        ["BuildingMedicalUniversityOccupantsTitle"] = "Medical staff, patients, and students",

                        ["BuildingHelicopterOccupantsTitle"] = "Pilots and workers",
                        ["BuildingFireStationOccupantsTitle"] = "Firefighters and workers",
                        ["BuildingRescueOccupantsTitle"] = "Rescue workers",
                        ["BuildingShelterOccupantsTitle"] = "Shelter occupants and workers",

                        // ============================================================
                        // Arrest and detention status
                        // ============================================================

                        ["CitizenArrestedStatus"] = "Citizen is under arrest",
                        ["CitizenTransportedToPrisonStatus"] = "Being transported to prison",
                        ["CitizenDetainedAtLabel"] = "Held at ",

                        // ============================================================
                        // Main window controls
                        // ============================================================

                        ["MainWindowResizeTooltip"] = "Drag to resize the window",
                        ["MainWindowResetSizeText"] = "Reset size",
                        ["MainWindowResetSizeTooltip"] = "Restore the default window size",

                        // ============================================================
                        // Building categories: residential
                        // ============================================================

                        ["BuildingTypeResidentialLowTitle"] = "Low-density residential",
                        ["BuildingTypeResidentialHighTitle"] = "High-density residential",

                        ["BuildingTypeResidentialLowEcoTitle"] = "Low-density eco residential",
                        ["BuildingTypeResidentialHighEcoTitle"] = "High-density eco residential",

                        ["BuildingTypeResidentialWallToWallTitle"] = "Wall-to-wall residential",

                        // ============================================================
                        // Building categories: commercial
                        // ============================================================

                        ["BuildingTypeCommercialLowTitle"] = "Low-density commercial",
                        ["BuildingTypeCommercialHighTitle"] = "High-density commercial",

                        ["BuildingTypeCommercialLeisureTitle"] = "Leisure commercial",
                        ["BuildingTypeCommercialTourismTitle"] = "Tourism commercial",
                        ["BuildingTypeCommercialEcoTitle"] = "Eco commercial",
                        ["BuildingTypeCommercialWallToWallTitle"] = "Wall-to-wall commercial",

                        // ============================================================
                        // Building category tooltips
                        // ============================================================

                        ["BuildingTypeCommercialEcoTooltip"] = "Eco commercial building",
                        ["BuildingTypeCommercialLeisureTooltip"] = "Leisure commercial building",
                        ["BuildingTypeCommercialTourismTooltip"] = "Tourism commercial building",
                        ["BuildingTypeCommercialWallToWallTooltip"] = "Wall-to-wall commercial building",

                        // ============================================================
                        // Building categories: other
                        // ============================================================

                        ["BuildingTypeIndustrialTitle"] = "Industrial",
                        ["BuildingTypeOfficeTitle"] = "Office"
                    };
					return en[index];
				}
				catch
				{
					return "language Error";
				}
			}

            string text = GameLanguage switch
            {
                "(ITALIAN)" => it[index],
                "(DUTCH)" => nl[index],
                "(RUSSIAN)" => rus[index],
                _ => en[index],
            };
            return text;
		}
	}
}
