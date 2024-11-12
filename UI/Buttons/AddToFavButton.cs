using System;
using System.Threading;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using FavoriteCims.Utils;
using UnityEngine;

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
            bool unLoading = FavCimsMainClass.UnLoading;
            if (!unLoading)
            {
                bool isVisible = base.isVisible;
                if (isVisible)
                {
                    if (!WorldInfoPanel.GetCurrentInstanceID().IsEmpty)
                    {
                        ThisHuman = WorldInfoPanel.GetCurrentInstanceID();
                        int num = (int)(uint)(UIntPtr)ThisHuman.Citizen;
                        string name = MyInstance.GetName(ThisHuman);
                        if (name != null && name.Length > 0)
                        {
                            tooltip = FavCimsLang.Text("FavStarButton_disable_tooltip");
                            normalBgSprite = "icon_fav_subscribed";
                            if (!FavCimsCore.RowID.ContainsKey(num) && !FavoriteCimsMainPanel.RowsAlreadyExist(ThisHuman))
                            {
                                object privateVariable = FavCimsCore.GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
                                while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
                                {
                                }
                                try
                                {
                                    CitizenRow citizenRow = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
                                    if (citizenRow != null)
                                    {
                                        citizenRow.MyInstanceID = ThisHuman;
                                        citizenRow.MyInstancedName = name;
                                    }
                                }
                                finally
                                {
                                    Monitor.Exit(privateVariable);
                                }
                            }
                        }
                        else
                        {
                            if (num != 0 && FavCimsCore.RowID.ContainsKey(num))
                            {
                                MyInstance.SetName(ThisHuman, MyCitizen.GetCitizenName(ThisHuman.Citizen));
                                tooltip = FavCimsLang.Text("FavStarButton_disable_tooltip");
                                normalBgSprite = "icon_fav_subscribed";
                            }
                            else
                            {
                                tooltip = FavCimsLang.Text("FavStarButton_enable_tooltip");
                                normalBgSprite = "icon_fav_unsubscribed";
                            }
                        }
                    }
                }
            }
        }
    }
}
