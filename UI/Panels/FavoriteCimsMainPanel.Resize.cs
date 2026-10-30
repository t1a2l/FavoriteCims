using ColossalFramework.UI;
using FavoriteCims.Utils;
using UnityEngine;

namespace FavoriteCims.UI.Panels
{
    public partial class FavoriteCimsMainPanel
    {
        private UITextureSprite resizeBackground;
        private UITextureSprite resizeMenu;
        private UITextureSprite resizeIndex;
        private UITextureSprite resizeFooter;
        private UIButton resizeClose;
        private UIComponent resizeScrollHost;
        private UIScrollbar resizeScrollbar;
        private UIButton resizeHandle;
        private UIButton resetSizeButton;
        private UIButton[] resizeHeaders;
        private bool resizeDragging;
        private Vector2 resizeStartMouse;
        private Vector2 resizeStartSize;
        private Vector2 resizeLastContainerSize;

        private void InitializeMainWindowResize(UITextureSprite background, UITextureSprite menu, UITextureSprite index, UITextureSprite footer, UIButton close, UIComponent scrollHost, UIScrollbar bar)
        {
            background.autoSize = false;
            menu.autoSize = false;
            index.autoSize = false;
            footer.autoSize = false;
            FavCimsTitleSprite.autoSize = false;
            FavCimsBodySprite.autoSize = false;
            resizeBackground = background;
            resizeMenu = menu;
            resizeIndex = index;
            resizeFooter = footer;
            resizeClose = close;
            resizeScrollHost = scrollHost;
            resizeScrollbar = bar;
            resizeHeaders =
            [
                FavCimsHappinesColText, FavCimsNameColText, FavCimsAgePhaseColText,
                FavCimsAgeColText, FavCimsEduColText, FavCimsHomeColText,
                FavCimsWorkingPlaceColText, FavCimsLastActColText, FavCimsCloseButtonCol
            ];

            resizeHandle = AddUIComponent<UIButton>();
            resizeHandle.name = "FavoriteCimsResizeHandle";
            resizeHandle.size = new Vector2(22f, 22f);
            resizeHandle.font = UIDynamicFont.FindByName("OpenSans-Regular");
            resizeHandle.text = "//";
            resizeHandle.textScale = 1.2f;
            resizeHandle.tooltipBox = UIView.GetAView().defaultTooltipBox;
            resizeHandle.tooltip = FavCimsLang.Text("MainResizeHandleTooltip");
            resizeHandle.eventMouseDown += BeginMainResize;

            resetSizeButton = AddUIComponent<UIButton>();
            resetSizeButton.name = "FavoriteCimsResetSize";
            resetSizeButton.size = new Vector2(100f, 20f);
            resetSizeButton.font = UIDynamicFont.FindByName("OpenSans-Regular");
            resetSizeButton.textScale = 0.8f;
            resetSizeButton.text = FavCimsLang.Text("MainResetSizeText");
            resetSizeButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
            resetSizeButton.tooltip = FavCimsLang.Text("MainResetSizeTooltip");
            resetSizeButton.eventClick += ResetMainSize;
            ApplyMainWindowSize(new Vector2(width, height));
        }

        private static Vector2 MainResizeMousePosition()
        {
            var container = FavCimsMainClass.FullScreenContainer;
            float uiWidth = container != null ? container.width : Screen.width;
            float uiHeight = container != null ? container.height : Screen.height;
            return new Vector2(Input.mousePosition.x * uiWidth / Mathf.Max(1, Screen.width), -Input.mousePosition.y * uiHeight / Mathf.Max(1, Screen.height));
        }

        private void StopMainWindowMovement()
        {
            if (PanelMover != null)
            {
                PanelMover.Stop = true;
                PanelMover.ComponentToMove = null;
            }
            opacity = 1f;
        }

        private void BeginMainResize(UIComponent component, UIMouseEventParameter param)
        {
            if ((param.buttons & UIMouseButton.Left) == 0) return;
            resizeDragging = true;
            StopMainWindowMovement();
            resizeStartMouse = MainResizeMousePosition();
            resizeStartSize = new Vector2(width, height);
            param.Use();
        }

        private void ResetMainSize(UIComponent component, UIMouseEventParameter param)
        {
            resizeDragging = false;
            StopMainWindowMovement();
            ApplyMainWindowSize(new Vector2(1200f, 700f));
            param.Use();
        }

        private void UpdateMainWindowResize()
        {
            if (resizeHandle == null) return;
            if (FavCimsMainClass.UnLoading || !isVisible)
            {
                resizeDragging = false;
                return;
            }
            var container = FavCimsMainClass.FullScreenContainer;
            Vector2 containerSize = container != null ? new Vector2(container.width, container.height) : new Vector2(Screen.width, Screen.height);
            if (containerSize != resizeLastContainerSize)
                ApplyMainWindowSize(new Vector2(width, height));
            if (!resizeDragging) return;
            StopMainWindowMovement();
            if (!Input.GetMouseButton(0))
            {
                resizeDragging = false;
                return;
            }
            ApplyMainWindowSize(resizeStartSize + MainResizeMousePosition() - resizeStartMouse);
        }

        private void ApplyMainWindowSize(Vector2 requested)
        {
            var container = FavCimsMainClass.FullScreenContainer;
            float containerWidth = container != null ? container.width : Screen.width;
            float containerHeight = container != null ? container.height : Screen.height;
            resizeLastContainerSize = new Vector2(containerWidth, containerHeight);
            float maxWidth = Mathf.Max(1f, containerWidth - 16f);
            float maxHeight = Mathf.Max(1f, containerHeight - 16f);
            Vector2 target = new(Mathf.Clamp(requested.x, Mathf.Min(960f, maxWidth), maxWidth), Mathf.Clamp(requested.y, Mathf.Min(350f, maxHeight), maxHeight));
            size = target;

            Vector3 position = absolutePosition;
            float left = container != null ? container.absolutePosition.x + 8f : 8f;
            float top = container != null ? container.absolutePosition.y + 8f : 8f;
            position.x = Mathf.Clamp(position.x, left, left + maxWidth - width);
            position.y = Mathf.Clamp(position.y, top, top + maxHeight - height);
            absolutePosition = position;

            resizeBackground.size = target;
            FavCimsTitleSprite.width = width;
            FavCimsTitleSprite.relativePosition = Vector3.zero;
            resizeMenu.width = Mathf.Max(1f, width - 10f);
            resizeMenu.relativePosition = new Vector3(5f, 58f);
            resizeClose.relativePosition = new Vector3(width - 48f, 13f);
            CitizensPanel.size = new Vector2(Mathf.Max(1f, width - 10f), Mathf.Max(1f, height - 142f));
            CitizensPanel.relativePosition = new Vector3(5f, 128f);
            FavCimsBodySprite.size = CitizensPanel.size;

            float rowWidth = Mathf.Max(1f, width - 66f);
            float viewportHeight = Mathf.Max(1f, CitizensPanel.height - 60f);
            resizeIndex.width = rowWidth + 12f;
            FavCimsCitizenRowsPanel.size = new Vector2(rowWidth, viewportHeight);
            FavCimsCitizenRowsPanel.relativePosition = new Vector3(27f, 33f);
            resizeScrollHost.relativePosition = new Vector3(27f + rowWidth + 6f, 33f);
            resizeScrollHost.height = viewportHeight;
            resizeScrollbar.relativePosition = Vector3.zero;
            resizeScrollbar.height = viewportHeight;
            if (resizeScrollbar.trackObject != null)
            {
                resizeScrollbar.trackObject.relativePosition = Vector3.zero;
                resizeScrollbar.trackObject.size = resizeScrollbar.size;
            }
            if (resizeScrollbar.thumbObject != null)
            {
                resizeScrollbar.thumbObject.relativePosition = Vector3.zero;
                resizeScrollbar.thumbObject.width = resizeScrollbar.width;
            }
            resizeFooter.width = rowWidth + 12f;
            resizeFooter.relativePosition = new Vector3(21f, 33f + viewportHeight);

            float[] minimum = { 60f, 110f, 90f, 40f, 100f, 120f, 120f, 140f, 50f };
            float[] flexible = { 0f, 0.20f, 0f, 0f, 0f, 0.25f, 0.25f, 0.30f, 0f };
            float scale = Mathf.Min(1f, rowWidth / 830f);
            float extra = Mathf.Max(0f, rowWidth - 830f);
            float x = 27f;
            for (int i = 0; i < resizeHeaders.Length; i++)
            {
                float columnWidth = minimum[i] * scale + extra * flexible[i];
                resizeHeaders[i].width = columnWidth;
                resizeHeaders[i].relativePosition = new Vector3(x, 8f);
                x += columnWidth;
            }
            resizeHandle.relativePosition = new Vector3(width - 25f, height - 24f);
            resetSizeButton.relativePosition = new Vector3(15f, height - 24f);
            resizeHandle.BringToFront();
            resetSizeButton.BringToFront();
        }

        private void ReleaseMainWindowResize()
        {
            resizeDragging = false;
            if (resizeHandle != null) resizeHandle.eventMouseDown -= BeginMainResize;
            if (resetSizeButton != null) resetSizeButton.eventClick -= ResetMainSize;
        }
    }
}