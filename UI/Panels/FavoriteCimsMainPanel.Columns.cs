using ColossalFramework.UI;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public partial class FavoriteCimsMainPanel
    {
        private static readonly bool[] mainColumnVisibility =
        [
            true, true, true, true, true, true, true, true, true
        ];

        private static readonly int[] optionalColumnIndices =
        [
            0, 2, 3, 4, 5, 6, 7
        ];

        private UIButton columnsButton;
        private UIPanel columnsPopup;
        private UIButton[] columnToggleButtons;

        public static bool IsMainColumnVisible(int index)
        {
            if (index < 0 || index >= mainColumnVisibility.Length)
            {
                return false;
            }

            // Name and Remove are always visible.
            return index == 1 || index == 8 || mainColumnVisibility[index];
        }

        private void InitializeMainColumnControls()
        {
            columnsButton = AddUIComponent<UIButton>();
            columnsButton.name = "FavoriteCimsColumnsButton";
            columnsButton.atlas = UIView.GetAView().defaultAtlas;
            columnsButton.normalBgSprite = "ButtonMenu";
            columnsButton.hoveredBgSprite = "ButtonMenuHovered";
            columnsButton.pressedBgSprite = "ButtonMenuPressed";
            columnsButton.size = new Vector2(140f, 30f);
            columnsButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            columnsButton.textScale = 0.85f;
            columnsButton.playAudioEvents = true;
            columnsButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            columnsButton.eventClick += ToggleMainColumnsPopup;

            columnsPopup = AddUIComponent<UIPanel>();
            columnsPopup.name = "FavoriteCimsColumnsPopup";
            columnsPopup.atlas = UIView.GetAView().defaultAtlas;
            columnsPopup.backgroundSprite = "MenuPanel2";
            columnsPopup.size = new Vector2(270f, optionalColumnIndices.Length * 30f + 16f);
            columnsPopup.clipChildren = true;
            columnsPopup.isInteractive = true;
            columnsPopup.Hide();

            columnToggleButtons = new UIButton[optionalColumnIndices.Length];

            for (int i = 0; i < optionalColumnIndices.Length; i++)
            {
                int columnIndex = optionalColumnIndices[i];

                var toggle = columnsPopup.AddUIComponent<UIButton>();
                toggle.name = "FavoriteCimsColumnToggle" + columnIndex;
                toggle.atlas = UIView.GetAView().defaultAtlas;
                toggle.normalBgSprite = "ButtonMenu";
                toggle.hoveredBgSprite = "ButtonMenuHovered";
                toggle.pressedBgSprite = "ButtonMenuPressed";
                toggle.size = new Vector2(254f, 28f);
                toggle.relativePosition = new Vector3(8f, 8f + i * 30f);
                toggle.font = UIDynamicFont.FindByName("OpenSans-Regular");
                toggle.textScale = 0.85f;
                toggle.textHorizontalAlignment = UIHorizontalAlignment.Left;
                toggle.textPadding = new RectOffset(10, 5, 0, 0);
                toggle.playAudioEvents = true;

                toggle.eventClick += (component, param) =>
                {
                    SetMainColumnVisible(columnIndex, !IsMainColumnVisible(columnIndex));
                    param.Use();
                };

                columnToggleButtons[i] = toggle;
            }

            RefreshMainColumnControlText();
        }

        private void ToggleMainColumnsPopup(UIComponent component, UIMouseEventParameter param)
        {
            if (columnsPopup.isVisible)
            {
                columnsPopup.Hide();
            }
            else
            {
                RefreshMainColumnControlText();
                PositionMainColumnControls();
                columnsPopup.Show();
                columnsPopup.BringToFront();
            }

            param.Use();
        }

        private void RefreshMainColumnControlText()
        {
            if (columnsButton == null)
            {
                return;
            }

            columnsButton.text = FavCimsLang.Text("MainColumnsButtonText");
            columnsButton.tooltip = FavCimsLang.Text("MainColumnsButtonTooltip");

            for (int i = 0; i < optionalColumnIndices.Length; i++)
            {
                int columnIndex = optionalColumnIndices[i];
                string title = resizeHeaders[columnIndex].text.Replace("\r", " ").Replace("\n", " ").Trim();
                columnToggleButtons[i].text = (IsMainColumnVisible(columnIndex) ? "[x] " : "[ ] ") + title;
            }
        }

        private void PositionMainColumnControls()
        {
            if (columnsButton == null)
            {
                return;
            }

            columnsButton.relativePosition = new Vector3(Mathf.Max(5f, width - columnsButton.width - 20f), 80f);

            columnsPopup.relativePosition = new Vector3(Mathf.Max(5f, width - columnsPopup.width - 20f), 116f);

            // Keep the popup inside short windows.
            if (columnsPopup.relativePosition.y + columnsPopup.height > height - 16f)
            {
                Vector3 position = columnsPopup.relativePosition;
                position.y = Mathf.Max(58f, height - columnsPopup.height - 16f);
                columnsPopup.relativePosition = position;
            }

            columnsButton.BringToFront();

            if (columnsPopup.isVisible)
            {
                columnsPopup.BringToFront();
            }
        }

        private void SetMainColumnVisible(int index, bool visible)
        {
            if (index < 0 || index >= mainColumnVisibility.Length || index == 1 || index == 8)
            {
                return;
            }

            mainColumnVisibility[index] = visible;

            Vector2 previousScroll = FavCimsCitizenRowsPanel.scrollPosition;

            ApplyMainColumnLayout(FavCimsCitizenRowsPanel.width);

            var rows = FavCimsCitizenRowsPanel.GetComponentsInChildren<PanelsRows.CitizenRowView>();

            foreach (var row in rows)
            {
                row.RefreshColumnLayout();
            }

            // Preserve the current position; the scroll panel may clamp it
            // when the new content is shorter than the old content.
            FavCimsCitizenRowsPanel.scrollPosition = previousScroll;

            RefreshMainColumnControlText();
        }

        private void ApplyMainColumnLayout(float rowWidth)
        {
            float[] minimum =
            [
                60f, 110f, 90f, 40f, 120f, 120f, 120f, 140f, 65f
            ];

            float[] flexible =
            [
                0f, 0.20f, 0f, 0f, 0f, 0.25f, 0.25f, 0.30f, 0f
            ];

            float minimumTotal = 0f;
            float flexibleTotal = 0f;

            for (int i = 0; i < resizeHeaders.Length; i++)
            {
                if (!IsMainColumnVisible(i))
                {
                    continue;
                }

                minimumTotal += minimum[i];
                flexibleTotal += flexible[i];
            }

            float scale = Mathf.Min(1f, rowWidth / Mathf.Max(1f, minimumTotal));

            float extra = Mathf.Max(0f, rowWidth - minimumTotal);
            float x = 27f;

            FavCimsCloseButtonCol.textPadding = new RectOffset(0, 0, 0, 0);

            for (int i = 0; i < resizeHeaders.Length; i++)
            {
                bool visible = IsMainColumnVisible(i);
                resizeHeaders[i].isVisible = visible;

                if (!visible)
                {
                    continue;
                }

                float weight = flexibleTotal > 0f ? flexible[i] / flexibleTotal : (i == 1 ? 1f : 0f);

                float columnWidth = minimum[i] * scale + extra * weight;

                resizeHeaders[i].width = columnWidth;
                resizeHeaders[i].relativePosition = new Vector3(x, 8f);

                x += columnWidth;
            }
        }

        private void ReleaseMainColumnControls()
        {
            if (columnsButton != null)
            {
                columnsButton.eventClick -= ToggleMainColumnsPopup;
            }

            // Controls are children of this panel and are destroyed with it.
        }
    }
}