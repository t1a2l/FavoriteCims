using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.Utils
{
	public static class GuiDebug
	{
		public static void Log(string message)
		{
			UIPanel uipanel = UIView.Find<UIPanel>("FullScreenContainer");
			bool flag = uipanel != null;
			if (flag)
			{
				bool flag2 = uipanel.Find<UILabel>("FavCimsDebugLabel") == null;
				UILabel uilabel;
				if (flag2)
				{
					uilabel = uipanel.AddUIComponent<UILabel>();
					uilabel.name = "FavCimsDebugLabel";
					uilabel.width = 700f;
					uilabel.height = 300f;
					uilabel.relativePosition = new Vector3(200f, 20f);
				}
				else
				{
					uilabel = uipanel.Find<UILabel>("FavCimsDebugLabel");
				}
				uilabel.text = message;
			}
		}

		public static void Destroy()
		{
			UIPanel uipanel = UIView.Find<UIPanel>("FullScreenContainer");
			bool flag = uipanel.Find<UILabel>("FavCimsDebugLabel") != null;
			if (flag)
			{
                Object.Destroy(uipanel.Find<UILabel>("FavCimsDebugLabel").gameObject);
			}
		}
	}
}
