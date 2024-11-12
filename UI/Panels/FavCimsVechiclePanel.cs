using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
	public class FavCimsVechiclePanel : UIPanel
	{
        private const float Run = 0.5f;

        private float seconds = 0.5f;

        private bool execute = false;

        private bool firstRun = true;

        public static bool Wait = false;

        private bool Garbage = false;

        public InstanceID VehicleID;

        public UIPanel RefPanel;

        private VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

        private CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        public static Dictionary<uint, uint> CimsOnVeh = new Dictionary<uint, uint>();

        private UIPanel DriverPanel;

        private UIPanel DriverPanelSubRow;

        private UIButton DriverPanelIcon;

        private UIButton DriverPanelText;

        private DriverPrivateVehiclePanelRow DriverPrivateBodyRow;

        private UIPanel PassengersPanel;

        private UIPanel PassengersPanelSubRow;

        private UIButton PassengersPanelIcon;

        private UIButton PassengersPanelText;

        private PassengersPrivateVehiclePanelRow[] PassengersPrivateBodyRow = new PassengersPrivateVehiclePanelRow[5];

        private uint VehicleUnits;

        private UIPanel Title;

        private UITextureSprite TitleSpriteBg;

        private UIButton TitleVehicleName;

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
				base.name = "FavCimsVechiclePanel";
				base.absolutePosition = new Vector3(0f, 0f);
				base.Hide();
				this.Title = base.AddUIComponent<UIPanel>();
				this.Title.name = "FavCimsVechiclePanelTitle";
				this.Title.width = base.width;
				this.Title.height = 41f;
				this.Title.relativePosition = Vector3.zero;
				this.TitleSpriteBg = this.Title.AddUIComponent<UITextureSprite>();
				this.TitleSpriteBg.name = "FavCimsVechiclePanelTitleBG";
				this.TitleSpriteBg.width = this.Title.width;
				this.TitleSpriteBg.height = this.Title.height;
				this.TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				this.TitleSpriteBg.relativePosition = Vector3.zero;
				this.TitleVehicleName = this.Title.AddUIComponent<UIButton>();
				this.TitleVehicleName.name = "TitleVehicleName";
				this.TitleVehicleName.width = this.Title.width;
				this.TitleVehicleName.height = this.Title.height;
				this.TitleVehicleName.textVerticalAlignment = UIVerticalAlignment.Middle;
				this.TitleVehicleName.textHorizontalAlignment = UIHorizontalAlignment.Center;
				this.TitleVehicleName.playAudioEvents = false;
				this.TitleVehicleName.font = UIDynamicFont.FindByName("OpenSans-Regular");
				this.TitleVehicleName.font.size = 15;
				this.TitleVehicleName.textScale = 1f;
				this.TitleVehicleName.wordWrap = true;
				this.TitleVehicleName.textPadding.left = 5;
				this.TitleVehicleName.textPadding.right = 5;
				this.TitleVehicleName.textColor = new Color32(204, 204, 51, 40);
				this.TitleVehicleName.hoveredTextColor = new Color32(204, 204, 51, 40);
				this.TitleVehicleName.pressedTextColor = new Color32(204, 204, 51, 40);
				this.TitleVehicleName.focusedTextColor = new Color32(204, 204, 51, 40);
				this.TitleVehicleName.useDropShadow = true;
				this.TitleVehicleName.dropShadowOffset = new Vector2(1f, -1f);
				this.TitleVehicleName.dropShadowColor = new Color32(0, 0, 0, 0);
				this.TitleVehicleName.relativePosition = Vector3.zero;
				this.Body = base.AddUIComponent<UIPanel>();
				this.Body.name = "VechiclePanelBody";
				this.Body.width = base.width;
				this.Body.autoLayoutDirection = LayoutDirection.Vertical;
				this.Body.autoLayout = true;
				this.Body.clipChildren = true;
				this.Body.height = 0f;
				this.Body.relativePosition = new Vector3(0f, this.Title.height);
				this.BodySpriteBg = this.Body.AddUIComponent<UITextureSprite>();
				this.BodySpriteBg.name = "VechiclePanelDataContainer";
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
				this.DriverPanel = this.BodyRows.AddUIComponent<UIPanel>();
				this.DriverPanel.width = 226f;
				this.DriverPanel.height = 25f;
				this.DriverPanel.name = "LabelPanel_Driver";
				this.DriverPanel.autoLayoutDirection = LayoutDirection.Vertical;
				this.DriverPanel.autoLayout = true;
				this.DriverPanel.Hide();
				this.DriverPanelSubRow = this.DriverPanel.AddUIComponent<UIPanel>();
				this.DriverPanelSubRow.width = 226f;
				this.DriverPanelSubRow.height = 25f;
				this.DriverPanelSubRow.name = "TitlePanel_Driver";
				this.DriverPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
				this.DriverPanelSubRow.backgroundSprite = "bg_row2";
				this.DriverPanelIcon = this.DriverPanelSubRow.AddUIComponent<UIButton>();
				this.DriverPanelIcon.name = "LabelPanelIcon_Driver";
				this.DriverPanelIcon.width = 17f;
				this.DriverPanelIcon.height = 17f;
				this.DriverPanelIcon.atlas = MyAtlas.FavCimsAtlas;
				this.DriverPanelIcon.normalFgSprite = "driverIcon";
				this.DriverPanelIcon.relativePosition = new Vector3(5f, 4f);
				this.DriverPanelText = this.DriverPanelSubRow.AddUIComponent<UIButton>();
				this.DriverPanelText.name = "LabelPanelText_Driver";
				this.DriverPanelText.width = 200f;
				this.DriverPanelText.height = 25f;
				this.DriverPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
				this.DriverPanelText.textHorizontalAlignment = 0;
				this.DriverPanelText.playAudioEvents = true;
				this.DriverPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
				this.DriverPanelText.font.size = 15;
				this.DriverPanelText.textScale = 0.8f;
				this.DriverPanelText.useDropShadow = true;
				this.DriverPanelText.dropShadowOffset = new Vector2(1f, -1f);
				this.DriverPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
				this.DriverPanelText.textPadding.left = 5;
				this.DriverPanelText.textPadding.right = 5;
				this.DriverPanelText.textColor = new Color32(51, 51, 51, 160);
				this.DriverPanelText.isInteractive = false;
				this.DriverPanelText.relativePosition = new Vector3(this.DriverPanelIcon.relativePosition.x + this.DriverPanelIcon.width, 1f);
				this.DriverPrivateBodyRow = this.BodyRows.AddUIComponent(typeof(DriverPrivateVehiclePanelRow)) as DriverPrivateVehiclePanelRow;
				this.DriverPrivateBodyRow.name = "RowPanel_Driver";
				this.DriverPrivateBodyRow.OnVehicle = 0;
				this.DriverPrivateBodyRow.citizen = 0U;
				this.DriverPrivateBodyRow.Hide();
				this.PassengersPanel = this.BodyRows.AddUIComponent<UIPanel>();
				this.PassengersPanel.width = 226f;
				this.PassengersPanel.height = 25f;
				this.PassengersPanel.name = "LabelPanel_Passengers";
				this.PassengersPanel.autoLayoutDirection = LayoutDirection.Vertical;
				this.PassengersPanel.autoLayout = true;
				this.PassengersPanel.Hide();
				this.PassengersPanelSubRow = this.PassengersPanel.AddUIComponent<UIPanel>();
				this.PassengersPanelSubRow.width = 226f;
				this.PassengersPanelSubRow.height = 25f;
				this.PassengersPanelSubRow.name = "TitlePanel_Passengers";
				this.PassengersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
				this.PassengersPanelSubRow.backgroundSprite = "bg_row2";
				this.PassengersPanelIcon = this.PassengersPanelSubRow.AddUIComponent<UIButton>();
				this.PassengersPanelIcon.name = "LabelPanelIcon_Passengers";
				this.PassengersPanelIcon.width = 17f;
				this.PassengersPanelIcon.height = 17f;
				this.PassengersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
				this.PassengersPanelIcon.normalFgSprite = "passengerIcon";
				this.PassengersPanelIcon.relativePosition = new Vector3(5f, 4f);
				this.PassengersPanelText = this.PassengersPanelSubRow.AddUIComponent<UIButton>();
				this.PassengersPanelText.name = "LabelPanelText_Passengers";
				this.PassengersPanelText.width = 200f;
				this.PassengersPanelText.height = 25f;
				this.PassengersPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
				this.PassengersPanelText.textHorizontalAlignment = 0;
				this.PassengersPanelText.playAudioEvents = true;
				this.PassengersPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
				this.PassengersPanelText.font.size = 15;
				this.PassengersPanelText.textScale = 0.8f;
				this.PassengersPanelText.useDropShadow = true;
				this.PassengersPanelText.dropShadowOffset = new Vector2(1f, -1f);
				this.PassengersPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
				this.PassengersPanelText.textPadding.left = 5;
				this.PassengersPanelText.textPadding.right = 5;
				this.PassengersPanelText.textColor = new Color32(51, 51, 51, 160);
				this.PassengersPanelText.isInteractive = false;
				this.PassengersPanelText.relativePosition = new Vector3(this.PassengersPanelIcon.relativePosition.x + this.PassengersPanelIcon.width, 1f);
				for (int i = 1; i < 5; i++)
				{
					this.PassengersPrivateBodyRow[i] = this.BodyRows.AddUIComponent(typeof(PassengersPrivateVehiclePanelRow)) as PassengersPrivateVehiclePanelRow;
					this.PassengersPrivateBodyRow[i].name = "RowPanel_Passengers_" + i.ToString();
					this.PassengersPrivateBodyRow[i].OnVehicle = 0;
					this.PassengersPrivateBodyRow[i].citizen = 0U;
					this.PassengersPrivateBodyRow[i].Hide();
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
					int num = Math.Sign(eventParam.wheelDelta);
					this.BodyRows.scrollPosition += new Vector2(0f, (float)(num * -1) * this.BodyScrollBar.incrementAmount);
				};
				this.Footer = base.AddUIComponent<UIPanel>();
				this.Footer.name = "VechiclePanelPTFooter";
				this.Footer.width = base.width;
				this.Footer.height = 12f;
				this.Footer.relativePosition = new Vector3(0f, this.Title.height + this.Body.height);
				this.FooterSpriteBg = this.Footer.AddUIComponent<UITextureSprite>();
				this.FooterSpriteBg.width = this.Footer.width;
				this.FooterSpriteBg.height = this.Footer.height;
				this.FooterSpriteBg.texture = TextureDB.VehiclePanelFooterBackground;
				this.FooterSpriteBg.relativePosition = Vector3.zero;
				UIComponent uicomponent = UIView.Find<UIButton>("Esc");
				bool flag = uicomponent != null;
				if (flag)
				{
					uicomponent.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
					{
						base.Hide();
					};
				}
			}
			catch (Exception ex)
			{
				Debug.Error(" Passengers Panel Start() : " + ex.ToString());
			}
		}

		public override void Update()
		{
			bool unLoading = FavCimsMainClass.UnLoading;
			if (!unLoading)
			{
				bool isEmpty = this.VehicleID.IsEmpty;
				if (isEmpty)
				{
					bool garbage = this.Garbage;
					if (garbage)
					{
						FavCimsVechiclePanel.Wait = true;
						FavCimsVechiclePanel.CimsOnVeh.Clear();
						try
						{
							this.DriverPanel.Hide();
							this.PassengersPanel.Hide();
							this.DriverPrivateBodyRow.Hide();
							this.DriverPrivateBodyRow.citizen = 0U;
							this.DriverPrivateBodyRow.OnVehicle = 0;
							this.DriverPrivateBodyRow.firstRun = true;
							for (int i = 1; i < 5; i++)
							{
								this.PassengersPrivateBodyRow[i].Hide();
								this.PassengersPrivateBodyRow[i].citizen = 0U;
								this.PassengersPrivateBodyRow[i].OnVehicle = 0;
								this.PassengersPrivateBodyRow[i].firstRun = true;
							}
							FavCimsVechiclePanel.Wait = false;
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
						bool flag = !WorldInfoPanel.GetCurrentInstanceID().IsEmpty && WorldInfoPanel.GetCurrentInstanceID() != this.VehicleID;
						if (flag)
						{
							FavCimsVechiclePanel.Wait = true;
							FavCimsVechiclePanel.CimsOnVeh.Clear();
							try
							{
								this.DriverPanel.Hide();
								this.PassengersPanel.Hide();
								this.DriverPrivateBodyRow.Hide();
								this.DriverPrivateBodyRow.citizen = 0U;
								this.DriverPrivateBodyRow.OnVehicle = 0;
								this.DriverPrivateBodyRow.firstRun = true;
								for (int j = 1; j < 5; j++)
								{
									this.PassengersPrivateBodyRow[j].Hide();
									this.PassengersPrivateBodyRow[j].citizen = 0U;
									this.PassengersPrivateBodyRow[j].OnVehicle = 0;
									this.PassengersPrivateBodyRow[j].firstRun = true;
								}
								this.VehicleID = WorldInfoPanel.GetCurrentInstanceID();
								bool isEmpty2 = this.VehicleID.IsEmpty;
								if (isEmpty2)
								{
									return;
								}
								FavCimsVechiclePanel.Wait = false;
							}
							catch
							{
							}
						}
						bool flag2 = base.isVisible && !this.VehicleID.IsEmpty;
						if (flag2)
						{
							this.Garbage = true;
							this.TitleVehicleName.text = FavCimsLang.text("Vehicle_Passengers");
							base.absolutePosition = new Vector3(this.RefPanel.absolutePosition.x + this.RefPanel.width + 5f, this.RefPanel.absolutePosition.y);
							base.height = this.RefPanel.height - 15f;
							bool flag3 = 50f + (float)FavCimsVechiclePanel.CimsOnVeh.Count * 25f < base.height - this.Title.height - this.Footer.height;
							if (flag3)
							{
								this.Body.height = base.height - this.Title.height - this.Footer.height;
							}
							else
							{
								bool flag4 = 50f + (float)FavCimsVechiclePanel.CimsOnVeh.Count * 25f > 400f;
								if (flag4)
								{
									this.Body.height = 400f;
								}
								else
								{
									this.Body.height = 50f + (float)FavCimsVechiclePanel.CimsOnVeh.Count * 25f;
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
								this.VehicleUnits = this.MyVehicle.m_vehicles.m_buffer[(int)this.VehicleID.Vehicle].m_citizenUnits;
								int num = 0;
								bool flag7 = FavCimsVechiclePanel.CimsOnVeh.Count == 0;
								if (flag7)
								{
									this.PassengersPanelText.text = FavCimsLang.text("View_NoPassengers");
								}
								this.DriverPanel.Show();
								this.DriverPanelText.text = FavCimsLang.text("Vehicle_DriverIconText");
								this.PassengersPanel.Show();
								while (this.VehicleUnits > 0U)
								{
									uint nextUnit = this.MyCitizen.m_units.m_buffer[(int)this.VehicleUnits].m_nextUnit;
									for (int k = 0; k < 5; k++)
									{
										uint citizen = this.MyCitizen.m_units.m_buffer[(int)this.VehicleUnits].GetCitizen(k);
										bool flag8 = citizen != 0U && !FavCimsVechiclePanel.CimsOnVeh.ContainsKey(citizen);
										if (flag8)
										{
											bool flag9 = k == 0;
											if (flag9)
											{
												bool flag10 = this.DriverPanel != null && this.DriverPrivateBodyRow != null;
												if (flag10)
												{
													FavCimsVechiclePanel.CimsOnVeh.Add(citizen, this.VehicleUnits);
													this.DriverPrivateBodyRow.citizen = citizen;
													this.DriverPrivateBodyRow.OnVehicle = this.VehicleID.Vehicle;
													this.DriverPrivateBodyRow.firstRun = true;
													this.DriverPrivateBodyRow.Show();
												}
											}
											else
											{
												this.PassengersPanelText.text = FavCimsLang.text("Vehicle_PasssengerIconText");
												bool flag11 = this.PassengersPanel != null && this.PassengersPrivateBodyRow[k] != null;
												if (flag11)
												{
													FavCimsVechiclePanel.CimsOnVeh.Add(citizen, this.VehicleUnits);
													this.PassengersPrivateBodyRow[k].citizen = citizen;
													this.PassengersPrivateBodyRow[k].OnVehicle = this.VehicleID.Vehicle;
													this.PassengersPrivateBodyRow[k].firstRun = true;
													this.PassengersPrivateBodyRow[k].Show();
												}
											}
										}
									}
									this.VehicleUnits = nextUnit;
									bool flag12 = ++num > 524288;
									if (flag12)
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
