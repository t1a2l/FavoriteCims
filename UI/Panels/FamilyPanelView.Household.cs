using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public sealed partial class FamilyPanelView
    {
        private readonly FamilyMemberRow[] householdRows = new FamilyMemberRow[5];
        private UIPanel noPartnerPanel;
        private UIPanel noChildrenPanel;
        private UILabel noPartnerLabel;
        private UILabel noChildrenLabel;

        private void InitializeHouseholdRows(UIComponent parent)
        {
            noPartnerPanel = CreateHouseholdPlaceholder(parent, "NoPartner", out noPartnerLabel);
            householdRows[0] = CreateHouseholdRow(parent, "FirstAdult");
            householdRows[1] = CreateHouseholdRow(parent, "SecondAdult");
            noChildrenPanel = CreateHouseholdPlaceholder(parent, "NoChildren", out noChildrenLabel);
            for (int slot = 2; slot < 5; slot++)
            { 
                householdRows[slot] = CreateHouseholdRow(parent, "Member" + slot); 
            }
            RefreshHouseholdLabels();
        }

        private FamilyMemberRow CreateHouseholdRow(UIComponent parent, string name)
        {
            var row = new FamilyMemberRow(parent, name, RenderActivityControls, GetFamilyMemberAgeColor);
            row.CitizenRequested += ForwardMemberCitizen;
            row.InstanceRequested += ForwardMemberInstance;
            row.FavoriteRequested += ForwardMemberFavorite;
            return row;
        }

        private static UIPanel CreateHouseholdPlaceholder(UIComponent parent, string name, out UILabel label)
        {
            var panel = parent.AddUIComponent<UIPanel>();
            panel.name = name;
            panel.width = parent.width;
            panel.height = 52f;
            panel.Hide();
            label = panel.AddUIComponent<UILabel>();
            label.autoSize = false;
            label.size = new Vector2(panel.width - 20f, 42f);
            label.relativePosition = new Vector3(10f, 5f);
            label.font = UIDynamicFont.FindByName("OpenSans-Regular");
            label.textScale = 0.9f;
            label.wordWrap = true;
            label.textColor = new Color32(51, 51, 51, 255);
            label.isInteractive = false;
            return panel;
        }

        private void RefreshHouseholdLabels()
        {
            noPartnerLabel?.text = FavCimsLang.Text("CitizenNoPartnerLabel");
            noChildrenLabel?.text = FavCimsLang.Text("CitizenNoChildrenLabel");
        }

        public void RenderHousehold(FamilyPanelData snapshot)
        {
            if (!IsReady) return;
            if (snapshot == null || !snapshot.HasHousehold || snapshot.SelectedCitizen == null)
            {
                ClearHouseholdSection();
                return;
            }
            RefreshHouseholdLabels();
            var selected = snapshot.SelectedCitizen;
            bool laterSlot = selected.HouseholdSlot >= 2;
            FamilyCitizenData first = null;
            FamilyCitizenData second = null;
            if (selected.HouseholdSlot == 0) first = FindHouseholdSlot(snapshot, 1);
            else if (selected.HouseholdSlot == 1) first = FindHouseholdSlot(snapshot, 0);
            else if (laterSlot)
            {
                first = FindHouseholdSlot(snapshot, 0);
                second = FindHouseholdSlot(snapshot, 1);
            }
            householdRows[0].Bind(first, laterSlot ? "InfoIconAge" : "InfoIconHealth");
            householdRows[1].Bind(second, "InfoIconAge");
            int visibleLaterMembers = 0;
            for (int slot = 2; slot < 5; slot++)
            {
                var member = FindHouseholdSlot(snapshot, slot);
                if (member != null && member.CitizenId == selected.CitizenId) member = null;
                householdRows[slot].Bind(member, null);
                if (member != null) visibleLaterMembers++;
            }
            noPartnerPanel.isVisible = !laterSlot && first == null;
            noChildrenPanel.isVisible = !laterSlot && visibleLaterMembers == 0;
            RenderHouseholdVehicle(snapshot);

            // All text and household-row heights have now been assigned.
            RefreshAdaptiveLayout();
        }

        public void ClearHouseholdSection()
        {
            if (!IsReady) return;
            foreach (var row in householdRows) row?.Clear();
            noPartnerPanel?.Hide();
            noChildrenPanel?.Hide();
            FamilyVehicleID = InstanceID.Empty;
            BubbleFamilyBarCarButton.texture = TextureDB.BubbleCarDisabled;
            BubbleFamilyBarCarButton.tooltip = string.Empty;
            BubbleFamilyBarCarButton.playAudioEvents = false;

            // Resize after hiding the household rows.
            RefreshAdaptiveLayout();
        }

        private void ForwardMemberCitizen(InstanceID target, UIMouseEventParameter param)
        {
            HouseholdCitizenRequested?.Invoke(target, param);
        }
        private void ForwardMemberInstance(InstanceID target, UIMouseEventParameter param)
        {
            HouseholdInstanceRequested?.Invoke(target, param);
        }
        private void ForwardMemberFavorite(InstanceID target)
        {
            HouseholdFavoriteRequested?.Invoke(target);
        }

        private void ReleaseHouseholdRows()
        {
            for (int i = 0; i < householdRows.Length; i++)
            {
                var row = householdRows[i];
                if (row == null) continue;
                row.CitizenRequested -= ForwardMemberCitizen;
                row.InstanceRequested -= ForwardMemberInstance;
                row.FavoriteRequested -= ForwardMemberFavorite;
                row.Dispose();
                householdRows[i] = null;
            }
        }
    }
}