using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
	public class ResidentialBuildingPanelRow : UIPanel
	{
        private float seconds = 0.5f;

        private bool execute = false;

        public bool firstRun = true;

        private const float RecentTimer = 8f;

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
				base.width = 226f;
				base.height = 25f;
				base.atlas = MyAtlas.FavCimsAtlas;
				base.backgroundSprite = "bg_row1";
				base.relativePosition = new Vector3(0f, 0f);
				this.Gender = base.AddUIComponent<UIButton>();
				this.Gender.name = "Gender";
				this.Gender.width = 17f;
				this.Gender.height = 17f;
				this.Gender.atlas = MyAtlas.FavCimsAtlas;
				this.Gender.relativePosition = new Vector3(5f, 4f);
				this.Name = base.AddUIComponent<UIButton>();
				this.Name.name = "Name";
				this.Name.width = 131f;
				this.Name.height = 25f;
				this.Name.textVerticalAlignment = UIVerticalAlignment.Middle;
				this.Name.textHorizontalAlignment = 0;
				this.Name.playAudioEvents = true;
				this.Name.font = UIDynamicFont.FindByName("OpenSans-Regular");
				this.Name.font.size = 15;
				this.Name.textScale = 0.8f;
				this.Name.useDropShadow = true;
				this.Name.dropShadowOffset = new Vector2(1f, -1f);
				this.Name.dropShadowColor = new Color32(0, 0, 0, 0);
				this.Name.textPadding.left = 5;
				this.Name.textPadding.right = 5;
				this.Name.textColor = new Color32(204, 204, 51, 40);
				this.Name.hoveredTextColor = new Color32(204, 102, 0, 20);
				this.Name.pressedTextColor = new Color32(102, 153, byte.MaxValue, 147);
				this.Name.focusedTextColor = new Color32(153, 0, 0, 0);
				this.Name.disabledTextColor = new Color32(51, 51, 51, 160);
				this.Name.relativePosition = new Vector3(this.Gender.relativePosition.x + this.Gender.width, 1f);
				this.Name.eventMouseUp += delegate(UIComponent component, UIMouseEventParameter eventParam)
				{
					FavCimsCore.GoToCitizen(base.position, this.MyInstanceID, this.tourist, eventParam);
				};
				this.Age = base.AddUIComponent<UIButton>();
				this.Age.name = "Age";
				this.Age.width = 23f;
				this.Age.height = 19f;
				this.Age.textHorizontalAlignment = UIHorizontalAlignment.Center;
				this.Age.textVerticalAlignment = UIVerticalAlignment.Middle;
				this.Age.font = UIDynamicFont.FindByName("OpenSans-Regular");
				this.Age.textScale = 0.9f;
				this.Age.font.size = 15;
				this.Age.dropShadowOffset = new Vector2(1f, -1f);
				this.Age.dropShadowColor = new Color32(0, 0, 0, 0);
				this.Age.isInteractive = false;
				this.Age.relativePosition = new Vector3(this.Name.relativePosition.x + this.Name.width + 10f, 4f);
				this.Star = base.AddUIComponent<UIButton>();
				this.Star.name = "Star";
				this.Star.atlas = MyAtlas.FavCimsAtlas;
				this.Star.size = new Vector2(16f, 16f);
				this.Star.playAudioEvents = true;
				this.Star.relativePosition = new Vector3(this.Age.relativePosition.x + this.Age.width + 10f, 4f);
				this.Star.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
				{
					FavCimsCore.AddToFavorites(this.MyInstanceID);
				};
			}
			catch (Exception ex)
			{
				Debug.Error("Error in Passenger Creation " + ex.ToString());
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
				bool flag = this.Wait();
				if (!flag)
				{
					bool flag2 = this.citizen == 0U;
					if (flag2)
					{
						base.Hide();
					}
					else
					{
						bool isVisible = base.isVisible;
						if (isVisible)
						{
							bool flag3 = this.firstRun;
							if (flag3)
							{
								this.RecentSeconds = 8f;
							}
							this.seconds -= 1f * Time.deltaTime;
							this.RecentSeconds -= 1f * Time.deltaTime;
							bool flag4 = this.seconds <= 0f || this.firstRun;
							if (flag4)
							{
								this.execute = true;
								this.seconds = 0.5f;
							}
							else
							{
								this.execute = false;
							}
							bool flag5 = this.execute;
							if (flag5)
							{
								this.firstRun = false;
								BuildingInfo info = this.MyBuilding.m_buildings.m_buffer[(int)this.OnBuilding].Info;
								bool flag6 = !this.GetCimsDict().ContainsKey(this.citizen);
								if (flag6)
								{
									base.Hide();
									this.citizen = 0U;
									this.OnBuilding = 0;
									this.firstRun = true;
								}
								else
								{
									bool flag7 = this.MyCitizen.GetCitizenName(this.citizen) == null || this.MyCitizen.GetCitizenName(this.citizen).Length < 1;
									if (flag7)
									{
										bool flag8 = info.m_class.m_service != ItemClass.Service.Residential;
										if (flag8)
										{
											bool flag9 = this.LocType == Citizen.Location.Visit;
											if (flag9)
											{
												this.DecreaseWorkersCount();
											}
											else
											{
												this.DecreaseGuestsCount();
											}
										}
										this.GetCimsDict().Remove(this.citizen);
										base.Hide();
										this.citizen = 0U;
										this.OnBuilding = 0;
										this.firstRun = true;
									}
									else
									{
										this.MyInstanceID.Citizen = this.citizen;
										CitizenInfo citizenInfo = this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].GetCitizenInfo(this.citizen);
                                        string localizedStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Citizen], out InstanceID empty);
                                        string buildingName = this.MyBuilding.GetBuildingName(empty.Building, this.MyInstanceID);
										bool flag10 = citizenInfo.m_class.m_service == ItemClass.Service.Tourism;
										if (flag10)
										{
											this.tourist = true;
											this.Gender.tooltip = Locale.Get("CITIZEN_OCCUPATION_TOURIST");
											this.Name.tooltip = localizedStatus + " " + buildingName;
											bool flag11 = Citizen.GetGender(this.citizen) == Citizen.Gender.Female;
											if (flag11)
											{
												this.Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
												this.Gender.normalBgSprite = "touristIcon";
											}
											else
											{
												this.Name.textColor = new Color32(204, 204, 51, 40);
												this.Gender.normalBgSprite = "touristIcon";
											}
										}
										else
										{
											this.Gender.tooltip = Locale.Get("ASSETTYPE_CITIZEN");
											bool flag12 = Citizen.GetGender(this.citizen) == Citizen.Gender.Female;
											if (flag12)
											{
												this.Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
												this.Gender.normalBgSprite = "Female";
											}
											else
											{
												this.Name.textColor = new Color32(204, 204, 51, 40);
												this.Gender.normalBgSprite = "Male";
											}
											bool arrested = this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].Arrested;
											if (arrested)
											{
												this.Name.tooltip = FavCimsLang.Text("Jailed_into") + " " + buildingName;
											}
											else
											{
												this.Name.tooltip = localizedStatus + " " + buildingName;
											}
										}
										bool flag13 = this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].GetBuildingByLocation() == this.OnBuilding;
										if (flag13)
										{
											bool flag14 = this.RecentSeconds <= 0f;
											if (flag14)
											{
												this.Gender.normalFgSprite = null;
											}
											else
											{
												this.Gender.normalFgSprite = "greenArrowIcon";
											}
										}
										else
										{
											bool flag15 = info.m_class.m_service == ItemClass.Service.Residential;
											if (flag15)
											{
												this.Gender.normalFgSprite = "redArrowIcon";
												this.RecentSeconds = 8f;
											}
										}
										this.Name.text = this.MyCitizen.GetCitizenName(this.citizen);
										this.RealAge = FavCimsCore.CalculateCitizenAge((int)this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].m_age);
										bool flag16 = this.RealAge <= 12;
										if (flag16)
										{
											this.Age.text = this.RealAge.ToString();
											this.Age.textColor = new Color32(83, 166, 0, 60);
										}
										else
										{
											bool flag17 = this.RealAge <= 19;
											if (flag17)
											{
												this.Age.text = this.RealAge.ToString();
												this.Age.textColor = new Color32(0, 102, 51, 100);
											}
											else
											{
												bool flag18 = this.RealAge <= 25;
												if (flag18)
												{
													this.Age.text = this.RealAge.ToString();
													this.Age.textColor = new Color32(byte.MaxValue, 204, 0, 32);
												}
												else
												{
													bool flag19 = this.RealAge <= 65;
													if (flag19)
													{
														this.Age.text = this.RealAge.ToString();
														this.Age.textColor = new Color32(byte.MaxValue, 102, 0, 16);
													}
													else
													{
														bool flag20 = this.RealAge <= 90;
														if (flag20)
														{
															this.Age.text = this.RealAge.ToString();
															this.Age.textColor = new Color32(153, 0, 0, 0);
														}
														else
														{
															this.Age.text = this.RealAge.ToString();
															this.Age.textColor = new Color32(byte.MaxValue, 0, 0, 0);
														}
													}
												}
											}
										}
										this.citizenINT = (int)(uint)((UIntPtr)this.citizen);
										bool flag21 = FavCimsCore.RowID.ContainsKey(this.citizenINT);
										if (flag21)
										{
											this.Star.normalBgSprite = "icon_fav_subscribed";
											this.Star.tooltip = FavCimsLang.Text("FavStarButton_disable_tooltip");
										}
										else
										{
											this.Star.normalBgSprite = "icon_fav_unsubscribed";
											this.Star.tooltip = FavCimsLang.Text("FavStarButton_enable_tooltip");
										}
										bool flag22 = info.m_class.m_service == ItemClass.Service.Residential && this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].m_homeBuilding != this.OnBuilding;
										if (flag22)
										{
											this.GetCimsDict().Remove(this.citizen);
											base.Hide();
											this.citizen = 0U;
											this.OnBuilding = 0;
											this.firstRun = true;
										}
										bool flag23 = info.m_class.m_service != ItemClass.Service.Residential && this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].GetBuildingByLocation() != this.OnBuilding;
										if (flag23)
										{
											bool flag24 = this.LocType == Citizen.Location.Work;
											if (flag24)
											{
												this.DecreaseWorkersCount();
											}
											else
											{
												this.DecreaseGuestsCount();
											}
											this.GetCimsDict().Remove(this.citizen);
											base.Hide();
											this.citizen = 0U;
											this.OnBuilding = 0;
											this.firstRun = true;
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
