using FavoriteCims.Utils;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace FavoriteCims.Utils
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
				return Path.GetDirectoryName(ResourceAssembly.Location) + Path.PathSeparator.ToString();
			}
		}

		public static byte[] LoadResourceData(string name)
		{
			name = "FavoriteCims.Utils.Resources." + name;
			UnmanagedMemoryStream unmanagedMemoryStream = (UnmanagedMemoryStream)ResourceAssembly.GetManifestResourceStream(name);
			byte[] array;
			if (unmanagedMemoryStream == null)
			{
				array = null;
			}
			else
			{
				BinaryReader binaryReader = new(unmanagedMemoryStream);
				array = binaryReader.ReadBytes((int)unmanagedMemoryStream.Length);
			}
			return array;
		}

		public static string LoadResourceString(string name)
		{
			name = "FavoriteCims.Utils.Resources." + name;
			UnmanagedMemoryStream unmanagedMemoryStream = (UnmanagedMemoryStream)ResourceAssembly.GetManifestResourceStream(name);
			string text;
			if (unmanagedMemoryStream == null)
			{
				text = null;
			}
			else
			{
				StreamReader streamReader = new(unmanagedMemoryStream);
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		public static Texture2D LoadTexture(int x, int y, string filename)
		{
			try
			{
				Texture2D texture2D = new(x, y, TextureFormat.ARGB32, false);
				texture2D.LoadImage(LoadResourceData(filename));
				return FixTransparency(texture2D);
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
					if (color.a == 0)
					{
						bool done = false;
						if (!done && j > 0)
						{
                            done = TryAdjacent(ref color, pixels[num - 1]);
						}
						if (!done && j < width - 1)
						{
                            done = TryAdjacent(ref color, pixels[num + 1]);
						}
						if (!done && i > 0)
						{
                            done = TryAdjacent(ref color, pixels[num - width]);
						}
						if (!done && i < height - 1)
						{
                            done = TryAdjacent(ref color, pixels[num + width]);
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
			if (adjacent.a == 0)
			{
				return false;
			}
            pixel.r = adjacent.r;
            pixel.g = adjacent.g;
            pixel.b = adjacent.b;
            return true;
        }
	}
}
