using AlgernonCommons.UI;
using ColossalFramework.UI;
using FavoriteCims.Utils;
using System;
using UnityEngine;

namespace FavoriteCims.UI.PanelsRows
{
    public class TitleRow : UIListRow
    {
        private UISprite icon;

        private UILabel text;

        public override void Awake()
        {
            base.Awake();
            width = 226f;
            height = 25f;
            atlas = MyAtlas.FavCimsAtlas;
            BackgroundSpriteName = "bg_row2";
            SelectedSpriteName = "bg_row2";
            backgroundSprite = "bg_row2";
            name = "TitleRow";
            relativePosition = new Vector3(0f, 0f);
            icon = AddUIComponent<UISprite>();
            icon.name = "TitleIcon";
            icon.size = new Vector2(17f, 17f);
            icon.atlas = MyAtlas.FavCimsAtlas;
            icon.relativePosition = new Vector3(Margin, 4f);
            text = AddLabel(icon.relativePosition.x + icon.width, 200f);
            text.name = "TitleText";
            text.size = new Vector2(200f, 25f);
            text.verticalAlignment = UIVerticalAlignment.Middle;
            text.playAudioEvents = true;
            text.font = UIFonts.Regular;
            text.font.size = 15;
            text.textScale = 0.80f;
            text.useDropShadow = true;
            text.dropShadowOffset = new Vector2(1, -1);
            text.dropShadowColor = new Color32(0, 0, 0, 0);
            text.padding.left = 5;
            text.padding.right = 5;
            text.textColor = new Color32(51, 51, 51, 160); //r,g,b,a
            text.isInteractive = false;
        }

        public override void Display(object data, int rowIndex)
        {
            var info = (TitleRowInfo)data;

            text.text = info.isEmpty != null && info.isEmpty() ? info.emptyText : info.text;

            icon.atlas = info.atlas ?? MyAtlas.FavCimsAtlas;
            icon.spriteName = info.spriteName;

            Deselect(rowIndex);
        }

        public override void Deselect(int rowIndex)
        {
            //Always use darker background
            BackgroundSpriteName = "UnlockingItemBackground";
            BackgroundColor = new Color32(0, 0, 0, 128);
        }
    }

    public struct TitleRowInfo(Func<bool> isEmpty, string text, string emptyText, string spriteName, UITextureAtlas atlas = null)
    {
        public Func<bool> isEmpty = isEmpty;
        public string text = text;
        public string emptyText = emptyText;
        public UITextureAtlas atlas = atlas;
        public string spriteName = spriteName;
    }
}
