using System;
using System.Collections.Generic;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI
{
    public struct RosterRowPlacement(UIComponent row, bool? isPresent)
    {
        public UIComponent Row = row;
        public bool? IsPresent = isPresent;
    }

    // All roster rows and headers remain direct children of the scroll panel.
    // This class owns labels, ordering, and section collapse visibility only.
    public sealed class RosterSectionsLayout
    {
        private readonly UIPanel presentHeader;
        private readonly UIButton presentText;
        private readonly UIPanel awayHeader;
        private readonly UIButton awayText;
        private readonly UIPanel unknownHeader;
        private readonly UIButton unknownText;

        private readonly BuildingSectionCollapse presentCollapse;
        private readonly BuildingSectionCollapse awayCollapse;
        private readonly BuildingSectionCollapse unknownCollapse;

        private readonly List<UIComponent> present = [];
        private readonly List<UIComponent> away = [];
        private readonly List<UIComponent> unknown = [];
        private readonly HashSet<UIComponent> seen = [];

        public int PresentCount => present.Count;

        public int AwayCount => away.Count;

        public int UnknownCount => unknown.Count;

        /// <summary>
        /// Raised once after a refresh changes roster-section visibility.
        /// The building panel should recalculate its visible body height.
        /// </summary>
        public event Action SectionsChanged;

        public RosterSectionsLayout(UIScrollablePanel bodyRows, UIPanel existingRosterHeader, UIButton existingRosterText)
        {
            presentHeader = existingRosterHeader;
            presentText = existingRosterText;

            awayHeader = CreateHeader(bodyRows, "RosterAwayHeader", existingRosterHeader, existingRosterText, out awayText);

            unknownHeader = CreateHeader(bodyRows, "RosterUnknownHeader", existingRosterHeader, existingRosterText, out unknownText);

            presentCollapse = new BuildingSectionCollapse(presentHeader, "RosterPresentCollapse", presentText);

            awayCollapse = new BuildingSectionCollapse(awayHeader, "RosterAwayCollapse", awayText);

            unknownCollapse = new BuildingSectionCollapse(unknownHeader, "RosterUnknownCollapse", unknownText);

            presentCollapse.StateChanged += OnSectionChanged;
            awayCollapse.StateChanged += OnSectionChanged;
            unknownCollapse.StateChanged += OnSectionChanged;

            Hide();
        }

        public void Apply(IList<RosterRowPlacement> rows, string presentLabel, string awayLabel, string unknownLabel)
        {
            present.Clear();
            away.Clear();
            unknown.Clear();
            seen.Clear();

            for (int i = 0; i < rows.Count; i++)
            {
                var item = rows[i];

                if (item.Row == null || !seen.Add(item.Row))
                {
                    continue;
                }

                if (!item.IsPresent.HasValue)
                {
                    unknown.Add(item.Row);
                }
                else if (item.IsPresent.Value)
                {
                    present.Add(item.Row);
                }
                else
                {
                    away.Add(item.Row);
                }
            }

            if (seen.Count == 0)
            {
                Hide();
                return;
            }

            int order = presentHeader.zOrder;

            presentHeader.zOrder = order++;
            presentText.text = presentLabel + " (" + present.Count + ")";
            presentHeader.Show();
            OrderRows(present, ref order);

            awayHeader.zOrder = order++;
            awayText.text = awayLabel + " (" + away.Count + ")";
            awayHeader.Show();
            OrderRows(away, ref order);

            unknownHeader.zOrder = order++;
            unknownText.text = unknownLabel + " (" + unknown.Count + ")";
            unknownHeader.isVisible = unknown.Count > 0;
            OrderRows(unknown, ref order);

            // Do not raise three separate layout events during one refresh.
            presentCollapse.SetRows(present, raiseEvent: false);
            awayCollapse.SetRows(away, raiseEvent: false);
            unknownCollapse.SetRows(unknown, raiseEvent: false);

            SectionsChanged?.Invoke();
        }

        public void Hide()
        {
            presentHeader.Hide();
            awayHeader.Hide();
            unknownHeader.Hide();

            presentCollapse.ClearRows(raiseEvent: false);
            awayCollapse.ClearRows(raiseEvent: false);
            unknownCollapse.ClearRows(raiseEvent: false);

            present.Clear();
            away.Clear();
            unknown.Clear();
            seen.Clear();
        }

        /// <summary>Expands every worker section, for example when the selected building changes.</summary>
        public void ResetCollapseStates()
        {
            presentCollapse.Reset();
            awayCollapse.Reset();
            unknownCollapse.Reset();
        }

        private void OnSectionChanged()
        {
            SectionsChanged?.Invoke();
        }

        private static void OrderRows(List<UIComponent> rows, ref int order)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i].zOrder = order++;
            }
        }

        private static UIPanel CreateHeader(UIScrollablePanel parent, string name, UIPanel template, UIButton textTemplate, out UIButton text)
        {
            var panel = parent.AddUIComponent<UIPanel>();

            panel.name = name;
            panel.width = template.width;
            panel.height = 25f;
            panel.autoLayout = false;
            panel.relativePosition = Vector3.zero;
            panel.atlas = MyAtlas.FavCimsAtlas;
            panel.backgroundSprite = "BgRow2";

            var icon = panel.AddUIComponent<UIButton>();

            icon.name = name + "Icon";
            icon.width = 17f;
            icon.height = 17f;
            icon.atlas = MyAtlas.FavCimsAtlas;
            icon.normalFgSprite = "WorkersIcon";
            icon.isInteractive = false;
            icon.playAudioEvents = false;
            icon.relativePosition = new Vector3(5f, 4f);

            text = panel.AddUIComponent<UIButton>();

            text.name = name + "Text";
            text.width = panel.width - 40f;
            text.height = 25f;
            text.font = textTemplate.font;
            text.textScale = textTemplate.textScale;
            text.textHorizontalAlignment = UIHorizontalAlignment.Left;
            text.textVerticalAlignment = UIVerticalAlignment.Middle;
            text.textPadding = new RectOffset(5, 5, 0, 0);
            text.wordWrap = false;

            text.textColor = textTemplate.textColor;
            text.hoveredTextColor = textTemplate.textColor;
            text.pressedTextColor = textTemplate.textColor;
            text.focusedTextColor = textTemplate.textColor;
            text.disabledTextColor = textTemplate.textColor;

            text.useDropShadow = textTemplate.useDropShadow;
            text.dropShadowOffset = textTemplate.dropShadowOffset;
            text.dropShadowColor = textTemplate.dropShadowColor;

            text.isInteractive = false;
            text.playAudioEvents = false;
            text.relativePosition = new Vector3(22f, 1f);

            panel.Hide();

            return panel;
        }
    }
}