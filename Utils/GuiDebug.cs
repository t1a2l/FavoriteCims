using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.Utils
{
	public static class GuiDebug
	{
		public static void Log(string message)
		{
			UIPanel uipanel = UIView.Find<UIPanel>("FullScreenContainer");
			if (uipanel != null)
			{
				UILabel uilabel;
				if (uipanel.Find<UILabel>("DebugLabel") == null)
				{
					uilabel = uipanel.AddUIComponent<UILabel>();
					uilabel.name = "DebugLabel";
					uilabel.width = 700f;
					uilabel.height = 300f;
					uilabel.relativePosition = new Vector3(200f, 20f);
				}
				else
				{
					uilabel = uipanel.Find<UILabel>("DebugLabel");
				}
				uilabel.text = message;
			}
		}

		public static void Destroy()
		{
			UIPanel uipanel = UIView.Find<UIPanel>("FullScreenContainer");
			if (uipanel.Find<UILabel>("DebugLabel") != null)
			{
                Object.Destroy(uipanel.Find<UILabel>("DebugLabel").gameObject);
			}
		}
	}
}
