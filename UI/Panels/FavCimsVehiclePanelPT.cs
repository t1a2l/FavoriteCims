using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class FavCimsVehiclePanelPT : UIPanel
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

        public static Dictionary<uint, uint> CimsOnPTVeh = [];

        const int MaxPassengersUnit = 20;

        private UIPanel PassengersPanel;

        private UIPanel PassengersPanelSubRow;

        private UIButton PassengersPanelIcon;

        private UIButton PassengersPanelText;

        private readonly PassengersVehiclePanelRow[] PassengersBodyRow = new PassengersVehiclePanelRow[MaxPassengersUnit * 5];

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
				BodyRows = BodySpriteBg.AddUIComponent<UIScrollablePanel>();
				BodyRows.name = "BodyRows";
				BodyRows.width = BodySpriteBg.width - 24f;
				BodyRows.autoLayoutDirection = LayoutDirection.Vertical;
				BodyRows.autoLayout = true;
				BodyRows.relativePosition = new Vector3(12f, 0f);
				PassengersPanel = BodyRows.AddUIComponent<UIPanel>();
				PassengersPanel.width = 226f;
				PassengersPanel.height = 25f;
				PassengersPanel.name = "LabelPanel_PT_0";
				PassengersPanel.autoLayoutDirection = LayoutDirection.Vertical;
				PassengersPanel.autoLayout = true;
				PassengersPanel.Hide();
				PassengersPanelSubRow = PassengersPanel.AddUIComponent<UIPanel>();
				PassengersPanelSubRow.width = 226f;
				PassengersPanelSubRow.height = 25f;
				PassengersPanelSubRow.name = "TitlePanel_PT_0";
				PassengersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
				PassengersPanelSubRow.backgroundSprite = "bg_row2";
				PassengersPanelIcon = PassengersPanelSubRow.AddUIComponent<UIButton>();
				PassengersPanelIcon.name = "LabelPanelIcon_PT_0";
				PassengersPanelIcon.width = 17f;
				PassengersPanelIcon.height = 17f;
				PassengersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
				PassengersPanelIcon.normalFgSprite = "passengerIcon";
				PassengersPanelIcon.relativePosition = new Vector3(5f, 4f);
				PassengersPanelText = PassengersPanelSubRow.AddUIComponent<UIButton>();
				PassengersPanelText.name = "LabelPanelText_PT_0";
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
				int row = 0;
				for (int i = 0; i < MaxPassengersUnit * 5; i++)
				{
					PassengersBodyRow[row] = BodyRows.AddUIComponent(typeof(PassengersVehiclePanelRow)) as PassengersVehiclePanelRow;
					PassengersBodyRow[row].name = "RowPanel_PT_" + i.ToString();
					PassengersBodyRow[row].OnVehicle = 0;
					PassengersBodyRow[row].citizen = 0U;
					PassengersBodyRow[row].Hide();
                    row++;
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
                        CimsOnPTVeh.Clear();
						try
						{
							PassengersPanel.Hide();
							for (int i = 0; i < MaxPassengersUnit * 5; i++)
							{
								PassengersBodyRow[i].Hide();
								PassengersBodyRow[i].citizen = 0U;
								PassengersBodyRow[i].OnVehicle = 0;
								PassengersBodyRow[i].firstRun = true;
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
                            CimsOnPTVeh.Clear();
							try
							{
								PassengersPanel.Hide();
								for (int j = 0; j < MaxPassengersUnit * 5; j++)
								{
									PassengersBodyRow[j].Hide();
									PassengersBodyRow[j].citizen = 0U;
									PassengersBodyRow[j].OnVehicle = 0;
									PassengersBodyRow[j].firstRun = true;
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
								VehicleUnits = MyVehicle.m_vehicles.m_buffer[VehicleID.Vehicle].m_citizenUnits;
								int unitnum = 0;
								int rownum = 0;
								if (CimsOnPTVeh.Count == 0)
								{
									PassengersPanelText.text = FavCimsLang.Text("View_NoPassengers");
								}
								PassengersPanel.Show();
								while (VehicleUnits != 0U && unitnum < MaxPassengersUnit)
								{
									uint nextUnit = MyCitizen.m_units.m_buffer[(int)VehicleUnits].m_nextUnit;
									for (int k = 0; k < 5; k++)
									{
										uint citizen = MyCitizen.m_units.m_buffer[(int)VehicleUnits].GetCitizen(k);
										if (citizen != 0U && !CimsOnPTVeh.ContainsKey(citizen))
										{
											PassengersPanelText.text = FavCimsLang.Text("Vehicle_PasssengerIconText");
											if (PassengersPanel != null && PassengersBodyRow[unitnum] != null)
											{
												if (PassengersBodyRow[rownum].citizen != 0U && CimsOnPTVeh.ContainsKey(PassengersBodyRow[rownum].citizen))
												{
                                                    Wait = true;
                                                    CimsOnPTVeh.Remove(PassengersBodyRow[rownum].citizen);
												}
                                                CimsOnPTVeh.Add(citizen, VehicleUnits);
												PassengersBodyRow[rownum].citizen = citizen;
												PassengersBodyRow[rownum].OnVehicle = VehicleID.Vehicle;
												PassengersBodyRow[rownum].firstRun = true;
												PassengersBodyRow[rownum].Show();
												bool wait = Wait;
												if (wait)
												{
                                                    Wait = false;
												}
											}
										}
                                        rownum++;
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
