using System;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
	public class PeopleInsideServiceBuildingsButton : UIButton
	{
        private InstanceID BuildingID = InstanceID.Empty;

        private BuildingManager MyBuilding = Singleton<BuildingManager>.instance;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        private PeopleInsideServiceBuildingsPanel BuildingPanel;

        public override void Start()
		{
			UIView aview = UIView.GetAView();
			base.name = "PeopleInsideServiceBuildingsButton";
			base.atlas = MyAtlas.FavCimsAtlas;
			base.size = new Vector2(32f, 36f);
			base.playAudioEvents = true;
			base.AlignTo(this.RefPanel, this.Alignment);
			base.tooltipBox = aview.defaultTooltipBox;
			bool flag = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideServiceBuildingsPanel>() != null;
			if (flag)
			{
				this.BuildingPanel = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideServiceBuildingsPanel>();
			}
			else
			{
				this.BuildingPanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(PeopleInsideServiceBuildingsPanel)) as PeopleInsideServiceBuildingsPanel;
			}
			this.BuildingPanel.BuildingID = InstanceID.Empty;
			this.BuildingPanel.Hide();
			base.eventClick += delegate(UIComponent component, UIMouseEventParameter eventParam)
			{
				bool flag2 = !this.BuildingID.IsEmpty && !this.BuildingPanel.isVisible;
				if (flag2)
				{
					this.BuildingPanel.BuildingID = this.BuildingID;
					this.BuildingPanel.RefPanel = this.RefPanel;
					this.BuildingPanel.Show();
				}
				else
				{
					this.BuildingPanel.BuildingID = InstanceID.Empty;
					this.BuildingPanel.Hide();
				}
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
					base.tooltip = null;
					bool flag = WorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty;
					if (flag)
					{
						this.BuildingID = WorldInfoPanel.GetCurrentInstanceID();
					}
					bool flag2 = this.BuildingPanel != null;
					if (flag2)
					{
						bool flag3 = !this.BuildingPanel.isVisible;
						if (flag3)
						{
							base.Unfocus();
						}
						else
						{
							base.Focus();
						}
					}
					bool flag4 = !this.BuildingID.IsEmpty && this.BuildingID.Type == InstanceType.Building;
					if (flag4)
					{
						BuildingInfo info = this.MyBuilding.m_buildings.m_buffer[(int)this.BuildingID.Building].Info;
						switch (info.m_class.m_service)
						{
						case ItemClass.Service.Electricity:
							base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
							base.normalBgSprite = "IndustrialBuildingButtonIcon";
							base.hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
							base.focusedBgSprite = "IndustrialBuildingButtonIconHovered";
							base.pressedBgSprite = "IndustrialBuildingButtonIconHovered";
							base.disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
							goto IL_050D;
						case ItemClass.Service.Beautification:
							base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Guests");
							base.normalBgSprite = "CommercialBuildingButtonIcon";
							base.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
							base.focusedBgSprite = "CommercialBuildingButtonIconHovered";
							base.pressedBgSprite = "CommercialBuildingButtonIconHovered";
							base.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
							goto IL_050D;
						case ItemClass.Service.Garbage:
							base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
							base.normalBgSprite = "IndustrialBuildingButtonIcon";
							base.hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
							base.focusedBgSprite = "IndustrialBuildingButtonIconHovered";
							base.pressedBgSprite = "IndustrialBuildingButtonIconHovered";
							base.disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
							goto IL_050D;
						case ItemClass.Service.HealthCare:
							base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
							base.normalBgSprite = "CommercialBuildingButtonIcon";
							base.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
							base.focusedBgSprite = "CommercialBuildingButtonIconHovered";
							base.pressedBgSprite = "CommercialBuildingButtonIconHovered";
							base.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
							goto IL_050D;
						case ItemClass.Service.PoliceDepartment:
							base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
							base.normalBgSprite = "CommercialBuildingButtonIcon";
							base.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
							base.focusedBgSprite = "CommercialBuildingButtonIconHovered";
							base.pressedBgSprite = "CommercialBuildingButtonIconHovered";
							base.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
							goto IL_050D;
						case ItemClass.Service.Education:
							base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
							base.normalBgSprite = "CommercialBuildingButtonIcon";
							base.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
							base.focusedBgSprite = "CommercialBuildingButtonIconHovered";
							base.pressedBgSprite = "CommercialBuildingButtonIconHovered";
							base.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
							goto IL_050D;
						case ItemClass.Service.Monument:
							base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("CitizenOnBuilding");
							base.normalBgSprite = "CommercialBuildingButtonIcon";
							base.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
							base.focusedBgSprite = "CommercialBuildingButtonIconHovered";
							base.pressedBgSprite = "CommercialBuildingButtonIconHovered";
							base.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
							goto IL_050D;
						case ItemClass.Service.FireDepartment:
							base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
							base.normalBgSprite = "CommercialBuildingButtonIcon";
							base.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
							base.focusedBgSprite = "CommercialBuildingButtonIconHovered";
							base.pressedBgSprite = "CommercialBuildingButtonIconHovered";
							base.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
							goto IL_050D;
						case ItemClass.Service.PublicTransport:
							base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
							base.normalBgSprite = "IndustrialBuildingButtonIcon";
							base.hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
							base.focusedBgSprite = "IndustrialBuildingButtonIconHovered";
							base.pressedBgSprite = "IndustrialBuildingButtonIconHovered";
							base.disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
							goto IL_050D;
						}
						base.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						base.normalBgSprite = "CommercialBuildingButtonIcon";
						base.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						base.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						base.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						base.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
						IL_050D:
						bool flag5 = Convert.ToInt32(this.MyBuilding.m_buildings.m_buffer[(int)this.BuildingID.Building].m_citizenCount) == 0;
						if (flag5)
						{
							this.BuildingPanel.Hide();
							base.tooltip = FavCimsLang.text("BuildingIsEmpty");
							base.isEnabled = false;
						}
						else
						{
							base.isEnabled = true;
						}
					}
					else
					{
						this.BuildingPanel.Hide();
						base.Unfocus();
						base.isEnabled = false;
					}
				}
				else
				{
					base.isEnabled = false;
					this.BuildingPanel.Hide();
					this.BuildingID = InstanceID.Empty;
				}
			}
		}


		
	}
}
