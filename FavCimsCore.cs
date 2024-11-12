using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using ColossalFramework;
using ColossalFramework.UI;
using FavoriteCims.UI.Panels;
using UnityEngine;

namespace FavoriteCims
{
    public class FavCimsCore : MonoBehaviour
	{
        public static InstanceID ThisHuman;

        public static Dictionary<int, int> RowID = [];

        public static T GetPrivateVariable<T>(object obj, string fieldName)
		{
			return (T)obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
		}

		public static Dictionary<InstanceID, string> FavoriteCimsList()
		{
			object privateVariable = GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
			while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
			{
			}
			Dictionary<InstanceID, string> dictionary;
			try
			{
				Dictionary<InstanceID, string> privateVariable2 = GetPrivateVariable<Dictionary<InstanceID, string>>(Singleton<InstanceManager>.instance, "m_names");
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
				object privateVariable = GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
				while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
				{
				}
				try
				{
					InstanceManager instance = Singleton<InstanceManager>.instance;
					CitizenManager instance2 = Singleton<CitizenManager>.instance;
					uint citizen = MyInstanceID.Citizen;
					string citizenName = instance2.GetCitizenName(citizen);
					int citizenINT = (int)(UIntPtr)citizen;
					if (citizenName != null && citizenName.Length > 0)
					{
						if (RowID.ContainsKey(citizenINT))
						{
							if (!FavoriteCimsMainPanel.RowsAlreadyExist(MyInstanceID))
							{
								try
								{
									instance.SetName(MyInstanceID, citizenName);
									CitizenRow citizenRow = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
									if (citizenRow != null)
									{
										citizenRow.MyInstanceID = MyInstanceID;
										citizenRow.MyInstancedName = citizenName;
									}
								}
								catch (Exception ex)
								{
									Utils.Debug.Error("Add To Favorites Fail : " + ex.ToString());
								}
							}
						}
						else
						{
							RemoveRowAndRemoveFav(MyInstanceID, citizenINT);
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
			object privateVariable = GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
			while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
			{
			}
			try
			{
				InstanceManager instance = Singleton<InstanceManager>.instance;
				if (!citizenInstanceID.IsEmpty)
				{
					string name = instance.GetName(citizenInstanceID);
					if (name != null && name.Length > 0)
					{
						instance.SetName(citizenInstanceID, null);
					}
				}
				RemoveIdFromArray(citizenID);
			}
			finally
			{
				Monitor.Exit(privateVariable);
			}
		}

		public static void UpdateMyCitizen(string action, UIPanel refPanel)
		{
            object privateVariable = GetPrivateVariable<object>(Singleton<InstanceManager>.instance, "m_lock");
			while (!Monitor.TryEnter(privateVariable, SimulationManager.SYNCHRONIZE_TIMEOUT))
			{
			}
			try
			{
				InstanceManager instance2 = Singleton<InstanceManager>.instance;
				refPanel.SimulateClick();
				ThisHuman = WorldInfoPanel.GetCurrentInstanceID();
				string name = instance2.GetName(ThisHuman);
				int citizenINT = (int)(UIntPtr)ThisHuman.Citizen;
				if (action == "toggle" && name != null)
				{
					try
					{
						instance2.SetName(ThisHuman, null);
						RemoveIdFromArray(citizenINT);
					}
					catch (Exception ex)
					{
						Utils.Debug.Error("Toggle Remove Fail : " + ex.ToString());
					}
				}
				else
				{
					try
					{
						UITextField componentInChildren = refPanel.GetComponentInChildren<UITextField>();
						instance2.SetName(ThisHuman, componentInChildren.text);
					}
					catch (Exception ex2)
					{
                        Utils.Debug.Error("Toggle Add Fail : " + ex2.ToString());
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
			RowID[citID] = citID;
		}

		public static void RemoveIdFromArray(int citID)
		{
			RowID.Remove(citID);
		}

		public static void ClearIdArray()
		{
			RowID.Clear();
		}

		public static void GoToCitizen(Vector3 position, InstanceID Target, bool tourist, UIMouseEventParameter eventParam)
		{
			bool isEmpty = Target.IsEmpty;
			if (!isEmpty)
			{
				InstanceManager instance = Singleton<InstanceManager>.instance;
				try
				{
					if (instance.SelectInstance(Target))
					{
						if (UIView.Find<UILabel>("DefaultTooltip"))
						{
							UIView.Find<UILabel>("DefaultTooltip").Hide();
						}
						if (eventParam.buttons == UIMouseButton.Middle)
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
							if (eventParam.buttons == UIMouseButton.Left)
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
                    Utils.Debug.Error("Can't find the Citizen " + ex.ToString());
				}
			}
		}

		public static int CalculateCitizenAge(int GameAge)
		{
			int num;
			if (GameAge <= 0)
			{
				num = 0;
			}
			else
			{
				if (GameAge > 0 && GameAge <= 15)
				{
					double num2 = GameAge / 15.0 * 100.0;
					int num3 = 1;
					int num4 = 12;
					double num5 = (num4 - num3) / 100.0 * num2;
					int num6 = num3 + (int)num5;
					num = num6;
				}
				else
				{
					if (GameAge <= 45)
					{
						double num2 = (GameAge - 15.0) / 30.0 * 100.0;
						int num3 = 13;
						int num4 = 19;
						double num5 = (num4 - num3) / 100.0 * num2;
						int num6 = num3 + (int)num5;
						num = num6;
					}
					else
					{
						if (GameAge <= 90)
						{
							double num2 = (GameAge - 45.0) / 45.0 * 100.0;
							int num3 = 20;
							int num4 = 25;
							double num5 = (num4 - num3) / 100.0 * num2;
							int num6 = num3 + (int)num5;
							num = num6;
						}
						else
						{
							if (GameAge <= 180)
							{
								double num2 = (GameAge - 90.0) / 90.0 * 100.0;
								int num3 = 26;
								int num4 = 65;
								double num5 = (num4 - num3) / 100.0 * num2;
								int num6 = num3 + (int)num5;
								num = num6;
							}
							else
							{
								if (GameAge <= 240)
								{
									double num2 = (GameAge - 180.0) / 60.0 * 100.0;
									int num3 = 66;
									int num4 = 90;
									double num5 = (num4 - num3) / 100.0 * num2;
									int num6 = num3 + (int)num5;
									num = num6;
								}
								else
								{
									int num7 = 400;
									double num2 = (GameAge - 240.0) / (num7 - 240) * 100.0;
									int num3 = 91;
									int num4 = 114;
									double num5 = (num4 - num3) / 100.0 * num2;
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
