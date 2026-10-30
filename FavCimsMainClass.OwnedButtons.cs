using System;
using ColossalFramework.UI;

namespace FavoriteCims
{
    public partial class FavCimsMainClass
    {
        private void DestroyWorldInfoButtons()
        {
            DestroyOwnedButton(ref favoriteButton, "citizen favorite button");
            DestroyOwnedButton(ref favoriteTouristButton, "tourist favorite button");
            DestroyOwnedButton(ref passengerButton, "private vehicle occupants button");
            DestroyOwnedButton(ref publicTransportPassengersButton, "public transport occupants button");
            DestroyOwnedButton(ref taxiPassengersButton, "service vehicle occupants button");
            DestroyOwnedButton(ref peopleZonedBuildingButton, "zoned building occupants button");
            DestroyOwnedButton(ref peopleServiceBuildingButton, "service building occupants button");
            DestroyOwnedButton(ref peopleFootballEventBuildingButton, "football building occupants button");
            DestroyOwnedButton(ref peopleVarsitySportsArenaBuildingButton, "sports arena occupants button");
            DestroyOwnedButton(ref peopleWarehouseBuildingButton, "warehouse occupants button");
            DestroyOwnedButton(ref peopleShelterBuildingButton, "shelter occupants button");
            DestroyOwnedButton(ref peopleUniqueFactoryBuildingButton, "unique factory occupants button");
            DestroyOwnedButton(ref peopleHotelBuildingButton, "hotel occupants button");
        }

        private static void DestroyOwnedButton<TButton>(ref TButton reference, string label) where TButton : UIComponent
        {
            var button = reference;
            reference = null;
            if (button == null) return;

            try
            {
                button.Hide();
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to hide " + label + " during unload: " + ex);
            }

            try
            {
                UnityEngine.Object.Destroy(button.gameObject);
            }
            catch (Exception ex)
            {
                Utils.Debug.Error("Failed to destroy " + label + " during unload: " + ex);
            }
        }
    }
}