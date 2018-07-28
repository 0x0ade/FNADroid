using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SDL2;

using System.Runtime.InteropServices;
using Android.Content.Res;
using Android.Util;
using System.Xml;

using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Environment = System.Environment;

namespace FNADroid
{
	delegate void Main();

	static class Bootstrap
	{

		// TODO: UNHARDCODE!
		public const string Game = "Celeste/Celeste.exe";
		
		public static void SDL_Main()
		{
			Console.WriteLine("FNADROIDFNADROIDFNADROID RUNNING SDL_Main");

			// Load stub Steamworks.NET
			Steamworks.SteamAPI.Init();

			string gamePath = null;
			foreach (Java.IO.File root in MainActivity.SDL2DCS_Instance.GetExternalFilesDirs(null))
			{
				string path = Path.Combine(root.AbsolutePath, Game);
				if (!File.Exists(path))
					continue;
				gamePath = path;
				break;
			}

			if (string.IsNullOrEmpty(gamePath))
			{
				// TODO: Show proper error message.
				throw new Exception("Game not found!");
			}

			Environment.CurrentDirectory = Path.GetDirectoryName(gamePath);
			Environment.SetEnvironmentVariable("FNADROID", "1");

			FNADroidPlatform.PreInitialize();

			// Replace the following with whatever was in your Program.Main method.

			/*
			using (TestGame game = new TestGame())
			{
				game.Run();
			}
			*/
			Assembly.LoadFrom(gamePath).EntryPoint.Invoke(null, new object[] { new string[] { /*args*/ } });
		}

		[DllImport("main")]
		public static extern void SetMain(Main main);

	}
}
