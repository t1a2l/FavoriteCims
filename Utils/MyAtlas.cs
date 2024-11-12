using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.Utils
{
	public class MyAtlas
	{
        public static UITextureAtlas FavCimsAtlas;

        public void LoadAtlasIcons()
		{
			string[] array =
            [
                "FavoriteCimsButton", "FavoriteCimsButtonHovered", "FavoriteCimsButtonPressed", "FavoriteCimsButtonFocused", "FavoriteCimsButtonDisabled", "icon_fav_subscribed", "icon_fav_unsubscribed", "vehicleButton", "vehicleButtonDisabled", "vehicleButtonHovered",
				"scrollbarthumb", "scrollbartrack", "bg_row1", "bg_row2", "touristIcon", "driverIcon", "passengerIcon", "citizenbuttonenabled", "citizenbuttondisabled", "CommercialHigh",
				"CommercialLow", "homeIconHigh", "homeIconLow", "homelessIcon", "houseofthedead", "FavCimsCrimeArrested", "FavCimsIconScooter", "FavCimsPoliceVehicle", "icon_citisenisgone", "IndustrialIcon",
				"nojob", "OfficeIcon", "workretired", "workstudy", "Car", "CarDisabled", "Dog", "DogDisabled", "Female", "Male",
				"BapartmentIcon", "BcommercialIcon", "BworkingIcon", "greenArrowIcon", "redArrowIcon", "BuildingButtonIcon", "BuildingButtonIconDisabled", "BuildingButtonIconHovered", "CommercialBuildingButtonIcon", "CommercialBuildingButtonIconDisabled",
				"CommercialBuildingButtonIconHovered", "IndustrialBuildingButtonIcon", "IndustrialBuildingButtonIconDisabled", "IndustrialBuildingButtonIconHovered", "focusIcon", "focusIconFocused", "focusIconDisabled"
			];
			string[] array2 =
            [
                "", "", "", "", "", "", "", "VehiclePanel.", "VehiclePanel.", "VehiclePanel.",
				"VehiclePanel.", "VehiclePanel.", "VehiclePanel.", "VehiclePanel.", "VehiclePanel.", "VehiclePanel.", "VehiclePanel.", "UIMainPanel.", "UIMainPanel.", "UIMainPanel.Rows.",
				"UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.",
				"UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.Rows.", "UIMainPanel.BubblePanel.", "UIMainPanel.BubblePanel.", "UIMainPanel.BubblePanel.", "UIMainPanel.BubblePanel.", "UIMainPanel.BubblePanel.", "UIMainPanel.BubblePanel.",
				"BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.",
				"BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels.", "BuildingPanels."
			];
			MyAtlas.FavCimsAtlas = CreateMyAtlas("FavCimsAtlas", UIView.GetAView().defaultAtlas.material, array2, array);
		}

		private UITextureAtlas CreateMyAtlas(string AtlasName, Material BaseMat, string[] sPritesPath, string[] sPritesNames)
		{
			int num = 1024;
			Texture2D texture2D = new(num, num, TextureFormat.ARGB32, false);
			Texture2D[] array = new Texture2D[sPritesNames.Length];
            for (int i = 0; i < sPritesNames.Length; i++)
			{
				array[i] = ResourceLoader.LoadTexture(0, 0, sPritesPath[i] + sPritesNames[i] + ".png");
			}
            Rect[] array2 = texture2D.PackTextures(array, 2, num);
            UITextureAtlas uitextureAtlas = ScriptableObject.CreateInstance<UITextureAtlas>();
			Material material = Object.Instantiate(BaseMat);
			material.mainTexture = texture2D;
			uitextureAtlas.material = material;
			uitextureAtlas.name = AtlasName;
			for (int j = 0; j < sPritesNames.Length; j++)
			{
				UITextureAtlas.SpriteInfo spriteInfo = new()
                {
					name = sPritesNames[j],
					texture = texture2D,
					region = array2[j]
				};
				uitextureAtlas.AddSprite(spriteInfo);
			}
			return uitextureAtlas;
		}
	}
}
