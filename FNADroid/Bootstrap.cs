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

using Environment = System.Environment;

namespace FNADroid
{
	delegate void Main();

	static class Bootstrap
	{

		public static void SDL_Main()
		{
			FNADroidPlatform.Initialize();

			// Replace the following "Program.Main via reflection" call with whatever was in your old Program.Main method.
			Assembly.LoadFrom(Environment.GetEnvironmentVariable("FNADROID_GAMEPATH")).EntryPoint.Invoke(null, new object[] { new string[] { /*args*/ } });
		}

		public static void SetupMain()
		{
			Java.Lang.JavaSystem.LoadLibrary("fnadroid-ext");
			// Required for OpenAL to function properly as it access the JNI env directly.
			Java.Lang.JavaSystem.LoadLibrary("soft_oal");

			// Give the main library something to call in Mono-Land afterwards
			SetMain(SDL_Main);

			// Load stub Steamworks.NET
			Steamworks.SteamAPI.Init();

			// FNA and FNADroid environment vars
			// If your game code is shipping with the APK (f.e. your game is referenced by the FNADroid project), FNADROID_GAMEPATH is useless to you.
			if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("FNADROID_GAMEPATH")))
			{
				// HARDCODED FOR DEMO PURPOSES
				Environment.SetEnvironmentVariable("FNADROID_GAMEPATH", "/storage/sdcard1/Stardew Valley/Stardew Valley.exe");
			}

			string storagePath = MainActivity.SDL2DCS_Instance.GetExternalFilesDir(null).AbsolutePath;

			string gamePath = Environment.GetEnvironmentVariable("FNADROID_GAMEPATH");
			string gameDir;
			if (!string.IsNullOrEmpty(gamePath))
			{
				// GAMEPATH defined: Set paths relative to game path.
				gameDir = Directory.GetParent(gamePath).FullName;
			}
			else
			{
				// GAMEPATH not defined: Set paths relative to storage path.
				gamePath = Path.Combine(storagePath, "game.exe"); // Fake!
				gameDir = storagePath;
			}

			Directory.SetCurrentDirectory(gameDir);

			Environment.SetEnvironmentVariable("FNADROID", "1");
			Environment.SetEnvironmentVariable("FNADROID_LOCALDIR", storagePath);
			Environment.SetEnvironmentVariable("FNA_CONFDIR", storagePath);
			Environment.SetEnvironmentVariable("FNA_TITLEDIR", gameDir);

			Environment.SetEnvironmentVariable("FNA_OPENGL_FORCE_ES3", "1");

			// At this point I'm just waiting for FACT...
			// Those were once required for old that old copy of OpenAL I found somewhere(tm).
			/*
			Environment.SetEnvironmentVariable("FNA_AUDIO_DEVICE_NAME", "Android Legacy");
			Environment.SetEnvironmentVariable("FNA_AUDIO_DEVICES_IN", " ");
			*/
			// This is required to save RAM.
			Environment.SetEnvironmentVariable("FNA_AUDIO_FORCE_STREAM", "1");
		}

		[DllImport("main")]
		static extern void SetMain(Main main);

	}
}
