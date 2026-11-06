using ColossalFramework.UI;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Buttons
{
    public class AddToFavButton : UIButton
    {
        private InstanceID ThisHuman = InstanceID.Empty;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        public override void Start()
        {
            UIView aview = UIView.GetAView();
            name = "FavCimsStarButton";
            normalBgSprite = "IconFavUnsubscribed";
            atlas = MyAtlas.FavCimsAtlas;
            size = new Vector2(32f, 32f);
            playAudioEvents = true;
            AlignTo(RefPanel, Alignment);
            tooltipBox = aview.defaultTooltipBox;
            eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                if (FavCimsMainClass.UnLoading || !isVisible)
                {
                    return;
                }

                ThisHuman = WorldInfoPanel.GetCurrentInstanceID();

                if (ThisHuman.IsEmpty || ThisHuman.Type != InstanceType.Citizen || ThisHuman.Citizen == 0)
                {
                    ThisHuman = InstanceID.Empty;
                    return;
                }

                FavCimsCore.ToggleCitizenFavorite(ThisHuman);
            };
        }

        public override void Update()
        {
            base.Update();

            if (FavCimsMainClass.UnLoading || !isVisible)
            { 
                return; 
            }

            var current = WorldInfoPanel.GetCurrentInstanceID();

            bool validCitizen = !current.IsEmpty && current.Type == InstanceType.Citizen && current.Citizen != 0;

            ThisHuman = validCitizen ? current : InstanceID.Empty;

            isEnabled = validCitizen;

            bool isFavorite = validCitizen && FavCimsCore.IsFavorite(current.Citizen);

            normalBgSprite = isFavorite ? "IconFavSubscribed" : "IconFavUnsubscribed";

            tooltip = validCitizen ? FavCimsLang.Text(isFavorite ? "FavoriteRemoveTooltip" : "FavoriteAddTooltip") : string.Empty;             
        }
    }
}
