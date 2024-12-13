using AlgernonCommons.Translation;
using ColossalFramework;
using ColossalFramework.UI;
using System;
using System.Threading;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using UnityEngine;
using static RenderManager;

namespace FavoriteCims.UI.Buttons
{
    public class AddToFavButton : UIButton
    {
        private InstanceID ThisHuman = InstanceID.Empty;

        private readonly InstanceManager MyInstance = Singleton<InstanceManager>.instance;

        private readonly CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        public override void Start()
        {
            UIView aview = UIView.GetAView();
            name = "FavCimsStarButton";
            normalBgSprite = "icon_fav_unsubscribed";
            atlas = MyAtlas.FavCimsAtlas;
            size = new Vector2(32f, 32f);
            playAudioEvents = true;
            AlignTo(RefPanel, Alignment);
            tooltipBox = aview.defaultTooltipBox;
            eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                FavCimsCore.UpdateMyCitizen("toggle", RefPanel);
            };
        }

        public override void Update()
        {
            bool unLoading = MainClass.UnLoading;
            if (!unLoading)
            {
                bool isVisible = base.isVisible;
                if (isVisible)
                {
                    if (!WorldInfoPanel.GetCurrentInstanceID().IsEmpty)
                    {
                        ThisHuman = WorldInfoPanel.GetCurrentInstanceID();
                        int num = (int)ThisHuman.Citizen;
                        string name = MyInstance.GetName(ThisHuman);
                        if (name != null && name.Length > 0)
                        {
                            tooltip = Translations.Translate("FavStarButton_disable_tooltip");
                            normalBgSprite = "icon_fav_subscribed";
                            if (!FavCimsCore.RowID.Contains(num))
                            {
                                object privateVariable = FavCimsCore.GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
                                while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
                                {
                                }
                                try
                                {
                                    FavCimsCore.InsertIdIntoArray(num);
                                }
                                finally
                                {
                                    Monitor.Exit(privateVariable);
                                }
                            }
                        }
                        else
                        {
                            if (num != 0 && FavCimsCore.RowID.Contains(num))
                            {
                                MyInstance.SetName(ThisHuman, MyCitizen.GetCitizenName(ThisHuman.Citizen));
                                tooltip = Translations.Translate("FavStarButton_disable_tooltip");
                                normalBgSprite = "icon_fav_subscribed";
                            }
                            else
                            {
                                tooltip = Translations.Translate("FavStarButton_enable_tooltip");
                                normalBgSprite = "icon_fav_unsubscribed";
                            }
                        }
                    }
                }
            }
        }
    }
}
