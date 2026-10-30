using System;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.PanelsRows
{
    public sealed class FamilyMemberRow : IDisposable
    {
        private readonly UIPanel panel;
        private readonly UITextureSprite top;
        private readonly UITextureSprite bottom;
        private readonly UIButton nameButton;
        private readonly UIButton ageButton;
        private readonly UIButton favoriteButton;
        private readonly UIButton vehicleButton;
        private readonly UIButton destinationButton;
        private readonly UIButton relationshipIcon;
        private readonly UITextureSprite portrait;
        private readonly Action<FamilyActivityData, UIButton, UIButton> renderActivity;
        private readonly Func<int, Color32> ageColor;
        private FamilyCitizenData member;
        private bool disposed;

        public event Action<InstanceID, UIMouseEventParameter> CitizenRequested;
        public event Action<InstanceID, UIMouseEventParameter> InstanceRequested;
        public event Action<InstanceID> FavoriteRequested;

        public FamilyMemberRow(UIComponent parent, string name, Action<FamilyActivityData, UIButton, UIButton> renderActivity, Func<int, Color32> ageColor)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            this.renderActivity = renderActivity ?? throw new ArgumentNullException("renderActivity");
            this.ageColor = ageColor ?? throw new ArgumentNullException("ageColor");
            panel = parent.AddUIComponent<UIPanel>();
            panel.name = name;
            panel.width = parent.width;
            panel.height = 52f;
            panel.clipChildren = true;
            panel.autoLayout = false;
            panel.Hide();

            top = panel.AddUIComponent<UITextureSprite>();
            top.size = new Vector2(panel.width, 26f);
            top.texture = TextureDB.BubbleBgBar1;
            top.relativePosition = Vector3.zero;
            bottom = panel.AddUIComponent<UITextureSprite>();
            bottom.size = new Vector2(panel.width, 26f);
            bottom.texture = TextureDB.BubbleBgBar2;
            bottom.relativePosition = new Vector3(0f, 26f);

            relationshipIcon = top.AddUIComponent<UIButton>();
            relationshipIcon.atlas = UIView.GetAView().defaultAtlas;
            relationshipIcon.size = new Vector2(18f, 18f);
            relationshipIcon.relativePosition = new Vector3(7f, 4f);
            relationshipIcon.isInteractive = false;
            portrait = top.AddUIComponent<UITextureSprite>();
            portrait.size = new Vector2(18f, 18f);
            portrait.relativePosition = new Vector3(7f, 4f);

            nameButton = CreateTextButton(top, "CitizenName", 29f, 132f, 0.8f);
            nameButton.eventMouseUp += OnCitizenClicked;
            ageButton = CreateTextButton(top, "Age", 163f, 26f, 0.9f);
            ageButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
            ageButton.isInteractive = false;
            favoriteButton = top.AddUIComponent<UIButton>();
            favoriteButton.name = "Favorite";
            favoriteButton.atlas = MyAtlas.FavCimsAtlas;
            favoriteButton.size = new Vector2(18f, 18f);
            favoriteButton.relativePosition = new Vector3(panel.width - 29f, 4f);
            favoriteButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            favoriteButton.playAudioEvents = true;
            favoriteButton.eventClick += OnFavoriteClicked;

            vehicleButton = bottom.AddUIComponent<UIButton>();
            vehicleButton.name = "Vehicle";
            vehicleButton.size = new Vector2(22f, 22f);
            vehicleButton.relativePosition = new Vector3(7f, 2f);
            vehicleButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            vehicleButton.eventMouseUp += OnVehicleClicked;
            destinationButton = CreateTextButton(bottom, "Activity", 34f, panel.width - 39f, 0.75f);
            destinationButton.eventMouseUp += OnDestinationClicked;
        }

        public void Bind(FamilyCitizenData value, string relationshipSprite)
        {
            if (disposed) return;
            member = value;
            if (value == null)
            {
                Clear();
                return;
            }
            bool usePortrait = string.IsNullOrEmpty(relationshipSprite);
            portrait.isVisible = usePortrait;
            relationshipIcon.isVisible = !usePortrait;
            relationshipIcon.normalBgSprite = relationshipSprite ?? string.Empty;
            portrait.texture = value.Gender == Citizen.Gender.Female ? TextureDB.BubbleHeaderIconSpriteTextureFemale : TextureDB.BubbleHeaderIconSpriteTextureMale;
            nameButton.text = value.Name;
            nameButton.isEnabled = true;
            nameButton.textColor = value.Gender == Citizen.Gender.Female ? new Color32(255, 102, 204, 255) : new Color32(110, 100, 0, 255);
            nameButton.tooltip = FamilyPanelText.CitizenNavigation;
            ageButton.text = value.RealAge.ToString();
            ageButton.textColor = ageColor(value.RealAge);
            favoriteButton.isEnabled = true;
            favoriteButton.normalBgSprite = value.IsFavorite ? "IconFavSubscribed" : "IconFavUnsubscribed";
            favoriteButton.tooltip = FavCimsLang.Text(value.IsFavorite ? "FavStarButtonDisableTooltip" : "FavStarButtonEnableTooltip");
            renderActivity(value.Activity, vehicleButton, destinationButton);
            RefreshLayout();
            panel.Show();
        }

        public void Clear()
        {
            member = null;
            if (panel == null) return;
            panel.Hide();
            nameButton.text = string.Empty;
            nameButton.tooltip = string.Empty;
            nameButton.isEnabled = false;
            ageButton.text = string.Empty;
            favoriteButton.isEnabled = false;
            favoriteButton.tooltip = string.Empty;
            renderActivity(null, vehicleButton, destinationButton);
        }

        public void RefreshLayout()
        {
            if (disposed || panel == null)
            {
                return;
            }

            const float padding = 3f;

            PrepareWrappedText(nameButton);
            PrepareWrappedText(ageButton);
            PrepareWrappedText(destinationButton);

            // Keep the activity inside its bar, after the vehicle icon.
            destinationButton.width = Mathf.Max(1f, panel.width - destinationButton.relativePosition.x - 5f);

            float topHeight = Mathf.Ceil(Mathf.Max(26f, Mathf.Max(MeasureTextHeight(nameButton), MeasureTextHeight(ageButton)) + padding * 2f));

            float bottomHeight = Mathf.Ceil(Mathf.Max(26f, MeasureTextHeight(destinationButton) + padding * 2f));

            top.size = new Vector2(panel.width, topHeight);
            top.relativePosition = Vector3.zero;

            bottom.size = new Vector2(panel.width, bottomHeight);
            bottom.relativePosition = new Vector3(0f, topHeight);

            SetTextHeight(nameButton, topHeight - padding * 2f, padding);
            SetTextHeight(ageButton, topHeight - padding * 2f, padding);

            SetTextHeight(destinationButton, bottomHeight - padding * 2f, padding);

            CenterVertically(relationshipIcon, topHeight);
            CenterVertically(portrait, topHeight);
            CenterVertically(favoriteButton, topHeight);
            CenterVertically(vehicleButton, bottomHeight);

            panel.height = topHeight + bottomHeight;
        }

        private static void PrepareWrappedText(UIButton button)
        {
            button.autoSize = false;
            button.wordWrap = true;

            // Remove any fixed-height limit applied during rendering.
            button.maximumSize = Vector2.zero;
        }

        private static float MeasureTextHeight(UIButton button)
        {
            if (button.font == null || string.IsNullOrEmpty(button.text))
            {
                return 0f;
            }

            var padding = button.textPadding;

            float availableWidth = Mathf.Max(1f, button.width - padding.left - padding.right);

            using var renderer = button.font.ObtainRenderer();
            renderer.pixelRatio = 1f;
            renderer.textScale = button.textScale;
            renderer.wordWrap = true;
            renderer.multiLine = true;

            renderer.maxSize = new Vector2(availableWidth, float.MaxValue);

            return Mathf.Ceil(renderer.MeasureString(button.text).y) + padding.top + padding.bottom + 2f;
        }

        private static void SetTextHeight(UIButton button, float height, float y)
        {
            button.height = height;
            Vector3 position = button.relativePosition;
            position.y = y;
            button.relativePosition = position;
        }

        private static void CenterVertically(UIComponent component, float barHeight)
        {
            Vector3 position = component.relativePosition;
            position.y = Mathf.Max(0f, (barHeight - component.height) * 0.5f);
            component.relativePosition = position;
        }

        private static UIButton CreateTextButton(UIComponent parent, string name, float x, float width, float scale)
        {
            var button = parent.AddUIComponent<UIButton>();
            button.name = name;
            button.size = new Vector2(width, 26f);
            button.relativePosition = new Vector3(x, 0f);
            button.font = UIDynamicFont.FindByName("OpenSans-Regular");
            button.textScale = scale;
            button.textHorizontalAlignment = UIHorizontalAlignment.Left;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textPadding = new RectOffset(2, 2, 0, 0);
            button.textColor = new Color32(21, 59, 96, 255);
            button.hoveredTextColor = new Color32(204, 102, 0, 255);
            button.wordWrap = true;
            button.playAudioEvents = true;
            button.tooltipBox = UIView.GetAView().defaultTooltipBox;
            return button;
        }

        private void OnCitizenClicked(UIComponent component, UIMouseEventParameter param)
        {
            if (disposed || member == null || member.InstanceId.IsEmpty) return;
            CitizenRequested?.Invoke(member.InstanceId, param);
        }
        private void OnFavoriteClicked(UIComponent component, UIMouseEventParameter param)
        {
            if (disposed || member == null || member.InstanceId.IsEmpty) return;
            FavoriteRequested?.Invoke(member.InstanceId);
        }
        private void OnVehicleClicked(UIComponent component, UIMouseEventParameter param)
        {
            if (disposed || member == null || member.Activity == null || !member.Activity.HasVehicle) return;
            InstanceRequested?.Invoke(member.Activity.VehicleId, param);
        }
        private void OnDestinationClicked(UIComponent component, UIMouseEventParameter param)
        {
            if (disposed || member == null || member.Activity == null || member.Activity.IsPrisonTransport || member.Activity.TargetId.IsEmpty) return;
            InstanceRequested?.Invoke(member.Activity.TargetId, param);
        }

        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            member = null;
            if (nameButton != null) nameButton.eventMouseUp -= OnCitizenClicked;
            if (favoriteButton != null) favoriteButton.eventClick -= OnFavoriteClicked;
            if (vehicleButton != null) vehicleButton.eventMouseUp -= OnVehicleClicked;
            if (destinationButton != null) destinationButton.eventMouseUp -= OnDestinationClicked;
            CitizenRequested = null;
            InstanceRequested = null;
            FavoriteRequested = null;
        }
    }
}