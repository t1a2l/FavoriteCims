using System;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.PanelsRows;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class CitizenRow : UIPanel
    {
        private const float RowWidth = 1134f;
        private const float RowHeight = 41f;

        private const float ActiveRefreshInterval = 0.5f;
        private const float InactiveRefreshInterval = 30f;

        private CitizenRowView view;

        private CitizenRowData data;
        private bool hasValidData;

        private InstanceID citizenInstance;
        private string instancedName;
        private uint citizenId;

        private bool initialRefreshPending = true;

        private float activeRefreshTimer;
        private float inactiveRefreshTimer;

        private FamilyPanelTemplate familyPanel;

        // A transient read failure must not repeatedly reroll the death/gone decision.
        private bool missingStateResolved;
        private bool resolvedAsGone;

        private MouseEventHandler citizenClickHandler;
        private MouseEventHandler homeClickHandler;
        private MouseEventHandler workClickHandler;
        private MouseEventHandler targetClickHandler;
        private MouseEventHandler vehicleClickHandler;

        public InstanceID MyInstanceID
        {
            get => citizenInstance;
            set
            {
                citizenInstance = value;
                citizenId = value.Citizen;

                if (view != null)
                {
                    RefreshRow();
                }
            }
        }

        public string MyInstancedName
        {
            get => instancedName;
            set
            {
                instancedName = value;

                if (view != null && view.IsReady)
                {
                    view.SetName(instancedName);
                }
            }
        }

        public override void Start()
        {
            try
            {
                width = RowWidth;
                height = RowHeight;
                autoLayoutDirection = LayoutDirection.Vertical;
                autoLayout = false;
                autoLayoutPadding = new RectOffset(0, 0, 1, 0);

                citizenClickHandler = (component, param) => OnCitizenClicked(param);
                homeClickHandler = (component, param) => OnHomeClicked(param);
                workClickHandler = (component, param) => OnWorkClicked(param);
                targetClickHandler = (component, param) => OnTargetClicked(param);
                vehicleClickHandler = (component, param) => OnVehicleClicked(param);

                view = AddUIComponent<CitizenRowView>();
                view.relativePosition = new Vector3(0f, 1f);
                view.PreferredHeightChanged += OnPreferredHeightChanged;

                view.CitizenClicked += citizenClickHandler;
                view.HomeClicked += homeClickHandler;
                view.WorkClicked += workClickHandler;
                view.TargetClicked += targetClickHandler;
                view.VehicleClicked += vehicleClickHandler;
                view.FamilyClicked += OnFamilyClicked;
                view.CloseClicked += OnCloseClicked;

                if (citizenId != 0)
                {
                    RefreshRow();
                }
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("CitizenRow initialization failed: " + ex);
            }
        }

        public override void Update()
        {
            base.Update();

            if (FavCimsMainClass.UnLoading || view == null || !view.IsReady)
            {
                return;
            }

            bool mainPanelVisible = FavCimsMainClass.FavCimsPanel != null && FavCimsMainClass.FavCimsPanel.isVisible;

            // Do not hide a row because its old geometry is temporarily
            // clipped while the viewport and columns are being resized.
            // The scroll panel remains responsible for visual clipping.
            view.SetRowVisible(mainPanelVisible);

            if (initialRefreshPending)
            {
                initialRefreshPending = false;

                RefreshRow();

                activeRefreshTimer = ActiveRefreshInterval;
                inactiveRefreshTimer = InactiveRefreshInterval;

                return;
            }

            // Use clipping only to choose the data-refresh frequency.
            bool refreshAsVisible = mainPanelVisible && !IsClippedFromParent();

            if (refreshAsVisible)
            {
                inactiveRefreshTimer = InactiveRefreshInterval;
                activeRefreshTimer -= Time.deltaTime;

                if (activeRefreshTimer <= 0f)
                {
                    activeRefreshTimer = ActiveRefreshInterval;
                    RefreshRow();
                }
            }
            else
            {
                inactiveRefreshTimer -= Time.deltaTime;

                if (inactiveRefreshTimer <= 0f)
                {
                    inactiveRefreshTimer = InactiveRefreshInterval;
                    RefreshRow();
                }
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            if (FavCimsMainClass.UnLoading)
            {
                return;
            }

            if (citizenInstance.IsEmpty || citizenId == 0 || !FavCimsCore.RowID.Contains(citizenId))
            {
                DestroyRow();
            }
        }

        private void OnPreferredHeightChanged(float contentHeight)
        {
            height = contentHeight + 1f;
        }

        private void RefreshRow()
        {
            if (view == null || !view.IsReady)
            {
                return;
            }

            try
            {
                if (!CitizenRowDataService.TryRead(citizenInstance, out var newData))
                {
                    // A failed read is not proof that the citizen died or left.
                    // Keep the last successful snapshot and resolve its state only once.
                    if (!hasValidData)
                    {
                        DestroyRow();
                        return;
                    }

                    ResolveMissingCitizenState();
                    return;
                }

                data = newData;
                hasValidData = true;

                instancedName = data.Name;

                view.SetName(data.Name);
                view.SetCitizenType(data.IsTourist);
                view.SetGender(data.Gender);
                view.SetEducation(data.Education, data.Gender);
                view.SetHappiness(data.HappinessLevel);
                view.SetAge(data.GameAge, data.RealAge, data.Gender);
                view.SetCurrentDistrict(GetDistrictName(data.Home.DistrictId));

                RefreshHome();
                RefreshWorkplace();
                RefreshActivity();
                RefreshSpecialStates();
                RefreshFamilyButton();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("CitizenRow refresh failed: " + ex);
            }
        }

        private void RefreshHome()
        {
            if (data.Home.CanNavigate)
            {
                view.SetHome(data);
                return;
            }

            if (data.IsTourist)
            {
                view.SetHomeOutsideCity();
                return;
            }

            view.SetNoLocalHome();
        }

        private void RefreshWorkplace()
        {
            if (data.IsStudent)
            {
                view.SetStudent(data);
                return;
            }

            if (!data.Workplace.CanNavigate)
            {
                if (data.IsTourist)
                {
                    view.SetTouristWorkplace(data);
                }
                else if (data.GameAge > 180)
                {
                    view.SetRetiredWorkplace();
                }
                else
                {
                    view.SetUnemployedWorkplace();
                }

                return;
            }

            view.SetWorkplace(data);
        }

        private void RefreshActivity()
        {
            view.SetActivity(data);
        }

        private void RefreshSpecialStates()
        {
            if (data.IsDead)
            {
                view.SetDead(data);
                return;
            }

            if (data.IsArrested && data.IsCriminal)
            {
                view.SetArrested(data);
                return;
            }

            view.SetNormalHappiness(data.HappinessLevel);
        }

        private void RefreshFamilyButton()
        {
            if (view == null || !view.IsReady)
            {
                return;
            }

            var openPanel = FindOpenFamilyTemplate();

            bool isOpen = openPanel != null && openPanel.MyInstanceID == citizenInstance;

            bool canShowFamily = hasValidData && !data.IsDead && !data.HasLeftCity && data.Home.CanNavigate;

            bool hasAvailablePanel = openPanel != null || GetTemplate() != -1;

            view.SetFamilyButtonState(canShowFamily && hasAvailablePanel, isOpen);
        }

        private void ResolveMissingCitizenState()
        {
            if (!hasValidData)
            {
                DestroyRow();
                return;
            }

            if (data.CitizenId == 0)
            {
                DestroyRow();
                return;
            }

            if (!missingStateResolved)
            {
                resolvedAsGone = ResolveGoneInsteadOfDead(data.GameAge);
                missingStateResolved = true;
            }

            data.HasLeftCity = resolvedAsGone;

            if (data.HasLeftCity)
            {
                view.SetGone(data);
            }
            else
            {
                view.SetDead(data);
            }
        }

        private static bool ResolveGoneInsteadOfDead(int gameAge)
        {
            int goneChance = gameAge switch
            {
                >= 180 => 1,
                >= 130 => 30,
                >= 90 => 50,
                >= 40 => 70,
                _ => 98
            };

            return UnityEngine.Random.Range(0, 100) < goneChance;
        }

        private static string GetDistrictName(int districtId)
        {
            var districtManager = Singleton<DistrictManager>.instance;

            return districtId == 0 ? FavCimsLang.Text("DistrictNameNoDistrict") : districtManager.GetDistrictName(districtId);
        }

        private void OnCitizenClicked(UIMouseEventParameter eventParam)
        {
            CitizenNavigationService.Navigate(citizenInstance, eventParam, hideFavoritesPanel: false, useCitizenPanel: true, isTourist: data.IsTourist);
        }

        private void OnHomeClicked(UIMouseEventParameter eventParam)
        {
            if (!hasValidData || !data.Home.CanNavigate)
            {
                return;
            }

            CitizenNavigationService.Navigate(data.Home.InstanceId, eventParam, hideFavoritesPanel: true, useCitizenPanel: true, isTourist: data.IsTourist);
        }

        private void OnWorkClicked(UIMouseEventParameter eventParam)
        {
            if (!hasValidData || !data.Workplace.CanNavigate)
            {
                return;
            }

            CitizenNavigationService.Navigate(data.Workplace.InstanceId, eventParam, hideFavoritesPanel: true, useCitizenPanel: true, isTourist: data.IsTourist);
        }

        private void OnTargetClicked(UIMouseEventParameter eventParam)
        {
            CitizenNavigationService.Navigate(data.TargetInstanceId, eventParam, hideFavoritesPanel: true, useCitizenPanel: true, isTourist: data.IsTourist);
        }

        private void OnVehicleClicked(UIMouseEventParameter eventParam)
        {
            CitizenNavigationService.Navigate(data.VehicleInstanceId, eventParam, hideFavoritesPanel: true, useCitizenPanel: true, isTourist: data.IsTourist);
        }

        private void OnFamilyClicked()
        {
            if (view == null || !view.IsReady || !hasValidData)
            {
                return;
            }

            var openPanel = FindOpenFamilyTemplate();

            if (openPanel != null && openPanel.MyInstanceID == citizenInstance)
            {
                // Also clean up any overlapping windows from the old behavior.
                for (int i = 0; i < FavCimsMainClass.Templates.Length; i++)
                {
                    var template = FavCimsMainClass.Templates[i];

                    if (template != null && template.isVisible)
                    {
                        template.ClosePanel();
                    }
                }

                familyPanel = null;

                RefreshFamilyButton();
                return;
            }

            if (data.IsDead || data.HasLeftCity || !data.Home.CanNavigate)
            {
                return;
            }

            try
            {
                // Prefer switching the currently visible window.
                FamilyPanelTemplate targetPanel = openPanel;

                bool preservePosition = targetPanel != null;

                Vector3 previousPosition = preservePosition ? targetPanel.absolutePosition : Vector3.zero;

                // Only allocate an unused template when no window is open.
                if (targetPanel == null)
                {
                    int templateIndex = GetTemplate();

                    if (templateIndex == -1)
                    {
                        Utils.Debug.Warning("Cannot open citizen family details: no free family panel template.");

                        RefreshFamilyButton();
                        return;
                    }

                    targetPanel = FavCimsMainClass.Templates[templateIndex];
                }

                // Enforce one visible family window.
                for (int i = 0; i < FavCimsMainClass.Templates.Length; i++)
                {
                    var template = FavCimsMainClass.Templates[i];

                    if (template != null && template != targetPanel && template.isVisible)
                    {
                        template.ClosePanel();
                    }
                }

                // Use the existing cleanup/reset path before changing citizens.
                targetPanel.ClosePanel();

                familyPanel = targetPanel;
                familyPanel.MyInstanceID = citizenInstance;
                familyPanel.Show();

                if (preservePosition)
                {
                    familyPanel.absolutePosition = previousPosition;
                }
                else
                {
                    PositionFamilyPanel();
                }

                familyPanel.BringToFront();

                RefreshFamilyButton();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to open citizen details: " + ex);
            }
        }

        private void OnCloseClicked()
        {
            try
            {
                if (!FavCimsCore.RemoveFavorite(citizenInstance))
                {
                    return;
                }

                DestroyRow();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to remove citizen row: " + ex);
            }
        }

        private void CloseFamilyPanel()
        {
            if (familyPanel == null)
            {
                return;
            }

            // Do not close a template that has been reused for another citizen.
            if (familyPanel.MyInstanceID == citizenInstance)
            {
                familyPanel.ClosePanel();
            }

            familyPanel = null;

            if (view != null && view.IsReady && hasValidData)
            {
                RefreshFamilyButton();
            }
        }

        private void DestroyRow()
        {
            if (familyPanel != null && familyPanel.MyInstanceID == citizenInstance)
            {
                CloseFamilyPanel();
            }
            else
            {
                familyPanel = null;
            }

            Destroy(gameObject);
        }

        public override void OnDestroy()
        {
            if (view != null)
            {
                view.PreferredHeightChanged -= OnPreferredHeightChanged;
                view.CitizenClicked -= citizenClickHandler;
                view.HomeClicked -= homeClickHandler;
                view.WorkClicked -= workClickHandler;
                view.TargetClicked -= targetClickHandler;
                view.VehicleClicked -= vehicleClickHandler;

                view.FamilyClicked -= OnFamilyClicked;
                view.CloseClicked -= OnCloseClicked;
            }

            base.OnDestroy();
        }

        private static int GetTemplate()
        {
            for (int i = 0; i < FavCimsMainClass.Templates.Length; i++)
            {
                var template = FavCimsMainClass.Templates[i];

                if (template != null && template.MyInstanceID.IsEmpty)
                {
                    return i;
                }
            }

            return -1;
        }

        private void PositionFamilyPanel()
        {
            var mainPanel = FavCimsMainClass.FavCimsPanel;
            var container = FavCimsMainClass.FullScreenContainer;

            if (familyPanel == null || mainPanel == null || container == null)
            {
                return;
            }

            const float gap = 8f;
            const float margin = 8f;

            float panelWidth = familyPanel.width > 0f ? familyPanel.width : 250f;

            float panelHeight = familyPanel.height > 0f ? familyPanel.height : 500f;

            float left = container.absolutePosition.x + margin;
            float top = container.absolutePosition.y + margin;

            float right = container.absolutePosition.x + container.width - margin;

            float bottom = container.absolutePosition.y + container.height - margin;

            float x = mainPanel.absolutePosition.x - panelWidth - gap;

            // If there is no room outside the main panel, use its left edge.
            if (x < left)
            {
                x = mainPanel.absolutePosition.x + gap;
            }

            float y = mainPanel.absolutePosition.y;

            x = Mathf.Clamp(x, left, Mathf.Max(left, right - panelWidth));
            y = Mathf.Clamp(y, top, Mathf.Max(top, bottom - panelHeight));

            familyPanel.absolutePosition = new Vector3(x, y, 0f);
        }

        private static FamilyPanelTemplate FindOpenFamilyTemplate()
        {
            for (int i = 0; i < FavCimsMainClass.Templates.Length; i++)
            {
                var template = FavCimsMainClass.Templates[i];

                if (template != null && template.isVisible && !template.MyInstanceID.IsEmpty)
                {
                    return template;
                }
            }
            return null;
        }
    }
}