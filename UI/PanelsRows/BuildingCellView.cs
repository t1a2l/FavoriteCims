using System;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.PanelsRows
{
    /// <summary>
    /// Shared home/workplace column. Displays one base image plus an optional
    /// level overlay. Only the text control requests navigation.
    /// </summary>
    public sealed class BuildingCellView : IDisposable
    {
        public UIPanel Column { get; private set; }
        public UIButton TextButton { get; private set; }
        public event MouseEventHandler NavigationRequested;

        private readonly UIPanel iconContainer;
        private readonly UITextureSprite textureImage;
        private readonly UIButton atlasImage;
        private readonly UITextureSprite levelImage;
        private BuildingCellData boundData;
        private bool disposed;
        private const float IconHeight = 40f;

        /// <summary>Builds the identical control hierarchy used for either building role.</summary>
        public BuildingCellView(UIPanel row, string columnName, string textName, float width, float x)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            Column = CitizenRowColumns.CreateColumn(row, columnName, width, x);
            TextButton = CitizenRowColumns.CreateTextButton(Column, textName, 0.85f);
            TextButton.autoSize = false;
            TextButton.textPadding = new RectOffset(40, 5, 0, 0);
            TextButton.textColor = DefaultTextColor;
            TextButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            TextButton.eventMouseUp += OnTextMouseUp;

            iconContainer = Column.AddUIComponent<UIPanel>();
            iconContainer.name = columnName + "Icon";
            iconContainer.size = new Vector2(20f, IconHeight);
            iconContainer.relativePosition = new Vector3(10f, 0f);
            iconContainer.isInteractive = true;
            iconContainer.tooltipBox = UIView.GetAView().defaultTooltipBox;

            textureImage = iconContainer.AddUIComponent<UITextureSprite>();
            textureImage.name = columnName + "Texture";
            textureImage.autoSize = false;
            textureImage.size = iconContainer.size;
            textureImage.relativePosition = Vector3.zero;
            textureImage.isInteractive = false;

            atlasImage = iconContainer.AddUIComponent<UIButton>();
            atlasImage.name = columnName + "Sprite";
            atlasImage.size = iconContainer.size;
            atlasImage.relativePosition = Vector3.zero;
            atlasImage.isInteractive = false;
            atlasImage.playAudioEvents = false;

            levelImage = iconContainer.AddUIComponent<UITextureSprite>();
            levelImage.name = columnName + "Level";
            levelImage.relativePosition = Vector3.zero;
            levelImage.isInteractive = false;
            CitizenRowColumns.CreateSeparator(Column);
            Bind(default);
        }

        public static Color32 DefaultTextColor => new(21, 59, 96, 140);

        /// <summary>Completely rebinds visual state so an old icon or tooltip cannot leak into another state.</summary>
        public void Bind(BuildingCellData data)
        {
            if (disposed) return;
            boundData = data;
            TextButton.text = data.DisplayName ?? string.Empty;
            TextButton.isEnabled = data.CanNavigate;
            TextButton.tooltip = data.TextTooltip ?? string.Empty;
            TextButton.textColor = data.TextColor;

            textureImage.texture = null;
            textureImage.Hide();
            atlasImage.normalBgSprite = string.Empty;
            atlasImage.hoveredBgSprite = string.Empty;
            atlasImage.focusedBgSprite = string.Empty;
            atlasImage.pressedBgSprite = string.Empty;
            atlasImage.disabledBgSprite = string.Empty;
            atlasImage.atlas = null;
            atlasImage.Hide();
            levelImage.texture = null;
            levelImage.Hide();

            bool hasIcon = false;
            if (data.Icon.HasTexture)
            {
                textureImage.texture = data.Icon.Texture;
                textureImage.size = new Vector2(20f, IconHeight);
                textureImage.Show();
                hasIcon = true;
            }
            else if (data.Icon.HasSprite)
            {
                atlasImage.atlas = data.Icon.Atlas;
                atlasImage.normalBgSprite = data.Icon.SpriteName;
                atlasImage.Show();
                hasIcon = true;
            }
            if (hasIcon && data.LevelTexture != null)
            {
                levelImage.texture = data.LevelTexture;
                levelImage.size = new Vector2(data.LevelTexture.width, data.LevelTexture.height);
                levelImage.Show();
                levelImage.BringToFront();
            }
            iconContainer.tooltip = CombineTooltips(data.TypeTooltip, data.DistrictTooltip);
            iconContainer.isVisible = hasIcon;
        }

        /// <summary>Shows a non-navigable state such as retired, unemployed, or outside the city.</summary>
        public void ShowState(string text, string textTooltip, BuildingIconData icon, string iconTooltip = null)
        {
            Bind(new BuildingCellData
            {
                DisplayName = text,
                TextTooltip = textTooltip,
                TypeTooltip = iconTooltip,
                TextColor = DefaultTextColor,
                Icon = icon
            });
        }

        /// <summary>Keeps the current display but removes navigation, tooltips, and level details for special states.</summary>
        public void DisableNavigationAndDetails()
        {
            var state = boundData;
            state.BuildingId = 0;
            state.TextTooltip = string.Empty;
            state.TypeTooltip = string.Empty;
            state.DistrictTooltip = string.Empty;
            state.LevelTexture = null;
            Bind(state);
        }

        /// <summary>Resizes the text and its column without altering the fixed icon canvas.</summary>
        public void SetWidth(float width)
        {
            if (disposed) return;
            Column.width = Mathf.Max(1f, width);
            TextButton.maximumSize = Vector2.zero;
            TextButton.width = Column.width;
        }

        /// <summary>Applies the measured row height and centers the shared icon container.</summary>
        public void SetRowHeight(float height)
        {
            if (disposed) return;
            Column.height = height;
            TextButton.maximumSize = new Vector2(TextButton.width, height);
            TextButton.height = height;
            iconContainer.relativePosition = new Vector3(10f, Mathf.Max(0f, (height - IconHeight) * 0.5f));
        }

        /// <summary>Provides one hover tooltip instead of competing overlapping child tooltips.</summary>
        private static string CombineTooltips(string type, string district)
        {
            if (string.IsNullOrEmpty(type)) return district ?? string.Empty;
            if (string.IsNullOrEmpty(district) || type == district) return type;
            return type + "\n" + district;
        }

        private void OnTextMouseUp(UIComponent component, UIMouseEventParameter param)
        {
            if (!disposed && boundData.CanNavigate) NavigationRequested?.Invoke(component, param);
        }

        /// <summary>Unsubscribes managed handlers; the owning row destroys the control hierarchy.</summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            TextButton.eventMouseUp -= OnTextMouseUp;
            NavigationRequested = null;
        }
    }
}