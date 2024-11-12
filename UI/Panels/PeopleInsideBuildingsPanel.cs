using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
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

        private BuildingInfo buildingInfo;

        private readonly UIPanel[] ResidentialPanels = new UIPanel[26];

        private readonly UIPanel[] ResidentialPanelSubRow = new UIPanel[26];

        private readonly UIButton[] ResidentialPanelIcon = new UIButton[26];

        private readonly UIButton[] ResidentialPanelText = new UIButton[26];

        private readonly ResidentialBuildingPanelRow[] ResidentialBodyRow = new ResidentialBuildingPanelRow[130];

        private UIPanel WorkersPanel;

        private UIPanel WorkersPanelSubRow;

        private UIButton WorkersPanelIcon;

        private UIButton WorkersPanelText;

        private readonly WorkersBuildingPanelRow[] WorkersBodyRow = new WorkersBuildingPanelRow[100];

        private UIPanel GuestsPanel;

        private UIPanel GuestsPanelSubRow;

        private UIButton GuestsPanelIcon;

        private UIButton GuestsPanelText;

        private readonly GuestsBuildingPanelRow[] GuestsBodyRow = new GuestsBuildingPanelRow[100];

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

        public override void Start()
		{
			try
			{
				base.width = 250f;
				base.height = 0f;
				base.name = "FavCimsPeopleInsideBuildingsPanel";
				base.absolutePosition = new Vector3(0f, 0f);
				base.Hide();
				this.Title = base.AddUIComponent<UIPanel>();
				this.Title.name = "PeopleInsideBuildingsPanelTitle";
				this.Title.width = base.width;
				this.Title.height = 41f;
				this.Title.relativePosition = Vector3.zero;
				this.TitleSpriteBg = this.Title.AddUIComponent<UITextureSprite>();
				this.TitleSpriteBg.name = "PeopleInsideBuildingsPanelTitleBG";
				this.TitleSpriteBg.width = this.Title.width;
				this.TitleSpriteBg.height = this.Title.height;
				this.TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				this.TitleSpriteBg.relativePosition = Vector3.zero;
				this.TitleBuildingName = this.Title.AddUIComponent<UIButton>();
				this.TitleBuildingName.name = "PeopleInsideBuildingsPanelName";
				this.TitleBuildingName.width = this.Title.width;
				this.TitleBuildingName.height = this.Title.height;
				this.TitleBuildingName.textVerticalAlignment = UIVerticalAlignment.Middle;
				this.TitleBuildingName.textHorizontalAlignment = UIHorizontalAlignment.Center;
				this.TitleBuildingName.playAudioEvents = false;
				this.TitleBuildingName.font = UIDynamicFont.FindByName("OpenSans-Regular");
				this.TitleBuildingName.font.size = 15;
				this.TitleBuildingName.textScale = 1f;
				this.TitleBuildingName.wordWrap = true;
				this.TitleBuildingName.textPadding.left = 5;
				this.TitleBuildingName.textPadding.right = 5;
				this.TitleBuildingName.textColor = new Color32(204, 204, 51, 40);
				this.TitleBuildingName.hoveredTextColor = new Color32(204, 204, 51, 40);
				this.TitleBuildingName.pressedTextColor = new Color32(204, 204, 51, 40);
				this.TitleBuildingName.focusedTextColor = new Color32(204, 204, 51, 40);
				this.TitleBuildingName.useDropShadow = true;
				this.TitleBuildingName.dropShadowOffset = new Vector2(1f, -1f);
				this.TitleBuildingName.dropShadowColor = new Color32(0, 0, 0, 0);
				this.TitleBuildingName.relativePosition = Vector3.zero;
				this.Body = base.AddUIComponent<UIPanel>();
				this.Body.name = "PeopleInsideBuildingsBody";
				this.Body.width = base.width;
				this.Body.autoLayoutDirection = LayoutDirection.Vertical;
				this.Body.autoLayout = true;
				this.Body.clipChildren = true;
				this.Body.height = 0f;
				this.Body.relativePosition = new Vector3(0f, this.Title.height);
				this.BodySpriteBg = this.Body.AddUIComponent<UITextureSprite>();
				this.BodySpriteBg.name = "PeopleInsideBuildingsDataContainer";
				this.BodySpriteBg.width = this.Body.width;
				this.BodySpriteBg.height = this.Body.height;
				this.BodySpriteBg.texture = TextureDB.VehiclePanelBackground;
				this.BodySpriteBg.relativePosition = Vector3.zero;
				this.BodyRows = this.BodySpriteBg.AddUIComponent<UIScrollablePanel>();
				this.BodyRows.name = "BodyRows";
				this.BodyRows.width = this.BodySpriteBg.width - 24f;
				this.BodyRows.autoLayoutDirection = LayoutDirection.Vertical;
				this.BodyRows.autoLayout = true;
				this.BodyRows.relativePosition = new Vector3(12f, 0f);
				string[] array = new string[] { "Residential", "Workers", "Guests" };
				for (int i = 0; i < 3; i++)
				{
					bool flag = i == 0;
					if (flag)
					{
						int num = 0;
						for (int j = 0; j < 26; j++)
						{
							this.ResidentialPanels[j] = this.BodyRows.AddUIComponent<UIPanel>();
							this.ResidentialPanels[j].width = 226f;
							this.ResidentialPanels[j].height = 25f;
							this.ResidentialPanels[j].name = "LabelPanel_" + array[i] + "_" + j.ToString();
							this.ResidentialPanels[j].autoLayoutDirection = LayoutDirection.Vertical;
							this.ResidentialPanels[j].autoLayout = true;
							this.ResidentialPanels[j].Hide();
							this.ResidentialPanelSubRow[j] = this.ResidentialPanels[j].AddUIComponent<UIPanel>();
							this.ResidentialPanelSubRow[j].width = 226f;
							this.ResidentialPanelSubRow[j].height = 25f;
							this.ResidentialPanelSubRow[j].name = "TitlePanel_" + array[i] + "_" + j.ToString();
							this.ResidentialPanelSubRow[j].atlas = MyAtlas.FavCimsAtlas;
							this.ResidentialPanelSubRow[j].backgroundSprite = "bg_row2";
							this.ResidentialPanelIcon[j] = this.ResidentialPanelSubRow[j].AddUIComponent<UIButton>();
							this.ResidentialPanelIcon[j].name = "LabelPanelIcon_" + array[i] + "_" + j.ToString();
							this.ResidentialPanelIcon[j].width = 17f;
							this.ResidentialPanelIcon[j].height = 17f;
							this.ResidentialPanelIcon[j].atlas = MyAtlas.FavCimsAtlas;
							this.ResidentialPanelIcon[j].relativePosition = new Vector3(5f, 4f);
							this.ResidentialPanelText[j] = this.ResidentialPanelSubRow[j].AddUIComponent<UIButton>();
							this.ResidentialPanelText[j].name = "LabelPanelText_" + array[i] + "_" + j.ToString();
							this.ResidentialPanelText[j].width = 200f;
							this.ResidentialPanelText[j].height = 25f;
							this.ResidentialPanelText[j].textVerticalAlignment = UIVerticalAlignment.Middle;
							this.ResidentialPanelText[j].textHorizontalAlignment = 0;
							this.ResidentialPanelText[j].playAudioEvents = true;
							this.ResidentialPanelText[j].font = UIDynamicFont.FindByName("OpenSans-Regular");
							this.ResidentialPanelText[j].font.size = 15;
							this.ResidentialPanelText[j].textScale = 0.8f;
							this.ResidentialPanelText[j].useDropShadow = true;
							this.ResidentialPanelText[j].dropShadowOffset = new Vector2(1f, -1f);
							this.ResidentialPanelText[j].dropShadowColor = new Color32(0, 0, 0, 0);
							this.ResidentialPanelText[j].textPadding.left = 5;
							this.ResidentialPanelText[j].textPadding.right = 5;
							this.ResidentialPanelText[j].textColor = new Color32(51, 51, 51, 160);
							this.ResidentialPanelText[j].isInteractive = false;
							this.ResidentialPanelText[j].relativePosition = new Vector3(this.ResidentialPanelIcon[j].relativePosition.x + this.ResidentialPanelIcon[j].width, 1f);
							for (int k = 0; k < 5; k++)
							{
								this.ResidentialBodyRow[num] = this.BodyRows.AddUIComponent(typeof(ResidentialBuildingPanelRow)) as ResidentialBuildingPanelRow;
								this.ResidentialBodyRow[num].name = "Row_" + array[i] + "_" + k.ToString();
								this.ResidentialBodyRow[num].OnBuilding = 0;
								this.ResidentialBodyRow[num].citizen = 0U;
								this.ResidentialBodyRow[num].Hide();
								num++;
							}
						}
					}
					else
					{
						bool flag2 = i == 1;
						if (flag2)
						{
							this.WorkersPanel = this.BodyRows.AddUIComponent<UIPanel>();
							this.WorkersPanel.width = 226f;
							this.WorkersPanel.height = 25f;
							this.WorkersPanel.name = "LabelPanel_" + array[i] + "_0";
							this.WorkersPanel.autoLayoutDirection = LayoutDirection.Vertical;
							this.WorkersPanel.autoLayout = true;
							this.WorkersPanel.Hide();
							this.WorkersPanelSubRow = this.WorkersPanel.AddUIComponent<UIPanel>();
							this.WorkersPanelSubRow.width = 226f;
							this.WorkersPanelSubRow.height = 25f;
							this.WorkersPanelSubRow.name = "TitlePanel_" + array[i] + "_0";
							this.WorkersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
							this.WorkersPanelSubRow.backgroundSprite = "bg_row2";
							this.WorkersPanelIcon = this.WorkersPanelSubRow.AddUIComponent<UIButton>();
							this.WorkersPanelIcon.name = "LabelPanelIcon_" + array[i] + "_0";
							this.WorkersPanelIcon.width = 17f;
							this.WorkersPanelIcon.height = 17f;
							this.WorkersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
							this.WorkersPanelIcon.relativePosition = new Vector3(5f, 4f);
							this.WorkersPanelText = this.WorkersPanelSubRow.AddUIComponent<UIButton>();
							this.WorkersPanelText.name = "LabelPanelText_" + array[i] + "_0";
							this.WorkersPanelText.width = 200f;
							this.WorkersPanelText.height = 25f;
							this.WorkersPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
							this.WorkersPanelText.textHorizontalAlignment = 0;
							this.WorkersPanelText.playAudioEvents = true;
							this.WorkersPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
							this.WorkersPanelText.font.size = 15;
							this.WorkersPanelText.textScale = 0.8f;
							this.WorkersPanelText.useDropShadow = true;
							this.WorkersPanelText.dropShadowOffset = new Vector2(1f, -1f);
							this.WorkersPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
							this.WorkersPanelText.textPadding.left = 5;
							this.WorkersPanelText.textPadding.right = 5;
							this.WorkersPanelText.textColor = new Color32(51, 51, 51, 160);
							this.WorkersPanelText.isInteractive = false;
							this.WorkersPanelText.relativePosition = new Vector3(this.WorkersPanelIcon.relativePosition.x + this.WorkersPanelIcon.width, 1f);
							int num2 = 0;
							for (int l = 0; l < 100; l++)
							{
								this.WorkersBodyRow[num2] = this.BodyRows.AddUIComponent(typeof(WorkersBuildingPanelRow)) as WorkersBuildingPanelRow;
								this.WorkersBodyRow[num2].name = "Row_" + array[i] + "_" + l.ToString();
								this.WorkersBodyRow[num2].OnBuilding = 0;
								this.WorkersBodyRow[num2].citizen = 0U;
								this.WorkersBodyRow[num2].Hide();
								num2++;
							}
						}
						else
						{
							this.GuestsPanel = this.BodyRows.AddUIComponent<UIPanel>();
							this.GuestsPanel.width = 226f;
							this.GuestsPanel.height = 25f;
							this.GuestsPanel.name = "LabelPanel_" + array[i] + "_0";
							this.GuestsPanel.autoLayoutDirection = LayoutDirection.Vertical;
							this.GuestsPanel.autoLayout = true;
							this.GuestsPanel.Hide();
							this.GuestsPanelSubRow = this.GuestsPanel.AddUIComponent<UIPanel>();
							this.GuestsPanelSubRow.width = 226f;
							this.GuestsPanelSubRow.height = 25f;
							this.GuestsPanelSubRow.name = "TitlePanel_" + array[i] + "_0";
							this.GuestsPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
							this.GuestsPanelSubRow.backgroundSprite = "bg_row2";
							this.GuestsPanelIcon = this.GuestsPanelSubRow.AddUIComponent<UIButton>();
							this.GuestsPanelIcon.name = "LabelPanelIcon_" + array[i] + "_0";
							this.GuestsPanelIcon.width = 17f;
							this.GuestsPanelIcon.height = 17f;
							this.GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
							this.GuestsPanelIcon.relativePosition = new Vector3(5f, 4f);
							this.GuestsPanelText = this.GuestsPanelSubRow.AddUIComponent<UIButton>();
							this.GuestsPanelText.name = "LabelPanelText_" + array[i] + "_0";
							this.GuestsPanelText.width = 200f;
							this.GuestsPanelText.height = 25f;
							this.GuestsPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
							this.GuestsPanelText.textHorizontalAlignment = 0;
							this.GuestsPanelText.playAudioEvents = true;
							this.GuestsPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
							this.GuestsPanelText.font.size = 15;
							this.GuestsPanelText.textScale = 0.8f;
							this.GuestsPanelText.useDropShadow = true;
							this.GuestsPanelText.dropShadowOffset = new Vector2(1f, -1f);
							this.GuestsPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
							this.GuestsPanelText.textPadding.left = 5;
							this.GuestsPanelText.textPadding.right = 5;
							this.GuestsPanelText.textColor = new Color32(51, 51, 51, 160);
							this.GuestsPanelText.isInteractive = false;
							this.GuestsPanelText.relativePosition = new Vector3(this.GuestsPanelIcon.relativePosition.x + this.GuestsPanelIcon.width, 1f);
							int num3 = 0;
							for (int m = 0; m < 100; m++)
							{
								this.GuestsBodyRow[num3] = this.BodyRows.AddUIComponent(typeof(GuestsBuildingPanelRow)) as GuestsBuildingPanelRow;
								this.GuestsBodyRow[num3].name = "Row_" + array[i] + "_" + m.ToString();
								this.GuestsBodyRow[num3].OnBuilding = 0;
								this.GuestsBodyRow[num3].citizen = 0U;
								this.GuestsBodyRow[num3].Hide();
								num3++;
							}
						}
					}
				}
				this.BodyPanelScrollBar = this.BodySpriteBg.AddUIComponent<UIScrollablePanel>();
				this.BodyPanelScrollBar.name = "BodyPanelScrollBar";
				this.BodyPanelScrollBar.width = 10f;
				this.BodyPanelScrollBar.relativePosition = new Vector3(this.BodyRows.width + 12f, 0f);
				this.BodyScrollBar = this.BodyPanelScrollBar.AddUIComponent<UIScrollbar>();
				this.BodyScrollBar.width = 10f;
				this.BodyScrollBar.name = "BodyScrollBar";
				this.BodyScrollBar.orientation = UIOrientation.Vertical;
				this.BodyScrollBar.pivot = UIPivotPoint.TopRight;
				this.BodyScrollBar.AlignTo(this.BodyScrollBar.parent, 0);
				this.BodyScrollBar.minValue = 0f;
				this.BodyScrollBar.value = 0f;
				this.BodyScrollBar.incrementAmount = 25f;
				this.BodyPanelTrackSprite = this.BodyScrollBar.AddUIComponent<UISlicedSprite>();
				this.BodyPanelTrackSprite.autoSize = true;
				this.BodyPanelTrackSprite.name = "BodyScrollBarTrackSprite";
				this.BodyPanelTrackSprite.fillDirection = UIFillDirection.Vertical;
				this.BodyPanelTrackSprite.atlas = MyAtlas.FavCimsAtlas;
				this.BodyPanelTrackSprite.spriteName = "scrollbartrack";
				this.BodyPanelTrackSprite.relativePosition = this.BodyScrollBar.relativePosition;
				this.BodyScrollBar.trackObject = this.BodyPanelTrackSprite;
				this.thumbSprite = this.BodyScrollBar.AddUIComponent<UISlicedSprite>();
				this.thumbSprite.name = "BodyScrollBarThumbSprite";
				this.thumbSprite.autoSize = true;
				this.thumbSprite.width = this.thumbSprite.parent.width;
				this.thumbSprite.fillDirection = UIFillDirection.Vertical;
				this.thumbSprite.atlas = MyAtlas.FavCimsAtlas;
				this.thumbSprite.spriteName = "scrollbarthumb";
				this.thumbSprite.relativePosition = this.BodyScrollBar.relativePosition;
				this.BodyScrollBar.thumbObject = this.thumbSprite;
				this.BodyRows.verticalScrollbar = this.BodyScrollBar;
				this.BodyRows.eventMouseWheel += delegate(UIComponent component, UIMouseEventParameter eventParam)
				{
					int num4 = Math.Sign(eventParam.wheelDelta);
					this.BodyRows.scrollPosition += new Vector2(0f, (float)(num4 * -1) * this.BodyScrollBar.incrementAmount);
				};
				this.Footer = base.AddUIComponent<UIPanel>();
				this.Footer.name = "PeopleInsideBuildingsPanelFooter";
				this.Footer.width = base.width;
				this.Footer.height = 12f;
				this.Footer.relativePosition = new Vector3(0f, this.Title.height + this.Body.height);
				this.FooterSpriteBg = this.Footer.AddUIComponent<UITextureSprite>();
				this.FooterSpriteBg.width = this.Footer.width;
				this.FooterSpriteBg.height = this.Footer.height;
				this.FooterSpriteBg.texture = TextureDB.VehiclePanelFooterBackground;
				this.FooterSpriteBg.relativePosition = Vector3.zero;
				UIComponent uicomponent = UIView.Find<UIButton>("Esc");
				bool flag3 = uicomponent != null;
				if (flag3)
				{
					uicomponent.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
					{
						base.Hide();
					};
				}
			}
			catch (Exception ex)
			{
				Debug.Error(" Building Panel Start() : " + ex.ToString());
			}
		}

		public override void Update()
		{
			bool unLoading = FavCimsMainClass.UnLoading;
			if (!unLoading)
			{
				bool isEmpty = this.BuildingID.IsEmpty;
				if (isEmpty)
				{
					bool garbage = this.Garbage;
					if (garbage)
					{
						PeopleInsideBuildingsPanel.Wait = true;
						PeopleInsideBuildingsPanel.CimsOnBuilding.Clear();
						PeopleInsideBuildingsPanel.WorkersCount = 0;
						PeopleInsideBuildingsPanel.GuestsCount = 0;
						try
						{
							bool flag = this.buildingInfo.m_class.m_service == ItemClass.Service.Residential;
							if (flag)
							{
								int num = 0;
								for (int i = 0; i < 26; i++)
								{
									this.ResidentialPanels[i].Hide();
									for (int j = 0; j < 5; j++)
									{
										this.ResidentialBodyRow[num].Hide();
										this.ResidentialBodyRow[num].citizen = 0U;
										this.ResidentialBodyRow[num].OnBuilding = 0;
										this.ResidentialBodyRow[num].firstRun = true;
										num++;
									}
								}
							}
							else
							{
								bool flag2 = this.buildingInfo.m_class.m_service == ItemClass.Service.Commercial;
								if (flag2)
								{
									this.WorkersPanel.Hide();
									for (int k = 0; k < 100; k++)
									{
										this.WorkersBodyRow[k].Hide();
										this.WorkersBodyRow[k].citizen = 0U;
										this.WorkersBodyRow[k].OnBuilding = 0;
										this.WorkersBodyRow[k].firstRun = true;
									}
									this.GuestsPanel.Hide();
									for (int l = 0; l < 100; l++)
									{
										this.GuestsBodyRow[l].Hide();
										this.GuestsBodyRow[l].citizen = 0U;
										this.GuestsBodyRow[l].OnBuilding = 0;
										this.GuestsBodyRow[l].firstRun = true;
									}
								}
								else
								{
									this.WorkersPanel.Hide();
									for (int m = 0; m < 100; m++)
									{
										this.WorkersBodyRow[m].Hide();
										this.WorkersBodyRow[m].citizen = 0U;
										this.WorkersBodyRow[m].OnBuilding = 0;
										this.WorkersBodyRow[m].firstRun = true;
									}
								}
							}
							PeopleInsideBuildingsPanel.Wait = false;
						}
						catch
						{
						}
						this.Garbage = false;
					}
					this.firstRun = true;
				}
				else
				{
					try
					{
						this.buildingInfo = this.MyBuilding.m_buildings.m_buffer[(int)this.BuildingID.Building].Info;
						bool flag3 = !WorldInfoPanel.GetCurrentInstanceID().IsEmpty && WorldInfoPanel.GetCurrentInstanceID() != this.BuildingID;
						if (flag3)
						{
							PeopleInsideBuildingsPanel.Wait = true;
							PeopleInsideBuildingsPanel.CimsOnBuilding.Clear();
							PeopleInsideBuildingsPanel.WorkersCount = 0;
							PeopleInsideBuildingsPanel.GuestsCount = 0;
							bool flag4 = this.buildingInfo.m_class.m_service == ItemClass.Service.Residential;
							if (flag4)
							{
								int num2 = 0;
								for (int n = 0; n < 26; n++)
								{
									this.ResidentialPanels[n].Hide();
									for (int num3 = 0; num3 < 5; num3++)
									{
										this.ResidentialBodyRow[num2].Hide();
										this.ResidentialBodyRow[num2].citizen = 0U;
										this.ResidentialBodyRow[num2].OnBuilding = 0;
										this.ResidentialBodyRow[num2].firstRun = true;
										num2++;
									}
								}
							}
							else
							{
								bool flag5 = this.buildingInfo.m_class.m_service == ItemClass.Service.Commercial;
								if (flag5)
								{
									this.WorkersPanel.Hide();
									for (int num4 = 0; num4 < 100; num4++)
									{
										this.WorkersBodyRow[num4].Hide();
										this.WorkersBodyRow[num4].citizen = 0U;
										this.WorkersBodyRow[num4].OnBuilding = 0;
										this.WorkersBodyRow[num4].firstRun = true;
									}
									this.GuestsPanel.Hide();
									for (int num5 = 0; num5 < 100; num5++)
									{
										this.GuestsBodyRow[num5].Hide();
										this.GuestsBodyRow[num5].citizen = 0U;
										this.GuestsBodyRow[num5].OnBuilding = 0;
										this.GuestsBodyRow[num5].firstRun = true;
									}
								}
								else
								{
									this.WorkersPanel.Hide();
									for (int num6 = 0; num6 < 100; num6++)
									{
										this.WorkersBodyRow[num6].Hide();
										this.WorkersBodyRow[num6].citizen = 0U;
										this.WorkersBodyRow[num6].OnBuilding = 0;
										this.WorkersBodyRow[num6].firstRun = true;
									}
								}
							}
							this.BuildingID = WorldInfoPanel.GetCurrentInstanceID();
							bool isEmpty2 = this.BuildingID.IsEmpty;
							if (isEmpty2)
							{
								return;
							}
							PeopleInsideBuildingsPanel.Wait = false;
						}
						bool flag6 = base.isVisible && !this.BuildingID.IsEmpty;
						if (flag6)
						{
							this.Garbage = true;
							base.absolutePosition = new Vector3(this.RefPanel.absolutePosition.x + this.RefPanel.width + 5f, this.RefPanel.absolutePosition.y);
							base.height = this.RefPanel.height - 15f;
							bool flag7 = 50f + (float)PeopleInsideBuildingsPanel.CimsOnBuilding.Count * 25f < base.height - this.Title.height - this.Footer.height;
							if (flag7)
							{
								this.Body.height = base.height - this.Title.height - this.Footer.height;
							}
							else
							{
								bool flag8 = 50f + (float)PeopleInsideBuildingsPanel.CimsOnBuilding.Count * 25f > 400f;
								if (flag8)
								{
									this.Body.height = 400f;
								}
								else
								{
									this.Body.height = 50f + (float)PeopleInsideBuildingsPanel.CimsOnBuilding.Count * 25f;
								}
							}
							this.BodySpriteBg.height = this.Body.height;
							this.Footer.relativePosition = new Vector3(0f, this.Title.height + this.Body.height);
							this.BodyRows.height = this.Body.height;
							this.BodyPanelScrollBar.height = this.Body.height;
							this.BodyScrollBar.height = this.Body.height;
							this.BodyPanelTrackSprite.size = this.BodyPanelTrackSprite.parent.size;
							this.seconds -= 1f * Time.deltaTime;
							bool flag9 = this.seconds <= 0f || this.firstRun;
							if (flag9)
							{
								this.execute = true;
								this.seconds = 0.5f;
							}
							else
							{
								this.execute = false;
							}
							bool flag10 = this.execute;
							if (flag10)
							{
								this.firstRun = false;
								this.BuildingUnits = this.MyBuilding.m_buildings.m_buffer[(int)this.BuildingID.Building].m_citizenUnits;
								int num7 = 0;
								int num8 = 0;
								bool flag11 = this.buildingInfo.m_class.m_service == ItemClass.Service.Residential;
								int num9;
								if (flag11)
								{
									num9 = 26;
								}
								else
								{
									bool flag12 = this.buildingInfo.m_class.m_service == ItemClass.Service.Commercial;
									if (flag12)
									{
										num9 = 20;
									}
									else
									{
										num9 = 20;
									}
								}
								while (this.BuildingUnits != 0U && num7 < num9)
								{
									uint nextUnit = this.MyCitizen.m_units.m_buffer[(int)this.BuildingUnits].m_nextUnit;
									for (int num10 = 0; num10 < 5; num10++)
									{
										uint citizen = this.MyCitizen.m_units.m_buffer[(int)this.BuildingUnits].GetCitizen(num10);
										Citizen citizen2 = this.MyCitizen.m_citizens.m_buffer[(int)citizen];
										bool flag13 = citizen != 0U && !PeopleInsideBuildingsPanel.CimsOnBuilding.ContainsKey(citizen);
										if (flag13)
										{
											bool flag14 = this.buildingInfo.m_class.m_service == ItemClass.Service.Residential;
											if (flag14)
											{
												this.TitleBuildingName.text = FavCimsLang.Text("Citizens_HouseHoldsTitle");
												bool flag15 = this.ResidentialPanels[num7] != null;
												if (flag15)
												{
													this.ResidentialPanels[num7].Show();
													this.ResidentialPanelIcon[num7].normalFgSprite = "BapartmentIcon";
													this.ResidentialPanelText[num7].text = FavCimsLang.Text("OnBuilding_Residential") + " " + (num7 + 1).ToString();
													bool flag16 = this.ResidentialBodyRow[num8].citizen != 0U && PeopleInsideBuildingsPanel.CimsOnBuilding.ContainsKey(this.ResidentialBodyRow[num8].citizen);
													if (flag16)
													{
														PeopleInsideBuildingsPanel.Wait = true;
														PeopleInsideBuildingsPanel.CimsOnBuilding.Remove(this.ResidentialBodyRow[num8].citizen);
													}
													PeopleInsideBuildingsPanel.CimsOnBuilding.Add(citizen, this.BuildingUnits);
													this.ResidentialBodyRow[num8].OnBuilding = this.BuildingID.Building;
													this.ResidentialBodyRow[num8].citizen = citizen;
													this.ResidentialBodyRow[num8].LocType = 0;
													this.ResidentialBodyRow[num8].firstRun = true;
													this.ResidentialBodyRow[num8].Show();
													bool wait = PeopleInsideBuildingsPanel.Wait;
													if (wait)
													{
														PeopleInsideBuildingsPanel.Wait = false;
													}
												}
											}
											else
											{
												bool flag17 = this.buildingInfo.m_class.m_service == ItemClass.Service.Industrial || this.buildingInfo.m_class.m_service == ItemClass.Service.Office;
												if (flag17)
												{
													this.TitleBuildingName.text = FavCimsLang.Text("WorkersOnBuilding");
													this.WorkersPanel.Show();
													this.WorkersPanelIcon.normalFgSprite = "BworkingIcon";
													this.WorkersPanelText.text = FavCimsLang.Text("OnBuilding_Workers");
													bool flag18 = citizen2.GetBuildingByLocation() == this.BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving;
													if (flag18)
													{
														PeopleInsideBuildingsPanel.WorkersCount++;
														bool flag19 = this.WorkersPanel != null && this.WorkersBodyRow[num7] != null;
														if (flag19)
														{
															bool flag20 = this.WorkersBodyRow[num8].citizen != 0U && PeopleInsideBuildingsPanel.CimsOnBuilding.ContainsKey(this.WorkersBodyRow[num8].citizen);
															if (flag20)
															{
																PeopleInsideBuildingsPanel.Wait = true;
																PeopleInsideBuildingsPanel.CimsOnBuilding.Remove(this.WorkersBodyRow[num8].citizen);
															}
															PeopleInsideBuildingsPanel.CimsOnBuilding.Add(citizen, this.BuildingUnits);
															this.WorkersBodyRow[num8].OnBuilding = this.BuildingID.Building;
															this.WorkersBodyRow[num8].citizen = citizen;
															this.WorkersBodyRow[num8].LocType = Citizen.Location.Work;
															this.WorkersBodyRow[num8].firstRun = true;
															this.WorkersBodyRow[num8].Show();
															bool wait2 = PeopleInsideBuildingsPanel.Wait;
															if (wait2)
															{
																PeopleInsideBuildingsPanel.Wait = false;
															}
														}
													}
													bool flag21 = PeopleInsideBuildingsPanel.WorkersCount == 0;
													if (flag21)
													{
														this.WorkersPanelText.text = FavCimsLang.Text("OnBuilding_NoWorkers");
													}
												}
												else
												{
													this.TitleBuildingName.text = FavCimsLang.Text("CitizenOnBuildingTitle");
													bool flag22 = this.BuildingID.Building == citizen2.m_workBuilding;
													if (flag22)
													{
														this.WorkersPanel.Show();
														this.WorkersPanelIcon.normalFgSprite = "BworkingIcon";
														this.WorkersPanelText.text = FavCimsLang.Text("OnBuilding_Workers");
														bool flag23 = citizen2.GetBuildingByLocation() == this.BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving;
														if (flag23)
														{
															PeopleInsideBuildingsPanel.WorkersCount++;
															bool flag24 = this.WorkersPanel != null && this.WorkersBodyRow[num7] != null;
															if (flag24)
															{
																bool flag25 = this.WorkersBodyRow[num8].citizen != 0U && PeopleInsideBuildingsPanel.CimsOnBuilding.ContainsKey(this.WorkersBodyRow[num8].citizen);
																if (flag25)
																{
																	PeopleInsideBuildingsPanel.Wait = true;
																	PeopleInsideBuildingsPanel.CimsOnBuilding.Remove(this.WorkersBodyRow[num8].citizen);
																}
																PeopleInsideBuildingsPanel.CimsOnBuilding.Add(citizen, this.BuildingUnits);
																this.WorkersBodyRow[num8].OnBuilding = this.BuildingID.Building;
																this.WorkersBodyRow[num8].citizen = citizen;
																this.WorkersBodyRow[num8].LocType = Citizen.Location.Work;
																this.WorkersBodyRow[num8].firstRun = true;
																this.WorkersBodyRow[num8].Show();
																bool wait3 = PeopleInsideBuildingsPanel.Wait;
																if (wait3)
																{
																	PeopleInsideBuildingsPanel.Wait = false;
																}
															}
														}
														bool flag26 = PeopleInsideBuildingsPanel.WorkersCount == 0;
														if (flag26)
														{
															this.WorkersPanelText.text = FavCimsLang.Text("OnBuilding_NoWorkers");
														}
													}
													else
													{
														this.GuestsPanel.Show();
														this.GuestsPanelIcon.normalFgSprite = "BcommercialIcon";
														this.GuestsPanelText.text = FavCimsLang.Text("OnBuilding_Guests");
														bool flag27 = citizen2.GetBuildingByLocation() == this.BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving;
														if (flag27)
														{
															PeopleInsideBuildingsPanel.GuestsCount++;
															bool flag28 = this.GuestsPanel != null && this.GuestsBodyRow[num7] != null;
															if (flag28)
															{
																bool flag29 = this.GuestsBodyRow[num8].citizen != 0U && PeopleInsideBuildingsPanel.CimsOnBuilding.ContainsKey(this.GuestsBodyRow[num8].citizen);
																if (flag29)
																{
																	PeopleInsideBuildingsPanel.Wait = true;
																	PeopleInsideBuildingsPanel.CimsOnBuilding.Remove(this.GuestsBodyRow[num8].citizen);
																}
																PeopleInsideBuildingsPanel.CimsOnBuilding.Add(citizen, this.BuildingUnits);
																this.GuestsBodyRow[num8].OnBuilding = this.BuildingID.Building;
																this.GuestsBodyRow[num8].citizen = citizen;
																this.GuestsBodyRow[num8].LocType = Citizen.Location.Visit;
																this.GuestsBodyRow[num8].firstRun = true;
																this.GuestsBodyRow[num8].Show();
																bool wait4 = PeopleInsideBuildingsPanel.Wait;
																if (wait4)
																{
																	PeopleInsideBuildingsPanel.Wait = false;
																}
															}
														}
														bool flag30 = PeopleInsideBuildingsPanel.GuestsCount == 0;
														if (flag30)
														{
															this.GuestsPanelText.text = FavCimsLang.Text("OnBuilding_NoGuests");
														}
													}
												}
											}
										}
										num8++;
									}
									bool flag31 = this.BuildingUnits == 0U && this.buildingInfo.m_class.m_service == ItemClass.Service.Residential;
									if (flag31)
									{
										this.ResidentialPanels[num7].Hide();
									}
									this.BuildingUnits = nextUnit;
									bool flag32 = ++num7 > 524288;
									if (flag32)
									{
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
	}
}
