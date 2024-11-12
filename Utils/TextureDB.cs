using System;
using UnityEngine;

namespace FavoriteCims.Utils
{
    public class TextureDB : Texture
	{
        public static Texture FavCimsOtherInfoTexture;

        public static Texture BubbleHeaderIconSpriteTextureFemale;

        public static Texture BubbleHeaderIconSpriteTextureMale;

        public static Texture BubbleFamPortBgSpriteTexture;

        public static Texture BubbleFamPortBgSpriteBackTexture;

        public static Texture BubbleDetailsBgSprite;

        public static Texture BubbleDetailsBgSpriteProblems;

        public static Texture BubbleBgBar1;

        public static Texture BubbleBgBar2;

        public static Texture BubbleBgBar1Big;

        public static Texture BubbleBgBar2Big;

        public static Texture BubbleBgBar1Small;

        public static Texture BubbleBgBar2Small;

        public static Texture BubbleBg1Special;

        public static Texture BubbleBg1Special2;

        public static Texture BubbleBgBarHover;

        public static Texture BubbleDog;

        public static Texture BubbleDogDisabled;

        public static Texture BubbleCar;

        public static Texture BubbleCarDisabled;

        public static Texture LittleStarGrey;

        public static Texture LittleStarGold;

        public static Texture VehiclePanelTitleBackground;

        public static Texture VehiclePanelBackground;

        public static Texture VehiclePanelFooterBackground;

        public static Texture FavCimsSeparator;

        public static Texture FavCimsHappyOverride_texture;

        public static Texture FavCimsNameBgOverride_texture;

        public static Texture FavCimsCitizenHomeTexture;

        public static Texture FavCimsCitizenHomeTextureHigh;

        public static Texture FavCimsWorkingPlaceTexture;

        public static Texture FavCimsCitizenHomeTextureDead;

        public static Texture FavCimsCitizenHomeTextureHomeless;

        public static Texture FavCimsWorkingPlaceTextureStudent;

        public static Texture FavCimsWorkingPlaceTextureRetired;

        public static Texture FavCimsCitizenCommercialLowTexture;

        public static Texture FavCimsCitizenCommercialHighTexture;

        public static Texture FavCimsCitizenIndustrialGenericTexture;

        public static Texture FavCimsCitizenOfficeTexture;

        public static Texture[] FavCimsResidentialLevel = new Texture[6];

        public static Texture[] FavCimsIndustrialLevel = new Texture[6];

        public static Texture[] FavCimsCommercialLevel = new Texture[6];

        public static Texture[] FavCimsOfficeLevel = new Texture[6];

        public static void LoadFavCimsTextures()
		{
			try
			{
				FavCimsResidentialLevel[0] = null;
				for (int i = 1; i <= 5; i++)
				{
					FavCimsResidentialLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.ResidentialLevel" + i.ToString() + ".png");
					FavCimsResidentialLevel[i].wrapMode = TextureWrapMode.Clamp;
					FavCimsResidentialLevel[i].filterMode = FilterMode.Bilinear;
					FavCimsResidentialLevel[i].mipMapBias = -0.5f;
					FavCimsResidentialLevel[i].name = "FavCimsResidentialLevel" + i.ToString();
					FavCimsIndustrialLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.IndustrialLevel" + i.ToString() + ".png");
					FavCimsIndustrialLevel[i].wrapMode = TextureWrapMode.Clamp;
					FavCimsIndustrialLevel[i].filterMode = FilterMode.Bilinear;
					FavCimsIndustrialLevel[i].mipMapBias = -0.5f;
					FavCimsIndustrialLevel[i].name = "FavCimsIndustrialLevel" + i.ToString();
					FavCimsCommercialLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.CommercialLevel" + i.ToString() + ".png");
					FavCimsCommercialLevel[i].wrapMode = TextureWrapMode.Clamp;
					FavCimsCommercialLevel[i].filterMode = FilterMode.Bilinear;
					FavCimsCommercialLevel[i].mipMapBias = -0.5f;
					FavCimsCommercialLevel[i].name = "FavCimsCommercialLevel" + i.ToString();
					FavCimsOfficeLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.OfficeLevel" + i.ToString() + ".png");
					FavCimsOfficeLevel[i].wrapMode = TextureWrapMode.Clamp;
					FavCimsOfficeLevel[i].filterMode = FilterMode.Bilinear;
					FavCimsOfficeLevel[i].mipMapBias = -0.5f;
					FavCimsOfficeLevel[i].name = "FavCimsOfficeLevel" + i.ToString();
				}
			}
			catch (Exception ex)
			{
				Debug.Error("Can't load level icons : " + ex.ToString());
			}
			try
			{
				FavCimsSeparator = ResourceLoader.LoadTexture(1, 40, "UIMainPanel.Rows.col_separator.png");
				FavCimsSeparator.wrapMode = TextureWrapMode.Clamp;
				FavCimsSeparator.filterMode = FilterMode.Bilinear;
				FavCimsSeparator.name = "FavCimsSeparator";
				FavCimsHappyOverride_texture = ResourceLoader.LoadTexture(30, 30, "UIMainPanel.Rows.icon_citisenisgone.png");
				FavCimsHappyOverride_texture.wrapMode = TextureWrapMode.Clamp;
				FavCimsHappyOverride_texture.filterMode = FilterMode.Bilinear;
				FavCimsHappyOverride_texture.name = "FavCimsHappyOverride_texture";
				FavCimsHappyOverride_texture.mipMapBias = -0.5f;
				FavCimsNameBgOverride_texture = ResourceLoader.LoadTexture(180, 40, "UIMainPanel.submenubar.png");
				FavCimsNameBgOverride_texture.wrapMode = TextureWrapMode.Clamp;
				FavCimsNameBgOverride_texture.filterMode = FilterMode.Bilinear;
				FavCimsNameBgOverride_texture.name = "FavCimsNameOverride_texture";
				FavCimsNameBgOverride_texture.mipMapBias = -0.5f;
				FavCimsCitizenHomeTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.homeIconLow.png");
				FavCimsCitizenHomeTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenHomeTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenHomeTexture.mipMapBias = -0.5f;
				FavCimsCitizenHomeTexture.name = "FavCimsCitizenHomeTexture";
				FavCimsCitizenHomeTextureHigh = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.homeIconHigh.png");
				FavCimsCitizenHomeTextureHigh.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenHomeTextureHigh.filterMode = FilterMode.Bilinear;
				FavCimsCitizenHomeTextureHigh.mipMapBias = -0.5f;
				FavCimsCitizenHomeTextureHigh.name = "FavCimsCitizenHomeTexture";
				FavCimsCitizenHomeTextureDead = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.houseofthedead.png");
				FavCimsCitizenHomeTextureDead.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenHomeTextureDead.filterMode = FilterMode.Bilinear;
				FavCimsCitizenHomeTextureDead.mipMapBias = -0.5f;
				FavCimsCitizenHomeTextureDead.name = "FavCimsCitizenHomeTextureDead";
				FavCimsCitizenHomeTextureHomeless = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.homelessIcon.png");
				FavCimsCitizenHomeTextureHomeless.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenHomeTextureHomeless.filterMode = FilterMode.Bilinear;
				FavCimsCitizenHomeTextureHomeless.mipMapBias = -0.5f;
				FavCimsCitizenHomeTextureHomeless.name = "FavCimsCitizenHomeTextureHomeless";
				FavCimsWorkingPlaceTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.nojob.png");
				FavCimsWorkingPlaceTextureStudent = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.workstudy.png");
				FavCimsWorkingPlaceTextureRetired = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.workretired.png");
				FavCimsCitizenCommercialLowTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.CommercialLow.png");
				FavCimsCitizenCommercialHighTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.CommercialHigh.png");
				FavCimsCitizenIndustrialGenericTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.IndustrialIcon.png");
				FavCimsCitizenOfficeTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.OfficeIcon.png");
				FavCimsWorkingPlaceTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsWorkingPlaceTextureStudent.wrapMode = TextureWrapMode.Clamp;
				FavCimsWorkingPlaceTextureRetired.wrapMode = TextureWrapMode.Clamp;
				FavCimsWorkingPlaceTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenCommercialLowTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenCommercialHighTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenIndustrialGenericTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenOfficeTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsWorkingPlaceTextureStudent.filterMode = FilterMode.Bilinear;
				FavCimsWorkingPlaceTextureRetired.filterMode = FilterMode.Bilinear;
				FavCimsCitizenCommercialLowTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenCommercialHighTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenIndustrialGenericTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenOfficeTexture.filterMode = FilterMode.Bilinear;
				FavCimsWorkingPlaceTextureStudent.mipMapBias = -0.5f;
				FavCimsWorkingPlaceTextureRetired.mipMapBias = -0.5f;
				FavCimsCitizenCommercialLowTexture.mipMapBias = -0.5f;
				FavCimsCitizenCommercialHighTexture.mipMapBias = -0.5f;
				FavCimsCitizenIndustrialGenericTexture.mipMapBias = -0.5f;
				FavCimsCitizenOfficeTexture.mipMapBias = -0.5f;
				FavCimsWorkingPlaceTexture.name = "FavCimsWorkingPlaceTexture";
				FavCimsWorkingPlaceTextureStudent.name = "FavCimsWorkingPlaceTextureStudent";
				FavCimsWorkingPlaceTextureRetired.name = "FavCimsWorkingPlaceTextureRetired";
				FavCimsCitizenCommercialLowTexture.name = "FavCimsCitizenCommercialLowTexture";
				FavCimsCitizenCommercialHighTexture.name = "FavCimsCitizenCommercialHighTexture";
				FavCimsCitizenIndustrialGenericTexture.name = "FavCimsCitizenIndustrialHighTexture";
				FavCimsCitizenOfficeTexture.name = "FavCimsCitizenOfficeTexture";
				BubbleHeaderIconSpriteTextureFemale = ResourceLoader.LoadTexture(28, 26, "UIMainPanel.BubblePanel.Female.png");
				BubbleHeaderIconSpriteTextureFemale.wrapMode = TextureWrapMode.Clamp;
				BubbleHeaderIconSpriteTextureFemale.filterMode = FilterMode.Bilinear;
				BubbleHeaderIconSpriteTextureFemale.mipMapBias = -0.5f;
				BubbleHeaderIconSpriteTextureFemale.name = "BubbleHeaderIconSpriteTextureFemale";
				BubbleHeaderIconSpriteTextureMale = ResourceLoader.LoadTexture(28, 26, "UIMainPanel.BubblePanel.Male.png");
				BubbleHeaderIconSpriteTextureMale.wrapMode = TextureWrapMode.Clamp;
				BubbleHeaderIconSpriteTextureMale.filterMode = FilterMode.Bilinear;
				BubbleHeaderIconSpriteTextureMale.mipMapBias = -0.5f;
				BubbleHeaderIconSpriteTextureMale.name = "BubbleHeaderIconSpriteTextureFemale";
				BubbleFamPortBgSpriteTexture = ResourceLoader.LoadTexture(238, 151, "UIMainPanel.BubblePanel.camBg.png");
				BubbleFamPortBgSpriteTexture.wrapMode = TextureWrapMode.Clamp;
				BubbleFamPortBgSpriteTexture.filterMode = FilterMode.Bilinear;
				BubbleFamPortBgSpriteTexture.name = "BubbleCamBgSpriteTexture";
				BubbleFamPortBgSpriteBackTexture = ResourceLoader.LoadTexture(234, 147, "UIMainPanel.BubblePanel.backgroundBack.jpg");
				BubbleFamPortBgSpriteBackTexture.wrapMode = TextureWrapMode.Clamp;
				BubbleFamPortBgSpriteBackTexture.filterMode = FilterMode.Bilinear;
				BubbleFamPortBgSpriteBackTexture.name = "BubbleCamBgSpriteBackTexture";
				BubbleDetailsBgSprite = ResourceLoader.LoadTexture(238, 151, "UIMainPanel.BubblePanel.BubbleDetailsBgSprite.png");
				BubbleDetailsBgSprite.wrapMode = TextureWrapMode.Clamp;
				BubbleDetailsBgSprite.filterMode = FilterMode.Bilinear;
				BubbleDetailsBgSprite.name = "BubbleDetailsBgSprite";
				BubbleDetailsBgSpriteProblems = ResourceLoader.LoadTexture(238, 151, "UIMainPanel.BubblePanel.BubbleDetailsBgSpriteProblems.png");
				BubbleDetailsBgSpriteProblems.wrapMode = TextureWrapMode.Clamp;
				BubbleDetailsBgSpriteProblems.filterMode = FilterMode.Bilinear;
				BubbleDetailsBgSpriteProblems.name = "BubbleDetailsBgSpriteProblems";
				BubbleBgBar1 = ResourceLoader.LoadTexture(236, 26, "UIMainPanel.BubblePanel.BubbleBg1.png");
				BubbleBgBar1.name = "BubbleBgBar1";
				BubbleBgBar1.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar1.filterMode = FilterMode.Bilinear;
				BubbleBgBar1.mipMapBias = -0.5f;
				BubbleBgBar2 = ResourceLoader.LoadTexture(236, 26, "UIMainPanel.BubblePanel.BubbleBg2.png");
				BubbleBgBar2.name = "BubbleBgBar1";
				BubbleBgBar2.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar2.filterMode = FilterMode.Bilinear;
				BubbleBgBar2.mipMapBias = -0.5f;
				BubbleBgBar1Big = ResourceLoader.LoadTexture(198, 40, "UIMainPanel.BubblePanel.BubbleBg1Big.png");
				BubbleBgBar1Big.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar1Big.filterMode = FilterMode.Bilinear;
				BubbleBgBar1Big.name = "BubbleBgBar1Big";
				BubbleBgBar1Small = ResourceLoader.LoadTexture(198, 15, "UIMainPanel.BubblePanel.BubbleBg1Small.png");
				BubbleBgBar1Small.name = "BubbleBgBar1Small";
				BubbleBgBar1Small.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar1Small.filterMode = FilterMode.Bilinear;
				BubbleBgBar2Small = ResourceLoader.LoadTexture(198, 15, "UIMainPanel.BubblePanel.BubbleBg2Small.png");
				BubbleBgBar2Small.name = "BubbleBgBar1Small";
				BubbleBgBar2Small.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar2Small.filterMode = FilterMode.Bilinear;
				BubbleBg1Special = ResourceLoader.LoadTexture(236, 26, "UIMainPanel.BubblePanel.BubbleBg1Special.png");
				BubbleBg1Special.wrapMode = TextureWrapMode.Clamp;
				BubbleBg1Special.filterMode = FilterMode.Bilinear;
				BubbleBg1Special.name = "BubbleBg1Special";
				BubbleBg1Special2 = ResourceLoader.LoadTexture(236, 26, "UIMainPanel.BubblePanel.BubbleBg1Special2.png");
				BubbleBg1Special2.wrapMode = TextureWrapMode.Clamp;
				BubbleBg1Special2.filterMode = FilterMode.Bilinear;
				BubbleBg1Special2.name = "BubbleBg1Special2";
				BubbleBgBarHover = ResourceLoader.LoadTexture(236, 20, "UIMainPanel.BubblePanel.BubbleBgHeader.png");
				BubbleBgBarHover.name = "BubbleBgBar1";
				BubbleBgBarHover.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBarHover.filterMode = FilterMode.Bilinear;
				BubbleDog = ResourceLoader.LoadTexture(14, 20, "UIMainPanel.BubblePanel.Dog.png");
				BubbleDog.name = "BubbleDog";
				BubbleDog.wrapMode = TextureWrapMode.Clamp;
				BubbleDog.filterMode = FilterMode.Bilinear;
				BubbleDog.mipMapBias = -0.5f;
				BubbleDogDisabled = ResourceLoader.LoadTexture(14, 20, "UIMainPanel.BubblePanel.DogDisabled.png");
				BubbleDogDisabled.name = "BubbleDogDisabled";
				BubbleDogDisabled.wrapMode = TextureWrapMode.Clamp;
				BubbleDogDisabled.filterMode = FilterMode.Bilinear;
				BubbleDogDisabled.mipMapBias = -0.5f;
				BubbleCar = ResourceLoader.LoadTexture(31, 20, "UIMainPanel.BubblePanel.Car.png");
				BubbleCar.name = "BubbleCar";
				BubbleCar.wrapMode = TextureWrapMode.Clamp;
				BubbleCar.filterMode = FilterMode.Bilinear;
				BubbleCar.mipMapBias = -0.5f;
				BubbleCarDisabled = ResourceLoader.LoadTexture(31, 20, "UIMainPanel.BubblePanel.CarDisabled.png");
				BubbleCarDisabled.name = "BubbleCarDisabled";
				BubbleCarDisabled.wrapMode = TextureWrapMode.Clamp;
				BubbleCarDisabled.filterMode = FilterMode.Bilinear;
				BubbleCarDisabled.mipMapBias = -0.5f;
				LittleStarGrey = ResourceLoader.LoadTexture(20, 20, "UIMainPanel.BubblePanel.LittleStarGrey.png");
				LittleStarGrey.name = "LittleStarGrey";
				LittleStarGrey.wrapMode = TextureWrapMode.Clamp;
				LittleStarGrey.filterMode = FilterMode.Bilinear;
				LittleStarGrey.mipMapBias = -0.5f;
				LittleStarGold = ResourceLoader.LoadTexture(20, 20, "UIMainPanel.BubblePanel.LittleStarGold.png");
				LittleStarGold.name = "LittleStarGold";
				LittleStarGold.wrapMode = TextureWrapMode.Clamp;
				LittleStarGold.filterMode = FilterMode.Bilinear;
				LittleStarGold.mipMapBias = -0.5f;
				FavCimsOtherInfoTexture = ResourceLoader.LoadTexture(250, 500, "UIMainPanel.panel_middle.png");
				FavCimsOtherInfoTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsOtherInfoTexture.filterMode = FilterMode.Bilinear;
				FavCimsOtherInfoTexture.name = "FavCimsOtherInfoTexture";
				VehiclePanelTitleBackground = ResourceLoader.LoadTexture(250, 41, "VehiclePanel.VehiclePanelTitleBg.png");
				VehiclePanelTitleBackground.wrapMode = TextureWrapMode.Clamp;
				VehiclePanelTitleBackground.filterMode = FilterMode.Bilinear;
				VehiclePanelTitleBackground.name = "VehiclePanelTitleBackground";
				VehiclePanelBackground = ResourceLoader.LoadTexture(250, 1, "VehiclePanel.VehiclePanelBg.png");
				VehiclePanelBackground.wrapMode = 0;
				VehiclePanelBackground.filterMode = 0;
				VehiclePanelBackground.name = "VehiclePanelBackground";
				VehiclePanelFooterBackground = ResourceLoader.LoadTexture(250, 12, "VehiclePanel.VehiclePanelBottomBg.png");
				VehiclePanelFooterBackground.wrapMode = TextureWrapMode.Clamp;
				VehiclePanelFooterBackground.filterMode = FilterMode.Bilinear;
				VehiclePanelFooterBackground.name = "VehiclePanelFooterBackground";
			}
			catch (Exception ex2)
			{
				Debug.Error("Can't load row icons : " + ex2.ToString());
			}
		}
	}
}
