using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class PeopleInsideBuildingsPanel : UIPanel
	{
        private float seconds = 0.5f;

        private bool execute = false;

        private bool firstRun = true;

        public static bool Wait = false;

        private bool Garbage = false;

        public InstanceID BuildingID;

        public UIPanel RefPanel;

        private readonly BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        private readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        public static Dictionary<uint, uint> CimsOnBuilding = [];

        public static int WorkersCount = 0;

        public static int GuestsCount = 0;

        public static int HotelGuestsCount = 0;

        private Building building;

        private BuildingInfo buildingInfo;

        const int MaxHouseHolds = 40;
        const int MaxWorkersUnit = 20; // **Important** *same of MaxGuestsUnit*
        const int MaxGuestsUnit = 20;
        const int MaxHotelRooms = 40;

        private readonly UIPanel[] ResidentialPanels = new UIPanel[MaxHouseHolds];

        private readonly UIPanel[] ResidentialPanelSubRow = new UIPanel[MaxHouseHolds];

        private readonly UIButton[] ResidentialPanelIcon = new UIButton[MaxHouseHolds];

        private readonly UIButton[] ResidentialPanelText = new UIButton[MaxHouseHolds];

        private readonly ResidentialBuildingPanelRow[] ResidentialBodyRow = new ResidentialBuildingPanelRow[MaxHouseHolds * 5];

        private UIPanel WorkersPanel;

        private UIPanel WorkersPanelSubRow;

        private UIButton WorkersPanelIcon;

        private UIButton WorkersPanelText;

        private readonly WorkersBuildingPanelRow[] WorkersBodyRow = new WorkersBuildingPanelRow[MaxWorkersUnit * 5];

        private UIPanel GuestsPanel;

        private UIPanel GuestsPanelSubRow;

        private UIButton GuestsPanelIcon;

        private UIButton GuestsPanelText;

        private readonly GuestsBuildingPanelRow[] GuestsBodyRow = new GuestsBuildingPanelRow[MaxGuestsUnit * 5];

        private readonly UIPanel[] HotelRoomsPanels = new UIPanel[MaxHotelRooms];

        private readonly UIPanel[] HotelRoomsPanelSubRow = new UIPanel[MaxHotelRooms];

        private readonly UIButton[] HotelRoomsPanelIcon = new UIButton[MaxHotelRooms];

        private readonly UIButton[] HotelRoomsPanelText = new UIButton[MaxHotelRooms];

        private readonly HotelBuildingPanelRow[] HotelRoomsBodyRow = new HotelBuildingPanelRow[MaxHotelRooms * 5];

        private uint BuildingUnits;

        private UIPanel Title;

        private UITextureSprite TitleSpriteBg;

        private UIButton TitleBuildingName;

        private UIPanel Body;

        private UITextureSprite BodySpriteBg;

        private UIScrollablePanel BodyRows;

        private UIPanel Footer;

        private UITextureSprite FooterSpriteBg;

        private UIScrollablePanel BodyPanelScrollBar;

        private UIScrollbar BodyScrollBar;

        private UISlicedSprite BodyPanelTrackSprite;

        private UISlicedSprite thumbSprite;

        public override void Awake()
        {
			try
			{
                base.Awake();
                width = 250f;
				height = 0f;
				name = "FavCimsPeopleInsideBuildingsPanel";
				absolutePosition = new Vector3(0f, 0f);
				Hide();
				Title = AddUIComponent<UIPanel>();
				Title.name = "PeopleInsideBuildingsPanelTitle";
				Title.width = width;
				Title.height = 41f;
				Title.relativePosition = Vector3.zero;
				TitleSpriteBg = Title.AddUIComponent<UITextureSprite>();
				TitleSpriteBg.name = "PeopleInsideBuildingsPanelTitleBG";
				TitleSpriteBg.width = Title.width;
				TitleSpriteBg.height = Title.height;
				TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				TitleSpriteBg.relativePosition = Vector3.zero;
				TitleBuildingName = Title.AddUIComponent<UIButton>();
				TitleBuildingName.name = "PeopleInsideBuildingsPanelName";
				TitleBuildingName.width = Title.width;
				TitleBuildingName.height = Title.height;
				TitleBuildingName.textVerticalAlignment = UIVerticalAlignment.Middle;
				TitleBuildingName.textHorizontalAlignment = UIHorizontalAlignment.Center;
				TitleBuildingName.playAudioEvents = false;
				TitleBuildingName.font = UIDynamicFont.FindByName("OpenSans-Regular");
				TitleBuildingName.font.size = 15;
				TitleBuildingName.textScale = 1f;
				TitleBuildingName.wordWrap = true;
				TitleBuildingName.textPadding.left = 5;
				TitleBuildingName.textPadding.right = 5;
				TitleBuildingName.textColor = new Color32(204, 204, 51, 40);
				TitleBuildingName.hoveredTextColor = new Color32(204, 204, 51, 40);
				TitleBuildingName.pressedTextColor = new Color32(204, 204, 51, 40);
				TitleBuildingName.focusedTextColor = new Color32(204, 204, 51, 40);
				TitleBuildingName.useDropShadow = true;
				TitleBuildingName.dropShadowOffset = new Vector2(1f, -1f);
				TitleBuildingName.dropShadowColor = new Color32(0, 0, 0, 0);
				TitleBuildingName.relativePosition = Vector3.zero;
				Body = AddUIComponent<UIPanel>();
				Body.name = "PeopleInsideBuildingsBody";
				Body.width = width;
				Body.autoLayoutDirection = LayoutDirection.Vertical;
				Body.autoLayout = true;
				Body.clipChildren = true;
				Body.height = 0f;
				Body.relativePosition = new Vector3(0f, Title.height);
				BodySpriteBg = Body.AddUIComponent<UITextureSprite>();
				BodySpriteBg.name = "PeopleInsideBuildingsDataContainer";
				BodySpriteBg.width = Body.width;
				BodySpriteBg.height = Body.height;
				BodySpriteBg.texture = TextureDB.VehiclePanelBackground;
				BodySpriteBg.relativePosition = Vector3.zero;
				BodyRows = BodySpriteBg.AddUIComponent<UIScrollablePanel>();
				BodyRows.name = "BodyRows";
				BodyRows.width = BodySpriteBg.width - 24f;
				BodyRows.autoLayoutDirection = LayoutDirection.Vertical;
				BodyRows.autoLayout = true;
				BodyRows.relativePosition = new Vector3(12f, 0f);
                string[] array = ["Residential", "Workers", "Guests", "HotelGuests"];
				for (int i = 0; i < 4; i++)
				{
					if (i == 0)
					{
						int row = 0;
						for (int j = 0; j < MaxHouseHolds; j++)
						{
							ResidentialPanels[j] = BodyRows.AddUIComponent<UIPanel>();
							ResidentialPanels[j].width = 226f;
							ResidentialPanels[j].height = 25f;
							ResidentialPanels[j].name = "LabelPanel_" + array[i] + "_" + j.ToString();
							ResidentialPanels[j].autoLayoutDirection = LayoutDirection.Vertical;
							ResidentialPanels[j].autoLayout = true;
							ResidentialPanels[j].Hide();
							ResidentialPanelSubRow[j] = ResidentialPanels[j].AddUIComponent<UIPanel>();
							ResidentialPanelSubRow[j].width = 226f;
							ResidentialPanelSubRow[j].height = 25f;
							ResidentialPanelSubRow[j].name = "TitlePanel_" + array[i] + "_" + j.ToString();
							ResidentialPanelSubRow[j].atlas = MyAtlas.FavCimsAtlas;
							ResidentialPanelSubRow[j].backgroundSprite = "bg_row2";
							ResidentialPanelIcon[j] = ResidentialPanelSubRow[j].AddUIComponent<UIButton>();
							ResidentialPanelIcon[j].name = "LabelPanelIcon_" + array[i] + "_" + j.ToString();
							ResidentialPanelIcon[j].width = 17f;
							ResidentialPanelIcon[j].height = 17f;
							ResidentialPanelIcon[j].atlas = MyAtlas.FavCimsAtlas;
							ResidentialPanelIcon[j].relativePosition = new Vector3(5f, 4f);
							ResidentialPanelText[j] = ResidentialPanelSubRow[j].AddUIComponent<UIButton>();
							ResidentialPanelText[j].name = "LabelPanelText_" + array[i] + "_" + j.ToString();
							ResidentialPanelText[j].width = 200f;
							ResidentialPanelText[j].height = 25f;
							ResidentialPanelText[j].textVerticalAlignment = UIVerticalAlignment.Middle;
							ResidentialPanelText[j].textHorizontalAlignment = 0;
							ResidentialPanelText[j].playAudioEvents = true;
							ResidentialPanelText[j].font = UIDynamicFont.FindByName("OpenSans-Regular");
							ResidentialPanelText[j].font.size = 15;
							ResidentialPanelText[j].textScale = 0.8f;
							ResidentialPanelText[j].useDropShadow = true;
							ResidentialPanelText[j].dropShadowOffset = new Vector2(1f, -1f);
							ResidentialPanelText[j].dropShadowColor = new Color32(0, 0, 0, 0);
							ResidentialPanelText[j].textPadding.left = 5;
							ResidentialPanelText[j].textPadding.right = 5;
							ResidentialPanelText[j].textColor = new Color32(51, 51, 51, 160);
							ResidentialPanelText[j].isInteractive = false;
							ResidentialPanelText[j].relativePosition = new Vector3(ResidentialPanelIcon[j].relativePosition.x + ResidentialPanelIcon[j].width, 1f);
							for (int k = 0; k < 5; k++)
							{
								ResidentialBodyRow[row] = BodyRows.AddUIComponent(typeof(ResidentialBuildingPanelRow)) as ResidentialBuildingPanelRow;
								ResidentialBodyRow[row].name = "Row_" + array[i] + "_" + k.ToString();
								ResidentialBodyRow[row].OnBuilding = 0;
								ResidentialBodyRow[row].citizen = 0U;
								ResidentialBodyRow[row].Hide();
                                row++;
							}
						}
					}
					else
					{
						if (i == 1)
						{
                            WorkersPanel = BodyRows.AddUIComponent<UIPanel>();
							WorkersPanel.width = 226f;
							WorkersPanel.height = 25f;
							WorkersPanel.name = "LabelPanel_" + array[i] + "_0";
							WorkersPanel.autoLayoutDirection = LayoutDirection.Vertical;
							WorkersPanel.autoLayout = true;
							WorkersPanel.Hide();
							WorkersPanelSubRow = WorkersPanel.AddUIComponent<UIPanel>();
							WorkersPanelSubRow.width = 226f;
							WorkersPanelSubRow.height = 25f;
							WorkersPanelSubRow.name = "TitlePanel_" + array[i] + "_0";
							WorkersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
							WorkersPanelSubRow.backgroundSprite = "bg_row2";
							WorkersPanelIcon = WorkersPanelSubRow.AddUIComponent<UIButton>();
							WorkersPanelIcon.name = "LabelPanelIcon_" + array[i] + "_0";
							WorkersPanelIcon.width = 17f;
							WorkersPanelIcon.height = 17f;
							WorkersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
							WorkersPanelIcon.relativePosition = new Vector3(5f, 4f);
							WorkersPanelText = WorkersPanelSubRow.AddUIComponent<UIButton>();
							WorkersPanelText.name = "LabelPanelText_" + array[i] + "_0";
							WorkersPanelText.width = 200f;
							WorkersPanelText.height = 25f;
							WorkersPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
							WorkersPanelText.textHorizontalAlignment = 0;
							WorkersPanelText.playAudioEvents = true;
							WorkersPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
							WorkersPanelText.font.size = 15;
							WorkersPanelText.textScale = 0.8f;
							WorkersPanelText.useDropShadow = true;
							WorkersPanelText.dropShadowOffset = new Vector2(1f, -1f);
							WorkersPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
							WorkersPanelText.textPadding.left = 5;
							WorkersPanelText.textPadding.right = 5;
							WorkersPanelText.textColor = new Color32(51, 51, 51, 160);
							WorkersPanelText.isInteractive = false;
							WorkersPanelText.relativePosition = new Vector3(WorkersPanelIcon.relativePosition.x + WorkersPanelIcon.width, 1f);
							int row = 0;
							for (int l = 0; l < MaxWorkersUnit * 5; l++)
							{
								WorkersBodyRow[row] = BodyRows.AddUIComponent(typeof(WorkersBuildingPanelRow)) as WorkersBuildingPanelRow;
								WorkersBodyRow[row].name = "Row_" + array[i] + "_" + l.ToString();
								WorkersBodyRow[row].OnBuilding = 0;
								WorkersBodyRow[row].citizen = 0U;
								WorkersBodyRow[row].Hide();
                                row++;
							}
						}
						else
						{
                            if (i == 2)
							{
                                GuestsPanel = BodyRows.AddUIComponent<UIPanel>();
                                GuestsPanel.width = 226f;
                                GuestsPanel.height = 25f;
                                GuestsPanel.name = "LabelPanel_" + array[i] + "_0";
                                GuestsPanel.autoLayoutDirection = LayoutDirection.Vertical;
                                GuestsPanel.autoLayout = true;
                                GuestsPanel.Hide();
                                GuestsPanelSubRow = GuestsPanel.AddUIComponent<UIPanel>();
                                GuestsPanelSubRow.width = 226f;
                                GuestsPanelSubRow.height = 25f;
                                GuestsPanelSubRow.name = "TitlePanel_" + array[i] + "_0";
                                GuestsPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
                                GuestsPanelSubRow.backgroundSprite = "bg_row2";
                                GuestsPanelIcon = GuestsPanelSubRow.AddUIComponent<UIButton>();
                                GuestsPanelIcon.name = "LabelPanelIcon_" + array[i] + "_0";
                                GuestsPanelIcon.width = 17f;
                                GuestsPanelIcon.height = 17f;
                                GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
                                GuestsPanelIcon.relativePosition = new Vector3(5f, 4f);
                                GuestsPanelText = GuestsPanelSubRow.AddUIComponent<UIButton>();
                                GuestsPanelText.name = "LabelPanelText_" + array[i] + "_0";
                                GuestsPanelText.width = 200f;
                                GuestsPanelText.height = 25f;
                                GuestsPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
                                GuestsPanelText.textHorizontalAlignment = 0;
                                GuestsPanelText.playAudioEvents = true;
                                GuestsPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
                                GuestsPanelText.font.size = 15;
                                GuestsPanelText.textScale = 0.8f;
                                GuestsPanelText.useDropShadow = true;
                                GuestsPanelText.dropShadowOffset = new Vector2(1f, -1f);
                                GuestsPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
                                GuestsPanelText.textPadding.left = 5;
                                GuestsPanelText.textPadding.right = 5;
                                GuestsPanelText.textColor = new Color32(51, 51, 51, 160);
                                GuestsPanelText.isInteractive = false;
                                GuestsPanelText.relativePosition = new Vector3(GuestsPanelIcon.relativePosition.x + GuestsPanelIcon.width, 1f);
                                int row = 0;
                                for (int m = 0; m < MaxGuestsUnit * 5; m++)
                                {
                                    GuestsBodyRow[row] = BodyRows.AddUIComponent(typeof(GuestsBuildingPanelRow)) as GuestsBuildingPanelRow;
                                    GuestsBodyRow[row].name = "Row_" + array[i] + "_" + m.ToString();
                                    GuestsBodyRow[row].OnBuilding = 0;
                                    GuestsBodyRow[row].citizen = 0U;
                                    GuestsBodyRow[row].Hide();
                                    row++;
                                }
                            }
                            else
							{
                                for (int s = 0; s < MaxHotelRooms; s++)
                                {
                                    HotelRoomsPanels[s] = BodyRows.AddUIComponent<UIPanel>();
                                    HotelRoomsPanels[s].width = 226f;
                                    HotelRoomsPanels[s].height = 25f;
                                    HotelRoomsPanels[s].name = "LabelPanel_" + array[i] + "_" + s.ToString();
                                    HotelRoomsPanels[s].autoLayoutDirection = LayoutDirection.Vertical;
                                    HotelRoomsPanels[s].autoLayout = true;
                                    HotelRoomsPanels[s].Hide();
                                    HotelRoomsPanelSubRow[s] = HotelRoomsPanels[s].AddUIComponent<UIPanel>();
                                    HotelRoomsPanelSubRow[s].width = 226f;
                                    HotelRoomsPanelSubRow[s].height = 25f;
                                    HotelRoomsPanelSubRow[s].name = "TitlePanel_" + array[i] + "_" + s.ToString();
                                    HotelRoomsPanelSubRow[s].atlas = MyAtlas.FavCimsAtlas;
                                    HotelRoomsPanelSubRow[s].backgroundSprite = "bg_row2";
                                    HotelRoomsPanelIcon[s] = HotelRoomsPanelSubRow[s].AddUIComponent<UIButton>();
                                    HotelRoomsPanelIcon[s].name = "LabelPanelIcon_" + array[i] + "_" + s.ToString();
                                    HotelRoomsPanelIcon[s].width = 17f;
                                    HotelRoomsPanelIcon[s].height = 17f;
                                    HotelRoomsPanelIcon[s].atlas = MyAtlas.FavCimsAtlas;
                                    HotelRoomsPanelIcon[s].relativePosition = new Vector3(5f, 4f);
                                    HotelRoomsPanelText[s] = HotelRoomsPanelSubRow[s].AddUIComponent<UIButton>();
                                    HotelRoomsPanelText[s].name = "LabelPanelText_" + array[i] + "_" + s.ToString();
                                    HotelRoomsPanelText[s].width = 200f;
                                    HotelRoomsPanelText[s].height = 25f;
                                    HotelRoomsPanelText[s].textVerticalAlignment = UIVerticalAlignment.Middle;
                                    HotelRoomsPanelText[s].textHorizontalAlignment = 0;
                                    HotelRoomsPanelText[s].playAudioEvents = true;
                                    HotelRoomsPanelText[s].font = UIDynamicFont.FindByName("OpenSans-Regular");
                                    HotelRoomsPanelText[s].font.size = 15;
                                    HotelRoomsPanelText[s].textScale = 0.8f;
                                    HotelRoomsPanelText[s].useDropShadow = true;
                                    HotelRoomsPanelText[s].dropShadowOffset = new Vector2(1f, -1f);
                                    HotelRoomsPanelText[s].dropShadowColor = new Color32(0, 0, 0, 0);
                                    HotelRoomsPanelText[s].textPadding.left = 5;
                                    HotelRoomsPanelText[s].textPadding.right = 5;
                                    HotelRoomsPanelText[s].textColor = new Color32(51, 51, 51, 160);
                                    HotelRoomsPanelText[s].isInteractive = false;
                                    HotelRoomsPanelText[s].relativePosition = new Vector3(HotelRoomsPanelIcon[s].relativePosition.x + HotelRoomsPanelIcon[s].width, 1f);
                                    HotelRoomsBodyRow[s] = BodyRows.AddUIComponent(typeof(HotelBuildingPanelRow)) as HotelBuildingPanelRow;
                                    HotelRoomsBodyRow[s].name = "Row_" + array[i] + "_" + s.ToString();
                                    HotelRoomsBodyRow[s].OnBuilding = 0;
                                    HotelRoomsBodyRow[s].citizen = 0U;
                                    HotelRoomsBodyRow[s].Hide();
                                }
                            }
						}
					}
				}

				BodyPanelScrollBar = BodySpriteBg.AddUIComponent<UIScrollablePanel>();
				BodyPanelScrollBar.name = "BodyPanelScrollBar";
				BodyPanelScrollBar.width = 10f;
				BodyPanelScrollBar.relativePosition = new Vector3(BodyRows.width + 12f, 0f);
				BodyScrollBar = BodyPanelScrollBar.AddUIComponent<UIScrollbar>();
				BodyScrollBar.width = 10f;
				BodyScrollBar.name = "BodyScrollBar";
				BodyScrollBar.orientation = UIOrientation.Vertical;
				BodyScrollBar.pivot = UIPivotPoint.TopRight;
				BodyScrollBar.AlignTo(BodyScrollBar.parent, 0);
				BodyScrollBar.minValue = 0f;
				BodyScrollBar.value = 0f;
				BodyScrollBar.incrementAmount = 25f;
				BodyPanelTrackSprite = BodyScrollBar.AddUIComponent<UISlicedSprite>();
				BodyPanelTrackSprite.autoSize = true;
				BodyPanelTrackSprite.name = "BodyScrollBarTrackSprite";
				BodyPanelTrackSprite.fillDirection = UIFillDirection.Vertical;
				BodyPanelTrackSprite.atlas = MyAtlas.FavCimsAtlas;
				BodyPanelTrackSprite.spriteName = "scrollbartrack";
				BodyPanelTrackSprite.relativePosition = BodyScrollBar.relativePosition;
				BodyScrollBar.trackObject = BodyPanelTrackSprite;
				thumbSprite = BodyScrollBar.AddUIComponent<UISlicedSprite>();
				thumbSprite.name = "BodyScrollBarThumbSprite";
				thumbSprite.autoSize = true;
				thumbSprite.width = thumbSprite.parent.width;
				thumbSprite.fillDirection = UIFillDirection.Vertical;
				thumbSprite.atlas = MyAtlas.FavCimsAtlas;
				thumbSprite.spriteName = "scrollbarthumb";
				thumbSprite.relativePosition = BodyScrollBar.relativePosition;
				BodyScrollBar.thumbObject = thumbSprite;
				BodyRows.verticalScrollbar = BodyScrollBar;
				BodyRows.eventMouseWheel += delegate(UIComponent component, UIMouseEventParameter eventParam)
				{
					int sign = Math.Sign(eventParam.wheelDelta);
					BodyRows.scrollPosition += new Vector2(0f, (sign * -1) * BodyScrollBar.incrementAmount);
				};
				Footer = AddUIComponent<UIPanel>();
				Footer.name = "PeopleInsideBuildingsPanelFooter";
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
                Utils.Debug.Error(" Building Panel Start() : " + ex.ToString());
			}
		}

		public override void Update()
		{
			bool unLoading = FavCimsMainClass.UnLoading;
			if (!unLoading)
			{
                building = MyBuilding.m_buildings.m_buffer[BuildingID.Building];
                buildingInfo = building.Info;
                int homeCount = 0;
                int workCount = 0;
                int visitCount = 0;
                int studentCount = 0;
                int hotelCount = 0;
                CountCitizenUnits(ref building, ref homeCount, ref workCount, ref visitCount, ref studentCount, ref hotelCount);
                int totalUnitsCount = homeCount + workCount + visitCount + studentCount + hotelCount;

                bool isCimCareBuilding = FavCimsCore.IsCimCareBuilding(BuildingID.Building);
                bool isAreaResidentalBuilding = FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building);
                bool isHotel = FavCimsCore.IsHotel(BuildingID.Building);
                bool isInternationalTradeOfficeBuilding = FavCimsCore.IsInternationalTradeOfficeBuilding(BuildingID.Building);

                bool isEmpty = BuildingID.IsEmpty;
				if (isEmpty)
				{
					bool garbage = Garbage;
					if (garbage)
					{
						Wait = true;
						CimsOnBuilding.Clear();
						WorkersCount = 0;
						GuestsCount = 0;
						HotelGuestsCount = 0;
                        try
						{
                            int row = 0;
                            for (int i = 0; i < MaxHouseHolds; i++)
                            {
                                ResidentialPanels[i].Hide();
                                for (int j = 0; j < 5; j++)
                                {
                                    ResidentialBodyRow[row].Hide();
                                    ResidentialBodyRow[row].citizen = 0U;
                                    ResidentialBodyRow[row].OnBuilding = 0;
                                    ResidentialBodyRow[row].firstRun = true;
                                    row++;
                                }
                            }

                            for (int i = 0; i < MaxHotelRooms; i++)
                            {
                                HotelRoomsPanels[i].Hide();
                                HotelRoomsBodyRow[i].Hide();
                                HotelRoomsBodyRow[i].citizen = 0U;
                                HotelRoomsBodyRow[i].OnBuilding = 0;
                                HotelRoomsBodyRow[i].firstRun = true;
                            }

                            WorkersPanel.Hide();
                            for (int k = 0; k < MaxWorkersUnit*5; k++)
                            {
                                WorkersBodyRow[k].Hide();
                                WorkersBodyRow[k].citizen = 0U;
                                WorkersBodyRow[k].OnBuilding = 0;
                                WorkersBodyRow[k].firstRun = true;
                            }
                            GuestsPanel.Hide();
                            for (int l = 0; l < MaxGuestsUnit * 5; l++)
                            {
                                GuestsBodyRow[l].Hide();
                                GuestsBodyRow[l].citizen = 0U;
                                GuestsBodyRow[l].OnBuilding = 0;
                                GuestsBodyRow[l].firstRun = true;
                            }

							Wait = false;
						}
						catch
						{
						}
						Garbage = false;
					}
					firstRun = true;
				}
				else
				{
					try
					{
						if (!WorldInfoPanel.GetCurrentInstanceID().IsEmpty && WorldInfoPanel.GetCurrentInstanceID() != BuildingID)
						{
							Wait = true;
							CimsOnBuilding.Clear();
							WorkersCount = 0;
							GuestsCount = 0;
							HotelGuestsCount = 0;
                            int row = 0;
                            for (int i = 0; i < MaxHouseHolds; i++)
                            {
                                ResidentialPanels[i].Hide();
                                for (int j = 0; j < 5; j++)
                                {
                                    ResidentialBodyRow[row].Hide();
                                    ResidentialBodyRow[row].citizen = 0U;
                                    ResidentialBodyRow[row].OnBuilding = 0;
                                    ResidentialBodyRow[row].firstRun = true;
                                    row++;
                                }
                            }

                            for (int i = 0; i < MaxHotelRooms; i++)
                            {
                                HotelRoomsPanels[i].Hide();
                                HotelRoomsBodyRow[i].Hide();
                                HotelRoomsBodyRow[i].citizen = 0U;
                                HotelRoomsBodyRow[i].OnBuilding = 0;
                                HotelRoomsBodyRow[i].firstRun = true;
                            }

                            WorkersPanel.Hide();
                            for (int k = 0; k < MaxWorkersUnit * 5; k++)
                            {
                                WorkersBodyRow[k].Hide();
                                WorkersBodyRow[k].citizen = 0U;
                                WorkersBodyRow[k].OnBuilding = 0;
                                WorkersBodyRow[k].firstRun = true;
                            }
                            GuestsPanel.Hide();
                            for (int l = 0; l < MaxGuestsUnit * 5; l++)
                            {
                                GuestsBodyRow[l].Hide();
                                GuestsBodyRow[l].citizen = 0U;
                                GuestsBodyRow[l].OnBuilding = 0;
                                GuestsBodyRow[l].firstRun = true;
                            }
                            BuildingID = WorldInfoPanel.GetCurrentInstanceID();
							bool isEmpty2 = BuildingID.IsEmpty;
							if (isEmpty2)
							{
								return;
							}
							Wait = false;
						}
						if (isVisible && !BuildingID.IsEmpty)
						{
							Garbage = true;
							absolutePosition = new Vector3(RefPanel.absolutePosition.x + RefPanel.width + 5f, RefPanel.absolutePosition.y);
							height = RefPanel.height - 15f;
							if (50f + CimsOnBuilding.Count * 25f < height - Title.height - Footer.height)
							{
								Body.height = height - Title.height - Footer.height;
							}
							else
							{
								if (50f + CimsOnBuilding.Count * 25f > 400f)
								{
									Body.height = 400f;
								}
								else
								{
									Body.height = 50f + CimsOnBuilding.Count * 25f;
								}
							}
							BodySpriteBg.height = Body.height;
							Footer.relativePosition = new Vector3(0f, Title.height + Body.height);
							BodyRows.height = Body.height;
							BodyPanelScrollBar.height = Body.height;
							BodyScrollBar.height = Body.height;
							BodyPanelTrackSprite.size = BodyPanelTrackSprite.parent.size;
							seconds -= 1f * Time.deltaTime;
							if (seconds <= 0f || firstRun)
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
								firstRun = false;
								BuildingUnits = MyBuilding.m_buildings.m_buffer[BuildingID.Building].m_citizenUnits;
                                int unitnum = 0;
								int rownum = 0;
                                int total_workers = 0;
                                int hotel_guest_unit = 0;
                                bool hotelrow = false;
                                while (BuildingUnits != 0U && unitnum < totalUnitsCount)
								{
                                    uint nextUnit = MyCitizen.m_units.m_buffer[(int)BuildingUnits].m_nextUnit;
                                    if (MyCitizen.m_units.m_buffer[BuildingUnits].m_flags != MyCitizen.m_units.m_buffer[nextUnit].m_flags &&
                                        (MyCitizen.m_units.m_buffer[BuildingUnits].m_flags & CitizenUnit.Flags.Hotel) == 0 &&
                                        (MyCitizen.m_units.m_buffer[nextUnit].m_flags & CitizenUnit.Flags.Hotel) != 0)
                                    {
                                        hotelrow = true;
                                    }
                                    for (int i = 0; i < 5; i++)
									{
										uint citizen = MyCitizen.m_units.m_buffer[(int)BuildingUnits].GetCitizen(i);
										Citizen citizen2 = MyCitizen.m_citizens.m_buffer[(int)citizen];

                                        if (citizen != 0U && !CimsOnBuilding.ContainsKey(citizen))
										{
                                            int forcedToGuest = 0;
                                            CitizenInfo citizenInfo = MyCitizen.m_citizens.m_buffer[(int)citizen].GetCitizenInfo(citizen);
                                            string localizedStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref MyCitizen.m_citizens.m_buffer[(int)citizen], out InstanceID instanceID);
                                            if (citizen2.m_workBuilding == BuildingID.Building && (buildingInfo.m_class.m_service == ItemClass.Service.Education || 
												buildingInfo.m_class.m_service == ItemClass.Service.PlayerEducation)  && (citizen2.m_flags & Citizen.Flags.Student) != 0)
                                            {
                                                forcedToGuest = 1;
                                            }
                                            if (BuildingID.Building == citizen2.m_workBuilding && forcedToGuest == 0)
                                            {
                                                total_workers++;
                                            }
                                            if ((buildingInfo.m_class.m_service == ItemClass.Service.Residential || isAreaResidentalBuilding || isCimCareBuilding)
												&& (MyCitizen.m_units.m_buffer[BuildingUnits].m_flags & CitizenUnit.Flags.Home) != 0)
											{
												TitleBuildingName.text = FavCimsLang.Text("Citizens_HouseHoldsTitle");
												if (ResidentialPanels[unitnum] != null)
												{
													ResidentialPanels[unitnum].Show();
													ResidentialPanelIcon[unitnum].normalFgSprite = "BapartmentIcon";
													ResidentialPanelText[unitnum].text = FavCimsLang.Text("OnBuilding_Residential") + " " + (unitnum + 1).ToString();
													if (ResidentialBodyRow[rownum].citizen != 0U && CimsOnBuilding.ContainsKey(ResidentialBodyRow[rownum].citizen))
													{
														Wait = true;
														CimsOnBuilding.Remove(ResidentialBodyRow[rownum].citizen);
													}
													CimsOnBuilding.Add(citizen, BuildingUnits);
													ResidentialBodyRow[rownum].OnBuilding = BuildingID.Building;
													ResidentialBodyRow[rownum].citizen = citizen;
													ResidentialBodyRow[rownum].LocType = Citizen.Location.Home;
													ResidentialBodyRow[rownum].firstRun = true;
													ResidentialBodyRow[rownum].Show();
													bool wait = Wait;
													if (wait)
													{
														Wait = false;
													}
												}
											}
											else
											{
												if ((buildingInfo.m_class.m_service == ItemClass.Service.Industrial || buildingInfo.m_class.m_service == ItemClass.Service.Office ||
                                                    isInternationalTradeOfficeBuilding || isCimCareBuilding) && (MyCitizen.m_units.m_buffer[BuildingUnits].m_flags & CitizenUnit.Flags.Work) != 0)
												{
                                                    TitleBuildingName.text = FavCimsLang.Text("WorkersOnBuilding");
                                                    WorkersPanel.Show();
                                                    WorkersPanelIcon.normalFgSprite = "BworkingIcon";
                                                    WorkersPanelText.text = FavCimsLang.Text("OnBuilding_Workers");
                                                    if (citizen2.GetBuildingByLocation() == BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving)
                                                    {
                                                        WorkersCount++;

                                                        if (WorkersPanel != null && WorkersBodyRow[unitnum] != null)
                                                        {
                                                            if (WorkersBodyRow[rownum].citizen != 0U && CimsOnBuilding.ContainsKey(WorkersBodyRow[rownum].citizen))
                                                            {
                                                                Wait = true;
                                                                CimsOnBuilding.Remove(WorkersBodyRow[rownum].citizen);
                                                            }
                                                            CimsOnBuilding.Add(citizen, BuildingUnits);
                                                            WorkersBodyRow[rownum].OnBuilding = BuildingID.Building;
                                                            WorkersBodyRow[rownum].citizen = citizen;
                                                            WorkersBodyRow[rownum].LocType = Citizen.Location.Work;
                                                            WorkersBodyRow[rownum].firstRun = true;
                                                            WorkersBodyRow[rownum].Show();
                                                            bool wait2 = Wait;
                                                            if (wait2)
                                                            {
                                                                Wait = false;
                                                            }
                                                        }
                                                    }
                                                    if (WorkersCount == 0)
                                                    {
                                                        WorkersPanelText.text = FavCimsLang.Text("OnBuilding_NoWorkers");
                                                    }
                                                }
                                                else if (isHotel)
                                                {
                                                    TitleBuildingName.text = FavCimsLang.Text("CitizenOnHotelBuildingTitle");
                                                    if ((MyCitizen.m_units.m_buffer[BuildingUnits].m_flags & CitizenUnit.Flags.Work) != 0)
                                                    {
                                                        WorkersPanel.Show();
                                                        WorkersPanelIcon.normalFgSprite = "BworkingIcon";
                                                        WorkersPanelText.text = FavCimsLang.Text("OnBuilding_Workers");
                                                        if (citizen2.GetBuildingByLocation() == BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving)
                                                        {
                                                            WorkersCount++;

                                                            if (WorkersPanel != null && WorkersBodyRow[unitnum] != null)
                                                            {
                                                                if (WorkersBodyRow[rownum].citizen != 0U && CimsOnBuilding.ContainsKey(WorkersBodyRow[rownum].citizen))
                                                                {
                                                                    Wait = true;
                                                                    CimsOnBuilding.Remove(WorkersBodyRow[rownum].citizen);
                                                                }
                                                                CimsOnBuilding.Add(citizen, BuildingUnits);
                                                                WorkersBodyRow[rownum].OnBuilding = BuildingID.Building;
                                                                WorkersBodyRow[rownum].citizen = citizen;
                                                                WorkersBodyRow[rownum].LocType = Citizen.Location.Work;
                                                                WorkersBodyRow[rownum].firstRun = true;
                                                                WorkersBodyRow[rownum].Show();
                                                                bool wait2 = Wait;
                                                                if (wait2)
                                                                {
                                                                    Wait = false;
                                                                }
                                                            }
                                                        }
                                                        if (WorkersCount == 0)
                                                        {
                                                            WorkersPanelText.text = FavCimsLang.Text("OnBuilding_NoWorkers");
                                                        }
                                                    }
                                                    else if((MyCitizen.m_units.m_buffer[BuildingUnits].m_flags & CitizenUnit.Flags.Hotel) != 0)
                                                    {
                                                        if (HotelRoomsPanels[hotel_guest_unit] != null)
                                                        {
                                                            HotelRoomsPanels[hotel_guest_unit].Show();
                                                            HotelRoomsPanelIcon[hotel_guest_unit].normalFgSprite = "BapartmentIcon";
                                                            HotelRoomsPanelText[hotel_guest_unit].text = FavCimsLang.Text("OnBuilding_HotelRooms") + " " + (hotel_guest_unit + 1);  
                                                            if (HotelRoomsBodyRow[hotel_guest_unit].citizen != 0 && CimsOnBuilding.ContainsKey(HotelRoomsBodyRow[hotel_guest_unit].citizen))
                                                            {
                                                                Utils.Debug.Log("here");
                                                                Wait = true;
                                                                CimsOnBuilding.Remove(HotelRoomsBodyRow[rownum].citizen);
                                                            }

                                                            CimsOnBuilding.Add(citizen, BuildingUnits);
                                                            HotelRoomsBodyRow[hotel_guest_unit].OnBuilding = BuildingID.Building;
                                                            HotelRoomsBodyRow[hotel_guest_unit].citizen = citizen;
                                                            HotelRoomsBodyRow[hotel_guest_unit].LocType = Citizen.Location.Hotel;
                                                            HotelRoomsBodyRow[hotel_guest_unit].firstRun = true;
                                                            HotelRoomsBodyRow[hotel_guest_unit].Show();
                                                            Utils.Debug.Log("citizenId: " + citizen + " hotel_guest_unit: " + hotel_guest_unit);
                                                            bool wait3 = Wait;
                                                            if (wait3)
                                                            {
                                                                Wait = false;
                                                            }
                                                            hotel_guest_unit++;
                                                            //if (HotelGuestsCount == 0)
                                                            //{
                                                            //    WorkersPanelText.text = FavCimsLang.Text("OnBuilding_noHotelGuests");
                                                            //}
                                                        }
                                                    }
                                                    else if ((MyCitizen.m_units.m_buffer[BuildingUnits].m_flags & CitizenUnit.Flags.Visit) != 0)
                                                    {
                                                        if (citizen2.GetBuildingByLocation() == BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving)
                                                        {
                                                            GuestsCount++;
                                                            if (GuestsPanel != null && GuestsBodyRow[unitnum] != null)
                                                            {
                                                                if (GuestsBodyRow[rownum].citizen != 0U && CimsOnBuilding.ContainsKey(GuestsBodyRow[rownum].citizen))
                                                                {
                                                                    Wait = true;
                                                                    CimsOnBuilding.Remove(GuestsBodyRow[rownum].citizen);
                                                                }
                                                                CimsOnBuilding.Add(citizen, BuildingUnits);
                                                                GuestsBodyRow[rownum].OnBuilding = BuildingID.Building;
                                                                GuestsBodyRow[rownum].citizen = citizen;
                                                                GuestsBodyRow[rownum].LocType = Citizen.Location.Visit;
                                                                GuestsBodyRow[rownum].firstRun = true;
                                                                GuestsBodyRow[rownum].Show();
                                                                bool wait4 = Wait;
                                                                if (wait4)
                                                                {
                                                                    Wait = false;
                                                                }
                                                            }
                                                        }
                                                        if (GuestsCount == 0)
                                                        {
                                                            WorkersPanelText.text = FavCimsLang.Text("OnBuilding_NoGuests");
                                                        }
                                                    }
                                                }
                                                else
												{
                                                    switch (buildingInfo.m_class.m_service)
                                                    {
                                                        case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_level == ItemClass.Level.Level3:
                                                        case ItemClass.Service.HealthCare when buildingInfo.m_class.m_level == ItemClass.Level.Level3:
                                                        case ItemClass.Service.FireDepartment when buildingInfo.m_class.m_level == ItemClass.Level.Level3:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnHelicopter_Building_Service");
                                                            break;
                                                        case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_level == ItemClass.Level.Level4:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnPrison_Building_Service");
                                                            break;
                                                        case ItemClass.Service.PoliceDepartment:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnPolice_Building_Service");
                                                            break;
                                                        case ItemClass.Service.FireDepartment:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnFire_Building_Service");
                                                            break;
                                                        case ItemClass.Service.Disaster when buildingInfo.GetAI() is ShelterAI:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnRescue_Building_Service");
                                                            break;
                                                        case ItemClass.Service.Disaster:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnShelter_Building_Service");
                                                            break;
                                                        case ItemClass.Service.Education:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnEducation_Building_Service");
                                                            break;
                                                        case ItemClass.Service.PlayerEducation when !FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building):
                                                            TitleBuildingName.text = FavCimsLang.Text("OnHighEducation_Building_Service");
                                                            break;
                                                        case ItemClass.Service.HealthCare:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnMedical_Building_Service");
                                                            break;
                                                        case ItemClass.Service.Beautification:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnBuilding_Guests");
                                                            break;
                                                        case ItemClass.Service.Monument:
                                                            TitleBuildingName.text = FavCimsLang.Text("CitizenOnBuildingTitle");
                                                            break;
                                                        default:
                                                            TitleBuildingName.text = FavCimsLang.Text("OnBuilding_Workers");
                                                            break;
                                                    }
                                                    if (BuildingID.Building == citizen2.m_workBuilding && forcedToGuest == 0)
													{
														WorkersPanel.Show();
														WorkersPanelIcon.normalFgSprite = "BworkingIcon";
														WorkersPanelText.text = FavCimsLang.Text("OnBuilding_Workers");
                                                        if (citizen2.GetBuildingByLocation() == BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving)
														{
															WorkersCount++;
															if (WorkersPanel != null && WorkersBodyRow[unitnum] != null)
															{
																if (WorkersBodyRow[rownum].citizen != 0U && CimsOnBuilding.ContainsKey(WorkersBodyRow[rownum].citizen))
																{
																	Wait = true;
																	CimsOnBuilding.Remove(WorkersBodyRow[rownum].citizen);
																}
																CimsOnBuilding.Add(citizen, BuildingUnits);
																WorkersBodyRow[rownum].OnBuilding = BuildingID.Building;
																WorkersBodyRow[rownum].citizen = citizen;
																WorkersBodyRow[rownum].LocType = Citizen.Location.Work;
																WorkersBodyRow[rownum].firstRun = true;
																WorkersBodyRow[rownum].Show();
																bool wait3 = Wait;
																if (wait3)
																{
																	Wait = false;
																}
															}
														}
														if (WorkersCount == 0)
														{
                                                            WorkersPanelText.text = string.Concat(
															[
																FavCimsLang.Text("OnBuilding_NoWorkers"),
																" (",
																FavCimsLang.Text("OnBuilding_TotalWorkers"),
																" ",
																total_workers.ToString(),
																")"
															]);
                                                        }
													}
													else
													{
														GuestsPanel.Show();

                                                        switch (buildingInfo.m_class.m_service)
                                                        {
                                                            case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_subService != ItemClass.SubService.PoliceDepartmentBank:
                                                                GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
                                                                GuestsPanelIcon.normalFgSprite = "FavCimsCrimeArrested";
                                                                GuestsPanelText.text = FavCimsLang.Text("Citizen_Under_Arrest");
                                                                break;
                                                            case ItemClass.Service.Education:
                                                                GuestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
                                                                GuestsPanelIcon.normalFgSprite = "IconPolicySchoolsOut";
                                                                GuestsPanelText.text = FavCimsLang.Text("Citizen_at_School");
                                                                break;
                                                            case ItemClass.Service.PlayerEducation when !FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building):
                                                                GuestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
                                                                GuestsPanelIcon.normalFgSprite = "IconPolicySchoolsOut";
                                                                GuestsPanelText.text = FavCimsLang.Text("Citizen_at_University");
                                                                break;
                                                            case ItemClass.Service.HealthCare when !FavCimsCore.IsCimCareBuilding(BuildingID.Building):
                                                                GuestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
                                                                GuestsPanelIcon.normalFgSprite = "SubBarHealthcareDefault";
                                                                GuestsPanelText.text = FavCimsLang.Text("Citizen_at_Clinic");
                                                                break;
                                                            default:
                                                                GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
                                                                GuestsPanelIcon.normalFgSprite = "BcommercialIcon";
                                                                GuestsPanelText.text = FavCimsLang.Text("OnBuilding_Guests");
                                                                break;
                                                        }
                                                        if (citizen2.GetBuildingByLocation() == BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving)
														{
															GuestsCount++;
															if (GuestsPanel != null && GuestsBodyRow[unitnum] != null)
															{
																if (GuestsBodyRow[rownum].citizen != 0U && CimsOnBuilding.ContainsKey(GuestsBodyRow[rownum].citizen))
																{
																	Wait = true;
																	CimsOnBuilding.Remove(GuestsBodyRow[rownum].citizen);
																}
																CimsOnBuilding.Add(citizen, BuildingUnits);
																GuestsBodyRow[rownum].OnBuilding = BuildingID.Building;
																GuestsBodyRow[rownum].citizen = citizen;
																GuestsBodyRow[rownum].LocType = Citizen.Location.Visit;
																GuestsBodyRow[rownum].firstRun = true;
																GuestsBodyRow[rownum].Show();
																bool wait4 = Wait;
																if (wait4)
																{
																	Wait = false;
																}
															}
														}
														if (GuestsCount == 0)
														{
                                                            switch (buildingInfo.m_class.m_service)
                                                            {
                                                                case ItemClass.Service.PoliceDepartment when buildingInfo.m_class.m_subService != ItemClass.SubService.PoliceDepartmentBank:
                                                                    GuestsPanelText.text = FavCimsLang.Text("OnBuilding_noArrested");
                                                                    break;
                                                                case ItemClass.Service.Education:
                                                                case ItemClass.Service.PlayerEducation when !FavCimsCore.IsAreaResidentalBuilding(BuildingID.Building):
                                                                    GuestsPanelText.text = FavCimsLang.Text("OnBuilding_noStudents");
                                                                    break;
                                                                case ItemClass.Service.HealthCare when !FavCimsCore.IsCimCareBuilding(BuildingID.Building):
                                                                    GuestsPanelText.text = FavCimsLang.Text("OnBuilding_noPatients");
                                                                    break;
                                                                default:
                                                                    GuestsPanelText.text = FavCimsLang.Text("OnBuilding_NoGuests");
                                                                    break;
                                                            }
                                                        }
													}
												}
											}
										}
                                        rownum++;  
                                    }
                                    if (hotelrow)
                                    {
                                        rownum = 0;
                                        hotelrow = false;
                                    }
                                    if (BuildingUnits == 0U && buildingInfo.m_class.m_service == ItemClass.Service.Residential)
									{
										ResidentialPanels[unitnum].Hide();
									}
                                    if (BuildingUnits == 0U && isHotel)
                                    {
                                        HotelRoomsPanels[unitnum].Hide();
                                    }
                                    BuildingUnits = nextUnit;
                                    if (++unitnum > Singleton<CitizenManager>.instance.m_units.m_size)
                                    {
                                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + System.Environment.StackTrace);
                                        break;
                                    }
                                }
							}
						}
					}
					catch
					{
					}
				}
			}
		}

        private void CountCitizenUnits(ref Building data, ref int homeCount, ref int workCount, ref int visitCount, ref int studentCount, ref int hotelCount)
        {
            if ((data.m_flags & (Building.Flags.Abandoned | Building.Flags.Collapsed)) != 0)
            {
                return;
            }
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint currentUnit = data.m_citizenUnits;
            while (currentUnit != 0)
            {
                CitizenUnit.Flags flags = instance.m_units.m_buffer[currentUnit].m_flags;
                if ((flags & CitizenUnit.Flags.Home) != 0)
                {
                    homeCount++;
                }
                if ((flags & CitizenUnit.Flags.Work) != 0)
                {
                    workCount++;
                }
                if ((flags & CitizenUnit.Flags.Visit) != 0)
                {
                    visitCount++;
                }
                if ((flags & CitizenUnit.Flags.Student) != 0)
                {
                    studentCount++;
                }
                if ((flags & CitizenUnit.Flags.Hotel) != 0)
                {
                    hotelCount++;
                }
                currentUnit = instance.m_units.m_buffer[currentUnit].m_nextUnit;
            }
        }
    }
}
