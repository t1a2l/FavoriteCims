using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
	public class FavCimsCore : MonoBehaviour
	{
        public static InstanceID ThisHuman;

        public static Dictionary<int, int> RowID = [];

        public static T GetPrivateVariable<T>(object obj, string fieldName)
		{
			return (T)((object)obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj));
		}

		public static Dictionary<InstanceID, string> FavoriteCimsList()
		{
			object privateVariable = FavCimsCore.GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
			while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
			{
			}
			Dictionary<InstanceID, string> dictionary;
			try
			{
				Dictionary<InstanceID, string> privateVariable2 = FavCimsCore.GetPrivateVariable<Dictionary<InstanceID, string>>(Singleton<InstanceManager>.instance, "m_names");
				dictionary = privateVariable2;
			}
			finally
			{
				Monitor.Exit(privateVariable);
			}
			return dictionary;
		}

		public static void AddToFavorites(InstanceID MyInstanceID)
		{
			bool isEmpty = MyInstanceID.IsEmpty;
			if (!isEmpty)
			{
				object privateVariable = FavCimsCore.GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
				while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
				{
				}
				try
				{
					InstanceManager instance = Singleton<InstanceManager>.instance;
					CitizenManager instance2 = Singleton<CitizenManager>.instance;
					uint citizen = MyInstanceID.Citizen;
					string citizenName = instance2.GetCitizenName(citizen);
					int num = (int)(uint)((UIntPtr)citizen);
					bool flag = citizenName != null && citizenName.Length > 0;
					if (flag)
					{
						bool flag2 = !FavCimsCore.RowID.ContainsKey(num);
						if (flag2)
						{
							bool flag3 = !FavoriteCimsMainPanel.RowsAlreadyExist(MyInstanceID);
							if (flag3)
							{
								try
								{
									instance.SetName(MyInstanceID, citizenName);
									CitizenRow citizenRow = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
									bool flag4 = citizenRow != null;
									if (flag4)
									{
										citizenRow.MyInstanceID = MyInstanceID;
										citizenRow.MyInstancedName = citizenName;
									}
								}
								catch (Exception ex)
								{
									Debug.Error("Add To Favorites Fail : " + ex.ToString());
								}
							}
						}
						else
						{
							FavCimsCore.RemoveRowAndRemoveFav(MyInstanceID, num);
						}
					}
				}
				finally
				{
					Monitor.Exit(privateVariable);
				}
			}
		}

		public static void RemoveRowAndRemoveFav(InstanceID citizenInstanceID, int citizenID)
		{
			object privateVariable = FavCimsCore.GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
			while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
			{
			}
			try
			{
				InstanceManager instance = Singleton<InstanceManager>.instance;
				bool flag = !citizenInstanceID.IsEmpty;
				if (flag)
				{
					string name = instance.GetName(citizenInstanceID);
					bool flag2 = name != null && name.Length > 0;
					if (flag2)
					{
						instance.SetName(citizenInstanceID, null);
					}
				}
				FavCimsCore.RemoveIdFromArray(citizenID);
			}
			finally
			{
				Monitor.Exit(privateVariable);
			}
		}

		public static void UpdateMyCitizen(string action, UIPanel refPanel)
		{
            object privateVariable = FavCimsCore.GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
			while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
			{
			}
			try
			{
				InstanceManager instance2 = Singleton<InstanceManager>.instance;
				refPanel.SimulateClick();
				FavCimsCore.ThisHuman = WorldInfoPanel.GetCurrentInstanceID();
				string name = instance2.GetName(FavCimsCore.ThisHuman);
				int num = (int)(uint)((UIntPtr)FavCimsCore.ThisHuman.Citizen);
				bool flag = action == "toggle" && name != null;
				if (flag)
				{
					try
					{
						instance2.SetName(FavCimsCore.ThisHuman, null);
						FavCimsCore.RemoveIdFromArray(num);
					}
					catch (Exception ex)
					{
						Debug.Error("Toggle Remove Fail : " + ex.ToString());
					}
				}
				else
				{
					try
					{
						UITextField componentInChildren = refPanel.GetComponentInChildren<UITextField>();
						instance2.SetName(FavCimsCore.ThisHuman, componentInChildren.text);
					}
					catch (Exception ex2)
					{
						Debug.Error("Toggle Add Fail : " + ex2.ToString());
					}
				}
			}
			finally
			{
				Monitor.Exit(privateVariable);
			}
		}

		public static void InsertIdIntoArray(int citID)
		{
			FavCimsCore.RowID[citID] = citID;
		}

		public static void RemoveIdFromArray(int citID)
		{
			FavCimsCore.RowID.Remove(citID);
		}

		public static void ClearIdArray()
		{
			FavCimsCore.RowID.Clear();
		}

		public static void GoToCitizen(Vector3 position, InstanceID Target, bool tourist, UIMouseEventParameter eventParam)
		{
			bool isEmpty = Target.IsEmpty;
			if (!isEmpty)
			{
				InstanceManager instance = Singleton<InstanceManager>.instance;
				try
				{
					bool flag = instance.SelectInstance(Target);
					if (flag)
					{
						bool flag2 = UIView.Find<UILabel>("DefaultTooltip");
						if (flag2)
						{
							UIView.Find<UILabel>("DefaultTooltip").Hide();
						}
						bool flag3 = eventParam.buttons == UIMouseButton.Middle;
						if (flag3)
						{
							if (tourist)
							{
								WorldInfoPanel.Show<TouristWorldInfoPanel>(position, Target);
							}
							else
							{
								WorldInfoPanel.Show<CitizenWorldInfoPanel>(position, Target);
							}
						}
						else
						{
							bool flag4 = eventParam.buttons == UIMouseButton.Left;
							if (flag4)
							{
								ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
							}
							else
							{
								ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
								if (tourist)
								{
									WorldInfoPanel.Show<TouristWorldInfoPanel>(position, Target);
								}
								else
								{
									WorldInfoPanel.Show<CitizenWorldInfoPanel>(position, Target);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Debug.Error("Can't find the Citizen " + ex.ToString());
				}
			}
		}

		public static int CalculateCitizenAge(int GameAge)
		{
			bool flag = GameAge <= 0;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = GameAge > 0 && GameAge <= 15;
				if (flag2)
				{
					double num2 = (double)GameAge / 15.0 * 100.0;
					int num3 = 1;
					int num4 = 12;
					double num5 = ((double)num4 - (double)num3) / 100.0 * num2;
					int num6 = num3 + (int)num5;
					num = num6;
				}
				else
				{
					bool flag3 = GameAge <= 45;
					if (flag3)
					{
						double num2 = ((double)GameAge - 15.0) / 30.0 * 100.0;
						int num3 = 13;
						int num4 = 19;
						double num5 = ((double)num4 - (double)num3) / 100.0 * num2;
						int num6 = num3 + (int)num5;
						num = num6;
					}
					else
					{
						bool flag4 = GameAge <= 90;
						if (flag4)
						{
							double num2 = ((double)GameAge - 45.0) / 45.0 * 100.0;
							int num3 = 20;
							int num4 = 25;
							double num5 = ((double)num4 - (double)num3) / 100.0 * num2;
							int num6 = num3 + (int)num5;
							num = num6;
						}
						else
						{
							bool flag5 = GameAge <= 180;
							if (flag5)
							{
								double num2 = ((double)GameAge - 90.0) / 90.0 * 100.0;
								int num3 = 26;
								int num4 = 65;
								double num5 = ((double)num4 - (double)num3) / 100.0 * num2;
								int num6 = num3 + (int)num5;
								num = num6;
							}
							else
							{
								bool flag6 = GameAge <= 240;
								if (flag6)
								{
									double num2 = ((double)GameAge - 180.0) / 60.0 * 100.0;
									int num3 = 66;
									int num4 = 90;
									double num5 = ((double)num4 - (double)num3) / 100.0 * num2;
									int num6 = num3 + (int)num5;
									num = num6;
								}
								else
								{
									int num7 = 400;
									double num2 = ((double)GameAge - 240.0) / (double)(num7 - 240) * 100.0;
									int num3 = 91;
									int num4 = 114;
									double num5 = ((double)num4 - (double)num3) / 100.0 * num2;
									int num6 = num3 + (int)num5;
									num = num6;
								}
							}
						}
					}
				}
			}
			return num;
		}
	}
}
