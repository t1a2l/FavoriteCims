using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.Utils
{
    public static class UIFactory
    {
        public static UIButton CreateButton(UIComponent parent, string name, float width, float height, bool interactive = true)
        {
            var button = parent.AddUIComponent<UIButton>();

            button.name = name;
            button.width = width;
            button.height = height;
            button.atlas = MyAtlas.FavCimsAtlas;
            button.playAudioEvents = interactive;
            button.isInteractive = interactive;

            return button;
        }

        public static void StyleText(UIButton button, int fontSize, float textScale, Color32 color, UIHorizontalAlignment alignment = UIHorizontalAlignment.Left)
        {
            button.font = UIDynamicFont.FindByName("OpenSans-Regular");
            button.font.size = fontSize;
            button.textScale = textScale;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textHorizontalAlignment = alignment;
            button.wordWrap = false;

            button.textColor = color;
            button.hoveredTextColor = color;
            button.pressedTextColor = color;
            button.focusedTextColor = color;
            button.disabledTextColor = color;

            button.useDropShadow = true;
            button.dropShadowOffset = new Vector2(1f, -1f);
            button.dropShadowColor = new Color32(0, 0, 0, 0);

            button.textPadding = new RectOffset(5, 5, 0, 0);
        }

        public static UIButton CreateIconButton(UIComponent parent, string name, float width, float height, string sprite)
        {
            var button = CreateButton(parent, name, width, height, false);

            button.normalFgSprite = sprite;
            button.relativePosition = new Vector3(5f, 4f);

            return button;
        }

        public static UITextureSprite CreateRowBackground(UIPanel row, float width, float height)
        {
            var background = row.AddUIComponent<UITextureSprite>();

            background.name = "FavCimsCitizenSingleRowBGSprite";
            background.width = width;
            background.height = height;
            background.texture = TextureDB.VehiclePanelTitleBackground;
            background.relativePosition = Vector3.zero;

            return background;
        }
    }
}
