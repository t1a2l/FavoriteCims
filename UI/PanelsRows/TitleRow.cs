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

            icon = AddUIComponent<UISprite>();
            icon.size = new Vector2(17f, 17f);
            icon.atlas = MyAtlas.FavCimsAtlas;
            icon.relativePosition = new Vector3(Margin, 4f);
            text = AddLabel(icon.relativePosition.x + icon.width, 200f);
            text.useDropShadow = true;
            text.dropShadowOffset = new Vector2(1f, -1f);
            text.dropShadowColor = new Color32(0, 0, 0, 0);
            text.textColor = Color.white;
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
