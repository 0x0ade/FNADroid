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

namespace FNADroid
{
	delegate void Main();

	static class Bootstrap
	{

		public static void SDL_Main()
		{
			Assembly.LoadFrom(System.Environment.GetEnvironmentVariable("FNADROID_GAMEPATH")).EntryPoint.Invoke(null, new object[] { new string[] { /*args*/ } });
		}

		public static void SetupMain()
		{
			// Give the main library something to call in Mono-Land afterwards
			SetMain(SDL_Main);

			// Load stub Steamworks.NET
			Steamworks.SteamAPI.Init();

			// FNA and FNADroid environment vars

			System.Environment.SetEnvironmentVariable("FNADROID_GAMEPATH", "/storage/sdcard1/FEZ/FEZ.exe"); // FIXME: HARDCODED

			string gamePath = System.Environment.GetEnvironmentVariable("FNADROID_GAMEPATH");

			System.Environment.SetEnvironmentVariable("FNADROID", "1");
			System.Environment.SetEnvironmentVariable("FNADROID_LOCALDIR", MainActivity.SDL2DCS_Instance.FilesDir.AbsolutePath);
			System.Environment.SetEnvironmentVariable("FNA_CONFDIR", MainActivity.SDL2DCS_Instance.FilesDir.AbsolutePath);
			string gameDir = Directory.GetParent(gamePath).FullName;
			Directory.SetCurrentDirectory(gameDir);
			System.Environment.SetEnvironmentVariable("FNA_TITLEDIR", gameDir);

			System.Environment.SetEnvironmentVariable("FNA_OPENGL_FORCE_ES3", "1");

			System.Environment.SetEnvironmentVariable("FNA_AUDIO_DISABLE_SOUND", "1"); // FIXME: OpenAL opening device crashes
		}

		[DllImport("main")]
		static extern void SetMain(Main main);

	}
}
