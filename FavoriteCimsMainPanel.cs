using System;
using System.Collections.Generic;
using System.Threading;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
	public class FavoriteCimsMainPanel : UIPanel
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
			CitizenRow[] componentsInChildren = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.GetComponentsInChildren<CitizenRow>();
			foreach (CitizenRow citizenRow in componentsInChildren)
			{
				bool flag = citizenRow.MyInstanceID == instanceID;
				if (flag)
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
				CitizenRow[] componentsInChildren = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.GetComponentsInChildren<CitizenRow>();
				CitizenRow[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					CitizenRow Rows = array[i];
					bool flag = Rows != null && Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite") != null;
					if (flag)
					{
						bool flag2 = Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture != null;
						if (flag2)
						{
							bool flag3 = Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture.name.Length > 0;
							if (flag3)
							{
								bool flag4 = !FavoriteCimsMainPanel.RowAlternateBackground;
								Texture FavDot;
								if (flag4)
								{
									FavDot = ResourceLoader.LoadTexture((int)Rows.width, 40, "UIMainPanel.Rows.bgrow_1.png");
									FavDot.name = "FavDot_1";
									Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
									FavoriteCimsMainPanel.RowAlternateBackground = true;
								}
								else
								{
									FavDot = ResourceLoader.LoadTexture((int)Rows.width, 40, "UIMainPanel.Rows.bgrow_2.png");
									FavDot.name = "FavDot_2";
									Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
									FavoriteCimsMainPanel.RowAlternateBackground = false;
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
				Debug.Error("Reorder Background Error " + ex.ToString());
			}
			finally
			{
				Monitor.Exit(privateVariable);
			}
		}

		public void change_visibility_event()
		{
			this.FavCimsBCMenuButton.text = FavCimsLang.text("FavCimsBCMenuButton_text");
			this.FavCimsBCMenuButton.tooltip = FavCimsLang.text("FavCimsBCMenuButton_tooltip");
			FavoriteCimsMainPanel.FavCimsHappinesColText.text = FavCimsLang.text("FavCimsHappinesColText_text");
			FavoriteCimsMainPanel.FavCimsHappinesColText.tooltip = FavCimsLang.text("FavCimsHappinesColText_tooltip");
			FavoriteCimsMainPanel.FavCimsNameColText.text = FavCimsLang.text("FavCimsNameColText_text");
			FavoriteCimsMainPanel.FavCimsNameColText.tooltip = FavCimsLang.text("FavCimsNameColText_tooltip");
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.text = FavCimsLang.text("FavCimsAgePhaseColText_text");
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.tooltip = FavCimsLang.text("FavCimsAgePhaseColText_tooltip");
			FavoriteCimsMainPanel.FavCimsAgeColText.text = FavCimsLang.text("FavCimsAgeColText_text");
			FavoriteCimsMainPanel.FavCimsAgeColText.tooltip = FavCimsLang.text("FavCimsAgeColText_tooltip");
			FavoriteCimsMainPanel.FavCimsEduColText.text = FavCimsLang.text("FavCimsEduColText_text");
			FavoriteCimsMainPanel.FavCimsEduColText.tooltip = FavCimsLang.text("FavCimsEduColText_tooltip");
			FavoriteCimsMainPanel.FavCimsHomeColText.text = FavCimsLang.text("FavCimsHomeColText_text");
			FavoriteCimsMainPanel.FavCimsHomeColText.tooltip = FavCimsLang.text("FavCimsHomeColText_tooltip");
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.text = FavCimsLang.text("FavCimsWorkingPlaceColText_text");
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.tooltip = FavCimsLang.text("FavCimsWorkingPlaceColText_tooltip");
			FavoriteCimsMainPanel.FavCimsLastActColText.text = FavCimsLang.text("FavCimsLastActColText_text");
			FavoriteCimsMainPanel.FavCimsLastActColText.tooltip = FavCimsLang.text("FavCimsLastActColText_tooltip");
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.text = FavCimsLang.text("FavCimsCloseButtonCol_text");
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.tooltip = FavCimsLang.text("FavCimsCloseButtonCol_tooltip");
		}

		public override void Start()
		{
			UIView aview = UIView.GetAView();
			base.name = "FavCimsPanel";
			base.width = 1200f;
			base.height = 700f;
			base.opacity = 0.95f;
			base.eventVisibilityChanged += delegate(UIComponent component, bool value)
			{
				this.change_visibility_event();
			};
			Texture texture = ResourceLoader.LoadTexture((int)base.width, (int)base.height, "UIMainPanel.mainbg.png");
			texture.wrapMode = TextureWrapMode.Clamp;
			texture.filterMode = FilterMode.Bilinear;
			texture.name = "FavCimsMainBGTexture";
			UITextureSprite uitextureSprite = base.AddUIComponent<UITextureSprite>();
			uitextureSprite.name = "FavCimsMainBGSprite";
			uitextureSprite.texture = texture;
			uitextureSprite.relativePosition = new Vector3(0f, 0f);
			uitextureSprite.eventMouseDown += delegate
			{
				bool mouseButton = Input.GetMouseButton(0);
				if (mouseButton)
				{
					bool flag2 = this.GetComponentInChildren<WindowController>() != null;
					if (flag2)
					{
						this.PanelMover = this.GetComponentInChildren<WindowController>();
						this.PanelMover.ComponentToMove = this;
						this.PanelMover.Stop = false;
						this.PanelMover.Start();
					}
					else
					{
						this.PanelMover = this.AddUIComponent(typeof(WindowController)) as WindowController;
						this.PanelMover.ComponentToMove = this;
					}
					this.opacity = 0.5f;
				}
			};
			uitextureSprite.eventMouseUp += delegate
			{
				bool flag3 = this.PanelMover != null;
				if (flag3)
				{
					this.PanelMover.Stop = true;
					this.PanelMover.ComponentToMove = null;
					this.PanelMover = null;
				}
				this.opacity = 1f;
			};
			Texture texture2 = ResourceLoader.LoadTexture((int)base.width, 58, "UIMainPanel.favcimstitle.png");
			texture2.wrapMode = TextureWrapMode.Clamp;
			texture2.filterMode = FilterMode.Bilinear;
			texture2.mipMapBias = -0.5f;
			texture2.name = "FavCimsTitleTexture";
			this.FavCimsTitleSprite = uitextureSprite.AddUIComponent<UITextureSprite>();
			this.FavCimsTitleSprite.name = "FavCimsTitleSprite";
			this.FavCimsTitleSprite.texture = texture2;
			float num = base.width / 2f - (float)texture2.width / 2f;
			this.FavCimsTitleSprite.relativePosition = new Vector3(num, 0f);
			UIButton uibutton = base.AddUIComponent<UIButton>();
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
			uibutton.relativePosition = new Vector3(base.width - uibutton.width * 1.5f, (float)texture2.height / 2f - uibutton.height / 2f);
			Texture texture3 = ResourceLoader.LoadTexture((int)base.width - 10, 70, "UIMainPanel.submenubar.png");
			texture3.wrapMode = TextureWrapMode.Clamp;
			texture3.filterMode = FilterMode.Bilinear;
			texture3.name = "FavCimsBGMenuTexture";
			UITextureSprite uitextureSprite2 = uitextureSprite.AddUIComponent<UITextureSprite>();
			uitextureSprite2.name = "FavCimsBGMenuSprite";
			uitextureSprite2.texture = texture3;
			float num2 = base.width / 2f - (float)texture3.width / 2f;
			uitextureSprite2.relativePosition = new Vector3(num2, 58f);
			this.FavCimsCBETexture = ResourceLoader.LoadTexture(200, 59, "UIMainPanel.citizenbuttonenabled.png");
			this.FavCimsCBDTexture = ResourceLoader.LoadTexture(200, 59, "UIMainPanel.citizenbuttondisabled.png");
			this.FavCimsCBETexture.wrapMode = TextureWrapMode.Clamp;
			this.FavCimsCBETexture.filterMode = FilterMode.Bilinear;
			this.FavCimsCBETexture.name = "FavCimsCBETexture";
			this.FavCimsCBETexture.mipMapBias = -0.5f;
			this.FavCimsCBDTexture.wrapMode = TextureWrapMode.Clamp;
			this.FavCimsCBDTexture.filterMode = FilterMode.Bilinear;
			this.FavCimsCBDTexture.name = "FavCimsCBDTexture";
			this.FavCimsCBDTexture.mipMapBias = -0.5f;
			this.FavCimsCBMenuSprite = uitextureSprite.AddUIComponent<UITextureSprite>();
			this.FavCimsCBMenuSprite.name = "FavCimsBGMenuSprite";
			this.FavCimsCBMenuSprite.texture = this.FavCimsCBETexture;
			this.FavCimsBCMenuButton = base.AddUIComponent<UIButton>();
			this.FavCimsBCMenuButton.name = "FavCimsBCMenuButton";
			this.FavCimsBCMenuButton.width = this.FavCimsCBMenuSprite.width;
			this.FavCimsBCMenuButton.height = this.FavCimsCBMenuSprite.height;
			this.FavCimsBCMenuButton.useOutline = true;
			this.FavCimsBCMenuButton.playAudioEvents = true;
			this.FavCimsBCMenuButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
			this.FavCimsBCMenuButton.textScale = 1.8f;
			this.FavCimsBCMenuButton.textColor = new Color32(204, 204, 51, 40);
			this.FavCimsBCMenuButton.hoveredTextColor = new Color32(204, 102, 0, 20);
			this.FavCimsBCMenuButton.pressedTextColor = new Color32(153, 0, 0, 0);
			this.FavCimsBCMenuButton.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			this.FavCimsBCMenuButton.textPadding.left = 15;
			this.FavCimsBCMenuButton.useDropShadow = true;
			this.FavCimsBCMenuButton.tooltipBox = aview.defaultTooltipBox;
			this.FavCimsCBMenuSprite.relativePosition = new Vector3(27f, 69f);
			this.FavCimsBCMenuButton.relativePosition = new Vector3(27f, 69f);
			this.CitizensPanel = base.AddUIComponent<UIPanel>();
			this.CitizensPanel.name = "CitizensPanel";
			this.CitizensPanel.width = 1190f;
			this.CitizensPanel.height = 558f;
			this.CitizensPanel.relativePosition = new Vector3(base.width / 2f - this.CitizensPanel.width / 2f, 128f);
			this.FavCimsMainBodyTexture = ResourceLoader.LoadTexture(1190, 558, "UIMainPanel.bodybg.png");
			this.FavCimsMainBodyTexture.wrapMode = TextureWrapMode.Clamp;
			this.FavCimsMainBodyTexture.filterMode = FilterMode.Bilinear;
			this.FavCimsMainBodyTexture.name = "FavCimsMainBodyTexture";
			this.FavCimsBodySprite = this.CitizensPanel.AddUIComponent<UITextureSprite>();
			this.FavCimsBodySprite.name = "FavCimsCBGBodySprite";
			this.FavCimsBodySprite.texture = this.FavCimsMainBodyTexture;
			this.FavCimsBodySprite.relativePosition = Vector3.zero;
			Texture texture4 = ResourceLoader.LoadTexture(1146, 26, "UIMainPanel.indexerbgbar.png");
			texture4.wrapMode = TextureWrapMode.Clamp;
			texture4.filterMode = FilterMode.Bilinear;
			texture4.name = "FavCimsIndexBgBar";
			texture4.mipMapBias = -0.5f;
			UITextureSprite uitextureSprite3 = this.CitizensPanel.AddUIComponent<UITextureSprite>();
			uitextureSprite3.name = "FavCimsIndexBgBarSprite";
			uitextureSprite3.texture = texture4;
			uitextureSprite3.relativePosition = new Vector3(21f, 7f);
			FavoriteCimsMainPanel.FavCimsHappinesColText = this.CitizensPanel.AddUIComponent<UIButton>();
			FavoriteCimsMainPanel.FavCimsHappinesColText.name = "FavCimsHappinesColText";
			FavoriteCimsMainPanel.FavCimsHappinesColText.width = 60f;
			FavoriteCimsMainPanel.FavCimsHappinesColText.height = (float)texture4.height;
			FavoriteCimsMainPanel.FavCimsHappinesColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavoriteCimsMainPanel.FavCimsHappinesColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavoriteCimsMainPanel.FavCimsHappinesColText.playAudioEvents = true;
			FavoriteCimsMainPanel.FavCimsHappinesColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavoriteCimsMainPanel.FavCimsHappinesColText.textScale = 0.7f;
			FavoriteCimsMainPanel.FavCimsHappinesColText.textColor = new Color32(204, 204, 51, 40);
			FavoriteCimsMainPanel.FavCimsHappinesColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavoriteCimsMainPanel.FavCimsHappinesColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsHappinesColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavoriteCimsMainPanel.FavCimsHappinesColText.textPadding.left = 0;
			FavoriteCimsMainPanel.FavCimsHappinesColText.tooltipBox = aview.defaultTooltipBox;
			FavoriteCimsMainPanel.FavCimsHappinesColText.relativePosition = new Vector3(uitextureSprite3.relativePosition.x + 6f, uitextureSprite3.relativePosition.y + 1f);
			FavoriteCimsMainPanel.FavCimsNameColText = this.CitizensPanel.AddUIComponent<UIButton>();
			FavoriteCimsMainPanel.FavCimsNameColText.name = "FavCimsNameColText";
			FavoriteCimsMainPanel.FavCimsNameColText.width = 180f;
			FavoriteCimsMainPanel.FavCimsNameColText.height = (float)texture4.height;
			FavoriteCimsMainPanel.FavCimsNameColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavoriteCimsMainPanel.FavCimsNameColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavoriteCimsMainPanel.FavCimsNameColText.playAudioEvents = true;
			FavoriteCimsMainPanel.FavCimsNameColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavoriteCimsMainPanel.FavCimsNameColText.textScale = 0.7f;
			FavoriteCimsMainPanel.FavCimsNameColText.textColor = new Color32(204, 204, 51, 40);
			FavoriteCimsMainPanel.FavCimsNameColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavoriteCimsMainPanel.FavCimsNameColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsNameColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavoriteCimsMainPanel.FavCimsNameColText.textPadding.left = 0;
			FavoriteCimsMainPanel.FavCimsNameColText.tooltipBox = aview.defaultTooltipBox;
			FavoriteCimsMainPanel.FavCimsNameColText.relativePosition = new Vector3(FavoriteCimsMainPanel.FavCimsHappinesColText.relativePosition.x + FavoriteCimsMainPanel.FavCimsHappinesColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavoriteCimsMainPanel.FavCimsAgePhaseColText = this.CitizensPanel.AddUIComponent<UIButton>();
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.name = "FavCimsAgePhaseColText";
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.width = 120f;
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.height = (float)texture4.height;
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.playAudioEvents = true;
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.textScale = 0.7f;
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.textColor = new Color32(204, 204, 51, 40);
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.textPadding.left = 0;
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.tooltipBox = aview.defaultTooltipBox;
			FavoriteCimsMainPanel.FavCimsAgePhaseColText.relativePosition = new Vector3(FavoriteCimsMainPanel.FavCimsNameColText.relativePosition.x + FavoriteCimsMainPanel.FavCimsNameColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavoriteCimsMainPanel.FavCimsAgeColText = this.CitizensPanel.AddUIComponent<UIButton>();
			FavoriteCimsMainPanel.FavCimsAgeColText.name = "FavCimsAgeColText";
			FavoriteCimsMainPanel.FavCimsAgeColText.width = 40f;
			FavoriteCimsMainPanel.FavCimsAgeColText.height = (float)texture4.height;
			FavoriteCimsMainPanel.FavCimsAgeColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavoriteCimsMainPanel.FavCimsAgeColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavoriteCimsMainPanel.FavCimsAgeColText.playAudioEvents = true;
			FavoriteCimsMainPanel.FavCimsAgeColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavoriteCimsMainPanel.FavCimsAgeColText.textScale = 0.7f;
			FavoriteCimsMainPanel.FavCimsAgeColText.textColor = new Color32(204, 204, 51, 40);
			FavoriteCimsMainPanel.FavCimsAgeColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavoriteCimsMainPanel.FavCimsAgeColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsAgeColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavoriteCimsMainPanel.FavCimsAgeColText.textPadding.left = 0;
			FavoriteCimsMainPanel.FavCimsAgeColText.tooltipBox = aview.defaultTooltipBox;
			FavoriteCimsMainPanel.FavCimsAgeColText.relativePosition = new Vector3(FavoriteCimsMainPanel.FavCimsAgePhaseColText.relativePosition.x + FavoriteCimsMainPanel.FavCimsAgePhaseColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavoriteCimsMainPanel.FavCimsEduColText = this.CitizensPanel.AddUIComponent<UIButton>();
			FavoriteCimsMainPanel.FavCimsEduColText.name = "FavCimsEduColText";
			FavoriteCimsMainPanel.FavCimsEduColText.width = 140f;
			FavoriteCimsMainPanel.FavCimsEduColText.height = (float)texture4.height;
			FavoriteCimsMainPanel.FavCimsEduColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavoriteCimsMainPanel.FavCimsEduColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavoriteCimsMainPanel.FavCimsEduColText.playAudioEvents = true;
			FavoriteCimsMainPanel.FavCimsEduColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavoriteCimsMainPanel.FavCimsEduColText.textScale = 0.7f;
			FavoriteCimsMainPanel.FavCimsEduColText.textColor = new Color32(204, 204, 51, 40);
			FavoriteCimsMainPanel.FavCimsEduColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavoriteCimsMainPanel.FavCimsEduColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsEduColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavoriteCimsMainPanel.FavCimsEduColText.textPadding.left = 0;
			FavoriteCimsMainPanel.FavCimsEduColText.tooltipBox = aview.defaultTooltipBox;
			FavoriteCimsMainPanel.FavCimsEduColText.relativePosition = new Vector3(FavoriteCimsMainPanel.FavCimsAgeColText.relativePosition.x + FavoriteCimsMainPanel.FavCimsAgeColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavoriteCimsMainPanel.FavCimsHomeColText = this.CitizensPanel.AddUIComponent<UIButton>();
			FavoriteCimsMainPanel.FavCimsHomeColText.name = "FavCimsHomeColText";
			FavoriteCimsMainPanel.FavCimsHomeColText.width = 184f;
			FavoriteCimsMainPanel.FavCimsHomeColText.height = (float)texture4.height;
			FavoriteCimsMainPanel.FavCimsHomeColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavoriteCimsMainPanel.FavCimsHomeColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavoriteCimsMainPanel.FavCimsHomeColText.playAudioEvents = true;
			FavoriteCimsMainPanel.FavCimsHomeColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavoriteCimsMainPanel.FavCimsHomeColText.textScale = 0.7f;
			FavoriteCimsMainPanel.FavCimsHomeColText.textColor = new Color32(204, 204, 51, 40);
			FavoriteCimsMainPanel.FavCimsHomeColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavoriteCimsMainPanel.FavCimsHomeColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsHomeColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavoriteCimsMainPanel.FavCimsHomeColText.textPadding.left = 0;
			FavoriteCimsMainPanel.FavCimsHomeColText.tooltipBox = aview.defaultTooltipBox;
			FavoriteCimsMainPanel.FavCimsHomeColText.relativePosition = new Vector3(FavoriteCimsMainPanel.FavCimsEduColText.relativePosition.x + FavoriteCimsMainPanel.FavCimsEduColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText = this.CitizensPanel.AddUIComponent<UIButton>();
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.name = "FavCimsWorkingPlaceColText";
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.width = 180f;
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.height = (float)texture4.height;
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.playAudioEvents = true;
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.textScale = 0.7f;
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.textColor = new Color32(204, 204, 51, 40);
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.textPadding.left = 0;
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.tooltipBox = aview.defaultTooltipBox;
			FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.relativePosition = new Vector3(FavoriteCimsMainPanel.FavCimsHomeColText.relativePosition.x + FavoriteCimsMainPanel.FavCimsHomeColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavoriteCimsMainPanel.FavCimsLastActColText = this.CitizensPanel.AddUIComponent<UIButton>();
			FavoriteCimsMainPanel.FavCimsLastActColText.name = "FavCimsLastActColText";
			FavoriteCimsMainPanel.FavCimsLastActColText.width = 180f;
			FavoriteCimsMainPanel.FavCimsLastActColText.height = (float)texture4.height;
			FavoriteCimsMainPanel.FavCimsLastActColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavoriteCimsMainPanel.FavCimsLastActColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavoriteCimsMainPanel.FavCimsLastActColText.playAudioEvents = true;
			FavoriteCimsMainPanel.FavCimsLastActColText.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavoriteCimsMainPanel.FavCimsLastActColText.textScale = 0.7f;
			FavoriteCimsMainPanel.FavCimsLastActColText.textColor = new Color32(204, 204, 51, 40);
			FavoriteCimsMainPanel.FavCimsLastActColText.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavoriteCimsMainPanel.FavCimsLastActColText.pressedTextColor = new Color32(153, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsLastActColText.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavoriteCimsMainPanel.FavCimsLastActColText.textPadding.left = 0;
			FavoriteCimsMainPanel.FavCimsLastActColText.tooltipBox = aview.defaultTooltipBox;
			FavoriteCimsMainPanel.FavCimsLastActColText.relativePosition = new Vector3(FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.relativePosition.x + FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavoriteCimsMainPanel.FavCimsCloseButtonCol = this.CitizensPanel.AddUIComponent<UIButton>();
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.name = "FavCimsCloseButtonCol";
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.width = 50f;
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.height = (float)texture4.height;
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.playAudioEvents = true;
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.font = UIDynamicFont.FindByName("OpenSans-Regular");
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.textScale = 0.7f;
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.textColor = new Color32(204, 204, 51, 40);
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.hoveredTextColor = new Color32(204, 102, 0, 20);
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.pressedTextColor = new Color32(153, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.focusedTextColor = new Color32(102, 153, byte.MaxValue, 147);
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.textPadding.right = 6;
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.tooltipBox = aview.defaultTooltipBox;
			FavoriteCimsMainPanel.FavCimsCloseButtonCol.relativePosition = new Vector3(FavoriteCimsMainPanel.FavCimsLastActColText.relativePosition.x + FavoriteCimsMainPanel.FavCimsLastActColText.width, uitextureSprite3.relativePosition.y + 1f);
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel = this.CitizensPanel.AddUIComponent<UIScrollablePanel>();
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.name = "FavCimsCitizenRowsPanel";
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.width = uitextureSprite3.width - 12f;
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.height = 500f;
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.autoLayoutDirection = LayoutDirection.Vertical;
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.autoLayout = true;
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.clipChildren = true;
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.relativePosition = new Vector3(uitextureSprite3.relativePosition.x + 6f, uitextureSprite3.relativePosition.y + uitextureSprite3.height);
			UIScrollablePanel uiscrollablePanel = this.CitizensPanel.AddUIComponent<UIScrollablePanel>();
			uiscrollablePanel.name = "FavCimsCitizenRowsPanelScrollBar";
			uiscrollablePanel.width = 10f;
			uiscrollablePanel.height = 500f;
			uiscrollablePanel.relativePosition = new Vector3(uitextureSprite3.relativePosition.x + uitextureSprite3.width, FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.relativePosition.y);
			UIScrollbar FavCimsMainPanelScrollBar = uiscrollablePanel.AddUIComponent<UIScrollbar>();
			FavCimsMainPanelScrollBar.width = 10f;
			FavCimsMainPanelScrollBar.height = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.height;
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
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.verticalScrollbar = FavCimsMainPanelScrollBar;
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.eventMouseWheel += delegate(UIComponent component, UIMouseEventParameter eventParam)
			{
				int num3 = Math.Sign(eventParam.wheelDelta);
				FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.scrollPosition += new Vector2(0f, (float)(num3 * -1) * FavCimsMainPanelScrollBar.incrementAmount);
			};
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.eventComponentAdded += delegate(UIComponent component, UIComponent eventParam)
			{
				this.ReorderRowsBackgrounds();
			};
			FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.eventComponentRemoved += delegate(UIComponent component, UIComponent eventParam)
			{
				this.ReorderRowsBackgrounds();
			};
			UITextureSprite uitextureSprite4 = this.CitizensPanel.AddUIComponent<UITextureSprite>();
			uitextureSprite4.name = "FavCimsFooterBgBarSprite";
			uitextureSprite4.width = uitextureSprite3.width;
			uitextureSprite4.height = 15f;
			uitextureSprite4.texture = texture4;
			uitextureSprite4.relativePosition = new Vector3(21f, FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.relativePosition.y + FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.height);
			foreach (KeyValuePair<InstanceID, string> keyValuePair in FavCimsCore.FavoriteCimsList())
			{
				bool flag = keyValuePair.Key.Type == InstanceType.Citizen;
				if (flag)
				{
					CitizenRow citizenRow = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
					citizenRow.MyInstanceID = keyValuePair.Key;
					citizenRow.MyInstancedName = keyValuePair.Value;
				}
			}
		}

		
		
	}
}
