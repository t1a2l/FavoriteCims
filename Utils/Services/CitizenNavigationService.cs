using System;
using ColossalFramework.UI;

namespace FavoriteCims.Utils.Services
{
    public static class CitizenNavigationService
    {
        public static void Navigate(InstanceID target, UIMouseEventParameter eventParam, bool hideFavoritesPanel = false,bool useCitizenPanel = false, bool isTourist = false)
        {
            if (target.IsEmpty)
            { 
                return; 
            }

            try
            {
                if (hideFavoritesPanel && FavCimsMainClass.FavCimsPanel != null)
                {
                    FavCimsMainClass.FavCimsPanel.Hide();
                }

                if (eventParam.buttons == UIMouseButton.Middle)
                {
                    DefaultTool.OpenWorldInfoPanel(target, ToolsModifierControl.cameraController.transform.position);
                    return;
                }

                ToolsModifierControl.cameraController.SetTarget(target, ToolsModifierControl.cameraController.transform.position, true);

                if (eventParam.buttons == UIMouseButton.Right)   
                { 
                    DefaultTool.OpenWorldInfoPanel(target, ToolsModifierControl.cameraController.transform.position); 
                }
            }
            catch (Exception ex)
            {
                Debug.Error("Navigation failed: " + ex);
            }
        }


    }
}
