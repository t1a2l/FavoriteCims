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

        const int MaxWorkersUnit = 40; // **Important** *same of MaxGuestsUnit*
        const int MaxGuestsUnit = 40;

        private UIPanel WorkersPanel;

        private UIPanel WorkersPanelSubRow;

        private UIButton WorkersPanelIcon;

        private UIButton WorkersPanelText;

        private readonly WorkersServiceBuildingPanelRow[] WorkersBodyRow = new WorkersServiceBuildingPanelRow[MaxWorkersUnit*5];

        private UIPanel GuestsPanel;

        private UIPanel GuestsPanelSubRow;

        private UIButton GuestsPanelIcon;

        private UIButton GuestsPanelText;

        private readonly GuestsServiceBuildingPanelRow[] GuestsBodyRow = new GuestsServiceBuildingPanelRow[MaxGuestsUnit*5];

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
				width = 250f;
				height = 0f;
				name = "FavCimsPeopleInsideServiceBuildingsPanel";
				absolutePosition = new Vector3(0f, 0f);
				Hide();
				Title = AddUIComponent<UIPanel>();
				Title.name = "PeopleInsideServiceBuildingsPanelTitle";
				Title.width = width;
				Title.height = 41f;
				Title.relativePosition = Vector3.zero;
				TitleSpriteBg = Title.AddUIComponent<UITextureSprite>();
				TitleSpriteBg.name = "PeopleInsideServiceBuildingsPanelTitleBG";
				TitleSpriteBg.width = Title.width;
				TitleSpriteBg.height = Title.height;
				TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				TitleSpriteBg.relativePosition = Vector3.zero;
				TitleBuildingName = Title.AddUIComponent<UIButton>();
				TitleBuildingName.name = "PeopleInsideServiceBuildingsPanelName";
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
				Body.name = "PeopleInsideServiceBuildingsBody";
				Body.width = width;
				Body.autoLayoutDirection = LayoutDirection.Vertical;
				Body.autoLayout = true;
				Body.clipChildren = true;
				Body.height = 0f;
				Body.relativePosition = new Vector3(0f, Title.height);
				BodySpriteBg = Body.AddUIComponent<UITextureSprite>();
				BodySpriteBg.name = "PeopleInsideServiceBuildingsDataContainer";
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
				string[] array = ["Workers", "Guests"];
				for (int i = 0; i < 2; i++)
				{
					if (i == 0)
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
						for (int j = 0; j < 200; j++)
						{
							WorkersBodyRow[row] = BodyRows.AddUIComponent(typeof(WorkersServiceBuildingPanelRow)) as WorkersServiceBuildingPanelRow;
							WorkersBodyRow[row].name = "Row_" + array[i] + "_" + j.ToString();
							WorkersBodyRow[row].OnBuilding = 0;
							WorkersBodyRow[row].citizen = 0U;
							WorkersBodyRow[row].Hide();
                            row++;
						}
					}
					else
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
						for (int k = 0; k < 200; k++)
						{
							GuestsBodyRow[row] = BodyRows.AddUIComponent(typeof(GuestsServiceBuildingPanelRow)) as GuestsServiceBuildingPanelRow;
							GuestsBodyRow[row].name = "Row_" + array[i] + "_" + k.ToString();
							GuestsBodyRow[row].OnBuilding = 0;
							GuestsBodyRow[row].citizen = 0U;
							GuestsBodyRow[row].Hide();
                            row++;
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
				Footer.name = "PeopleInsideServiceBuildingsFooter";
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
                Utils.Debug.Error(" Service Building Panel Start() : " + ex.ToString());
			}
		}

		public override void Update()
		{
			bool unLoading = FavCimsMainClass.UnLoading;
			if (!unLoading)
			{
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
						try
						{
							WorkersPanel.Hide();
							for (int i = 0; i < MaxWorkersUnit*5; i++)
							{
								WorkersBodyRow[i].Hide();
								WorkersBodyRow[i].citizen = 0U;
								WorkersBodyRow[i].OnBuilding = 0;
								WorkersBodyRow[i].firstRun = true;
							}
							GuestsPanel.Hide();
							for (int j = 0; j < MaxGuestsUnit*5; j++)
							{
								GuestsBodyRow[j].Hide();
								GuestsBodyRow[j].citizen = 0U;
								GuestsBodyRow[j].OnBuilding = 0;
								GuestsBodyRow[j].firstRun = true;
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
						buildingInfo = MyBuilding.m_buildings.m_buffer[(int)BuildingID.Building].Info;
						if (!WorldInfoPanel.GetCurrentInstanceID().IsEmpty && WorldInfoPanel.GetCurrentInstanceID() != BuildingID)
						{
							Wait = true;
							CimsOnBuilding.Clear();
							WorkersCount = 0;
							GuestsCount = 0;
							WorkersPanel.Hide();
							for (int k = 0; k < MaxWorkersUnit*5; k++)
							{
								WorkersBodyRow[k].Hide();
								WorkersBodyRow[k].citizen = 0U;
								WorkersBodyRow[k].OnBuilding = 0;
								WorkersBodyRow[k].firstRun = true;
							}
							GuestsPanel.Hide();
							for (int l = 0; l < MaxGuestsUnit*5; l++)
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
							if (25f + (float)CimsOnBuilding.Count * 25f < height - Title.height - Footer.height)
							{
								Body.height = height - Title.height - Footer.height;
							}
							else
							{
								if (25f + (float)CimsOnBuilding.Count * 25f > 400f)
								{
									Body.height = 400f;
								}
								else
								{
									Body.height = 25f + (float)CimsOnBuilding.Count * 25f;
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
								BuildingUnits = MyBuilding.m_buildings.m_buffer[(int)BuildingID.Building].m_citizenUnits;
								int unitnum = 0;
								int rownum = 0;
								int total_workers = 0;
								while (BuildingUnits != 0U && unitnum < MaxGuestsUnit)
								{
									uint nextUnit = MyCitizen.m_units.m_buffer[(int)BuildingUnits].m_nextUnit;
									for (int m = 0; m < 5; m++)
									{
										uint citizen = MyCitizen.m_units.m_buffer[(int)BuildingUnits].GetCitizen(m);
										Citizen citizen2 = MyCitizen.m_citizens.m_buffer[(int)citizen];
										if (citizen > 0U)
										{
											int forcedToGuest = 0;
											CitizenInfo citizenInfo = MyCitizen.m_citizens.m_buffer[(int)citizen].GetCitizenInfo(citizen);
                                            string localizedStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref MyCitizen.m_citizens.m_buffer[(int)citizen], out InstanceID instanceID);
											if (citizen2.m_workBuilding == BuildingID.Building && buildingInfo.m_class.m_service == ItemClass.Service.Education)
											{
												if (Locale.Get("CITIZEN_STATUS_AT_SCHOOL") == localizedStatus)
												{
                                                    forcedToGuest = 1;
												}
											}
											if (BuildingID.Building == citizen2.m_workBuilding && forcedToGuest == 0)
											{
                                                total_workers++;
											}
                                            if (!CimsOnBuilding.ContainsKey(citizen))
											{
                                                switch (buildingInfo.m_class.m_service)
												{
													case ItemClass.Service.PoliceDepartment:
														TitleBuildingName.text = FavCimsLang.Text("OnPolice_Building_Service");
														break;
                                                    case ItemClass.Service.Education:
                                                        TitleBuildingName.text = FavCimsLang.Text("OnEducation_Building_Service");
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
													WorkersPanelText.text = string.Concat(
                                                    [
                                                        FavCimsLang.Text("OnBuilding_Workers"),
														" (",
														FavCimsLang.Text("OnBuilding_TotalWorkers"),
														" ",
                                                        total_workers.ToString(),
														")"
													]);
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
															bool wait = Wait;
															if (wait)
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
                                                        case ItemClass.Service.PoliceDepartment:
                                                            GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
                                                            GuestsPanelIcon.normalFgSprite = "FavCimsCrimeArrested";
                                                            GuestsPanelText.text = FavCimsLang.Text("Citizen_Under_Arrest");
                                                            break;
                                                        case ItemClass.Service.Education:
                                                            GuestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
                                                            GuestsPanelIcon.normalFgSprite = "IconPolicySchoolsOut";
                                                            GuestsPanelText.text = FavCimsLang.Text("Citizen_at_School");
                                                            break;
                                                        case ItemClass.Service.HealthCare:
                                                            GuestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
                                                            GuestsPanelIcon.normalFgSprite = "SubBarHealthcareDefault";
                                                            GuestsPanelText.text = FavCimsLang.Text("Citizen_on_Clinic");
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
															bool wait2 = Wait;
															if (wait2)
															{
																Wait = false;
															}
														}
													}
													if (GuestsCount == 0)
													{
                                                        switch (buildingInfo.m_class.m_service)
                                                        {
                                                            case ItemClass.Service.PoliceDepartment:
                                                                GuestsPanelText.text = FavCimsLang.Text("OnBuilding_noArrested");
                                                                break;
                                                            case ItemClass.Service.Education:
                                                                GuestsPanelText.text = FavCimsLang.Text("OnBuilding_noStudents");
                                                                break;
                                                            case ItemClass.Service.HealthCare:
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
                                        rownum++;
									}
									BuildingUnits = nextUnit;
									if (++unitnum > 524288)
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
