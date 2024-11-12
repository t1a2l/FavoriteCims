using System;
using System.Threading;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
	public class AddToFavButton : UIButton
	{
        private InstanceID ThisHuman = InstanceID.Empty;

        private InstanceManager MyInstance = Singleton<InstanceManager>.instance;

        private CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        public override void Start()
		{
			UIView aview = UIView.GetAView();
			base.name = "FavCimsStarButton";
			base.normalBgSprite = "icon_fav_unsubscribed";
			base.atlas = MyAtlas.FavCimsAtlas;
			base.size = new Vector2(32f, 32f);
			base.playAudioEvents = true;
			base.AlignTo(this.RefPanel, this.Alignment);
			base.tooltipBox = aview.defaultTooltipBox;
			base.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
			{
				FavCimsCore.UpdateMyCitizen("toggle", this.RefPanel);
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
					bool flag = !WorldInfoPanel.GetCurrentInstanceID().IsEmpty;
					if (flag)
					{
						this.ThisHuman = WorldInfoPanel.GetCurrentInstanceID();
						int num = (int)(uint)((UIntPtr)this.ThisHuman.Citizen);
						string name = this.MyInstance.GetName(this.ThisHuman);
						bool flag2 = name != null && name.Length > 0;
						if (flag2)
						{
							base.tooltip = FavCimsLang.text("FavStarButton_disable_tooltip");
							base.normalBgSprite = "icon_fav_subscribed";
							bool flag3 = !FavCimsCore.RowID.ContainsKey(num) && !FavoriteCimsMainPanel.RowsAlreadyExist(this.ThisHuman);
							if (flag3)
							{
								object privateVariable = FavCimsCore.GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
								while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
								{
								}
								try
								{
									CitizenRow citizenRow = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
									bool flag4 = citizenRow != null;
									if (flag4)
									{
										citizenRow.MyInstanceID = this.ThisHuman;
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
							bool flag5 = num != 0 && FavCimsCore.RowID.ContainsKey(num);
							if (flag5)
							{
								this.MyInstance.SetName(this.ThisHuman, this.MyCitizen.GetCitizenName(this.ThisHuman.Citizen));
								base.tooltip = FavCimsLang.text("FavStarButton_disable_tooltip");
								base.normalBgSprite = "icon_fav_subscribed";
							}
							else
							{
								base.tooltip = FavCimsLang.text("FavStarButton_enable_tooltip");
								base.normalBgSprite = "icon_fav_unsubscribed";
							}
						}
					}
				}
			}
		}

		public AddToFavButton()
		{
		}

	}
}
