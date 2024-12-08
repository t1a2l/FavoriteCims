using AlgernonCommons.Translation;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class MainPanel : UIPanel
	{
		public static UIButton HappinesColText;

		public static UIButton NameColText;

		public static UIButton EduColText;

		public static UIButton WorkingPlaceColText;

		public static UIButton AgePhaseColText;

		public static UIButton AgeColText;

		public static UIButton HomeColText;

		public static UIButton LastActColText;

		public static UIButton CloseButtonCol;

        private WindowController PanelMover;

        private UITextureSprite TitleSprite;

        private UIButton BCMenuButton;

        private UITextureSprite CBMenuSprite;

        private Texture CBETexture;

        private Texture CBDTexture;

        private UIPanel CitizensPanel;

        private Texture MainBodyTexture;

        private UITextureSprite BodySprite;

        public UIScrollbar MainPanelScrollBar;

        public static UIScrollablePanel CitizenRowsPanel;

        public static bool RowAlternateBackground;

        public static bool ColumnSpecialBackground;

        public static bool RowsAlreadyExist(InstanceID instanceID)
		{
			CitizenRow[] componentsInChildren = CitizenRowsPanel.GetComponentsInChildren<CitizenRow>();
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
				CitizenRow[] componentsInChildren = CitizenRowsPanel.GetComponentsInChildren<CitizenRow>();
				CitizenRow[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					CitizenRow Rows = array[i];
					if (Rows != null && Rows.Find<UITextureSprite>("CitizenSingleRowBGSprite") != null)
					{
						if (Rows.Find<UITextureSprite>("CitizenSingleRowBGSprite").texture != null)
						{
							if (Rows.Find<UITextureSprite>("CitizenSingleRowBGSprite").texture.name.Length > 0)
							{
								Texture FavDot;
								if (!RowAlternateBackground)
								{
									FavDot = ResourceLoader.LoadTexture((int)Rows.width, 40, "UIMainPanel.Rows.bgrow_1.png");
									FavDot.name = "FavDot_1";
									Rows.Find<UITextureSprite>("CitizenSingleRowBGSprite").texture = FavDot;
									RowAlternateBackground = true;
								}
								else
								{
									FavDot = ResourceLoader.LoadTexture((int)Rows.width, 40, "UIMainPanel.Rows.bgrow_2.png");
									FavDot.name = "FavDot_2";
									Rows.Find<UITextureSprite>("CitizenSingleRowBGSprite").texture = FavDot;
									RowAlternateBackground = false;
								}
								Rows.eventMouseLeave -= delegate(UIComponent component, UIMouseEventParameter eventParam)
								{
									Rows.Find<UITextureSprite>("CitizenSingleRowBGSprite").texture = FavDot;
								};
								Rows.eventMouseLeave += delegate(UIComponent component, UIMouseEventParameter eventParam)
								{
									Rows.Find<UITextureSprite>("CitizenSingleRowBGSprite").texture = FavDot;
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
			BCMenuButton.text = Translations.Translate("BCMenuButton_text");
			BCMenuButton.tooltip =Translations.Translate("BCMenuButton_tooltip");
			HappinesColText.text = Translations.Translate("HappinesColText_text");
			HappinesColText.tooltip = Translations.Translate("HappinesColText_tooltip");
			NameColText.text = Translations.Translate("NameColText_text");
			NameColText.tooltip = Translations.Translate("NameColText_tooltip");
			AgePhaseColText.text = Translations.Translate("AgePhaseColText_text");
			AgePhaseColText.tooltip = Translations.Translate("AgePhaseColText_tooltip");
			AgeColText.text = Translations.Translate("AgeColText_text");
			AgeColText.tooltip = Translations.Translate("AgeColText_tooltip");
			EduColText.text = Translations.Translate("EduColText_text");
			EduColText.tooltip = Translations.Translate("EduColText_tooltip");
			HomeColText.text = Translations.Translate("HomeColText_text");
			HomeColText.tooltip = Translations.Translate("HomeColText_tooltip");
			WorkingPlaceColText.text = Translations.Translate("WorkingPlaceColText_text");
			WorkingPlaceColText.tooltip = Translations.Translate("WorkingPlaceColText_tooltip");
			LastActColText.text = Translations.Translate("LastActColText_text");
			LastActColText.tooltip = Translations.Translate("LastActColText_tooltip");
			CloseButtonCol.text = Translations.Translate("CloseButtonCol_text");
			CloseButtonCol.tooltip = Translations.Translate("CloseButtonCol_tooltip");
		}

		public override void Start()
		{
			UIView aview = UIView.GetAView();
			name = "Panel";
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
			texture.name = "MainBGTexture";
			UITextureSprite uitextureSprite = AddUIComponent<UITextureSprite>();
			uitextureSprite.name = "MainBGSprite";
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
			Texture texture2 = ResourceLoader.LoadTexture((int)width, 58, "UIMainPanel.title.png");
			texture2.wrapMode = TextureWrapMode.Clamp;
			texture2.filterMode = FilterMode.Bilinear;
			texture2.mipMapBias = -0.5f;
			texture2.name = "TitleTexture";
			TitleSprite = uitextureSprite.AddUIComponent<UITextureSprite>();
			TitleSprite.name = "TitleSprite";
			TitleSprite.texture = texture2;
			float num = width / 2f - texture2.width / 2f;
			TitleSprite.relativePosition = new Vector3(num, 0f);
			UIButton uibutton = AddUIComponent<UIButton>();
			uibutton.name = "MenuCloseButton";
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
				MainClass.PanelToggle();
			};
			uibutton.relativePosition = new Vector3(width - uibutton.width * 1.5f, texture2.height / 2f - uibutton.height / 2f);
			Texture texture3 = ResourceLoader.LoadTexture((int)width - 10, 70, "UIMainPanel.submenubar.png");
			texture3.wrapMode = TextureWrapMode.Clamp;
			texture3.filterMode = FilterMode.Bilinear;
			texture3.name = "BGMenuTexture";
			UITextureSprite uitextureSprite2 = uitextureSprite.AddUIComponent<UITextureSprite>();
			uitextureSprite2.name = "BGMenuSprite";
			uitextureSprite2.texture = texture3;
			float num2 = width / 2f - texture3.width / 2f;
			uitextureSprite2.relativePosition = new Vector3(num2, 58f);
			CBETexture = ResourceLoader.LoadTexture(200, 59, "UIMainPanel.citizenbuttonenabled.png");
			CBDTexture = ResourceLoader.LoadTexture(200, 59, "UIMainPanel.citizenbuttondisabled.png");
			CBETexture.wrapMode = TextureWrapMode.Clamp;
			CBETexture.filterMode = FilterMode.Bilinear;
			CBETexture.name = "CBETexture";
			CBETexture.mipMapBias = -0.5f;
			CBDTexture.wrapMode = TextureWrapMode.Clamp;
			CBDTexture.filterMode = FilterMode.Bilinear;
			CBDTexture.name = "CBDTexture";
			CBDTexture.mipMapBias = -0.5f;
			CBMenuSprite = uitextureSprite.AddUIComponent<UITextureSprite>();
			CBMenuSprite.name = "BGMenuSprite";
			CBMenuSprite.texture = CBETexture;
			BCMenuButton = AddUIComponent<UIButton>();
			BCMenuButton.name = "BCMenuButton";
			BCMenuButton.width = CBMenuSprite.width;
			BCMenuButton.height = CBMenuSprite.height;
			BCMenuButton.useOutline = true;
			BCMenuButton.playAudioEvents = true;
			BCMenuButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
			BCMenuButton.textScale = 1.8f;
			BCMenuButton.textColor = new Color32(204, 204, 51, 40);
			BCMenuButton.hoveredTextColor = new Color32(204, 102, 0, 20);
			BCMenuButton.pressedTextColor = new Color32(153, 0, 0, 0);
			BCMenuButton.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			BCMenuButton.textPadding.left = 15;
			BCMenuButton.useDropShadow = true;
			BCMenuButton.tooltipBox = aview.defaultTooltipBox;
			CBMenuSprite.relativePosition = new Vector3(27f, 69f);
			BCMenuButton.relativePosition = new Vector3(27f, 69f);
			CitizensPanel = AddUIComponent<UIPanel>();
			CitizensPanel.name = "CitizensPanel";
			CitizensPanel.width = 1190f;
			CitizensPanel.height = 558f;
			CitizensPanel.relativePosition = new Vector3(width / 2f - CitizensPanel.width / 2f, 128f);
			MainBodyTexture = ResourceLoader.LoadTexture(1190, 558, "UIMainPanel.bodybg.png");
			MainBodyTexture.wrapMode = TextureWrapMode.Clamp;
			MainBodyTexture.filterMode = FilterMode.Bilinear;
			MainBodyTexture.name = "MainBodyTexture";
			BodySprite = CitizensPanel.AddUIComponent<UITextureSprite>();
			BodySprite.name = "CBGBodySprite";
			BodySprite.texture = MainBodyTexture;
			BodySprite.relativePosition = Vector3.zero;
			Texture texture4 = ResourceLoader.LoadTexture(1146, 26, "UIMainPanel.indexerbgbar.png");
			texture4.wrapMode = TextureWrapMode.Clamp;
			texture4.filterMode = FilterMode.Bilinear;
			texture4.name = "IndexBgBar";
			texture4.mipMapBias = -0.5f;
			UITextureSprite uitextureSprite3 = CitizensPanel.AddUIComponent<UITextureSprite>();
			uitextureSprite3.name = "IndexBgBarSprite";
			uitextureSprite3.texture = texture4;
			uitextureSprite3.relativePosition = new Vector3(21f, 7f);
			HappinesColText = CitizensPanel.AddUIComponent<UIButton>();
			HappinesColText.name = "HappinesColText";
			HappinesColText.width = 60f;
			HappinesColText.height = texture4.height;
			HappinesColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			HappinesColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			HappinesColText.playAudioEvents = true;
			HappinesColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			HappinesColText.textScale = 0.7f;
			HappinesColText.textColor = new Color32(204, 204, 51, 40);
			HappinesColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			HappinesColText.pressedTextColor = new Color32(153, 0, 0, 0);
			HappinesColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			HappinesColText.textPadding.left = 0;
			HappinesColText.tooltipBox = aview.defaultTooltipBox;
			HappinesColText.relativePosition = new Vector3(uitextureSprite3.relativePosition.x + 6f, uitextureSprite3.relativePosition.y + 1f);
			NameColText = CitizensPanel.AddUIComponent<UIButton>();
			NameColText.name = "NameColText";
			NameColText.width = 180f;
			NameColText.height = texture4.height;
			NameColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			NameColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			NameColText.playAudioEvents = true;
			NameColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			NameColText.textScale = 0.7f;
			NameColText.textColor = new Color32(204, 204, 51, 40);
			NameColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			NameColText.pressedTextColor = new Color32(153, 0, 0, 0);
			NameColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			NameColText.textPadding.left = 0;
			NameColText.tooltipBox = aview.defaultTooltipBox;
			NameColText.relativePosition = new Vector3(HappinesColText.relativePosition.x + HappinesColText.width, uitextureSprite3.relativePosition.y + 1f);
			AgePhaseColText = CitizensPanel.AddUIComponent<UIButton>();
			AgePhaseColText.name = "AgePhaseColText";
			AgePhaseColText.width = 120f;
			AgePhaseColText.height = texture4.height;
			AgePhaseColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			AgePhaseColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			AgePhaseColText.playAudioEvents = true;
			AgePhaseColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			AgePhaseColText.textScale = 0.7f;
			AgePhaseColText.textColor = new Color32(204, 204, 51, 40);
			AgePhaseColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			AgePhaseColText.pressedTextColor = new Color32(153, 0, 0, 0);
			AgePhaseColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			AgePhaseColText.textPadding.left = 0;
			AgePhaseColText.tooltipBox = aview.defaultTooltipBox;
			AgePhaseColText.relativePosition = new Vector3(NameColText.relativePosition.x + NameColText.width, uitextureSprite3.relativePosition.y + 1f);
			AgeColText = CitizensPanel.AddUIComponent<UIButton>();
			AgeColText.name = "AgeColText";
			AgeColText.width = 40f;
			AgeColText.height = texture4.height;
			AgeColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			AgeColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			AgeColText.playAudioEvents = true;
			AgeColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			AgeColText.textScale = 0.7f;
			AgeColText.textColor = new Color32(204, 204, 51, 40);
			AgeColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			AgeColText.pressedTextColor = new Color32(153, 0, 0, 0);
			AgeColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			AgeColText.textPadding.left = 0;
			AgeColText.tooltipBox = aview.defaultTooltipBox;
			AgeColText.relativePosition = new Vector3(AgePhaseColText.relativePosition.x + AgePhaseColText.width, uitextureSprite3.relativePosition.y + 1f);
			EduColText = CitizensPanel.AddUIComponent<UIButton>();
			EduColText.name = "EduColText";
			EduColText.width = 140f;
			EduColText.height = texture4.height;
			EduColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			EduColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			EduColText.playAudioEvents = true;
			EduColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			EduColText.textScale = 0.7f;
			EduColText.textColor = new Color32(204, 204, 51, 40);
			EduColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			EduColText.pressedTextColor = new Color32(153, 0, 0, 0);
			EduColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			EduColText.textPadding.left = 0;
			EduColText.tooltipBox = aview.defaultTooltipBox;
			EduColText.relativePosition = new Vector3(AgeColText.relativePosition.x + AgeColText.width, uitextureSprite3.relativePosition.y + 1f);
			HomeColText = CitizensPanel.AddUIComponent<UIButton>();
			HomeColText.name = "HomeColText";
			HomeColText.width = 184f;
			HomeColText.height = texture4.height;
			HomeColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			HomeColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			HomeColText.playAudioEvents = true;
			HomeColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			HomeColText.textScale = 0.7f;
			HomeColText.textColor = new Color32(204, 204, 51, 40);
			HomeColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			HomeColText.pressedTextColor = new Color32(153, 0, 0, 0);
			HomeColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			HomeColText.textPadding.left = 0;
			HomeColText.tooltipBox = aview.defaultTooltipBox;
			HomeColText.relativePosition = new Vector3(EduColText.relativePosition.x + EduColText.width, uitextureSprite3.relativePosition.y + 1f);
			WorkingPlaceColText = CitizensPanel.AddUIComponent<UIButton>();
			WorkingPlaceColText.name = "WorkingPlaceColText";
			WorkingPlaceColText.width = 180f;
			WorkingPlaceColText.height = texture4.height;
			WorkingPlaceColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			WorkingPlaceColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			WorkingPlaceColText.playAudioEvents = true;
			WorkingPlaceColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			WorkingPlaceColText.textScale = 0.7f;
			WorkingPlaceColText.textColor = new Color32(204, 204, 51, 40);
			WorkingPlaceColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			WorkingPlaceColText.pressedTextColor = new Color32(153, 0, 0, 0);
			WorkingPlaceColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			WorkingPlaceColText.textPadding.left = 0;
			WorkingPlaceColText.tooltipBox = aview.defaultTooltipBox;
			WorkingPlaceColText.relativePosition = new Vector3(HomeColText.relativePosition.x + HomeColText.width, uitextureSprite3.relativePosition.y + 1f);
			LastActColText = CitizensPanel.AddUIComponent<UIButton>();
			LastActColText.name = "LastActColText";
			LastActColText.width = 180f;
			LastActColText.height = texture4.height;
			LastActColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			LastActColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			LastActColText.playAudioEvents = true;
			LastActColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			LastActColText.textScale = 0.7f;
			LastActColText.textColor = new Color32(204, 204, 51, 40);
			LastActColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			LastActColText.pressedTextColor = new Color32(153, 0, 0, 0);
			LastActColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			LastActColText.textPadding.left = 0;
			LastActColText.tooltipBox = aview.defaultTooltipBox;
			LastActColText.relativePosition = new Vector3(WorkingPlaceColText.relativePosition.x + WorkingPlaceColText.width, uitextureSprite3.relativePosition.y + 1f);
			CloseButtonCol = CitizensPanel.AddUIComponent<UIButton>();
			CloseButtonCol.name = "CloseButtonCol";
			CloseButtonCol.width = 50f;
			CloseButtonCol.height = texture4.height;
			CloseButtonCol.textVerticalAlignment = UIVerticalAlignment.Middle;
			CloseButtonCol.textHorizontalAlignment = UIHorizontalAlignment.Center;
			CloseButtonCol.playAudioEvents = true;
			CloseButtonCol.font = UIDynamicFont.FindByName("OpenSans-Regular");
			CloseButtonCol.textScale = 0.7f;
			CloseButtonCol.textColor = new Color32(204, 204, 51, 40);
			CloseButtonCol.hoveredTextColor = new Color32(204, 102, 0, 20);
			CloseButtonCol.pressedTextColor = new Color32(153, 0, 0, 0);
			CloseButtonCol.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			CloseButtonCol.textPadding.right = 6;
			CloseButtonCol.tooltipBox = aview.defaultTooltipBox;
			CloseButtonCol.relativePosition = new Vector3(LastActColText.relativePosition.x + LastActColText.width, uitextureSprite3.relativePosition.y + 1f);
			CitizenRowsPanel = CitizensPanel.AddUIComponent<UIScrollablePanel>();
			CitizenRowsPanel.name = "CitizenRowsPanel";
			CitizenRowsPanel.width = uitextureSprite3.width - 12f;
			CitizenRowsPanel.height = 500f;
			CitizenRowsPanel.autoLayoutDirection = LayoutDirection.Vertical;
			CitizenRowsPanel.autoLayout = true;
			CitizenRowsPanel.clipChildren = true;
			CitizenRowsPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
			CitizenRowsPanel.relativePosition = new Vector3(uitextureSprite3.relativePosition.x + 6f, uitextureSprite3.relativePosition.y + uitextureSprite3.height);
			UIScrollablePanel uiscrollablePanel = CitizensPanel.AddUIComponent<UIScrollablePanel>();
			uiscrollablePanel.name = "CitizenRowsPanelScrollBar";
			uiscrollablePanel.width = 10f;
			uiscrollablePanel.height = 500f;
			uiscrollablePanel.relativePosition = new Vector3(uitextureSprite3.relativePosition.x + uitextureSprite3.width, CitizenRowsPanel.relativePosition.y);
			UIScrollbar MainPanelScrollBar = uiscrollablePanel.AddUIComponent<UIScrollbar>();
			MainPanelScrollBar.width = 10f;
			MainPanelScrollBar.height = CitizenRowsPanel.height;
			MainPanelScrollBar.orientation = UIOrientation.Vertical;
			MainPanelScrollBar.pivot = UIPivotPoint.TopRight;
			MainPanelScrollBar.AlignTo(MainPanelScrollBar.parent, 0);
			MainPanelScrollBar.minValue = 0f;
			MainPanelScrollBar.value = 0f;
			MainPanelScrollBar.incrementAmount = 40f;
			UISlicedSprite uislicedSprite = MainPanelScrollBar.AddUIComponent<UISlicedSprite>();
			uislicedSprite.relativePosition = MainPanelScrollBar.relativePosition;
			uislicedSprite.autoSize = true;
			uislicedSprite.size = uislicedSprite.parent.size;
			uislicedSprite.fillDirection = UIFillDirection.Vertical;
			uislicedSprite.spriteName = "ScrollbarTrack";
			MainPanelScrollBar.trackObject = uislicedSprite;
			UISlicedSprite uislicedSprite2 = MainPanelScrollBar.AddUIComponent<UISlicedSprite>();
			uislicedSprite2.relativePosition = MainPanelScrollBar.relativePosition;
			uislicedSprite2.autoSize = true;
			uislicedSprite2.width = uislicedSprite2.parent.width;
			uislicedSprite2.fillDirection = UIFillDirection.Vertical;
			uislicedSprite2.spriteName = "ScrollbarThumb";
			MainPanelScrollBar.thumbObject = uislicedSprite2;
			CitizenRowsPanel.verticalScrollbar = MainPanelScrollBar;
			CitizenRowsPanel.eventMouseWheel += delegate(UIComponent component, UIMouseEventParameter eventParam)
			{
				int sign = Math.Sign(eventParam.wheelDelta);
				CitizenRowsPanel.scrollPosition += new Vector2(0f, (sign * -1) * MainPanelScrollBar.incrementAmount);
			};
			CitizenRowsPanel.eventComponentAdded += delegate(UIComponent component, UIComponent eventParam)
			{
				ReorderRowsBackgrounds();
			};
			CitizenRowsPanel.eventComponentRemoved += delegate(UIComponent component, UIComponent eventParam)
			{
				ReorderRowsBackgrounds();
			};
			UITextureSprite uitextureSprite4 = CitizensPanel.AddUIComponent<UITextureSprite>();
			uitextureSprite4.name = "FooterBgBarSprite";
			uitextureSprite4.width = uitextureSprite3.width;
			uitextureSprite4.height = 15f;
			uitextureSprite4.texture = texture4;
			uitextureSprite4.relativePosition = new Vector3(21f, CitizenRowsPanel.relativePosition.y + CitizenRowsPanel.height);
			foreach (KeyValuePair<InstanceID, string> keyValuePair in FavCimsCore.FavoriteCimsList())
			{
				if (keyValuePair.Key.Type == InstanceType.Citizen)
				{
					CitizenRow citizenRow = CitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
					citizenRow.MyInstanceID = keyValuePair.Key;
					citizenRow.MyInstancedName = keyValuePair.Value;
				}
			}
		}
	}
}
