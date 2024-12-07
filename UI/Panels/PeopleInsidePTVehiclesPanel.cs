using System;
using System.Collections.Generic;
using AlgernonCommons.UI;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class PeopleInsidePTVehiclesPanel : UIPanel
	{
        private float seconds = 0.5f;

        private bool execute = false;

        public static bool Wait = false;

        public InstanceID VehicleID;

        public UIPanel RefPanel;

        private readonly VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

        private readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        private Vehicle vehicle;

        public static Dictionary<uint, uint> CimsOnPTVeh = [];

        private readonly FastList<object> fastList = new();

        private uint VehicleUnits;

        private CitizenUnit CitizenUnit => MyCitizen.m_units.m_buffer[VehicleUnits];

        private UIPanel Title;

        private UITextureSprite TitleSpriteBg;

        private UIButton TitleVehicleName;

        private UIPanel Body;

        private UITextureSprite BodySpriteBg;

        private UIList BodyList;

        private UIPanel Footer;

        private UITextureSprite FooterSpriteBg;

        public override void Awake()
		{
			try
			{
                base.Awake();
                width = 250f;
				height = 0f;
				name = "FavCimsVehiclePanelPT";
				absolutePosition = new Vector3(0f, 0f);
				Hide();
				Title = AddUIComponent<UIPanel>();
				Title.name = "FavCimsVehiclePanelPTTitle";
				Title.width = width;
				Title.height = 41f;
				Title.relativePosition = Vector3.zero;
				TitleSpriteBg = Title.AddUIComponent<UITextureSprite>();
				TitleSpriteBg.name = "FavCimsVehiclePanelPTTitleBG";
				TitleSpriteBg.width = Title.width;
				TitleSpriteBg.height = Title.height;
				TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				TitleSpriteBg.relativePosition = Vector3.zero;
				TitleVehicleName = Title.AddUIComponent<UIButton>();
				TitleVehicleName.name = "TitleVehiclePTName";
				TitleVehicleName.width = Title.width;
				TitleVehicleName.height = Title.height;
				TitleVehicleName.textVerticalAlignment = UIVerticalAlignment.Middle;
				TitleVehicleName.textHorizontalAlignment = UIHorizontalAlignment.Center;
				TitleVehicleName.playAudioEvents = false;
				TitleVehicleName.font = UIDynamicFont.FindByName("OpenSans-Regular");
				TitleVehicleName.font.size = 15;
				TitleVehicleName.textScale = 1f;
				TitleVehicleName.wordWrap = true;
				TitleVehicleName.textPadding.left = 5;
				TitleVehicleName.textPadding.right = 5;
				TitleVehicleName.textColor = new Color32(204, 204, 51, 40);
				TitleVehicleName.hoveredTextColor = new Color32(204, 204, 51, 40);
				TitleVehicleName.pressedTextColor = new Color32(204, 204, 51, 40);
				TitleVehicleName.focusedTextColor = new Color32(204, 204, 51, 40);
				TitleVehicleName.useDropShadow = true;
				TitleVehicleName.dropShadowOffset = new Vector2(1f, -1f);
				TitleVehicleName.dropShadowColor = new Color32(0, 0, 0, 0);
				TitleVehicleName.relativePosition = Vector3.zero;
				Body = AddUIComponent<UIPanel>();
				Body.name = "VehiclePanelPTBody";
				Body.width = width;
				Body.autoLayoutDirection = LayoutDirection.Vertical;
				Body.autoLayout = true;
				Body.clipChildren = true;
				Body.height = 0f;
				Body.relativePosition = new Vector3(0f, Title.height);
				BodySpriteBg = Body.AddUIComponent<UITextureSprite>();
				BodySpriteBg.name = "VehiclePanelPTDataContainer";
				BodySpriteBg.width = Body.width;
				BodySpriteBg.height = Body.height;
				BodySpriteBg.texture = TextureDB.VehiclePanelBackground;
				BodySpriteBg.relativePosition = Vector3.zero;
                BodyList = UIList.AddUIList<MultiTypeRow>(BodySpriteBg, 12f, 0f, BodySpriteBg.width - 24f, Body.height);
                BodyList.EventSelectionChanged += (_, obj) => BodyList.SelectedIndex = -1;
                BodyList.name = "BodyList";
				Footer = AddUIComponent<UIPanel>();
				Footer.name = "VehiclePanelPTFooter";
				Footer.width = width;
				Footer.height = 12f;
				Footer.relativePosition = new Vector3(0f, Title.height + Body.height);
				FooterSpriteBg = Footer.AddUIComponent<UITextureSprite>();
				FooterSpriteBg.width = Footer.width;
				FooterSpriteBg.height = Footer.height;
				FooterSpriteBg.texture = TextureDB.VehiclePanelFooterBackground;
				FooterSpriteBg.relativePosition = Vector3.zero;
				UIComponent uicomponent = UIView.Find<UIButton>("Esc");
				if (uicomponent != null)
				{
					uicomponent.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
					{
						Hide();
					};
				}
			}
			catch (Exception ex)
			{
                Utils.Debug.Error(" Passengers Panel Start() : " + ex.ToString());
			}
		}

		public override void Update()
		{
            if (FavCimsMainClass.UnLoading)
            {
                return;
            }

            if (isVisible && !VehicleID.IsEmpty)
            {
                UpdatePanelLayout();
                seconds -= 1f * Time.deltaTime;
                if (seconds <= 0f)
                {
                    execute = true;
                    seconds = 0.5f;
                }
                else
                {
                    execute = false;
                }
                if (execute)
                {
                    if (!WorldInfoPanel.GetCurrentInstanceID().IsEmpty && WorldInfoPanel.GetCurrentInstanceID() != VehicleID)
                    {
                        VehicleID = WorldInfoPanel.GetCurrentInstanceID();
                        if (!VehicleID.IsEmpty)
                        {
                            UpdateList();
                        }
                    }
                }
            }
        }

        public void UpdateList()
		{
            CimsOnPTVeh.Clear();
            fastList.Clear();
            BodyList.Clear();

            TitleVehicleName.text = FavCimsLang.Text("Vehicle_Passengers");

            vehicle = MyVehicle.m_vehicles.m_buffer[VehicleID.Vehicle];
            int totalVehicleUnitsCount = 0;
            CountCitizenUnits(ref vehicle, ref totalVehicleUnitsCount);

            fastList.Add(new TitleRowInfo(
                           () => CimsOnPTVeh.Count == 0,
                           FavCimsLang.Text("Vehicle_Passengers"),
                           FavCimsLang.Text("View_NoPassengers"),
                           "TitleVehiclePTName",
                           UITextures.InGameAtlas));

            VehicleUnits = MyVehicle.m_vehicles.m_buffer[VehicleID.Vehicle].m_citizenUnits;

            int unitnum = 0;
            
            while (VehicleUnits != 0U && unitnum < totalVehicleUnitsCount)
            {
                uint nextUnit = MyCitizen.m_units.m_buffer[VehicleUnits].m_nextUnit;
                for (int k = 0; k < 5; k++)
                {
                    uint citizen = CitizenUnit.GetCitizen(k);
                    if (citizen != 0U && !CimsOnPTVeh.ContainsKey(citizen) && CitizenUnit.m_flags.IsFlagSet(CitizenUnit.Flags.Vehicle))
                    {
                        fastList.Add(new TitleRowInfo
                        {
                            atlas = null,
                            spriteName = "passengerIcon",
                            text = FavCimsLang.Text("Vehicle_PasssengerIconText")
                        });
                        CimsOnPTVeh.Add(citizen, VehicleUnits);
                        fastList.Add(citizen);
                    }
                }
                VehicleUnits = nextUnit;
                if (++unitnum > Singleton<CitizenManager>.instance.m_units.m_size)
                {
                    break;
                }
            }
            BodyList.Data = fastList;
            BodyList.CurrentPosition = 0;
        }

        private void UpdatePanelLayout()
        {
            absolutePosition = new Vector3(RefPanel.absolutePosition.x + RefPanel.width + 5f, RefPanel.absolutePosition.y);
            height = RefPanel.height - 15f;
            if (25f + CimsOnPTVeh.Count * 25f < height - Title.height - Footer.height)
            {
                Body.height = height - Title.height - Footer.height;
            }
            else
            {
                if (25f + CimsOnPTVeh.Count * 25f > 400f)
                {
                    Body.height = 400f;
                }
                else
                {
                    Body.height = 25f + CimsOnPTVeh.Count * 25f;
                }
            }
            BodySpriteBg.height = Body.height;
            Footer.relativePosition = new Vector3(0f, Title.height + Body.height);
            BodyList.height = Body.height;
        }

        private void CountCitizenUnits(ref Vehicle data, ref int vehicleCount)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint currentUnit = data.m_citizenUnits;
            while (currentUnit != 0)
            {
                CitizenUnit.Flags flags = instance.m_units.m_buffer[currentUnit].m_flags;
                if ((flags & CitizenUnit.Flags.Vehicle) != 0)
                {
                    vehicleCount++;
                }
                currentUnit = instance.m_units.m_buffer[currentUnit].m_nextUnit;
            }
        }
    }
}