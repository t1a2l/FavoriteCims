using System;
using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.UI.PanelsRows
{
    public partial class CitizenRowView
    {
        public event Action<float> PreferredHeightChanged;
        private const float ContentPadding = 4f;
        private const float NameTypeGap = 2f;
        private UIButton[] adaptiveButtons;
        private TextHeightCache[] adaptiveMetrics;
        private readonly TextHeightCache typeMetric = new TextHeightCache();
        private readonly List<UIPanel> adaptiveColumns = new List<UIPanel>();
        private readonly List<UITextureSprite> adaptiveSeparators = new List<UITextureSprite>();
        private float appliedContentHeight = -1f;
        private float appliedNameHeight = -1f;
        private float appliedTypeHeight = -1f;
        private bool adaptiveLayoutFailed;

        /// <summary>Registers text and separators, including the two shared building cells.</summary>
        private void InitializeAdaptiveLayout()
        {
            adaptiveButtons =
            [
                nameButton, agePhaseButton, realAgeButton, educationButton,
                homeCell.TextButton, workplaceCell.TextButton, activityButton
            ];
            adaptiveMetrics = new TextHeightCache[adaptiveButtons.Length];
            for (int i = 0; i < adaptiveButtons.Length; i++)
            {
                var button = adaptiveButtons[i];
                button.autoSize = false;
                button.maximumSize = new Vector2(button.width, 0f);
                button.wordWrap = true;
                adaptiveMetrics[i] = new TextHeightCache();
            }
            nameButton.textVerticalAlignment = UIVerticalAlignment.Top;
            citizenTypeLabel.autoSize = false;
            citizenTypeLabel.wordWrap = true;
            citizenTypeLabel.textAlignment = UIHorizontalAlignment.Left;
            citizenTypeLabel.verticalAlignment = UIVerticalAlignment.Top;
            citizenTypeLabel.font = nameButton.font;
            citizenTypeLabel.maximumSize = new Vector2(citizenTypeLabel.width, 0f);
            rowPanel.autoLayout = false;
            rowPanel.relativePosition = Vector3.zero;
            foreach (UIComponent component in rowPanel.components)
            {
                var column = component as UIPanel;
                if (column == null) continue;
                adaptiveColumns.Add(column);
                foreach (UIComponent child in column.components)
                {
                    var separator = child as UITextureSprite;
                    if (separator != null && separator.name == column.name + "Separator")
                    {
                        separator.autoSize = false;
                        separator.width = separator.texture != null ? separator.texture.width : 1f;
                        adaptiveSeparators.Add(separator);
                    }
                }
            }
        }

        /// <summary>Applies current column widths before measuring wrapped text.</summary>
        public override void LateUpdate()
        {
            base.LateUpdate();
            if (!IsReady || FavCimsMainClass.UnLoading || adaptiveLayoutFailed) return;
            try
            {
                ApplyCurrentColumnWidths();
                UpdateAdaptiveHeight();
            }
            catch (Exception ex)
            {
                adaptiveLayoutFailed = true;
                Utils.Debug.Error("Adaptive citizen-row layout failed; keeping its last height: " + ex);
            }
        }

        /// <summary>Fits the tallest text cell and reserves separate space for the citizen-type subtitle.</summary>
        private void UpdateAdaptiveHeight()
        {
            float required = RowHeight;
            float nameHeight = 0f;

            for (int i = 0; i < adaptiveButtons.Length; i++)
            {
                var button = adaptiveButtons[i];

                if (!button.parent.isVisible)
                {
                    continue;
                }

                var padding = button.textPadding;

                float measured = adaptiveMetrics[i].Read(button.text, button.font, button.textScale, Mathf.Max(1f, button.width - padding.left - padding.right));

                measured += padding.top + padding.bottom;

                if (i == 0)
                { 
                    nameHeight = measured; 
                }
                else 
                { 
                    required = Mathf.Max(required, measured + ContentPadding * 2f); 
                }
            }

            float typeHeight = typeMetric.Read(citizenTypeLabel.text, citizenTypeLabel.font, citizenTypeLabel.textScale, Mathf.Max(1f, citizenTypeLabel.width));
            float gap = typeHeight > 0f ? NameTypeGap : 0f;
            required = Mathf.Ceil(Mathf.Max(required, nameHeight + gap + typeHeight + ContentPadding * 2f));
            if (Mathf.Approximately(required, appliedContentHeight) && Mathf.Approximately(nameHeight, appliedNameHeight) && Mathf.Approximately(typeHeight, appliedTypeHeight)) return;

            foreach (var column in adaptiveColumns) column.height = required;
            foreach (var separator in adaptiveSeparators) separator.height = required;
            rowPanel.height = required;
            rowBackground.height = required;
            nameColumnBackground.height = required;
            height = required;
            for (int i = 1; i < adaptiveButtons.Length; i++) SetTextHeight(adaptiveButtons[i], required);
            SetTextHeight(nameButton, Mathf.Max(1f, nameHeight));
            nameButton.relativePosition = new Vector3(0f, ContentPadding);
            citizenTypeLabel.height = Mathf.Max(1f, typeHeight);
            citizenTypeLabel.relativePosition = new Vector3(40f, ContentPadding + nameHeight + gap);

            CenterVertically(happinessIcon, required);
            CenterVertically(familyButton, required);
            homeCell.SetRowHeight(required);
            workplaceCell.SetRowHeight(required);
            CenterVertically(activityVehicleButton, required);
            CenterVertically(closeButton, required);
            appliedContentHeight = required;
            appliedNameHeight = nameHeight;
            appliedTypeHeight = typeHeight;
            PreferredHeightChanged?.Invoke(required);
        }

        private static void SetTextHeight(UIButton button, float targetHeight)
        {
            button.maximumSize = new Vector2(button.width, targetHeight);
            button.height = targetHeight;
        }

        private static void CenterVertically(UIComponent component, float contentHeight)
        {
            Vector3 position = component.relativePosition;
            position.y = Mathf.Max(0f, (contentHeight - component.height) * 0.5f);
            component.relativePosition = position;
        }

        /// <summary>Caches font measurements until text, font size, scale, or available width changes.</summary>
        private sealed class TextHeightCache
        {
            private bool valid;
            private string previousText;
            private UIFont previousFont;
            private int previousFontSize;
            private float previousScale;
            private float previousWidth;
            private float measuredHeight;

            public float Read(string text, UIFont font, float scale, float availableWidth)
            {
                text = text ?? string.Empty;
                int fontSize = font != null ? font.size : 0;
                if (valid && text == previousText && font == previousFont && fontSize == previousFontSize && scale == previousScale && availableWidth == previousWidth) return measuredHeight;
                float result = 0f;
                if (font != null && text.Length > 0)
                {
                    using var renderer = font.ObtainRenderer();
                    renderer.pixelRatio = 1f;
                    renderer.textScale = scale;
                    renderer.wordWrap = true;
                    renderer.multiLine = true;
                    renderer.maxSize = new Vector2(availableWidth, float.MaxValue);
                    result = Mathf.Ceil(renderer.MeasureString(text).y) + 2f;
                }
                previousText = text;
                previousFont = font;
                previousFontSize = fontSize;
                previousScale = scale;
                previousWidth = availableWidth;
                measuredHeight = result;
                valid = true;
                return result;
            }
        }
    }
}