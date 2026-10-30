using System;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using FavoriteCims.Utils.Services;

namespace FavoriteCims.UI.Panels
{
    public sealed partial class FamilyPanelView
    {
        public void RenderSelectedActivity(FamilyActivityData activity)
        {
            if (!IsReady) return;
            RenderActivityControls(activity, FavCimsLastActivityVehicleButton, FavCimsLastActivity);
            FavCimsDistrictValue.text = activity == null || !activity.HasCurrentPosition ? string.Empty : activity.CurrentDistrictId == 0 ? FavCimsLang.Text("DistrictNameNoDistrict") : activity.CurrentDistrictName;
            FavCimsDistrictValue.tooltip = activity != null && activity.HasCurrentPosition ? FavCimsLang.Text("DistrictLabelTooltip") : string.Empty;
        }

        public void RenderPersonalVehicle(FamilyCitizenData citizen)
        {
            if (!IsReady) return;
            BubblePersonalCarButton.texture = TextureDB.BubbleCarDisabled;
            BubblePersonalCarButton.tooltip = string.Empty;
            BubblePersonalCarButton.playAudioEvents = false;
            if (citizen == null || !citizen.HasOwnedVehicle) return;
            BubblePersonalCarButton.texture = TextureDB.BubbleCar;
            BubblePersonalCarButton.playAudioEvents = true;
            BubblePersonalCarButton.tooltip = citizen.OwnedVehicleName + (citizen.OwnedVehicleIsParked ? " (" + Locale.Get("VEHICLE_STATUS_PARKED") + ")" : string.Empty) + " - " + Locale.Get("VEHICLE_OWNER") + " " + citizen.Name;
            BubblePersonalCarButton.tooltip = FamilyPanelText.AppendHint(BubblePersonalCarButton.tooltip, FamilyPanelText.InstanceNavigation);
        }

        private void RenderActivityControls(FamilyActivityData activity, UIButton vehicleButton, UIButton destinationButton)
        {
            vehicleButton.atlas = UIView.GetAView().defaultAtlas;
            vehicleButton.normalBgSprite = string.Empty;
            vehicleButton.hoveredBgSprite = string.Empty;
            vehicleButton.focusedBgSprite = string.Empty;
            vehicleButton.pressedBgSprite = string.Empty;
            vehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
            vehicleButton.isEnabled = false;
            vehicleButton.playAudioEvents = false;
            vehicleButton.tooltip = string.Empty;
            destinationButton.text = string.Empty;
            destinationButton.tooltip = string.Empty;
            destinationButton.isEnabled = false;
            if (activity == null) return;

            destinationButton.text = JoinFamilyActivity(activity.Status, activity.TargetName);
            destinationButton.isEnabled = !activity.TargetId.IsEmpty;
            if (activity.TargetId.Type == InstanceType.Building && !activity.TargetId.IsEmpty)
            { 
                destinationButton.tooltip = FavCimsLang.Text("DistrictLabel") + " " + (string.IsNullOrEmpty(activity.TargetDistrictName) ? FavCimsLang.Text("DistrictNameNoDistrict") : activity.TargetDistrictName); 
            }

            if (activity.HasVehicle)
            {
                string sprite = GetActivityVehicleSprite(activity);
                bool transportSprite = sprite.StartsWith("SubBar", StringComparison.Ordinal);
                vehicleButton.normalBgSprite = sprite;
                vehicleButton.hoveredBgSprite = transportSprite ? sprite + "Hovered" : sprite;
                vehicleButton.focusedBgSprite = transportSprite ? sprite + "Focused" : sprite;
                vehicleButton.pressedBgSprite = transportSprite ? sprite + "Pressed" : sprite;
                vehicleButton.isEnabled = true;
                vehicleButton.playAudioEvents = true;
                vehicleButton.tooltip = activity.VehicleName;
            }
            else
            {
                vehicleButton.tooltip = FavCimsLang.Text("OnFoot");
            }

            if (activity.IsPrisonTransport)
            {
                vehicleButton.atlas = MyAtlas.FavCimsAtlas;
                vehicleButton.normalBgSprite = "PoliceVehicleIcon";
                vehicleButton.hoveredBgSprite = "PoliceVehicleIcon";
                vehicleButton.focusedBgSprite = "PoliceVehicleIcon";
                vehicleButton.pressedBgSprite = "PoliceVehicleIcon";
                vehicleButton.tooltip = activity.VehicleName + " - " + Locale.Get("VEHICLE_STATUS_PRISON_RETURN");
                destinationButton.text = FavCimsLang.Text("TransportedToPrison");
                destinationButton.tooltip = string.Empty;
                destinationButton.isEnabled = false;
            }
            else if (activity.IsJailed)
            {
                destinationButton.text = FavCimsLang.Text("JailedInto") + " " + activity.TargetName;
            }

            AddActivityNavigationHints(vehicleButton, destinationButton);
        }

        private static string GetActivityVehicleSprite(FamilyActivityData activity)
        {
            string names = activity.VehicleName + " " + activity.VehiclePrefabName;
            if (activity.VehicleService == ItemClass.Service.Residential && names.IndexOf("Bicycle", StringComparison.OrdinalIgnoreCase) >= 0)
            { 
                return "IconTouristBicycleVehicle"; 
            }
            if (activity.VehicleService != ItemClass.Service.PublicTransport)
            { 
                return "IconCitizenVehicle"; 
            }
            return activity.VehicleSubService switch
            {
                ItemClass.SubService.PublicTransportBus => "SubBarPublicTransportBus",
                ItemClass.SubService.PublicTransportMetro => "SubBarPublicTransportMetro",
                ItemClass.SubService.PublicTransportTrain => "SubBarPublicTransportTrain",
                ItemClass.SubService.PublicTransportShip => "SubBarPublicTransportShip",
                ItemClass.SubService.PublicTransportPlane => names.IndexOf("Blimp", StringComparison.OrdinalIgnoreCase) >= 0 ? "IconPolicyEducationalBlimps" : "SubBarPublicTransportPlane",
                ItemClass.SubService.PublicTransportTaxi => "SubBarPublicTransportTaxi",
                ItemClass.SubService.PublicTransportTram => "SubBarPublicTransportTram",
                ItemClass.SubService.PublicTransportMonorail => "SubBarPublicTransportMonorail",
                ItemClass.SubService.PublicTransportCableCar => "SubBarPublicTransportCableCar",
                _ => "IconCitizenVehicle",
            };
        }

        private static void AddActivityNavigationHints(UIButton vehicleButton, UIButton destinationButton)
        {
            if (vehicleButton.isEnabled)
            {
                vehicleButton.tooltip = FamilyPanelText.AppendHint(vehicleButton.tooltip, FamilyPanelText.InstanceNavigation);
            }

            if (destinationButton.isEnabled)
            {
                destinationButton.tooltip = FamilyPanelText.AppendHint(destinationButton.tooltip, FamilyPanelText.InstanceNavigation);
            }
        }
    }
}