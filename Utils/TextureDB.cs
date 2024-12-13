using System;
using UnityEngine;

namespace FavoriteCims.Utils
{
    public class TextureDB : Texture
	{
        public static Texture OtherInfoTexture;

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

        public static Texture Separator;

        public static Texture HappyOverride_texture;

        public static Texture NameBgOverride_texture;

        public static Texture CitizenHomeTexture;

        public static Texture CitizenHomeTextureHigh;

        public static Texture WorkingPlaceTexture;

        public static Texture CitizenHomeTextureDead;

        public static Texture CitizenHomeTextureHomeless;

        public static Texture WorkingPlaceTextureStudent;

        public static Texture WorkingPlaceTextureRetired;

        public static Texture CitizenCommercialLowTexture;

        public static Texture CitizenCommercialHighTexture;

        public static Texture CitizenIndustrialGenericTexture;

        public static Texture CitizenOfficeTexture;

        public static Texture[] ResidentialLevel = new Texture[6];

        public static Texture[] IndustrialLevel = new Texture[6];

        public static Texture[] CommercialLevel = new Texture[6];

        public static Texture[] OfficeLevel = new Texture[6];

        public static void LoadTextures()
		{
			try
			{
				ResidentialLevel[0] = null;
				for (int i = 1; i <= 5; i++)
				{
					ResidentialLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.ResidentialLevel" + i.ToString() + ".png");
					ResidentialLevel[i].wrapMode = TextureWrapMode.Clamp;
					ResidentialLevel[i].filterMode = FilterMode.Bilinear;
					ResidentialLevel[i].mipMapBias = -0.5f;
					ResidentialLevel[i].name = "ResidentialLevel" + i.ToString();
					IndustrialLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.IndustrialLevel" + i.ToString() + ".png");
					IndustrialLevel[i].wrapMode = TextureWrapMode.Clamp;
					IndustrialLevel[i].filterMode = FilterMode.Bilinear;
					IndustrialLevel[i].mipMapBias = -0.5f;
					IndustrialLevel[i].name = "IndustrialLevel" + i.ToString();
					CommercialLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.CommercialLevel" + i.ToString() + ".png");
					CommercialLevel[i].wrapMode = TextureWrapMode.Clamp;
					CommercialLevel[i].filterMode = FilterMode.Bilinear;
					CommercialLevel[i].mipMapBias = -0.5f;
					CommercialLevel[i].name = "CommercialLevel" + i.ToString();
					OfficeLevel[i] = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.levels.OfficeLevel" + i.ToString() + ".png");
					OfficeLevel[i].wrapMode = TextureWrapMode.Clamp;
					OfficeLevel[i].filterMode = FilterMode.Bilinear;
					OfficeLevel[i].mipMapBias = -0.5f;
					OfficeLevel[i].name = "OfficeLevel" + i.ToString();
				}
			}
			catch (Exception ex)
			{
				Debug.Error("Can't load level icons : " + ex.ToString());
			}
			try
			{
				Separator = ResourceLoader.LoadTexture(1, 40, "UIMainPanel.Rows.col_separator.png");
				Separator.wrapMode = TextureWrapMode.Clamp;
				Separator.filterMode = FilterMode.Bilinear;
				Separator.name = "Separator";
				HappyOverride_texture = ResourceLoader.LoadTexture(30, 30, "UIMainPanel.Rows.icon_citisenisgone.png");
				HappyOverride_texture.wrapMode = TextureWrapMode.Clamp;
				HappyOverride_texture.filterMode = FilterMode.Bilinear;
				HappyOverride_texture.name = "HappyOverride_texture";
				HappyOverride_texture.mipMapBias = -0.5f;
				NameBgOverride_texture = ResourceLoader.LoadTexture(180, 40, "UIMainPanel.submenubar.png");
				NameBgOverride_texture.wrapMode = TextureWrapMode.Clamp;
				NameBgOverride_texture.filterMode = FilterMode.Bilinear;
				NameBgOverride_texture.name = "NameOverride_texture";
				NameBgOverride_texture.mipMapBias = -0.5f;
				CitizenHomeTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.homeIconLow.png");
				CitizenHomeTexture.wrapMode = TextureWrapMode.Clamp;
				CitizenHomeTexture.filterMode = FilterMode.Bilinear;
				CitizenHomeTexture.mipMapBias = -0.5f;
				CitizenHomeTexture.name = "CitizenHomeTexture";
				CitizenHomeTextureHigh = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.homeIconHigh.png");
				CitizenHomeTextureHigh.wrapMode = TextureWrapMode.Clamp;
				CitizenHomeTextureHigh.filterMode = FilterMode.Bilinear;
				CitizenHomeTextureHigh.mipMapBias = -0.5f;
				CitizenHomeTextureHigh.name = "CitizenHomeTexture";
				CitizenHomeTextureDead = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.houseofthedead.png");
				CitizenHomeTextureDead.wrapMode = TextureWrapMode.Clamp;
				CitizenHomeTextureDead.filterMode = FilterMode.Bilinear;
				CitizenHomeTextureDead.mipMapBias = -0.5f;
				CitizenHomeTextureDead.name = "CitizenHomeTextureDead";
				CitizenHomeTextureHomeless = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.homelessIcon.png");
				CitizenHomeTextureHomeless.wrapMode = TextureWrapMode.Clamp;
				CitizenHomeTextureHomeless.filterMode = FilterMode.Bilinear;
				CitizenHomeTextureHomeless.mipMapBias = -0.5f;
				CitizenHomeTextureHomeless.name = "CitizenHomeTextureHomeless";
				WorkingPlaceTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.nojob.png");
				WorkingPlaceTextureStudent = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.workstudy.png");
				WorkingPlaceTextureRetired = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.workretired.png");
				CitizenCommercialLowTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.CommercialLow.png");
				CitizenCommercialHighTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.CommercialHigh.png");
				CitizenIndustrialGenericTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.IndustrialIcon.png");
				CitizenOfficeTexture = ResourceLoader.LoadTexture(20, 40, "UIMainPanel.Rows.OfficeIcon.png");
				WorkingPlaceTexture.wrapMode = TextureWrapMode.Clamp;
				WorkingPlaceTextureStudent.wrapMode = TextureWrapMode.Clamp;
				WorkingPlaceTextureRetired.wrapMode = TextureWrapMode.Clamp;
				WorkingPlaceTexture.filterMode = FilterMode.Bilinear;
				CitizenCommercialLowTexture.wrapMode = TextureWrapMode.Clamp;
				CitizenCommercialHighTexture.wrapMode = TextureWrapMode.Clamp;
				CitizenIndustrialGenericTexture.wrapMode = TextureWrapMode.Clamp;
				CitizenOfficeTexture.wrapMode = TextureWrapMode.Clamp;
				WorkingPlaceTextureStudent.filterMode = FilterMode.Bilinear;
				WorkingPlaceTextureRetired.filterMode = FilterMode.Bilinear;
				CitizenCommercialLowTexture.filterMode = FilterMode.Bilinear;
				CitizenCommercialHighTexture.filterMode = FilterMode.Bilinear;
				CitizenIndustrialGenericTexture.filterMode = FilterMode.Bilinear;
				CitizenOfficeTexture.filterMode = FilterMode.Bilinear;
				WorkingPlaceTextureStudent.mipMapBias = -0.5f;
				WorkingPlaceTextureRetired.mipMapBias = -0.5f;
				CitizenCommercialLowTexture.mipMapBias = -0.5f;
				CitizenCommercialHighTexture.mipMapBias = -0.5f;
				CitizenIndustrialGenericTexture.mipMapBias = -0.5f;
				CitizenOfficeTexture.mipMapBias = -0.5f;
				WorkingPlaceTexture.name = "WorkingPlaceTexture";
				WorkingPlaceTextureStudent.name = "WorkingPlaceTextureStudent";
				WorkingPlaceTextureRetired.name = "WorkingPlaceTextureRetired";
				CitizenCommercialLowTexture.name = "CitizenCommercialLowTexture";
				CitizenCommercialHighTexture.name = "CitizenCommercialHighTexture";
				CitizenIndustrialGenericTexture.name = "CitizenIndustrialHighTexture";
				CitizenOfficeTexture.name = "CitizenOfficeTexture";
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
				OtherInfoTexture = ResourceLoader.LoadTexture(250, 500, "UIMainPanel.panel_middle.png");
				OtherInfoTexture.wrapMode = TextureWrapMode.Clamp;
				OtherInfoTexture.filterMode = FilterMode.Bilinear;
				OtherInfoTexture.name = "OtherInfoTexture";
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
