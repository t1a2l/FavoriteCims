using ColossalFramework.UI;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public static class CitizenRowColumns
    {
        public static UIPanel CreateColumn(UIPanel row, string name, float width, float x)
        {
            var column = row.AddUIComponent<UIPanel>();

            column.name = name;
            column.width = width;
            column.height = 40f;
            column.relativePosition = new Vector3(x, 0f);

            return column;
        }

        public static UIButton CreateTextButton(UIPanel column, string name, float textScale = 1f, bool interactive = true)        
        {
            var button = column.AddUIComponent<UIButton>();

            button.name = name;
            button.width = column.width;
            button.height = column.height;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textHorizontalAlignment = UIHorizontalAlignment.Left;
            button.playAudioEvents = interactive;
            button.isInteractive = interactive;
            button.font = UIDynamicFont.FindByName("OpenSans-Regular");
            button.font.size = 15;
            button.textScale = textScale;
            button.wordWrap = true;
            button.textPadding = new RectOffset(5, 5, 0, 0);
            button.useDropShadow = true;
            button.dropShadowOffset = new Vector2(1f, -1f);
            button.dropShadowColor = new Color32(0, 0, 0, 0);
            button.maximumSize = new Vector2(column.width, column.height);
            button.relativePosition = Vector3.zero;

            return button;
        }

        public static UITextureSprite CreateSeparator(UIPanel column)
        {
            var separator = column.AddUIComponent<UITextureSprite>();

            separator.name = column.name + "Separator";
            separator.texture = TextureDB.FavCimsSeparator;
            separator.relativePosition = Vector3.zero;

            return separator;
        }
    }
}
