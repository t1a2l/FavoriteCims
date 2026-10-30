using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using UnityEngine;

namespace FavoriteCims.UI.PanelsRows
{
    public partial class CitizenRowView
    {
        private UIButton[] resizeRowHeaders;
        private UIComponent[] resizeRowColumns;
        private float[] previousColumnWidths;

        /// <summary>Uses header widths for every row column, including shared building cells.</summary>
        private void ApplyCurrentColumnWidths()
        {
            if (!IsReady) return;
            if (resizeRowHeaders == null)
            {
                resizeRowHeaders =
                [
                    FavoriteCimsMainPanel.FavCimsHappinesColText,
                    FavoriteCimsMainPanel.FavCimsNameColText,
                    FavoriteCimsMainPanel.FavCimsAgePhaseColText,
                    FavoriteCimsMainPanel.FavCimsAgeColText,
                    FavoriteCimsMainPanel.FavCimsEduColText,
                    FavoriteCimsMainPanel.FavCimsHomeColText,
                    FavoriteCimsMainPanel.FavCimsWorkingPlaceColText,
                    FavoriteCimsMainPanel.FavCimsLastActColText,
                    FavoriteCimsMainPanel.FavCimsCloseButtonCol
                ];
                resizeRowColumns =
                [
                    happinessIcon.parent, nameButton.parent, agePhaseButton.parent,
                    realAgeButton.parent, educationButton.parent, homeCell.Column,
                    workplaceCell.Column, activityButton.parent, closeButton.parent
                ];
                previousColumnWidths = new float[resizeRowHeaders.Length];
            }
            bool changed = false;
            for (int i = 0; i < resizeRowHeaders.Length; i++)
            {
                if (!Mathf.Approximately(previousColumnWidths[i], resizeRowHeaders[i].width)) changed = true;
            }
            if (!changed) return;
            float x = 0f;
            for (int i = 0; i < resizeRowColumns.Length; i++)
            {
                float columnWidth = resizeRowHeaders[i].width;
                previousColumnWidths[i] = columnWidth;
                resizeRowColumns[i].width = columnWidth;
                resizeRowColumns[i].relativePosition = new Vector3(x, 0f);
                x += columnWidth;
            }
            width = x;
            rowPanel.width = x;
            rowBackground.width = x;
            parent?.width = x;
            nameColumnWidth = resizeRowHeaders[1].width;
            homeColumnWidth = resizeRowHeaders[5].width;
            workplaceColumnWidth = resizeRowHeaders[6].width;
            nameColumnBackground.width = nameColumnWidth;
            ResizeCellWidth(nameButton, nameColumnWidth);
            ResizeCellWidth(agePhaseButton, resizeRowHeaders[2].width);
            ResizeCellWidth(realAgeButton, resizeRowHeaders[3].width);
            ResizeCellWidth(educationButton, resizeRowHeaders[4].width);
            homeCell.SetWidth(homeColumnWidth);
            workplaceCell.SetWidth(workplaceColumnWidth);
            ResizeCellWidth(activityButton, Mathf.Max(1f, resizeRowHeaders[7].width - 40f));
            citizenTypeLabel.maximumSize = Vector2.zero;
            citizenTypeLabel.width = Mathf.Max(1f, nameColumnWidth - 45f);
            Vector3 closePosition = closeButton.relativePosition;
            closePosition.x = (resizeRowHeaders[8].width - closeButton.width) * 0.5f;
            closeButton.relativePosition = closePosition;
            appliedContentHeight = -1f;
        }

        private static void ResizeCellWidth(UIButton button, float targetWidth)
        {
            button.maximumSize = Vector2.zero;
            button.width = Mathf.Max(1f, targetWidth);
        }
    }
}