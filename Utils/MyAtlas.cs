using System;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.Utils
{
	public class MyAtlas
	{
        public static UITextureAtlas FavCimsAtlas;

        public void LoadAtlasIcons()
		{
			string[] AtlasIconNamesArr =
            [
                "FavoriteCimsButton", "FavoriteCimsButtonHovered", "FavoriteCimsButtonPressed", "FavoriteCimsButtonFocused", "FavoriteCimsButtonDisabled", 
				"IconFavSubscribed", "IconFavUnsubscribed", 
				
				"VehiclePanel.VehicleButton", "VehiclePanel.VehicleButtonDisabled", "VehiclePanel.VehicleButtonHovered", "VehiclePanel.ScrollbarThumb", 
				"VehiclePanel.ScrollbarTrack", "VehiclePanel.BgRow1", "VehiclePanel.BgRow2", "VehiclePanel.TouristIcon", "VehiclePanel.DriverIcon", 
				"VehiclePanel.PassengerIcon", 
				
				"UIMainPanel.CitizenButtonEnabled", "UIMainPanel.CitizenButtonDisabled",

				"UIMainPanel.Rows.CommercialHighIcon", "UIMainPanel.Rows.CommercialLowIcon", "UIMainPanel.Rows.ResidentialHighIcon", 
				"UIMainPanel.Rows.ResidentialLowIcon", "UIMainPanel.Rows.HomelessIcon", "UIMainPanel.Rows.GraveIcon", "UIMainPanel.Rows.CitizenArrestedIcon", 
				"UIMainPanel.Rows.ScooterIcon", "UIMainPanel.Rows.PoliceVehicleIcon", "IconCitizenIsGone", "UIMainPanel.Rows.IndustrialIcon", "UIMainPanel.Rows.NoJobIcon", 
				"UIMainPanel.Rows.OfficeIcon", "UIMainPanel.Rows.RetiredIcon", "UIMainPanel.Rows.StudyIcon", "UIMainPanel.Rows.HomeIcon", "UIMainPanel.Rows.GuestsIcon", 
				"UIMainPanel.Rows.WorkersIcon",

                "UIMainPanel.BubblePanel.Car", "UIMainPanel.BubblePanel.CarDisabled", "UIMainPanel.BubblePanel.Dog", "UIMainPanel.BubblePanel.DogDisabled", 
				"UIMainPanel.BubblePanel.Female", "UIMainPanel.BubblePanel.Male",

                "BuildingPanels.GreenArrowIcon", "BuildingPanels.RedArrowIcon", "BuildingPanels.ResidentialBuildingButton", "BuildingPanels.ResidentialBuildingButtonDisabled", 
				"BuildingPanels.ResidentialBuildingButtonHovered", "BuildingPanels.CommercialBuildingButton", "BuildingPanels.CommercialBuildingButtonIconDisabled", 
				"BuildingPanels.CommercialBuildingButtonHovered", "BuildingPanels.IndustrialBuildingButton", "BuildingPanels.IndustrialBuildingButtonDisabled", 
				"BuildingPanels.IndustrialBuildingButtonHovered", "BuildingPanels.FocusIcon", "BuildingPanels.FocusIconFocused", "BuildingPanels.FocusIconDisabled"
            ];

			FavCimsAtlas = CreateMyAtlas("FavCimsAtlas", UIView.GetAView().defaultAtlas.material, AtlasIconNamesArr);
		}

		private UITextureAtlas CreateMyAtlas(string AtlasName, Material BaseMat, string[] atlasIconNamesArr)
		{
			int num = 1024;
			Texture2D texture2D = new(num, num, TextureFormat.ARGB32, false);
			Texture2D[] array = new Texture2D[atlasIconNamesArr.Length];
            for (int i = 0; i < atlasIconNamesArr.Length; i++)
			{
				array[i] = ResourceLoader.LoadTexture(0, 0, atlasIconNamesArr[i] + ".png");
			}
            Rect[] array2 = texture2D.PackTextures(array, 2, num);
            UITextureAtlas uitextureAtlas = ScriptableObject.CreateInstance<UITextureAtlas>();
			Material material = UnityEngine.Object.Instantiate(BaseMat);
			material.mainTexture = texture2D;
			uitextureAtlas.material = material;
			uitextureAtlas.name = AtlasName;
			for (int j = 0; j < atlasIconNamesArr.Length; j++)
			{
                var name = atlasIconNamesArr[j].Substring(atlasIconNamesArr[j].LastIndexOf('.') + 1);
                UITextureAtlas.SpriteInfo spriteInfo = new()
                {
					name = name,
					texture = texture2D,
					region = array2[j]
				};
				uitextureAtlas.AddSprite(spriteInfo);
			}
			return uitextureAtlas;
		}
	}
}
