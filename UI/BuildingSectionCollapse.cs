using System;
using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.UI
{
    /// <summary>
    /// Adds a plus/minus toggle to one building-section header.
    /// It hides or shows the rows assigned to that section; the header remains visible.
    /// </summary>
    public sealed class BuildingSectionCollapse
    {
        private readonly UIButton toggle;
        private readonly List<UIComponent> rows = [];

        private bool expanded = true;

        public event Action StateChanged;

        public bool IsExpanded => expanded;

        public BuildingSectionCollapse(UIPanel header, string toggleName, UIButton textTemplate)
        {
            toggle = header.AddUIComponent<UIButton>();

            toggle.name = toggleName;
            toggle.width = 18f;
            toggle.height = 18f;
            toggle.relativePosition = new Vector3(header.width - 24f, 4f);

            toggle.font = textTemplate.font;
            toggle.textScale = textTemplate.textScale;
            toggle.text = "−";

            toggle.textColor = textTemplate.textColor;
            toggle.hoveredTextColor = textTemplate.textColor;
            toggle.pressedTextColor = textTemplate.textColor;
            toggle.focusedTextColor = textTemplate.textColor;
            toggle.disabledTextColor = textTemplate.textColor;

            toggle.isInteractive = true;
            toggle.playAudioEvents = false;
            toggle.textVerticalAlignment = UIVerticalAlignment.Middle;
            toggle.textHorizontalAlignment = UIHorizontalAlignment.Center;

            toggle.eventClick += OnToggleClicked;
        }

        public void SetRows(IEnumerable<UIComponent> sectionRows, bool raiseEvent = true)
        {
            rows.Clear();

            if (sectionRows != null)
            {
                foreach (var row in sectionRows)
                {
                    if (row != null)
                    {
                        rows.Add(row);
                    }
                }
            }

            Apply(raiseEvent);
        }

        public void ClearRows(bool raiseEvent = true)
        {
            rows.Clear();
            Apply(raiseEvent);
        }

        public void SetExpanded(bool value, bool raiseEvent = true)
        {
            expanded = value;
            Apply(raiseEvent);
        }

        /// <summary>Restores the default expanded state, for example when another building is selected.</summary>
        public void Reset()
        {
            expanded = true;
            Apply(raiseEvent: false);
        }

        private void Apply(bool raiseEvent)
        {
            toggle.text = expanded ? "−" : "+";

            for (int i = 0; i < rows.Count; i++)
            {
                rows[i].isVisible = expanded;
            }

            if (raiseEvent)
            {
                StateChanged?.Invoke();
            }
        }

        private void OnToggleClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            SetExpanded(!expanded);
        }
    }
}