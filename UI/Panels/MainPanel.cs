using System;
using System.Collections.Generic;
using System.Threading;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class MainPanel : UIPanel
	{
		public static UIButton FavCimsHappinesColText;

		public static UIButton FavCimsNameColText;

		public static UIButton FavCimsEduColText;

		public static UIButton FavCimsWorkingPlaceColText;

		public static UIButton FavCimsAgePhaseColText;

		public static UIButton FavCimsAgeColText;

		public static UIButton FavCimsHomeColText;

		public static UIButton FavCimsLastActColText;

		public static UIButton FavCimsCloseButtonCol;

        private WindowController PanelMover;

        private UITextureSprite FavCimsTitleSprite;

        private UIButton FavCimsBCMenuButton;

        private UITextureSprite FavCimsCBMenuSprite;

        private Texture FavCimsCBETexture;

        private Texture FavCimsCBDTexture;

        private UIPanel CitizensPanel;

        private Texture FavCimsMainBodyTexture;

        private UITextureSprite FavCimsBodySprite;

        public UIScrollbar FavCimsMainPanelScrollBar;

        public static UIScrollablePanel FavCimsCitizenRowsPanel;

        public static bool RowAlternateBackground;

        public static bool ColumnSpecialBackground;

        public static bool RowsAlreadyExist(InstanceID instanceID)
		{
			CitizenRow[] componentsInChildren = FavCimsCitizenRowsPanel.GetComponentsInChildren<CitizenRow>();
			foreach (CitizenRow citizenRow in componentsInChildren)
			{
				if (citizenRow.MyInstanceID == instanceID)
				{
					return true;
				}
			}
			return false;
		}

		private void ReorderRowsBackgrounds()
		{
			object privateVariable = FavCimsCore.GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
			while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
			{
			}
			try
			{
				CitizenRow[] componentsInChildren = FavCimsCitizenRowsPanel.GetComponentsInChildren<CitizenRow>();
				CitizenRow[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					CitizenRow Rows = array[i];
					if (Rows != null && Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite") != null)
					{
						if (Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture != null)
						{
							if (Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture.name.Length > 0)
							{
								Texture FavDot;
								if (!RowAlternateBackground)
								{
									FavDot = ResourceLoader.LoadTexture((int)Rows.width, 40, "UIMainPanel.Rows.bgrow_1.png");
									FavDot.name = "FavDot_1";
									Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
									RowAlternateBackground = true;
								}
								else
								{
									FavDot = ResourceLoader.LoadTexture((int)Rows.width, 40, "UIMainPanel.Rows.bgrow_2.png");
									FavDot.name = "FavDot_2";
									Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
									RowAlternateBackground = false;
								}
								Rows.eventMouseLeave -= delegate(UIComponent component, UIMouseEventParameter eventParam)
								{
									Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
								};
								Rows.eventMouseLeave += delegate(UIComponent component, UIMouseEventParameter eventParam)
								{
									Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
								};
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
                Utils.Debug.Error("Reorder Background Error " + ex.ToString());
			}
			finally
			{
				Monitor.Exit(privateVariable);
			}
		}

		public void Change_Visibility_Event()
		{
			FavCimsBCMenuButton.text = FavCimsLang.Text("FavCimsBCMenuButton_text");
			FavCimsBCMenuButton.tooltip = FavCimsLang.Text("FavCimsBCMenuButton_tooltip");
			FavCimsHappinesColText.text = FavCimsLang.Text("FavCimsHappinesColText_text");
			FavCimsHappinesColText.tooltip = FavCimsLang.Text("FavCimsHappinesColText_tooltip");
			FavCimsNameColText.text = FavCimsLang.Text("FavCimsNameColText_text");
			FavCimsNameColText.tooltip = FavCimsLang.Text("FavCimsNameColText_tooltip");
			FavCimsAgePhaseColText.text = FavCimsLang.Text("FavCimsAgePhaseColText_text");
			FavCimsAgePhaseColText.tooltip = FavCimsLang.Text("FavCimsAgePhaseColText_tooltip");
			FavCimsAgeColText.text = FavCimsLang.Text("FavCimsAgeColText_text");
			FavCimsAgeColText.tooltip = FavCimsLang.Text("FavCimsAgeColText_tooltip");
			FavCimsEduColText.text = FavCimsLang.Text("FavCimsEduColText_text");
			FavCimsEduColText.tooltip = FavCimsLang.Text("FavCimsEduColText_tooltip");
			FavCimsHomeColText.text = FavCimsLang.Text("FavCimsHomeColText_text");
			FavCimsHomeColText.tooltip = FavCimsLang.Text("FavCimsHomeColText_tooltip");
			FavCimsWorkingPlaceColText.text = FavCimsLang.Text("FavCimsWorkingPlaceColText_text");
			FavCimsWorkingPlaceColText.tooltip = FavCimsLang.Text("FavCimsWorkingPlaceColText_tooltip");
			FavCimsLastActColText.text = FavCimsLang.Text("FavCimsLastActColText_text");
			FavCimsLastActColText.tooltip = FavCimsLang.Text("FavCimsLastActColText_tooltip");
			FavCimsCloseButtonCol.text = FavCimsLang.Text("FavCimsCloseButtonCol_text");
			FavCimsCloseButtonCol.tooltip = FavCimsLang.Text("FavCimsCloseButtonCol_tooltip");
		}

		public override void Start()
		{
			UIView aview = UIView.GetAView();
			name = "FavCimsPanel";
			width = 1200f;
			height = 700f;
			opacity = 0.95f;
			eventVisibilityChanged += delegate(UIComponent component, bool value)
			{
                Change_Visibility_Event();
			};
			Texture texture = ResourceLoader.LoadTexture((int)width, (int)height, "UIMainPanel.mainbg.png");
			texture.wrapMode = TextureWrapMode.Clamp;
			texture.filterMode = FilterMode.Bilinear;
			texture.name = "FavCimsMainBGTexture";
			UITextureSprite uitextureSprite = AddUIComponent<UITextureSprite>();
			uitextureSprite.name = "FavCimsMainBGSprite";
			uitextureSprite.texture = texture;
			uitextureSprite.relativePosition = new Vector3(0f, 0f);
			uitextureSprite.eventMouseDown += delegate
			{
				bool mouseButton = Input.GetMouseButton(0);
				if (mouseButton)
				{
					if (GetComponentInChildren<WindowController>() != null)
					{
						PanelMover = GetComponentInChildren<WindowController>();
						PanelMover.ComponentToMove = this;
						PanelMover.Stop = false;
						PanelMover.Start();
					}
					else
					{
						PanelMover = AddUIComponent(typeof(WindowController)) as WindowController;
						PanelMover.ComponentToMove = this;
					}
					opacity = 0.5f;
				}
			};
			uitextureSprite.eventMouseUp += delegate
			{
				if (PanelMover != null)
				{
					PanelMover.Stop = true;
					PanelMover.ComponentToMove = null;
					PanelMover = null;
				}
				opacity = 1f;
			};
			Texture texture2 = ResourceLoader.LoadTexture((int)width, 58, "UIMainPanel.favcimstitle.png");
			texture2.wrapMode = TextureWrapMode.Clamp;
			texture2.filterMode = FilterMode.Bilinear;
			texture2.mipMapBias = -0.5f;
			texture2.name = "FavCimsTitleTexture";
			FavCimsTitleSprite = uitextureSprite.AddUIComponent<UITextureSprite>();
			FavCimsTitleSprite.name = "FavCimsTitleSprite";
			FavCimsTitleSprite.texture = texture2;
			float num = width / 2f - texture2.width / 2f;
			FavCimsTitleSprite.relativePosition = new Vector3(num, 0f);
			UIButton uibutton = AddUIComponent<UIButton>();
			uibutton.name = "FavCimsMenuCloseButton";
			uibutton.width = 32f;
			uibutton.height = 32f;
			uibutton.normalBgSprite = "buttonclose";
			uibutton.hoveredBgSprite = "buttonclosehover";
			uibutton.pressedBgSprite = "buttonclosepressed";
			uibutton.opacity = 1f;
			uibutton.useOutline = true;
			uibutton.playAudioEvents = true;
			uibutton.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
			{
				FavCimsMainClass.FavCimsPanelToggle();
			};
			uibutton.relativePosition = new Vector3(width - uibutton.width * 1.5f, texture2.height / 2f - uibutton.height / 2f);
			Texture texture3 = ResourceLoader.LoadTexture((int)width - 10, 70, "UIMainPanel.submenubar.png");
			texture3.wrapMode = TextureWrapMode.Clamp;
			texture3.filterMode = FilterMode.Bilinear;
			texture3.name = "FavCimsBGMenuTexture";
			UITextureSprite uitextureSprite2 = uitextureSprite.AddUIComponent<UITextureSprite>();
			uitextureSprite2.name = "FavCimsBGMenuSprite";
			uitextureSprite2.texture = texture3;
			float num2 = width / 2f - texture3.width / 2f;
			uitextureSprite2.relativePosition = new Vector3(num2, 58f);
			FavCimsCBETexture = ResourceLoader.LoadTexture(200, 59, "UIMainPanel.citizenbuttonenabled.png");
			FavCimsCBDTexture = ResourceLoader.LoadTexture(200, 59, "UIMainPanel.citizenbuttondisabled.png");
			FavCimsCBETexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsCBETexture.filterMode = FilterMode.Bilinear;
			FavCimsCBETexture.name = "FavCimsCBETexture";
			FavCimsCBETexture.mipMapBias = -0.5f;
			FavCimsCBDTexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsCBDTexture.filterMode = FilterMode.Bilinear;
			FavCimsCBDTexture.name = "FavCimsCBDTexture";
			FavCimsCBDTexture.mipMapBias = -0.5f;
			FavCimsCBMenuSprite = uitextureSprite.AddUIComponent<UITextureSprite>();
			FavCimsCBMenuSprite.name = "FavCimsBGMenuSprite";
			FavCimsCBMenuSprite.texture = FavCimsCBETexture;
			FavCimsBCMenuButton = AddUIComponent<UIButton>();
			FavCimsBCMenuButton.name = "FavCimsBCMenuButton";
			FavCimsBCMenuButton.width = FavCimsCBMenuSprite.width;
			FavCimsBCMenuButton.height = FavCimsCBMenuSprite.height;
			FavCimsBCMenuButton.useOutline = true;
			FavCimsBCMenuButton.playAudioEvents = true;
			FavCimsBCMenuButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsBCMenuButton.textScale = 1.8f;
			FavCimsBCMenuButton.textColor = new Color32(204, 204, 51, 40);
			FavCimsBCMenuButton.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsBCMenuButton.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsBCMenuButton.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsBCMenuButton.textPadding.left = 15;
			FavCimsBCMenuButton.useDropShadow = true;
			FavCimsBCMenuButton.tooltipBox = aview.defaultTooltipBox;
			FavCimsCBMenuSprite.relativePosition = new Vector3(27f, 69f);
			FavCimsBCMenuButton.relativePosition = new Vector3(27f, 69f);
			CitizensPanel = AddUIComponent<UIPanel>();
			CitizensPanel.name = "CitizensPanel";
			CitizensPanel.width = 1190f;
			CitizensPanel.height = 558f;
			CitizensPanel.relativePosition = new Vector3(width / 2f - CitizensPanel.width / 2f, 128f);
			FavCimsMainBodyTexture = ResourceLoader.LoadTexture(1190, 558, "UIMainPanel.bodybg.png");
			FavCimsMainBodyTexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsMainBodyTexture.filterMode = FilterMode.Bilinear;
			FavCimsMainBodyTexture.name = "FavCimsMainBodyTexture";
			FavCimsBodySprite = CitizensPanel.AddUIComponent<UITextureSprite>();
			FavCimsBodySprite.name = "FavCimsCBGBodySprite";
			FavCimsBodySprite.texture = FavCimsMainBodyTexture;
			FavCimsBodySprite.relativePosition = Vector3.zero;
			Texture texture4 = ResourceLoader.LoadTexture(1146, 26, "UIMainPanel.indexerbgbar.png");
			texture4.wrapMode = TextureWrapMode.Clamp;
			texture4.filterMode = FilterMode.Bilinear;
			texture4.name = "FavCimsIndexBgBar";
			texture4.mipMapBias = -0.5f;
			UITextureSprite uitextureSprite3 = CitizensPanel.AddUIComponent<UITextureSprite>();
			uitextureSprite3.name = "FavCimsIndexBgBarSprite";
			uitextureSprite3.texture = texture4;
			uitextureSprite3.relativePosition = new Vector3(21f, 7f);
			FavCimsHappinesColText = CitizensPanel.AddUIComponent<UIButton>();
			FavCimsHappinesColText.name = "FavCimsHappinesColText";
			FavCimsHappinesColText.width = 60f;
			FavCimsHappinesColText.height = texture4.height;
			FavCimsHappinesColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsHappinesColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsHappinesColText.playAudioEvents = true;
			FavCimsHappinesColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsHappinesColText.textScale = 0.7f;
			FavCimsHappinesColText.textColor = new Color32(204, 204, 51, 40);
			FavCimsHappinesColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsHappinesColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsHappinesColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsHappinesColText.textPadding.left = 0;
			FavCimsHappinesColText.tooltipBox = aview.defaultTooltipBox;
			FavCimsHappinesColText.relativePosition = new Vector3(uitextureSprite3.relativePosition.x + 6f, uitextureSprite3.relativePosition.y + 1f);
			FavCimsNameColText = CitizensPanel.AddUIComponent<UIButton>();
			FavCimsNameColText.name = "FavCimsNameColText";
			FavCimsNameColText.width = 180f;
			FavCimsNameColText.height = texture4.height;
			FavCimsNameColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsNameColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsNameColText.playAudioEvents = true;
			FavCimsNameColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsNameColText.textScale = 0.7f;
			FavCimsNameColText.textColor = new Color32(204, 204, 51, 40);
			FavCimsNameColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsNameColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsNameColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsNameColText.textPadding.left = 0;
			FavCimsNameColText.tooltipBox = aview.defaultTooltipBox;
			FavCimsNameColText.relativePosition = new Vector3(FavCimsHappinesColText.relativePosition.x + FavCimsHappinesColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavCimsAgePhaseColText = CitizensPanel.AddUIComponent<UIButton>();
			FavCimsAgePhaseColText.name = "FavCimsAgePhaseColText";
			FavCimsAgePhaseColText.width = 120f;
			FavCimsAgePhaseColText.height = texture4.height;
			FavCimsAgePhaseColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsAgePhaseColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsAgePhaseColText.playAudioEvents = true;
			FavCimsAgePhaseColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsAgePhaseColText.textScale = 0.7f;
			FavCimsAgePhaseColText.textColor = new Color32(204, 204, 51, 40);
			FavCimsAgePhaseColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsAgePhaseColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsAgePhaseColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsAgePhaseColText.textPadding.left = 0;
			FavCimsAgePhaseColText.tooltipBox = aview.defaultTooltipBox;
			FavCimsAgePhaseColText.relativePosition = new Vector3(FavCimsNameColText.relativePosition.x + FavCimsNameColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavCimsAgeColText = CitizensPanel.AddUIComponent<UIButton>();
			FavCimsAgeColText.name = "FavCimsAgeColText";
			FavCimsAgeColText.width = 40f;
			FavCimsAgeColText.height = texture4.height;
			FavCimsAgeColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsAgeColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsAgeColText.playAudioEvents = true;
			FavCimsAgeColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsAgeColText.textScale = 0.7f;
			FavCimsAgeColText.textColor = new Color32(204, 204, 51, 40);
			FavCimsAgeColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsAgeColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsAgeColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsAgeColText.textPadding.left = 0;
			FavCimsAgeColText.tooltipBox = aview.defaultTooltipBox;
			FavCimsAgeColText.relativePosition = new Vector3(FavCimsAgePhaseColText.relativePosition.x + FavCimsAgePhaseColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavCimsEduColText = CitizensPanel.AddUIComponent<UIButton>();
			FavCimsEduColText.name = "FavCimsEduColText";
			FavCimsEduColText.width = 140f;
			FavCimsEduColText.height = texture4.height;
			FavCimsEduColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsEduColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsEduColText.playAudioEvents = true;
			FavCimsEduColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsEduColText.textScale = 0.7f;
			FavCimsEduColText.textColor = new Color32(204, 204, 51, 40);
			FavCimsEduColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsEduColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsEduColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsEduColText.textPadding.left = 0;
			FavCimsEduColText.tooltipBox = aview.defaultTooltipBox;
			FavCimsEduColText.relativePosition = new Vector3(FavCimsAgeColText.relativePosition.x + FavCimsAgeColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavCimsHomeColText = CitizensPanel.AddUIComponent<UIButton>();
			FavCimsHomeColText.name = "FavCimsHomeColText";
			FavCimsHomeColText.width = 184f;
			FavCimsHomeColText.height = texture4.height;
			FavCimsHomeColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsHomeColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsHomeColText.playAudioEvents = true;
			FavCimsHomeColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsHomeColText.textScale = 0.7f;
			FavCimsHomeColText.textColor = new Color32(204, 204, 51, 40);
			FavCimsHomeColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsHomeColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsHomeColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsHomeColText.textPadding.left = 0;
			FavCimsHomeColText.tooltipBox = aview.defaultTooltipBox;
			FavCimsHomeColText.relativePosition = new Vector3(FavCimsEduColText.relativePosition.x + FavCimsEduColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavCimsWorkingPlaceColText = CitizensPanel.AddUIComponent<UIButton>();
			FavCimsWorkingPlaceColText.name = "FavCimsWorkingPlaceColText";
			FavCimsWorkingPlaceColText.width = 180f;
			FavCimsWorkingPlaceColText.height = texture4.height;
			FavCimsWorkingPlaceColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsWorkingPlaceColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsWorkingPlaceColText.playAudioEvents = true;
			FavCimsWorkingPlaceColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsWorkingPlaceColText.textScale = 0.7f;
			FavCimsWorkingPlaceColText.textColor = new Color32(204, 204, 51, 40);
			FavCimsWorkingPlaceColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsWorkingPlaceColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsWorkingPlaceColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsWorkingPlaceColText.textPadding.left = 0;
			FavCimsWorkingPlaceColText.tooltipBox = aview.defaultTooltipBox;
			FavCimsWorkingPlaceColText.relativePosition = new Vector3(FavCimsHomeColText.relativePosition.x + FavCimsHomeColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavCimsLastActColText = CitizensPanel.AddUIComponent<UIButton>();
			FavCimsLastActColText.name = "FavCimsLastActColText";
			FavCimsLastActColText.width = 180f;
			FavCimsLastActColText.height = texture4.height;
			FavCimsLastActColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsLastActColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsLastActColText.playAudioEvents = true;
			FavCimsLastActColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsLastActColText.textScale = 0.7f;
			FavCimsLastActColText.textColor = new Color32(204, 204, 51, 40);
			FavCimsLastActColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsLastActColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsLastActColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsLastActColText.textPadding.left = 0;
			FavCimsLastActColText.tooltipBox = aview.defaultTooltipBox;
			FavCimsLastActColText.relativePosition = new Vector3(FavCimsWorkingPlaceColText.relativePosition.x + FavCimsWorkingPlaceColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavCimsCloseButtonCol = CitizensPanel.AddUIComponent<UIButton>();
			FavCimsCloseButtonCol.name = "FavCimsCloseButtonCol";
			FavCimsCloseButtonCol.width = 50f;
			FavCimsCloseButtonCol.height = texture4.height;
			FavCimsCloseButtonCol.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsCloseButtonCol.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsCloseButtonCol.playAudioEvents = true;
			FavCimsCloseButtonCol.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavCimsCloseButtonCol.textScale = 0.7f;
			FavCimsCloseButtonCol.textColor = new Color32(204, 204, 51, 40);
			FavCimsCloseButtonCol.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavCimsCloseButtonCol.pressedTextColor = new Color32(153, 0, 0, 0);
			FavCimsCloseButtonCol.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavCimsCloseButtonCol.textPadding.right = 6;
			FavCimsCloseButtonCol.tooltipBox = aview.defaultTooltipBox;
			FavCimsCloseButtonCol.relativePosition = new Vector3(FavCimsLastActColText.relativePosition.x + FavCimsLastActColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavCimsCitizenRowsPanel = CitizensPanel.AddUIComponent<UIScrollablePanel>();
			FavCimsCitizenRowsPanel.name = "FavCimsCitizenRowsPanel";
			FavCimsCitizenRowsPanel.width = uitextureSprite3.width - 12f;
			FavCimsCitizenRowsPanel.height = 500f;
			FavCimsCitizenRowsPanel.autoLayoutDirection = LayoutDirection.Vertical;
			FavCimsCitizenRowsPanel.autoLayout = true;
			FavCimsCitizenRowsPanel.clipChildren = true;
			FavCimsCitizenRowsPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
			FavCimsCitizenRowsPanel.relativePosition = new Vector3(uitextureSprite3.relativePosition.x + 6f, uitextureSprite3.relativePosition.y + uitextureSprite3.height);
			UIScrollablePanel uiscrollablePanel = CitizensPanel.AddUIComponent<UIScrollablePanel>();
			uiscrollablePanel.name = "FavCimsCitizenRowsPanelScrollBar";
			uiscrollablePanel.width = 10f;
			uiscrollablePanel.height = 500f;
			uiscrollablePanel.relativePosition = new Vector3(uitextureSprite3.relativePosition.x + uitextureSprite3.width, FavCimsCitizenRowsPanel.relativePosition.y);
			UIScrollbar FavCimsMainPanelScrollBar = uiscrollablePanel.AddUIComponent<UIScrollbar>();
			FavCimsMainPanelScrollBar.width = 10f;
			FavCimsMainPanelScrollBar.height = FavCimsCitizenRowsPanel.height;
			FavCimsMainPanelScrollBar.orientation = UIOrientation.Vertical;
			FavCimsMainPanelScrollBar.pivot = UIPivotPoint.TopRight;
			FavCimsMainPanelScrollBar.AlignTo(FavCimsMainPanelScrollBar.parent, 0);
			FavCimsMainPanelScrollBar.minValue = 0f;
			FavCimsMainPanelScrollBar.value = 0f;
			FavCimsMainPanelScrollBar.incrementAmount = 40f;
			UISlicedSprite uislicedSprite = FavCimsMainPanelScrollBar.AddUIComponent<UISlicedSprite>();
			uislicedSprite.relativePosition = FavCimsMainPanelScrollBar.relativePosition;
			uislicedSprite.autoSize = true;
			uislicedSprite.size = uislicedSprite.parent.size;
			uislicedSprite.fillDirection = UIFillDirection.Vertical;
			uislicedSprite.spriteName = "ScrollbarTrack";
			FavCimsMainPanelScrollBar.trackObject = uislicedSprite;
			UISlicedSprite uislicedSprite2 = FavCimsMainPanelScrollBar.AddUIComponent<UISlicedSprite>();
			uislicedSprite2.relativePosition = FavCimsMainPanelScrollBar.relativePosition;
			uislicedSprite2.autoSize = true;
			uislicedSprite2.width = uislicedSprite2.parent.width;
			uislicedSprite2.fillDirection = UIFillDirection.Vertical;
			uislicedSprite2.spriteName = "ScrollbarThumb";
			FavCimsMainPanelScrollBar.thumbObject = uislicedSprite2;
			FavCimsCitizenRowsPanel.verticalScrollbar = FavCimsMainPanelScrollBar;
			FavCimsCitizenRowsPanel.eventMouseWheel += delegate(UIComponent component, UIMouseEventParameter eventParam)
			{
				int sign = Math.Sign(eventParam.wheelDelta);
				FavCimsCitizenRowsPanel.scrollPosition += new Vector2(0f, (sign * -1) * FavCimsMainPanelScrollBar.incrementAmount);
			};
			FavCimsCitizenRowsPanel.eventComponentAdded += delegate(UIComponent component, UIComponent eventParam)
			{
				ReorderRowsBackgrounds();
			};
			FavCimsCitizenRowsPanel.eventComponentRemoved += delegate(UIComponent component, UIComponent eventParam)
			{
				ReorderRowsBackgrounds();
			};
			UITextureSprite uitextureSprite4 = CitizensPanel.AddUIComponent<UITextureSprite>();
			uitextureSprite4.name = "FavCimsFooterBgBarSprite";
			uitextureSprite4.width = uitextureSprite3.width;
			uitextureSprite4.height = 15f;
			uitextureSprite4.texture = texture4;
			uitextureSprite4.relativePosition = new Vector3(21f, FavCimsCitizenRowsPanel.relativePosition.y + FavCimsCitizenRowsPanel.height);
			foreach (KeyValuePair<InstanceID, string> keyValuePair in FavCimsCore.FavoriteCimsList())
			{
				if (keyValuePair.Key.Type == InstanceType.Citizen)
				{
					CitizenRow citizenRow = FavCimsCitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
					citizenRow.MyInstanceID = keyValuePair.Key;
					citizenRow.MyInstancedName = keyValuePair.Value;
				}
			}
		}
	}
}
