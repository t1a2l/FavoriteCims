using System;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.Utils.Services;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public class FamilyPanelTemplate : UIPanel
    {
        private const float RefreshInterval = 0.3f;
        public InstanceID MyInstanceID;

        private FamilyPanelView view;
        private InstanceID renderedCitizen;
        private InstanceID homeTarget;
        private InstanceID workplaceTarget;
        private InstanceID vehicleTarget;
        private InstanceID activityTarget;
        private InstanceID personalVehicleTarget;
        private InstanceID petTarget;
        private float refreshTimer;
        private bool refreshPending = true;
        private bool initialized;
        private bool destroying;
        private bool dragging;
        private Vector3 dragMouseStart;
        private Vector3 dragPanelStart;

        public override void Start()
        {
            base.Start();
            try
            {
                view = new FamilyPanelView();
                view.CitizenRequested += OnCitizenRequested;
                view.InstanceRequested += OnInstanceRequested;
                view.HouseholdCitizenRequested += OnHouseholdCitizenRequested;
                view.HouseholdInstanceRequested += OnHouseholdInstanceRequested;
                view.HouseholdFavoriteRequested += OnHouseholdFavoriteRequested;
                view.CloseRequested += ClosePanel;
                view.DragStarted += OnDragStarted;
                view.DragEnded += OnDragEnded;
                eventVisibilityChanged += OnPanelVisibilityChanged;
                view.Build(this);
                initialized = view.IsReady;
                RequestRefresh();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Family panel initialization failed: " + ex);
                initialized = false;
                MyInstanceID = InstanceID.Empty;
                Hide();
                ReleaseView();
            }
        }

        public override void Update()
        {
            base.Update();
            if (destroying || !initialized) return;
            if (FavCimsMainClass.UnLoading)
            {
                EndDrag();
                return;
            }
            if (!isVisible) return;
            UpdateDrag();
            if (!IsCitizenSelection(MyInstanceID))
            {
                ClosePanel();
                return;
            }
            if (MyInstanceID != renderedCitizen)
            {
                ClearTargets();
                view.ResetForReuse();
                renderedCitizen = MyInstanceID;
                RequestRefresh();
            }
            refreshTimer -= Time.deltaTime;
            if (refreshTimer <= 0f) refreshPending = true;
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
            if (destroying || !initialized || !view.IsReady || FavCimsMainClass.UnLoading || !isVisible || !refreshPending) return;
            refreshPending = false;
            refreshTimer = RefreshInterval;
            try
            {
                if (!IsCitizenSelection(MyInstanceID) || !FamilyPanelDataService.TryRead(MyInstanceID, out FamilyPanelData snapshot) || snapshot == null || snapshot.SelectedCitizen == null)
                {
                    ClosePanel();
                    return;
                }
                var citizen = snapshot.SelectedCitizen;
                var activity = citizen.Activity;
                homeTarget = snapshot.Home != null ? snapshot.Home.InstanceId : InstanceID.Empty;
                workplaceTarget = citizen.Workplace != null ? citizen.Workplace.InstanceId : InstanceID.Empty;
                vehicleTarget = activity != null ? activity.VehicleId : InstanceID.Empty;
                activityTarget = activity != null ? activity.TargetId : InstanceID.Empty;
                personalVehicleTarget = citizen.OwnedVehicleInstanceId;
                petTarget = snapshot.Pet != null && snapshot.Pet.HasPet ? snapshot.Pet.InstanceId : InstanceID.Empty;

                view.RenderSectionLabels();
                view.RenderSelectedCitizen(citizen);
                view.RenderWorkplace(citizen.Workplace);
                view.RenderHome(snapshot);
                view.RenderSelectedActivity(activity);
                view.RenderPersonalVehicle(citizen);
                if (snapshot.HasHousehold) view.RenderHousehold(snapshot);
                else view.ClearHouseholdSection();
                view.RenderPet(snapshot.Pet);
                renderedCitizen = MyInstanceID;

                view.RefreshAdaptiveLayout();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Family panel refresh failed: " + ex);
                ClosePanel();
            }
        }

        private static bool IsCitizenSelection(InstanceID instance)
        {
            return !instance.IsEmpty && instance.Type == InstanceType.Citizen && instance.Citizen != 0;
        }

        private void RequestRefresh()
        {
            refreshPending = true;
            refreshTimer = 0f;
        }

        private void ClearTargets()
        {
            homeTarget = workplaceTarget = vehicleTarget = activityTarget = InstanceID.Empty;
            personalVehicleTarget = petTarget = InstanceID.Empty;
        }

        private void ResetBinding()
        {
            EndDrag();
            MyInstanceID = InstanceID.Empty;
            renderedCitizen = InstanceID.Empty;
            ClearTargets();
            refreshTimer = 0f;
            refreshPending = true;
            if (view != null && view.IsReady) view.ResetForReuse();
        }

        public void ClosePanel()
        {
            ResetBinding();
            if (isVisible) Hide();
        }

        private void OnPanelVisibilityChanged(UIComponent component, bool visible)
        {
            if (component != this || destroying) return;
            if (visible) RequestRefresh();
            else ResetBinding();
        }

        private InstanceID ResolveTarget(FamilyPanelTarget target)
        {
            return target switch
            {
                FamilyPanelTarget.MyInstanceID => MyInstanceID,
                FamilyPanelTarget.CitizenHomeID => homeTarget,
                FamilyPanelTarget.WorkPlaceID => workplaceTarget,
                FamilyPanelTarget.MyVehicleID => vehicleTarget,
                FamilyPanelTarget.MyTargetID => activityTarget,
                FamilyPanelTarget.PersonalVehicleID => personalVehicleTarget,
                FamilyPanelTarget.MyPetID => petTarget,
                _ => InstanceID.Empty,
            };
        }

        private void OnCitizenRequested(FamilyPanelTarget target, UIMouseEventParameter param)
        {
            GoToCitizen(ResolveTarget(target), param);
        }
        private void OnInstanceRequested(FamilyPanelTarget target, UIMouseEventParameter param)
        {
            GoToInstance(ResolveTarget(target), param);
        }
        private void OnHouseholdCitizenRequested(InstanceID target, UIMouseEventParameter param)
        {
            GoToCitizen(target, param);
        }
        private void OnHouseholdInstanceRequested(InstanceID target, UIMouseEventParameter param)
        {
            GoToInstance(target, param);
        }
        private void OnHouseholdFavoriteRequested(InstanceID target)
        {
            if (destroying || FavCimsMainClass.UnLoading || !isVisible || !IsCitizenSelection(target)) return;
            FavCimsCore.ToggleCitizenFavorite(target);
            RequestRefresh();
        }

        public void GoToCitizen(InstanceID target, UIMouseEventParameter param)
        {
            if (!CanNavigate(target, param) || !IsCitizenSelection(target)) return;
            var citizens = Singleton<CitizenManager>.instance.m_citizens.m_buffer;
            if (target.Citizen >= (uint)citizens.Length || (citizens[(int)target.Citizen].m_flags & Citizen.Flags.Created) == 0) return;
            try
            {
                var camera = ToolsModifierControl.cameraController;
                if (camera == null) return;
                FavCimsMainClass.FavCimsPanel?.Hide();
                bool middle = (param.buttons & UIMouseButton.Middle) != 0;
                bool right = (param.buttons & UIMouseButton.Right) != 0;
                bool left = (param.buttons & UIMouseButton.Left) != 0;
                if (left || right) camera.SetTarget(target, camera.transform.position, true);
                if (middle || right) WorldInfoPanel.Show<CitizenWorldInfoPanel>(position, target);
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Family citizen navigation failed: " + ex);
            }
        }

        public void GoToInstance(InstanceID target, UIMouseEventParameter param)
        {
            if (!CanNavigate(target, param)) return;
            try
            {
                var camera = ToolsModifierControl.cameraController;
                if (camera == null) return;
                FavCimsMainClass.FavCimsPanel?.Hide();
                bool middle = (param.buttons & UIMouseButton.Middle) != 0;
                if (!middle) camera.SetTarget(target, camera.transform.position, true);
                DefaultTool.OpenWorldInfoPanel(target, camera.transform.position);
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Family target navigation failed: " + ex);
            }
        }

        private bool CanNavigate(InstanceID target, UIMouseEventParameter param)
        {
            return !destroying && initialized && isVisible && !FavCimsMainClass.UnLoading && !target.IsEmpty && param != null && (param.buttons & (UIMouseButton.Left | UIMouseButton.Right | UIMouseButton.Middle)) != 0;
        }

        private void OnDragStarted(UIComponent component, UIMouseEventParameter param)
        {
            if (destroying || !isVisible || param == null || (param.buttons & UIMouseButton.Left) == 0) return;
            dragging = true;
            dragMouseStart = Input.mousePosition;
            dragPanelStart = absolutePosition;
            opacity = 0.5f;
        }
        private void OnDragEnded(UIComponent component, UIMouseEventParameter param)
        {
            EndDrag();
        }
        private void UpdateDrag()
        {
            if (!dragging) return;
            if (!Input.GetMouseButton(0))
            {
                EndDrag();
                return;
            }
            Vector3 delta = Input.mousePosition - dragMouseStart;
            absolutePosition = dragPanelStart + new Vector3(delta.x, -delta.y, 0f);
        }
        private void EndDrag()
        {
            dragging = false;
            opacity = 1f;
        }

        private void ReleaseView()
        {
            eventVisibilityChanged -= OnPanelVisibilityChanged;
            if (view == null) return;
            view.CitizenRequested -= OnCitizenRequested;
            view.InstanceRequested -= OnInstanceRequested;
            view.HouseholdCitizenRequested -= OnHouseholdCitizenRequested;
            view.HouseholdInstanceRequested -= OnHouseholdInstanceRequested;
            view.HouseholdFavoriteRequested -= OnHouseholdFavoriteRequested;
            view.CloseRequested -= ClosePanel;
            view.DragStarted -= OnDragStarted;
            view.DragEnded -= OnDragEnded;
            view.Dispose();
            view = null;
        }

        public override void OnDestroy()
        {
            destroying = true;
            initialized = false;
            EndDrag();
            MyInstanceID = InstanceID.Empty;
            ClearTargets();
            try { ReleaseView(); }
            finally { base.OnDestroy(); }
        }
    }
}