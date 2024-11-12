using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class PeopleInsideServiceBuildingsPanel : UIPanel
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

        private UIPanel WorkersPanel;

        private UIPanel WorkersPanelSubRow;

        private UIButton WorkersPanelIcon;

        private UIButton WorkersPanelText;

        private readonly WorkersServiceBuildingPanelRow[] WorkersBodyRow = new WorkersServiceBuildingPanelRow[200];

        private UIPanel GuestsPanel;

        private UIPanel GuestsPanelSubRow;

        private UIButton GuestsPanelIcon;

        private UIButton GuestsPanelText;

        private readonly GuestsServiceBuildingPanelRow[] GuestsBodyRow = new GuestsServiceBuildingPanelRow[200];

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
				base.name = "FavCimsPeopleInsideServiceBuildingsPanel";
				base.absolutePosition = new Vector3(0f, 0f);
				base.Hide();
				this.Title = base.AddUIComponent<UIPanel>();
				this.Title.name = "PeopleInsideServiceBuildingsPanelTitle";
				this.Title.width = base.width;
				this.Title.height = 41f;
				this.Title.relativePosition = Vector3.zero;
				this.TitleSpriteBg = this.Title.AddUIComponent<UITextureSprite>();
				this.TitleSpriteBg.name = "PeopleInsideServiceBuildingsPanelTitleBG";
				this.TitleSpriteBg.width = this.Title.width;
				this.TitleSpriteBg.height = this.Title.height;
				this.TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				this.TitleSpriteBg.relativePosition = Vector3.zero;
				this.TitleBuildingName = this.Title.AddUIComponent<UIButton>();
				this.TitleBuildingName.name = "PeopleInsideServiceBuildingsPanelName";
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
				this.Body.name = "PeopleInsideServiceBuildingsBody";
				this.Body.width = base.width;
				this.Body.autoLayoutDirection = LayoutDirection.Vertical;
				this.Body.autoLayout = true;
				this.Body.clipChildren = true;
				this.Body.height = 0f;
				this.Body.relativePosition = new Vector3(0f, this.Title.height);
				this.BodySpriteBg = this.Body.AddUIComponent<UITextureSprite>();
				this.BodySpriteBg.name = "PeopleInsideServiceBuildingsDataContainer";
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
				string[] array = new string[] { "Workers", "Guests" };
				for (int i = 0; i < 2; i++)
				{
					bool flag = i == 0;
					if (flag)
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
						int num = 0;
						for (int j = 0; j < 200; j++)
						{
							this.WorkersBodyRow[num] = this.BodyRows.AddUIComponent(typeof(WorkersServiceBuildingPanelRow)) as WorkersServiceBuildingPanelRow;
							this.WorkersBodyRow[num].name = "Row_" + array[i] + "_" + j.ToString();
							this.WorkersBodyRow[num].OnBuilding = 0;
							this.WorkersBodyRow[num].citizen = 0U;
							this.WorkersBodyRow[num].Hide();
							num++;
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
						int num2 = 0;
						for (int k = 0; k < 200; k++)
						{
							this.GuestsBodyRow[num2] = this.BodyRows.AddUIComponent(typeof(GuestsServiceBuildingPanelRow)) as GuestsServiceBuildingPanelRow;
							this.GuestsBodyRow[num2].name = "Row_" + array[i] + "_" + k.ToString();
							this.GuestsBodyRow[num2].OnBuilding = 0;
							this.GuestsBodyRow[num2].citizen = 0U;
							this.GuestsBodyRow[num2].Hide();
							num2++;
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
					int num3 = Math.Sign(eventParam.wheelDelta);
					this.BodyRows.scrollPosition += new Vector2(0f, (float)(num3 * -1) * this.BodyScrollBar.incrementAmount);
				};
				this.Footer = base.AddUIComponent<UIPanel>();
				this.Footer.name = "PeopleInsideServiceBuildingsFooter";
				this.Footer.width = base.width;
				this.Footer.height = 12f;
				this.Footer.relativePosition = new Vector3(0f, this.Title.height + this.Body.height);
				this.FooterSpriteBg = this.Footer.AddUIComponent<UITextureSprite>();
				this.FooterSpriteBg.width = this.Footer.width;
				this.FooterSpriteBg.height = this.Footer.height;
				this.FooterSpriteBg.texture = TextureDB.VehiclePanelFooterBackground;
				this.FooterSpriteBg.relativePosition = Vector3.zero;
				UIComponent uicomponent = UIView.Find<UIButton>("Esc");
				bool flag2 = uicomponent != null;
				if (flag2)
				{
					uicomponent.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
					{
						base.Hide();
					};
				}
			}
			catch (Exception ex)
			{
                Utils.Debug.Error(" Service Building Panel Start() : " + ex.ToString());
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
						PeopleInsideServiceBuildingsPanel.Wait = true;
						PeopleInsideServiceBuildingsPanel.CimsOnBuilding.Clear();
						PeopleInsideServiceBuildingsPanel.WorkersCount = 0;
						PeopleInsideServiceBuildingsPanel.GuestsCount = 0;
						try
						{
							this.WorkersPanel.Hide();
							for (int i = 0; i < 200; i++)
							{
								this.WorkersBodyRow[i].Hide();
								this.WorkersBodyRow[i].citizen = 0U;
								this.WorkersBodyRow[i].OnBuilding = 0;
								this.WorkersBodyRow[i].firstRun = true;
							}
							this.GuestsPanel.Hide();
							for (int j = 0; j < 200; j++)
							{
								this.GuestsBodyRow[j].Hide();
								this.GuestsBodyRow[j].citizen = 0U;
								this.GuestsBodyRow[j].OnBuilding = 0;
								this.GuestsBodyRow[j].firstRun = true;
							}
							PeopleInsideServiceBuildingsPanel.Wait = false;
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
						bool flag = !WorldInfoPanel.GetCurrentInstanceID().IsEmpty && WorldInfoPanel.GetCurrentInstanceID() != this.BuildingID;
						if (flag)
						{
							PeopleInsideServiceBuildingsPanel.Wait = true;
							PeopleInsideServiceBuildingsPanel.CimsOnBuilding.Clear();
							PeopleInsideServiceBuildingsPanel.WorkersCount = 0;
							PeopleInsideServiceBuildingsPanel.GuestsCount = 0;
							this.WorkersPanel.Hide();
							for (int k = 0; k < 200; k++)
							{
								this.WorkersBodyRow[k].Hide();
								this.WorkersBodyRow[k].citizen = 0U;
								this.WorkersBodyRow[k].OnBuilding = 0;
								this.WorkersBodyRow[k].firstRun = true;
							}
							this.GuestsPanel.Hide();
							for (int l = 0; l < 200; l++)
							{
								this.GuestsBodyRow[l].Hide();
								this.GuestsBodyRow[l].citizen = 0U;
								this.GuestsBodyRow[l].OnBuilding = 0;
								this.GuestsBodyRow[l].firstRun = true;
							}
							this.BuildingID = WorldInfoPanel.GetCurrentInstanceID();
							bool isEmpty2 = this.BuildingID.IsEmpty;
							if (isEmpty2)
							{
								return;
							}
							PeopleInsideServiceBuildingsPanel.Wait = false;
						}
						bool flag2 = base.isVisible && !this.BuildingID.IsEmpty;
						if (flag2)
						{
							this.Garbage = true;
							base.absolutePosition = new Vector3(this.RefPanel.absolutePosition.x + this.RefPanel.width + 5f, this.RefPanel.absolutePosition.y);
							base.height = this.RefPanel.height - 15f;
							bool flag3 = 25f + (float)PeopleInsideServiceBuildingsPanel.CimsOnBuilding.Count * 25f < base.height - this.Title.height - this.Footer.height;
							if (flag3)
							{
								this.Body.height = base.height - this.Title.height - this.Footer.height;
							}
							else
							{
								bool flag4 = 25f + (float)PeopleInsideServiceBuildingsPanel.CimsOnBuilding.Count * 25f > 400f;
								if (flag4)
								{
									this.Body.height = 400f;
								}
								else
								{
									this.Body.height = 25f + (float)PeopleInsideServiceBuildingsPanel.CimsOnBuilding.Count * 25f;
								}
							}
							this.BodySpriteBg.height = this.Body.height;
							this.Footer.relativePosition = new Vector3(0f, this.Title.height + this.Body.height);
							this.BodyRows.height = this.Body.height;
							this.BodyPanelScrollBar.height = this.Body.height;
							this.BodyScrollBar.height = this.Body.height;
							this.BodyPanelTrackSprite.size = this.BodyPanelTrackSprite.parent.size;
							this.seconds -= 1f * Time.deltaTime;
							bool flag5 = this.seconds <= 0f || this.firstRun;
							if (flag5)
							{
								this.execute = true;
								this.seconds = 0.5f;
							}
							else
							{
								this.execute = false;
							}
							bool flag6 = this.execute;
							if (flag6)
							{
								this.firstRun = false;
								this.BuildingUnits = this.MyBuilding.m_buildings.m_buffer[(int)this.BuildingID.Building].m_citizenUnits;
								int num = 0;
								int num2 = 0;
								int num3 = 0;
								while (this.BuildingUnits != 0U && num < 40)
								{
									uint nextUnit = this.MyCitizen.m_units.m_buffer[(int)this.BuildingUnits].m_nextUnit;
									for (int m = 0; m < 5; m++)
									{
										uint citizen = this.MyCitizen.m_units.m_buffer[(int)this.BuildingUnits].GetCitizen(m);
										Citizen citizen2 = this.MyCitizen.m_citizens.m_buffer[(int)citizen];
										bool flag7 = citizen > 0U;
										if (flag7)
										{
											int num4 = 0;
											CitizenInfo citizenInfo = this.MyCitizen.m_citizens.m_buffer[(int)citizen].GetCitizenInfo(citizen);
											InstanceID instanceID;
											string localizedStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref this.MyCitizen.m_citizens.m_buffer[(int)citizen], out instanceID);
											bool flag8 = citizen2.m_workBuilding == this.BuildingID.Building && this.buildingInfo.m_class.m_service == ItemClass.Service.Education;
											if (flag8)
											{
												bool flag9 = Locale.Get("CITIZEN_STATUS_AT_SCHOOL") == localizedStatus;
												if (flag9)
												{
													num4 = 1;
												}
											}
											bool flag10 = this.BuildingID.Building == citizen2.m_workBuilding && num4 == 0;
											if (flag10)
											{
												num3++;
											}
											bool flag11 = !PeopleInsideServiceBuildingsPanel.CimsOnBuilding.ContainsKey(citizen);
											if (flag11)
											{
												bool flag12 = this.buildingInfo.m_class.m_service == ItemClass.Service.PoliceDepartment;
												if (flag12)
												{
													this.TitleBuildingName.text = FavCimsLang.Text("OnPolice_Building_Service");
												}
												else
												{
													bool flag13 = this.buildingInfo.m_class.m_service == ItemClass.Service.Education;
													if (flag13)
													{
														this.TitleBuildingName.text = FavCimsLang.Text("OnEducation_Building_Service");
													}
													else
													{
														bool flag14 = this.buildingInfo.m_class.m_service == ItemClass.Service.HealthCare;
														if (flag14)
														{
															this.TitleBuildingName.text = FavCimsLang.Text("OnMedical_Building_Service");
														}
														else
														{
															bool flag15 = this.buildingInfo.m_class.m_service == ItemClass.Service.Beautification;
															if (flag15)
															{
																this.TitleBuildingName.text = FavCimsLang.Text("OnBuilding_Guests");
															}
															else
															{
																bool flag16 = this.buildingInfo.m_class.m_service == ItemClass.Service.Monument;
																if (flag16)
																{
																	this.TitleBuildingName.text = FavCimsLang.Text("CitizenOnBuildingTitle");
																}
																else
																{
																	this.TitleBuildingName.text = FavCimsLang.Text("OnBuilding_Workers");
																}
															}
														}
													}
												}
												bool flag17 = this.BuildingID.Building == citizen2.m_workBuilding && num4 == 0;
												if (flag17)
												{
													this.WorkersPanel.Show();
													this.WorkersPanelIcon.normalFgSprite = "BworkingIcon";
													this.WorkersPanelText.text = string.Concat(new string[]
													{
														FavCimsLang.Text("OnBuilding_Workers"),
														" (",
														FavCimsLang.Text("OnBuilding_TotalWorkers"),
														" ",
														num3.ToString(),
														")"
													});
													bool flag18 = citizen2.GetBuildingByLocation() == this.BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving;
													if (flag18)
													{
														PeopleInsideServiceBuildingsPanel.WorkersCount++;
														bool flag19 = this.WorkersPanel != null && this.WorkersBodyRow[num] != null;
														if (flag19)
														{
															bool flag20 = this.WorkersBodyRow[num2].citizen != 0U && PeopleInsideServiceBuildingsPanel.CimsOnBuilding.ContainsKey(this.WorkersBodyRow[num2].citizen);
															if (flag20)
															{
																PeopleInsideServiceBuildingsPanel.Wait = true;
																PeopleInsideServiceBuildingsPanel.CimsOnBuilding.Remove(this.WorkersBodyRow[num2].citizen);
															}
															PeopleInsideServiceBuildingsPanel.CimsOnBuilding.Add(citizen, this.BuildingUnits);
															this.WorkersBodyRow[num2].OnBuilding = this.BuildingID.Building;
															this.WorkersBodyRow[num2].citizen = citizen;
															this.WorkersBodyRow[num2].LocType = Citizen.Location.Work;
															this.WorkersBodyRow[num2].firstRun = true;
															this.WorkersBodyRow[num2].Show();
															bool wait = PeopleInsideServiceBuildingsPanel.Wait;
															if (wait)
															{
																PeopleInsideServiceBuildingsPanel.Wait = false;
															}
														}
													}
													bool flag21 = PeopleInsideServiceBuildingsPanel.WorkersCount == 0;
													if (flag21)
													{
														this.WorkersPanelText.text = string.Concat(new string[]
														{
															FavCimsLang.Text("OnBuilding_NoWorkers"),
															" (",
															FavCimsLang.Text("OnBuilding_TotalWorkers"),
															" ",
															num3.ToString(),
															")"
														});
													}
												}
												else
												{
													this.GuestsPanel.Show();
													bool flag22 = this.buildingInfo.m_class.m_service == ItemClass.Service.PoliceDepartment;
													if (flag22)
													{
														this.GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
														this.GuestsPanelIcon.normalFgSprite = "FavCimsCrimeArrested";
														this.GuestsPanelText.text = FavCimsLang.Text("Citizen_Under_Arrest");
													}
													else
													{
														bool flag23 = this.buildingInfo.m_class.m_service == ItemClass.Service.Education;
														if (flag23)
														{
															this.GuestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
															this.GuestsPanelIcon.normalFgSprite = "IconPolicySchoolsOut";
															this.GuestsPanelText.text = FavCimsLang.Text("Citizen_at_School");
														}
														else
														{
															bool flag24 = this.buildingInfo.m_class.m_service == ItemClass.Service.HealthCare;
															if (flag24)
															{
																this.GuestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
																this.GuestsPanelIcon.normalFgSprite = "SubBarHealthcareDefault";
																this.GuestsPanelText.text = FavCimsLang.Text("Citizen_on_Clinic");
															}
															else
															{
																this.GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
																this.GuestsPanelIcon.normalFgSprite = "BcommercialIcon";
																this.GuestsPanelText.text = FavCimsLang.Text("OnBuilding_Guests");
															}
														}
													}
													bool flag25 = citizen2.GetBuildingByLocation() == this.BuildingID.Building && citizen2.CurrentLocation != Citizen.Location.Moving;
													if (flag25)
													{
														PeopleInsideServiceBuildingsPanel.GuestsCount++;
														bool flag26 = this.GuestsPanel != null && this.GuestsBodyRow[num] != null;
														if (flag26)
														{
															bool flag27 = this.GuestsBodyRow[num2].citizen != 0U && PeopleInsideServiceBuildingsPanel.CimsOnBuilding.ContainsKey(this.GuestsBodyRow[num2].citizen);
															if (flag27)
															{
																PeopleInsideServiceBuildingsPanel.Wait = true;
																PeopleInsideServiceBuildingsPanel.CimsOnBuilding.Remove(this.GuestsBodyRow[num2].citizen);
															}
															PeopleInsideServiceBuildingsPanel.CimsOnBuilding.Add(citizen, this.BuildingUnits);
															this.GuestsBodyRow[num2].OnBuilding = this.BuildingID.Building;
															this.GuestsBodyRow[num2].citizen = citizen;
															this.GuestsBodyRow[num2].LocType = Citizen.Location.Visit;
															this.GuestsBodyRow[num2].firstRun = true;
															this.GuestsBodyRow[num2].Show();
															bool wait2 = PeopleInsideServiceBuildingsPanel.Wait;
															if (wait2)
															{
																PeopleInsideServiceBuildingsPanel.Wait = false;
															}
														}
													}
													bool flag28 = PeopleInsideServiceBuildingsPanel.GuestsCount == 0;
													if (flag28)
													{
														bool flag29 = this.buildingInfo.m_class.m_service == ItemClass.Service.PoliceDepartment;
														if (flag29)
														{
															this.GuestsPanelText.text = FavCimsLang.Text("OnBuilding_noArrested");
														}
														else
														{
															bool flag30 = this.buildingInfo.m_class.m_service == ItemClass.Service.Education;
															if (flag30)
															{
																this.GuestsPanelText.text = "Non ci sono studenti";
															}
															else
															{
																bool flag31 = this.buildingInfo.m_class.m_service == ItemClass.Service.HealthCare;
																if (flag31)
																{
																	this.GuestsPanelText.text = "Nessun paziente";
																}
																else
																{
																	this.GuestsPanelText.text = FavCimsLang.Text("OnBuilding_NoGuests");
																}
															}
														}
													}
												}
											}
										}
										num2++;
									}
									this.BuildingUnits = nextUnit;
									bool flag32 = ++num > 524288;
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
