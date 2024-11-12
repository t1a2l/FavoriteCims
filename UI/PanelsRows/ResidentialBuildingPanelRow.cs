using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.PanelsRows
{
    public class ResidentialBuildingPanelRow : UIPanel
	{
        private float seconds = 0.5f;

        private bool execute = false;

        public bool firstRun = true;

        private float RecentSeconds = 8f;

        private InstanceID MyInstanceID = InstanceID.Empty;

        public ushort OnBuilding;

        public uint citizen;

        public Citizen.Location LocType;

        private readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        private readonly BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        private int RealAge;

        private int citizenINT;

        private bool tourist = false;

        private UIButton Gender;

        private UIButton Name;

        private UIButton Age;

        private UIButton Star;

        public override void Start()
		{
			try
			{
				width = 226f;
				height = 25f;
				atlas = MyAtlas.FavCimsAtlas;
				backgroundSprite = "bg_row1";
				relativePosition = new Vector3(0f, 0f);
				Gender = AddUIComponent<UIButton>();
				Gender.name = "Gender";
				Gender.width = 17f;
				Gender.height = 17f;
				Gender.atlas = MyAtlas.FavCimsAtlas;
				Gender.relativePosition = new Vector3(5f, 4f);
				Name = AddUIComponent<UIButton>();
				Name.name = "Name";
				Name.width = 131f;
				Name.height = 25f;
				Name.textVerticalAlignment = UIVerticalAlignment.Middle;
				Name.textHorizontalAlignment = 0;
				Name.playAudioEvents = true;
				Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
				Name.font.size = 15;
				Name.textScale = 0.8f;
				Name.useDropShadow = true;
				Name.dropShadowOffset = new Vector2(1f, -1f);
				Name.dropShadowColor = new Color32(0, 0, 0, 0);
				Name.textPadding.left = 5;
				Name.textPadding.right = 5;
				Name.textColor = new Color32(204, 204, 51, 40);
				Name.hoveredTextColor = new Color32(204, 102, 0, 20);
				Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
				Name.focusedTextColor = new Color32(153, 0, 0, 0);
				Name.disabledTextColor = new Color32(51, 51, 51, 160);
				Name.relativePosition = new Vector3(Gender.relativePosition.x + Gender.width, 1f);
				Name.eventMouseUp += delegate(UIComponent component, UIMouseEventParameter eventParam)
				{
					FavCimsCore.GoToCitizen(position, MyInstanceID, tourist, eventParam);
				};
				Age = AddUIComponent<UIButton>();
				Age.name = "Age";
				Age.width = 23f;
				Age.height = 19f;
				Age.textHorizontalAlignment = UIHorizontalAlignment.Center;
				Age.textVerticalAlignment = UIVerticalAlignment.Middle;
				Age.font = UIDynamicFont.FindByName("OpenSans-Regular");
				Age.textScale = 0.9f;
				Age.font.size = 15;
				Age.dropShadowOffset = new Vector2(1f, -1f);
				Age.dropShadowColor = new Color32(0, 0, 0, 0);
				Age.isInteractive = false;
				Age.relativePosition = new Vector3(Name.relativePosition.x + Name.width + 10f, 4f);
				Star = AddUIComponent<UIButton>();
				Star.name = "Star";
				Star.atlas = MyAtlas.FavCimsAtlas;
				Star.size = new Vector2(16f, 16f);
				Star.playAudioEvents = true;
				Star.relativePosition = new Vector3(Age.relativePosition.x + Age.width + 10f, 4f);
				Star.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
				{
					FavCimsCore.AddToFavorites(MyInstanceID);
				};
			}
			catch (Exception ex)
			{
                Utils.Debug.Error("Error in Passenger Creation " + ex.ToString());
			}
		}

		public virtual bool Wait()
		{
			return PeopleInsideBuildingsPanel.Wait;
		}

		public virtual Dictionary<uint, uint> GetCimsDict()
		{
			return PeopleInsideBuildingsPanel.CimsOnBuilding;
		}

		public virtual void DecreaseWorkersCount()
		{
			PeopleInsideBuildingsPanel.WorkersCount--;
		}

		public virtual void DecreaseGuestsCount()
		{
			PeopleInsideBuildingsPanel.GuestsCount--;
		}

		public override void Update()
		{
			bool unLoading = FavCimsMainClass.UnLoading;
			if (!unLoading)
			{
				if (!Wait())
				{
					if (citizen == 0U)
					{
						Hide();
					}
					else
					{
						bool isVisible = base.isVisible;
						if (isVisible)
						{
							if (firstRun)
							{
								RecentSeconds = 8f;
							}
							seconds -= 1f * Time.deltaTime;
							RecentSeconds -= 1f * Time.deltaTime;
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
								BuildingInfo info = MyBuilding.m_buildings.m_buffer[(int)OnBuilding].Info;
								if (!GetCimsDict().ContainsKey(citizen))
								{
									Hide();
									citizen = 0U;
									OnBuilding = 0;
									firstRun = true;
								}
								else
								{
									if (MyCitizen.GetCitizenName(citizen) == null || MyCitizen.GetCitizenName(citizen).Length < 1)
									{
										if (info.m_class.m_service != ItemClass.Service.Residential)
										{
											if (LocType == Citizen.Location.Visit)
											{
												DecreaseWorkersCount();
											}
											else
											{
												DecreaseGuestsCount();
											}
										}
										GetCimsDict().Remove(citizen);
										Hide();
										citizen = 0U;
										OnBuilding = 0;
										firstRun = true;
									}
									else
									{
										MyInstanceID.Citizen = citizen;
										CitizenInfo citizenInfo = MyCitizen.m_citizens.m_buffer[(int)citizen].GetCitizenInfo(citizen);
                                        string localizedStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref MyCitizen.m_citizens.m_buffer[(int)MyInstanceID.Citizen], out InstanceID empty);
                                        string buildingName = MyBuilding.GetBuildingName(empty.Building, MyInstanceID);
										if (citizenInfo.m_class.m_service == ItemClass.Service.Tourism)
										{
											tourist = true;
											Gender.tooltip = Locale.Get("CITIZEN_OCCUPATION_TOURIST");
											Name.tooltip = localizedStatus + " " + buildingName;
											if (Citizen.GetGender(citizen) == Citizen.Gender.Female)
											{
												Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
												Gender.normalBgSprite = "touristIcon";
											}
											else
											{
												Name.textColor = new Color32(204, 204, 51, 40);
												Gender.normalBgSprite = "touristIcon";
											}
										}
										else
										{
											Gender.tooltip = Locale.Get("ASSETTYPE_CITIZEN");
											if (Citizen.GetGender(citizen) == Citizen.Gender.Female)
											{
												Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
												Gender.normalBgSprite = "Female";
											}
											else
											{
												Name.textColor = new Color32(204, 204, 51, 40);
												Gender.normalBgSprite = "Male";
											}
											bool arrested = MyCitizen.m_citizens.m_buffer[(int)citizen].Arrested;
											if (arrested)
											{
												Name.tooltip = FavCimsLang.Text("Jailed_into") + " " + buildingName;
											}
											else
											{
												Name.tooltip = localizedStatus + " " + buildingName;
											}
										}
										if (MyCitizen.m_citizens.m_buffer[(int)citizen].GetBuildingByLocation() == OnBuilding)
										{
											if (RecentSeconds <= 0f)
											{
												Gender.normalFgSprite = null;
											}
											else
											{
												Gender.normalFgSprite = "greenArrowIcon";
											}
										}
										else
										{
											if (info.m_class.m_service == ItemClass.Service.Residential)
											{
												Gender.normalFgSprite = "redArrowIcon";
												RecentSeconds = 8f;
											}
										}

										

										Name.text = MyCitizen.GetCitizenName(citizen);
										RealAge = FavCimsCore.CalculateCitizenAge((int)MyCitizen.m_citizens.m_buffer[(int)citizen].m_age);

                                        switch(RealAge)
										{
											case int n when n <= 12:
                                                Age.text = RealAge.ToString();
                                                Age.textColor = new Color32(83, 166, 0, 60);
                                                break;
                                            case int n when n <= 19:
                                                Age.text = RealAge.ToString();
                                                Age.textColor = new Color32(0, 102, 51, 100);
                                                break;
                                            case int n when n <= 25:
                                                Age.text = RealAge.ToString();
                                                Age.textColor = new Color32(byte.MaxValue, 204, 0, 32);
                                                break;
                                            case int n when n <= 65:
                                                Age.text = RealAge.ToString();
                                                Age.textColor = new Color32(byte.MaxValue, 102, 0, 16);
                                                break;
                                            case int n when n <= 90:
                                                Age.text = RealAge.ToString();
                                                Age.textColor = new Color32(153, 0, 0, 0);
                                                break;
											default:
                                                Age.text = RealAge.ToString();
                                                Age.textColor = new Color32(byte.MaxValue, 0, 0, 0);
                                                break;
                                        }

										citizenINT = (int)(uint)((UIntPtr)citizen);
										if (FavCimsCore.RowID.ContainsKey(citizenINT))
										{
											Star.normalBgSprite = "icon_fav_subscribed";
											Star.tooltip = FavCimsLang.Text("FavStarButton_disable_tooltip");
										}
										else
										{
											Star.normalBgSprite = "icon_fav_unsubscribed";
											Star.tooltip = FavCimsLang.Text("FavStarButton_enable_tooltip");
										}
										if (info.m_class.m_service == ItemClass.Service.Residential && MyCitizen.m_citizens.m_buffer[(int)citizen].m_homeBuilding != OnBuilding)
										{
											GetCimsDict().Remove(citizen);
											Hide();
											citizen = 0U;
											OnBuilding = 0;
											firstRun = true;
										}
										if (info.m_class.m_service != ItemClass.Service.Residential && MyCitizen.m_citizens.m_buffer[(int)citizen].GetBuildingByLocation() != OnBuilding)
										{
											if (LocType == Citizen.Location.Work)
											{
												DecreaseWorkersCount();
											}
											else
											{
												DecreaseGuestsCount();
											}
											GetCimsDict().Remove(citizen);
											Hide();
											citizen = 0U;
											OnBuilding = 0;
											firstRun = true;
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
