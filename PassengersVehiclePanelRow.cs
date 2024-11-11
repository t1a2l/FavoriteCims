using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
	public class PassengersVehiclePanelRow : UIPanel
	{
        private const float Run = 0.5f;

        private float seconds = 0.5f;

        private bool execute = false;

        public bool firstRun = true;

        private const float RecentTimer = 8f;

        private float RecentSeconds = 8f;

        private InstanceID MyInstanceID = InstanceID.Empty;

        public ushort OnVehicle;

        public uint citizen;

        private CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        private BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        private VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

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
			return FavCimsVechiclePanelPT.Wait;
		}

		public virtual Dictionary<uint, uint> GetCimsDict()
		{
			return FavCimsVechiclePanelPT.CimsOnPTVeh;
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
								bool flag6 = !this.GetCimsDict().ContainsKey(this.citizen);
								if (flag6)
								{
									base.Hide();
									this.citizen = 0U;
									this.OnVehicle = 0;
									this.firstRun = true;
								}
								else
								{
									this.MyInstanceID.Citizen = this.citizen;
									CitizenInfo citizenInfo = this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].GetCitizenInfo(this.citizen);
									VehicleInfo info = this.MyVehicle.m_vehicles.m_buffer[(int)this.OnVehicle].Info;
									InstanceID empty = InstanceID.Empty;
									string localizedStatus = citizenInfo.m_citizenAI.GetLocalizedStatus(this.citizen, ref this.MyCitizen.m_citizens.m_buffer[(int)this.MyInstanceID.Citizen], out empty);
									string buildingName = this.MyBuilding.GetBuildingName(empty.Building, this.MyInstanceID);
									bool flag7 = citizenInfo.m_class.m_service == ItemClass.Service.Tourism;
									if (flag7)
									{
										this.tourist = true;
										this.Gender.tooltip = Locale.Get("CITIZEN_OCCUPATION_TOURIST");
										this.Name.tooltip = localizedStatus + " " + buildingName;
										bool flag8 = Citizen.GetGender(this.citizen) == Citizen.Gender.Female;
										if (flag8)
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
										bool flag9 = Citizen.GetGender(this.citizen) == Citizen.Gender.Female;
										if (flag9)
										{
											this.Name.textColor = new Color32(byte.MaxValue, 102, 204, 213);
											this.Gender.normalBgSprite = "Female";
										}
										else
										{
											this.Name.textColor = new Color32(204, 204, 51, 40);
											this.Gender.normalBgSprite = "Male";
										}
										this.Name.tooltip = localizedStatus + " " + buildingName;
									}
									bool flag10 = info.m_class.m_service == ItemClass.Service.PublicTransport;
									if (flag10)
									{
										bool flag11 = this.RecentSeconds <= 0f;
										if (flag11)
										{
											this.Gender.normalFgSprite = null;
										}
										else
										{
											this.Gender.normalFgSprite = "greenArrowIcon";
										}
									}
									this.Name.text = this.MyCitizen.GetCitizenName(this.citizen);
									this.RealAge = FavCimsCore.CalculateCitizenAge((int)this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].m_age);
									bool flag12 = this.RealAge <= 12;
									if (flag12)
									{
										this.Age.text = this.RealAge.ToString();
										this.Age.textColor = new Color32(83, 166, 0, 60);
									}
									else
									{
										bool flag13 = this.RealAge <= 19;
										if (flag13)
										{
											this.Age.text = this.RealAge.ToString();
											this.Age.textColor = new Color32(0, 102, 51, 100);
										}
										else
										{
											bool flag14 = this.RealAge <= 25;
											if (flag14)
											{
												this.Age.text = this.RealAge.ToString();
												this.Age.textColor = new Color32(byte.MaxValue, 204, 0, 32);
											}
											else
											{
												bool flag15 = this.RealAge <= 65;
												if (flag15)
												{
													this.Age.text = this.RealAge.ToString();
													this.Age.textColor = new Color32(byte.MaxValue, 102, 0, 16);
												}
												else
												{
													bool flag16 = this.RealAge <= 90;
													if (flag16)
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
									bool flag17 = FavCimsCore.RowID.ContainsKey(this.citizenINT);
									if (flag17)
									{
										this.Star.normalBgSprite = "icon_fav_subscribed";
										this.Star.tooltip = FavCimsLang.text("FavStarButton_disable_tooltip");
									}
									else
									{
										this.Star.normalBgSprite = "icon_fav_unsubscribed";
										this.Star.tooltip = FavCimsLang.text("FavStarButton_enable_tooltip");
									}
									bool flag18 = this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].m_vehicle == 0 || this.MyCitizen.m_citizens.m_buffer[(int)this.citizen].m_vehicle != this.OnVehicle;
									if (flag18)
									{
										this.GetCimsDict().Remove(this.citizen);
										base.Hide();
										this.citizen = 0U;
										this.OnVehicle = 0;
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
