using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace FavoriteCims
{
	public class ResourceLoader
	{
		public static Assembly ResourceAssembly
		{
			get
			{
				return Assembly.GetAssembly(typeof(ResourceLoader));
			}
		}

		public static string BaseDir
		{
			get
			{
				return Path.GetDirectoryName(ResourceLoader.ResourceAssembly.Location) + Path.PathSeparator.ToString();
			}
		}

		public static byte[] LoadResourceData(string name)
		{
			name = "FavoriteCims.Resources." + name;
			UnmanagedMemoryStream unmanagedMemoryStream = (UnmanagedMemoryStream)ResourceLoader.ResourceAssembly.GetManifestResourceStream(name);
			bool flag = unmanagedMemoryStream == null;
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				BinaryReader binaryReader = new BinaryReader(unmanagedMemoryStream);
				array = binaryReader.ReadBytes((int)unmanagedMemoryStream.Length);
			}
			return array;
		}

		public static string LoadResourceString(string name)
		{
			name = "FavoriteCims.Resources." + name;
			UnmanagedMemoryStream unmanagedMemoryStream = (UnmanagedMemoryStream)ResourceLoader.ResourceAssembly.GetManifestResourceStream(name);
			bool flag = unmanagedMemoryStream == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				StreamReader streamReader = new StreamReader(unmanagedMemoryStream);
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		public static Texture2D LoadTexture(int x, int y, string filename)
		{
			try
			{
				Texture2D texture2D = new Texture2D(x, y, TextureFormat.ARGB32, false);
				texture2D.LoadImage(ResourceLoader.LoadResourceData(filename));
				return ResourceLoader.FixTransparency(texture2D);
			}
			catch (Exception ex)
			{
				string text = "Exception Error ";
				Exception ex2 = ex;
				Debug.Log(text + (ex2?.ToString()));
			}
			return null;
		}

		internal static Texture2D FixTransparency(Texture2D texture)
		{
			Color32[] pixels = texture.GetPixels32();
			int width = texture.width;
			int height = texture.height;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					int num = i * width + j;
					Color32 color = pixels[num];
					bool flag = color.a == 0;
					if (flag)
					{
						bool flag2 = false;
						bool flag3 = !flag2 && j > 0;
						if (flag3)
						{
							flag2 = ResourceLoader.TryAdjacent(ref color, pixels[num - 1]);
						}
						bool flag4 = !flag2 && j < width - 1;
						if (flag4)
						{
							flag2 = ResourceLoader.TryAdjacent(ref color, pixels[num + 1]);
						}
						bool flag5 = !flag2 && i > 0;
						if (flag5)
						{
							flag2 = ResourceLoader.TryAdjacent(ref color, pixels[num - width]);
						}
						bool flag6 = !flag2 && i < height - 1;
						if (flag6)
						{
							flag2 = ResourceLoader.TryAdjacent(ref color, pixels[num + width]);
						}
						pixels[num] = color;
					}
				}
			}
			texture.SetPixels32(pixels);
			texture.Apply();
			return texture;
		}

		private static bool TryAdjacent(ref Color32 pixel, Color32 adjacent)
		{
			bool flag = adjacent.a == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				pixel.r = adjacent.r;
				pixel.g = adjacent.g;
				pixel.b = adjacent.b;
				flag2 = true;
			}
			return flag2;
		}
	}
}
