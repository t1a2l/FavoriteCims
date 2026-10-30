using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.PanelsRows
{
    public abstract class CompactCitizenPanelRow : UIPanel
    {
        private const float RefreshInterval = 0.5f;

        private float refreshTimer = RefreshInterval;

        private InstanceID citizenInstance = InstanceID.Empty;

        protected readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;
        protected readonly BuildingManager MyBuilding = Singleton<BuildingManager>.instance;
        protected readonly VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

        protected UIButton Gender;
        protected UIButton Name;
        protected UIButton Age;
        protected UIButton Star;

        protected bool Tourist;

        public bool firstRun = true;

        public uint citizen;

        private CompactCitizenRowContext rowContext;

        public void BindOwner(CompactCitizenRowContext context)
        {
            rowContext = context ?? throw new ArgumentNullException("context");
            firstRun = true;
        }

        public virtual bool Wait()
        {
            return rowContext == null || rowContext.IsUpdating;
        }

        public virtual Dictionary<uint, uint> GetCimsDict()
        {
            if (rowContext == null)
            {
                throw new InvalidOperationException("Compact citizen row has no owning panel context.");
            }

            return rowContext.Citizens;
        }

        protected abstract bool IsRowStillValid();

        protected abstract void ReleaseRow();

        protected virtual void UpdatePresenceArrow()
        {
        }

        public override void Start()
        {
            try
            {
                width = 226f;
                height = 25f;
                atlas = MyAtlas.FavCimsAtlas;
                backgroundSprite = "BgRow1";
                relativePosition = Vector3.zero;

                CreateGenderControl();
                CreateNameControl();
                CreateAgeControl();
                CreateFavoriteControl();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Error in compact citizen row creation: " + ex);
            }
        }

        private void CreateGenderControl()
        {
            Gender = AddUIComponent<UIButton>();
            Gender.name = "Gender";
            Gender.width = 17f;
            Gender.height = 17f;
            Gender.atlas = MyAtlas.FavCimsAtlas;
            Gender.relativePosition = new Vector3(5f, 4f);
        }

        private void CreateNameControl()
        {
            Name = AddUIComponent<UIButton>();
            Name.name = "Name";
            Name.width = 131f;
            Name.height = 25f;
            Name.textVerticalAlignment = UIVerticalAlignment.Middle;
            Name.textHorizontalAlignment = UIHorizontalAlignment.Left;
            Name.playAudioEvents = true;
            Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
            Name.font.size = 15;
            Name.textScale = 0.8f;
            Name.useDropShadow = true;
            Name.dropShadowOffset = new Vector2(1f, -1f);
            Name.dropShadowColor = new Color32(0, 0, 0, 0);
            Name.textPadding = new RectOffset(5, 5, 0, 0);
            Name.textColor = new Color32(204, 204, 51, 40);
            Name.hoveredTextColor = new Color32(204, 102, 0, 20);
            Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
            Name.focusedTextColor = new Color32(153, 0, 0, 0);
            Name.disabledTextColor = new Color32(51, 51, 51, 160);
            Name.relativePosition = new Vector3(Gender.relativePosition.x + Gender.width, 1f);

            Name.eventMouseUp += OnNameClicked;
        }

        private void CreateAgeControl()
        {
            Age = AddUIComponent<UIButton>();
            Age.name = "Age";
            Age.width = 23f;
            Age.height = 19f;
            Age.textHorizontalAlignment = UIHorizontalAlignment.Center;
            Age.textVerticalAlignment = UIVerticalAlignment.Middle;
            Age.font = UIDynamicFont.FindByName("OpenSans-Regular");
            Age.font.size = 15;
            Age.textScale = 0.9f;
            Age.useDropShadow = true;
            Age.dropShadowOffset = new Vector2(1f, -1f);
            Age.dropShadowColor = new Color32(0, 0, 0, 0);
            Age.isInteractive = false;
            Age.relativePosition = new Vector3(Name.relativePosition.x + Name.width + 10f, 4f);
        }

        private void CreateFavoriteControl()
        {
            Star = AddUIComponent<UIButton>();
            Star.name = "Star";
            Star.atlas = MyAtlas.FavCimsAtlas;
            Star.size = new Vector2(16f, 16f);
            Star.playAudioEvents = true;
            Star.relativePosition = new Vector3(Age.relativePosition.x + Age.width + 10f, 4f);

            Star.eventClick += OnStarClicked;
        }

        public override void Update()
        {
            if (FavCimsMainClass.UnLoading || Wait())
            {
                return;
            }

            if (citizen == 0)
            {
                Hide();
                return;
            }

            if (!isVisible)
            {
                return;
            }

            refreshTimer -= Time.deltaTime;

            if (refreshTimer > 0f && !firstRun)
            {
                return;
            }

            refreshTimer = RefreshInterval;
            firstRun = false;

            if (!GetCimsDict().ContainsKey(citizen))
            {
                ReleaseRow();
                return;
            }

            if (!IsRowStillValid())
            {
                ReleaseRow();
                return;
            }

            RefreshRow();
        }

        protected void RefreshRow()
        {
            citizenInstance.Citizen = citizen;

            var citizenInfo = MyCitizen.m_citizens.m_buffer[(int)citizen].GetCitizenInfo(citizen);

            if (citizenInfo == null || citizenInfo.m_citizenAI == null)
            {
                ReleaseRow();
                return;
            }

            string localizedStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref MyCitizen.m_citizens.m_buffer[(int)citizen], out InstanceID target);

            string targetBuildingName = MyBuilding.GetBuildingName(target.Building, citizenInstance);

            UpdateCitizenType(citizenInfo);
            UpdateName(localizedStatus, targetBuildingName);
            UpdateAge();
            UpdateFavoriteStar();
            UpdatePresenceArrow();
        }

        private void UpdateCitizenType(CitizenInfo citizenInfo)
        {
            Tourist = citizenInfo.m_class != null && citizenInfo.m_class.m_service == ItemClass.Service.Tourism;

            if (Tourist)
            {
                Gender.tooltip = Locale.Get("CITIZEN_OCCUPATION_TOURIST");
                Gender.normalBgSprite = "TouristIcon";
            }
            else
            {
                Gender.tooltip = Locale.Get("ASSETTYPE_CITIZEN");
                Gender.normalBgSprite = Citizen.GetGender(citizen) == Citizen.Gender.Female ? "Female" : "Male";
            }
        }

        private void UpdateName(string localizedStatus, string targetBuildingName)
        {
            Name.text = MyCitizen.GetCitizenName(citizen);
            if (string.IsNullOrEmpty(Name.text))
            {
                ReleaseRow();
                return;
            }

            Name.tooltip = localizedStatus + " " + targetBuildingName;
            Name.textColor = Citizen.GetGender(citizen) == Citizen.Gender.Female ? new Color32(255, 102, 204, 213) : new Color32(204, 204, 51, 40);
        }

        private void UpdateAge()
        {
            int realAge = FavCimsCore.CalculateCitizenAge((int)MyCitizen.m_citizens.m_buffer[(int)citizen].m_age);
            Age.text = realAge.ToString();
            Age.textColor = GetAgeColor(realAge);
        }

        private void UpdateFavoriteStar()
        {
            if (FavCimsCore.IsFavorite(citizen))
            {
                Star.normalBgSprite = "IconFavSubscribed";
                Star.tooltip = FavCimsLang.Text("FavStarButtonDisableTooltip");
            }
            else
            {
                Star.normalBgSprite = "IconFavUnsubscribed";
                Star.tooltip = FavCimsLang.Text("FavStarButtonEnableTooltip");
            }
        }

        private void OnNameClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            FavCimsCore.GoToCitizen(position, citizenInstance, Tourist, eventParam);
        }

        private void OnStarClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            FavCimsCore.ToggleCitizenFavorite(citizenInstance);
        }

        private static Color32 GetAgeColor(int realAge)
        {
            if (realAge <= 12)
            {
                return new Color32(83, 166, 0, 60);
            }

            if (realAge <= 19)
            {
                return new Color32(0, 102, 51, 100);
            }

            if (realAge <= 25)
            {
                return new Color32(255, 204, 0, 32);
            }

            if (realAge <= 65)
            {
                return new Color32(255, 102, 0, 16);
            }

            if (realAge <= 90)
            {
                return new Color32(153, 0, 0, 0);
            }

            return new Color32(255, 0, 0, 0);
        }
    }
}