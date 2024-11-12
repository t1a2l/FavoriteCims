using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class FavCimsVehiclePanel : UIPanel
	{
        private float seconds = 0.5f;

        private bool execute = false;

        private bool firstRun = true;

        public static bool Wait = false;

        private bool Garbage = false;

        public InstanceID VehicleID;

        public UIPanel RefPanel;

        private readonly VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

        private readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        public static Dictionary<uint, uint> CimsOnVeh = [];

        private UIPanel DriverPanel;

        private UIPanel DriverPanelSubRow;

        private UIButton DriverPanelIcon;

        private UIButton DriverPanelText;

        private DriverPrivateVehiclePanelRow DriverPrivateBodyRow;

        private UIPanel PassengersPanel;

        private UIPanel PassengersPanelSubRow;

        private UIButton PassengersPanelIcon;

        private UIButton PassengersPanelText;

        private readonly PassengersPrivateVehiclePanelRow[] PassengersPrivateBodyRow = new PassengersPrivateVehiclePanelRow[5];

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
				width = 250f;
				height = 0f;
				name = "FavCimsVechiclePanel";
				absolutePosition = new Vector3(0f, 0f);
				Hide();
				Title = AddUIComponent<UIPanel>();
				Title.name = "FavCimsVechiclePanelTitle";
				Title.width = width;
				Title.height = 41f;
				Title.relativePosition = Vector3.zero;
				TitleSpriteBg = Title.AddUIComponent<UITextureSprite>();
				TitleSpriteBg.name = "FavCimsVechiclePanelTitleBG";
				TitleSpriteBg.width = Title.width;
				TitleSpriteBg.height = Title.height;
				TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				TitleSpriteBg.relativePosition = Vector3.zero;
				TitleVehicleName = Title.AddUIComponent<UIButton>();
				TitleVehicleName.name = "TitleVehicleName";
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
				Body.name = "VechiclePanelBody";
				Body.width = width;
				Body.autoLayoutDirection = LayoutDirection.Vertical;
				Body.autoLayout = true;
				Body.clipChildren = true;
				Body.height = 0f;
				Body.relativePosition = new Vector3(0f, Title.height);
				BodySpriteBg = Body.AddUIComponent<UITextureSprite>();
				BodySpriteBg.name = "VechiclePanelDataContainer";
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
				DriverPanel = BodyRows.AddUIComponent<UIPanel>();
				DriverPanel.width = 226f;
				DriverPanel.height = 25f;
				DriverPanel.name = "LabelPanel_Driver";
				DriverPanel.autoLayoutDirection = LayoutDirection.Vertical;
				DriverPanel.autoLayout = true;
				DriverPanel.Hide();
				DriverPanelSubRow = DriverPanel.AddUIComponent<UIPanel>();
				DriverPanelSubRow.width = 226f;
				DriverPanelSubRow.height = 25f;
				DriverPanelSubRow.name = "TitlePanel_Driver";
				DriverPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
				DriverPanelSubRow.backgroundSprite = "bg_row2";
				DriverPanelIcon = DriverPanelSubRow.AddUIComponent<UIButton>();
				DriverPanelIcon.name = "LabelPanelIcon_Driver";
				DriverPanelIcon.width = 17f;
				DriverPanelIcon.height = 17f;
				DriverPanelIcon.atlas = MyAtlas.FavCimsAtlas;
				DriverPanelIcon.normalFgSprite = "driverIcon";
				DriverPanelIcon.relativePosition = new Vector3(5f, 4f);
				DriverPanelText = DriverPanelSubRow.AddUIComponent<UIButton>();
				DriverPanelText.name = "LabelPanelText_Driver";
				DriverPanelText.width = 200f;
				DriverPanelText.height = 25f;
				DriverPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
				DriverPanelText.textHorizontalAlignment = 0;
				DriverPanelText.playAudioEvents = true;
				DriverPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
				DriverPanelText.font.size = 15;
				DriverPanelText.textScale = 0.8f;
				DriverPanelText.useDropShadow = true;
				DriverPanelText.dropShadowOffset = new Vector2(1f, -1f);
				DriverPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
				DriverPanelText.textPadding.left = 5;
				DriverPanelText.textPadding.right = 5;
				DriverPanelText.textColor = new Color32(51, 51, 51, 160);
				DriverPanelText.isInteractive = false;
				DriverPanelText.relativePosition = new Vector3(DriverPanelIcon.relativePosition.x + DriverPanelIcon.width, 1f);
				DriverPrivateBodyRow = BodyRows.AddUIComponent(typeof(DriverPrivateVehiclePanelRow)) as DriverPrivateVehiclePanelRow;
				DriverPrivateBodyRow.name = "RowPanel_Driver";
				DriverPrivateBodyRow.OnVehicle = 0;
				DriverPrivateBodyRow.citizen = 0U;
				DriverPrivateBodyRow.Hide();
				PassengersPanel = BodyRows.AddUIComponent<UIPanel>();
				PassengersPanel.width = 226f;
				PassengersPanel.height = 25f;
				PassengersPanel.name = "LabelPanel_Passengers";
				PassengersPanel.autoLayoutDirection = LayoutDirection.Vertical;
				PassengersPanel.autoLayout = true;
				PassengersPanel.Hide();
				PassengersPanelSubRow = PassengersPanel.AddUIComponent<UIPanel>();
				PassengersPanelSubRow.width = 226f;
				PassengersPanelSubRow.height = 25f;
				PassengersPanelSubRow.name = "TitlePanel_Passengers";
				PassengersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
				PassengersPanelSubRow.backgroundSprite = "bg_row2";
				PassengersPanelIcon = PassengersPanelSubRow.AddUIComponent<UIButton>();
				PassengersPanelIcon.name = "LabelPanelIcon_Passengers";
				PassengersPanelIcon.width = 17f;
				PassengersPanelIcon.height = 17f;
				PassengersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
				PassengersPanelIcon.normalFgSprite = "passengerIcon";
				PassengersPanelIcon.relativePosition = new Vector3(5f, 4f);
				PassengersPanelText = PassengersPanelSubRow.AddUIComponent<UIButton>();
				PassengersPanelText.name = "LabelPanelText_Passengers";
				PassengersPanelText.width = 200f;
				PassengersPanelText.height = 25f;
				PassengersPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
				PassengersPanelText.textHorizontalAlignment = 0;
				PassengersPanelText.playAudioEvents = true;
				PassengersPanelText.font = UIDynamicFont.FindByName("OpenSans-Regular");
				PassengersPanelText.font.size = 15;
				PassengersPanelText.textScale = 0.8f;
				PassengersPanelText.useDropShadow = true;
				PassengersPanelText.dropShadowOffset = new Vector2(1f, -1f);
				PassengersPanelText.dropShadowColor = new Color32(0, 0, 0, 0);
				PassengersPanelText.textPadding.left = 5;
				PassengersPanelText.textPadding.right = 5;
				PassengersPanelText.textColor = new Color32(51, 51, 51, 160);
				PassengersPanelText.isInteractive = false;
				PassengersPanelText.relativePosition = new Vector3(PassengersPanelIcon.relativePosition.x + PassengersPanelIcon.width, 1f);
				for (int i = 1; i < 5; i++)
				{
					PassengersPrivateBodyRow[i] = BodyRows.AddUIComponent(typeof(PassengersPrivateVehiclePanelRow)) as PassengersPrivateVehiclePanelRow;
					PassengersPrivateBodyRow[i].name = "RowPanel_Passengers_" + i.ToString();
					PassengersPrivateBodyRow[i].OnVehicle = 0;
					PassengersPrivateBodyRow[i].citizen = 0U;
					PassengersPrivateBodyRow[i].Hide();
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
				Footer.name = "VechiclePanelPTFooter";
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
			bool unLoading = FavCimsMainClass.UnLoading;
			if (!unLoading)
			{
				bool isEmpty = VehicleID.IsEmpty;
				if (isEmpty)
				{
					bool garbage = Garbage;
					if (garbage)
					{
                        Wait = true;
                        CimsOnVeh.Clear();
						try
						{
							DriverPanel.Hide();
							PassengersPanel.Hide();
							DriverPrivateBodyRow.Hide();
							DriverPrivateBodyRow.citizen = 0U;
							DriverPrivateBodyRow.OnVehicle = 0;
							DriverPrivateBodyRow.firstRun = true;
							for (int i = 1; i < 5; i++)
							{
								PassengersPrivateBodyRow[i].Hide();
								PassengersPrivateBodyRow[i].citizen = 0U;
								PassengersPrivateBodyRow[i].OnVehicle = 0;
								PassengersPrivateBodyRow[i].firstRun = true;
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
						if (!WorldInfoPanel.GetCurrentInstanceID().IsEmpty && WorldInfoPanel.GetCurrentInstanceID() != VehicleID)
						{
                            Wait = true;
                            CimsOnVeh.Clear();
							try
							{
								DriverPanel.Hide();
								PassengersPanel.Hide();
								DriverPrivateBodyRow.Hide();
								DriverPrivateBodyRow.citizen = 0U;
								DriverPrivateBodyRow.OnVehicle = 0;
								DriverPrivateBodyRow.firstRun = true;
								for (int j = 1; j < 5; j++)
								{
									PassengersPrivateBodyRow[j].Hide();
									PassengersPrivateBodyRow[j].citizen = 0U;
									PassengersPrivateBodyRow[j].OnVehicle = 0;
									PassengersPrivateBodyRow[j].firstRun = true;
								}
								VehicleID = WorldInfoPanel.GetCurrentInstanceID();
								bool isEmpty2 = VehicleID.IsEmpty;
								if (isEmpty2)
								{
									return;
								}
                                Wait = false;
							}
							catch
							{
							}
						}
						if (isVisible && !VehicleID.IsEmpty)
						{
							Garbage = true;
							TitleVehicleName.text = FavCimsLang.Text("Vehicle_Passengers");
							absolutePosition = new Vector3(RefPanel.absolutePosition.x + RefPanel.width + 5f, RefPanel.absolutePosition.y);
							height = RefPanel.height - 15f;
							if (50f + CimsOnVeh.Count * 25f < height - Title.height - Footer.height)
							{
								Body.height = height - Title.height - Footer.height;
							}
							else
							{
								if (50f + CimsOnVeh.Count * 25f > 400f)
								{
									Body.height = 400f;
								}
								else
								{
									Body.height = 50f + CimsOnVeh.Count * 25f;
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
								VehicleUnits = MyVehicle.m_vehicles.m_buffer[(int)VehicleID.Vehicle].m_citizenUnits;
								int unitnum = 0;
								if (CimsOnVeh.Count == 0)
								{
									PassengersPanelText.text = FavCimsLang.Text("View_NoPassengers");
								}
								DriverPanel.Show();
								DriverPanelText.text = FavCimsLang.Text("Vehicle_DriverIconText");
								PassengersPanel.Show();
								while (VehicleUnits > 0U)
								{
									uint nextUnit = MyCitizen.m_units.m_buffer[(int)VehicleUnits].m_nextUnit;
									for (int k = 0; k < 5; k++)
									{
										uint citizen = MyCitizen.m_units.m_buffer[(int)VehicleUnits].GetCitizen(k);
										if (citizen != 0U && !CimsOnVeh.ContainsKey(citizen))
										{
											if (k == 0)
											{
												if (DriverPanel != null && DriverPrivateBodyRow != null)
												{
                                                    CimsOnVeh.Add(citizen, VehicleUnits);
													DriverPrivateBodyRow.citizen = citizen;
													DriverPrivateBodyRow.OnVehicle = VehicleID.Vehicle;
													DriverPrivateBodyRow.firstRun = true;
													DriverPrivateBodyRow.Show();
												}
											}
											else
											{
												PassengersPanelText.text = FavCimsLang.Text("Vehicle_PasssengerIconText");
												if (PassengersPanel != null && PassengersPrivateBodyRow[k] != null)
												{
                                                    CimsOnVeh.Add(citizen, VehicleUnits);
													PassengersPrivateBodyRow[k].citizen = citizen;
													PassengersPrivateBodyRow[k].OnVehicle = VehicleID.Vehicle;
													PassengersPrivateBodyRow[k].firstRun = true;
													PassengersPrivateBodyRow[k].Show();
												}
											}
										}
									}
									VehicleUnits = nextUnit;
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
