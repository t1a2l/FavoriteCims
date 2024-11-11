using System;
using UnityEngine;

namespace FavoriteCims
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
				TextureDB.FavCimsResidentialLevel[0] = null;
				for (int i = 1; i <= 5; i++)
				{
					TextureDB.FavCimsResidentialLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.ResidentialLevel" + i.ToString() + ".png");
					TextureDB.FavCimsResidentialLevel[i].wrapMode = TextureWrapMode.Clamp;
					TextureDB.FavCimsResidentialLevel[i].filterMode = FilterMode.Bilinear;
					TextureDB.FavCimsResidentialLevel[i].mipMapBias = -0.5f;
					TextureDB.FavCimsResidentialLevel[i].name = "FavCimsResidentialLevel" + i.ToString();
					TextureDB.FavCimsIndustrialLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.IndustrialLevel" + i.ToString() + ".png");
					TextureDB.FavCimsIndustrialLevel[i].wrapMode = TextureWrapMode.Clamp;
					TextureDB.FavCimsIndustrialLevel[i].filterMode = FilterMode.Bilinear;
					TextureDB.FavCimsIndustrialLevel[i].mipMapBias = -0.5f;
					TextureDB.FavCimsIndustrialLevel[i].name = "FavCimsIndustrialLevel" + i.ToString();
					TextureDB.FavCimsCommercialLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.CommercialLevel" + i.ToString() + ".png");
					TextureDB.FavCimsCommercialLevel[i].wrapMode = TextureWrapMode.Clamp;
					TextureDB.FavCimsCommercialLevel[i].filterMode = FilterMode.Bilinear;
					TextureDB.FavCimsCommercialLevel[i].mipMapBias = -0.5f;
					TextureDB.FavCimsCommercialLevel[i].name = "FavCimsCommercialLevel" + i.ToString();
					TextureDB.FavCimsOfficeLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.OfficeLevel" + i.ToString() + ".png");
					TextureDB.FavCimsOfficeLevel[i].wrapMode = TextureWrapMode.Clamp;
					TextureDB.FavCimsOfficeLevel[i].filterMode = FilterMode.Bilinear;
					TextureDB.FavCimsOfficeLevel[i].mipMapBias = -0.5f;
					TextureDB.FavCimsOfficeLevel[i].name = "FavCimsOfficeLevel" + i.ToString();
				}
			}
			catch (Exception ex)
			{
				Debug.Error("Can't load level icons : " + ex.ToString());
			}
			try
			{
				TextureDB.FavCimsSeparator = ResourceLoader.LoadTexture(1, 40, "UIMainPanel.Rows.col_separator.png");
				TextureDB.FavCimsSeparator.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsSeparator.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsSeparator.name = "FavCimsSeparator";
				TextureDB.FavCimsHappyOverride_texture = ResourceLoader.LoadTexture(30, 30, "UIMainPanel.Rows.icon_citisenisgone.png");
				TextureDB.FavCimsHappyOverride_texture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsHappyOverride_texture.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsHappyOverride_texture.name = "FavCimsHappyOverride_texture";
				TextureDB.FavCimsHappyOverride_texture.mipMapBias = -0.5f;
				TextureDB.FavCimsNameBgOverride_texture = ResourceLoader.LoadTexture(180, 40, "UIMainPanel.submenubar.png");
				TextureDB.FavCimsNameBgOverride_texture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsNameBgOverride_texture.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsNameBgOverride_texture.name = "FavCimsNameOverride_texture";
				TextureDB.FavCimsNameBgOverride_texture.mipMapBias = -0.5f;
				TextureDB.FavCimsCitizenHomeTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.homeIconLow.png");
				TextureDB.FavCimsCitizenHomeTexture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsCitizenHomeTexture.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsCitizenHomeTexture.mipMapBias = -0.5f;
				TextureDB.FavCimsCitizenHomeTexture.name = "FavCimsCitizenHomeTexture";
				TextureDB.FavCimsCitizenHomeTextureHigh = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.homeIconHigh.png");
				TextureDB.FavCimsCitizenHomeTextureHigh.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsCitizenHomeTextureHigh.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsCitizenHomeTextureHigh.mipMapBias = -0.5f;
				TextureDB.FavCimsCitizenHomeTextureHigh.name = "FavCimsCitizenHomeTexture";
				TextureDB.FavCimsCitizenHomeTextureDead = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.houseofthedead.png");
				TextureDB.FavCimsCitizenHomeTextureDead.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsCitizenHomeTextureDead.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsCitizenHomeTextureDead.mipMapBias = -0.5f;
				TextureDB.FavCimsCitizenHomeTextureDead.name = "FavCimsCitizenHomeTextureDead";
				TextureDB.FavCimsCitizenHomeTextureHomeless = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.homelessIcon.png");
				TextureDB.FavCimsCitizenHomeTextureHomeless.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsCitizenHomeTextureHomeless.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsCitizenHomeTextureHomeless.mipMapBias = -0.5f;
				TextureDB.FavCimsCitizenHomeTextureHomeless.name = "FavCimsCitizenHomeTextureHomeless";
				TextureDB.FavCimsWorkingPlaceTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.nojob.png");
				TextureDB.FavCimsWorkingPlaceTextureStudent = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.workstudy.png");
				TextureDB.FavCimsWorkingPlaceTextureRetired = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.workretired.png");
				TextureDB.FavCimsCitizenCommercialLowTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.CommercialLow.png");
				TextureDB.FavCimsCitizenCommercialHighTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.CommercialHigh.png");
				TextureDB.FavCimsCitizenIndustrialGenericTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.IndustrialIcon.png");
				TextureDB.FavCimsCitizenOfficeTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.OfficeIcon.png");
				TextureDB.FavCimsWorkingPlaceTexture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsWorkingPlaceTextureStudent.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsWorkingPlaceTextureRetired.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsWorkingPlaceTexture.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsCitizenCommercialLowTexture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsCitizenCommercialHighTexture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsCitizenIndustrialGenericTexture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsCitizenOfficeTexture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsWorkingPlaceTextureStudent.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsWorkingPlaceTextureRetired.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsCitizenCommercialLowTexture.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsCitizenCommercialHighTexture.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsCitizenIndustrialGenericTexture.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsCitizenOfficeTexture.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsWorkingPlaceTextureStudent.mipMapBias = -0.5f;
				TextureDB.FavCimsWorkingPlaceTextureRetired.mipMapBias = -0.5f;
				TextureDB.FavCimsCitizenCommercialLowTexture.mipMapBias = -0.5f;
				TextureDB.FavCimsCitizenCommercialHighTexture.mipMapBias = -0.5f;
				TextureDB.FavCimsCitizenIndustrialGenericTexture.mipMapBias = -0.5f;
				TextureDB.FavCimsCitizenOfficeTexture.mipMapBias = -0.5f;
				TextureDB.FavCimsWorkingPlaceTexture.name = "FavCimsWorkingPlaceTexture";
				TextureDB.FavCimsWorkingPlaceTextureStudent.name = "FavCimsWorkingPlaceTextureStudent";
				TextureDB.FavCimsWorkingPlaceTextureRetired.name = "FavCimsWorkingPlaceTextureRetired";
				TextureDB.FavCimsCitizenCommercialLowTexture.name = "FavCimsCitizenCommercialLowTexture";
				TextureDB.FavCimsCitizenCommercialHighTexture.name = "FavCimsCitizenCommercialHighTexture";
				TextureDB.FavCimsCitizenIndustrialGenericTexture.name = "FavCimsCitizenIndustrialHighTexture";
				TextureDB.FavCimsCitizenOfficeTexture.name = "FavCimsCitizenOfficeTexture";
				TextureDB.BubbleHeaderIconSpriteTextureFemale = ResourceLoader.LoadTexture(28, 26, "UIMainPanel.BubblePanel.Female.png");
				TextureDB.BubbleHeaderIconSpriteTextureFemale.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleHeaderIconSpriteTextureFemale.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleHeaderIconSpriteTextureFemale.mipMapBias = -0.5f;
				TextureDB.BubbleHeaderIconSpriteTextureFemale.name = "BubbleHeaderIconSpriteTextureFemale";
				TextureDB.BubbleHeaderIconSpriteTextureMale = ResourceLoader.LoadTexture(28, 26, "UIMainPanel.BubblePanel.Male.png");
				TextureDB.BubbleHeaderIconSpriteTextureMale.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleHeaderIconSpriteTextureMale.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleHeaderIconSpriteTextureMale.mipMapBias = -0.5f;
				TextureDB.BubbleHeaderIconSpriteTextureMale.name = "BubbleHeaderIconSpriteTextureFemale";
				TextureDB.BubbleFamPortBgSpriteTexture = ResourceLoader.LoadTexture(238, 151, "UIMainPanel.BubblePanel.camBg.png");
				TextureDB.BubbleFamPortBgSpriteTexture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleFamPortBgSpriteTexture.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleFamPortBgSpriteTexture.name = "BubbleCamBgSpriteTexture";
				TextureDB.BubbleFamPortBgSpriteBackTexture = ResourceLoader.LoadTexture(234, 147, "UIMainPanel.BubblePanel.backgroundBack.jpg");
				TextureDB.BubbleFamPortBgSpriteBackTexture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleFamPortBgSpriteBackTexture.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleFamPortBgSpriteBackTexture.name = "BubbleCamBgSpriteBackTexture";
				TextureDB.BubbleDetailsBgSprite = ResourceLoader.LoadTexture(238, 151, "UIMainPanel.BubblePanel.BubbleDetailsBgSprite.png");
				TextureDB.BubbleDetailsBgSprite.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleDetailsBgSprite.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleDetailsBgSprite.name = "BubbleDetailsBgSprite";
				TextureDB.BubbleDetailsBgSpriteProblems = ResourceLoader.LoadTexture(238, 151, "UIMainPanel.BubblePanel.BubbleDetailsBgSpriteProblems.png");
				TextureDB.BubbleDetailsBgSpriteProblems.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleDetailsBgSpriteProblems.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleDetailsBgSpriteProblems.name = "BubbleDetailsBgSpriteProblems";
				TextureDB.BubbleBgBar1 = ResourceLoader.LoadTexture(236, 26, "UIMainPanel.BubblePanel.BubbleBg1.png");
				TextureDB.BubbleBgBar1.name = "BubbleBgBar1";
				TextureDB.BubbleBgBar1.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleBgBar1.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleBgBar1.mipMapBias = -0.5f;
				TextureDB.BubbleBgBar2 = ResourceLoader.LoadTexture(236, 26, "UIMainPanel.BubblePanel.BubbleBg2.png");
				TextureDB.BubbleBgBar2.name = "BubbleBgBar1";
				TextureDB.BubbleBgBar2.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleBgBar2.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleBgBar2.mipMapBias = -0.5f;
				TextureDB.BubbleBgBar1Big = ResourceLoader.LoadTexture(198, 40, "UIMainPanel.BubblePanel.BubbleBg1Big.png");
				TextureDB.BubbleBgBar1Big.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleBgBar1Big.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleBgBar1Big.name = "BubbleBgBar1Big";
				TextureDB.BubbleBgBar1Small = ResourceLoader.LoadTexture(198, 15, "UIMainPanel.BubblePanel.BubbleBg1Small.png");
				TextureDB.BubbleBgBar1Small.name = "BubbleBgBar1Small";
				TextureDB.BubbleBgBar1Small.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleBgBar1Small.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleBgBar2Small = ResourceLoader.LoadTexture(198, 15, "UIMainPanel.BubblePanel.BubbleBg2Small.png");
				TextureDB.BubbleBgBar2Small.name = "BubbleBgBar1Small";
				TextureDB.BubbleBgBar2Small.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleBgBar2Small.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleBg1Special = ResourceLoader.LoadTexture(236, 26, "UIMainPanel.BubblePanel.BubbleBg1Special.png");
				TextureDB.BubbleBg1Special.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleBg1Special.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleBg1Special.name = "BubbleBg1Special";
				TextureDB.BubbleBg1Special2 = ResourceLoader.LoadTexture(236, 26, "UIMainPanel.BubblePanel.BubbleBg1Special2.png");
				TextureDB.BubbleBg1Special2.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleBg1Special2.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleBg1Special2.name = "BubbleBg1Special2";
				TextureDB.BubbleBgBarHover = ResourceLoader.LoadTexture(236, 20, "UIMainPanel.BubblePanel.BubbleBgHeader.png");
				TextureDB.BubbleBgBarHover.name = "BubbleBgBar1";
				TextureDB.BubbleBgBarHover.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleBgBarHover.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleDog = ResourceLoader.LoadTexture(14, 20, "UIMainPanel.BubblePanel.Dog.png");
				TextureDB.BubbleDog.name = "BubbleDog";
				TextureDB.BubbleDog.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleDog.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleDog.mipMapBias = -0.5f;
				TextureDB.BubbleDogDisabled = ResourceLoader.LoadTexture(14, 20, "UIMainPanel.BubblePanel.DogDisabled.png");
				TextureDB.BubbleDogDisabled.name = "BubbleDogDisabled";
				TextureDB.BubbleDogDisabled.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleDogDisabled.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleDogDisabled.mipMapBias = -0.5f;
				TextureDB.BubbleCar = ResourceLoader.LoadTexture(31, 20, "UIMainPanel.BubblePanel.Car.png");
				TextureDB.BubbleCar.name = "BubbleCar";
				TextureDB.BubbleCar.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleCar.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleCar.mipMapBias = -0.5f;
				TextureDB.BubbleCarDisabled = ResourceLoader.LoadTexture(31, 20, "UIMainPanel.BubblePanel.CarDisabled.png");
				TextureDB.BubbleCarDisabled.name = "BubbleCarDisabled";
				TextureDB.BubbleCarDisabled.wrapMode = TextureWrapMode.Clamp;
				TextureDB.BubbleCarDisabled.filterMode = FilterMode.Bilinear;
				TextureDB.BubbleCarDisabled.mipMapBias = -0.5f;
				TextureDB.LittleStarGrey = ResourceLoader.LoadTexture(20, 20, "UIMainPanel.BubblePanel.LittleStarGrey.png");
				TextureDB.LittleStarGrey.name = "LittleStarGrey";
				TextureDB.LittleStarGrey.wrapMode = TextureWrapMode.Clamp;
				TextureDB.LittleStarGrey.filterMode = FilterMode.Bilinear;
				TextureDB.LittleStarGrey.mipMapBias = -0.5f;
				TextureDB.LittleStarGold = ResourceLoader.LoadTexture(20, 20, "UIMainPanel.BubblePanel.LittleStarGold.png");
				TextureDB.LittleStarGold.name = "LittleStarGold";
				TextureDB.LittleStarGold.wrapMode = TextureWrapMode.Clamp;
				TextureDB.LittleStarGold.filterMode = FilterMode.Bilinear;
				TextureDB.LittleStarGold.mipMapBias = -0.5f;
				TextureDB.FavCimsOtherInfoTexture = ResourceLoader.LoadTexture(250, 500, "UIMainPanel.panel_middle.png");
				TextureDB.FavCimsOtherInfoTexture.wrapMode = TextureWrapMode.Clamp;
				TextureDB.FavCimsOtherInfoTexture.filterMode = FilterMode.Bilinear;
				TextureDB.FavCimsOtherInfoTexture.name = "FavCimsOtherInfoTexture";
				TextureDB.VehiclePanelTitleBackground = ResourceLoader.LoadTexture(250, 41, "VehiclePanel.VehiclePanelTitleBg.png");
				TextureDB.VehiclePanelTitleBackground.wrapMode = TextureWrapMode.Clamp;
				TextureDB.VehiclePanelTitleBackground.filterMode = FilterMode.Bilinear;
				TextureDB.VehiclePanelTitleBackground.name = "VehiclePanelTitleBackground";
				TextureDB.VehiclePanelBackground = ResourceLoader.LoadTexture(250, 1, "VehiclePanel.VehiclePanelBg.png");
				TextureDB.VehiclePanelBackground.wrapMode = 0;
				TextureDB.VehiclePanelBackground.filterMode = 0;
				TextureDB.VehiclePanelBackground.name = "VehiclePanelBackground";
				TextureDB.VehiclePanelFooterBackground = ResourceLoader.LoadTexture(250, 12, "VehiclePanel.VehiclePanelBottomBg.png");
				TextureDB.VehiclePanelFooterBackground.wrapMode = TextureWrapMode.Clamp;
				TextureDB.VehiclePanelFooterBackground.filterMode = FilterMode.Bilinear;
				TextureDB.VehiclePanelFooterBackground.name = "VehiclePanelFooterBackground";
			}
			catch (Exception ex2)
			{
				Debug.Error("Can't load row icons : " + ex2.ToString());
			}
		}
	}
}
