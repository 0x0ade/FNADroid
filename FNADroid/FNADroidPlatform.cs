using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FNADroid
{
	public static class FNADroidPlatform
	{

		public static Game Game;

		public static void PreInitialize()
		{
			// Hook("ApplyWindowChanges");
			Hook("RunLoop");
		}

		public static void Initialize(Game game)
		{
			Game = game;
		}

		public readonly static Type t_FNADroidPlatform = typeof(FNADroidPlatform);
		public readonly static Assembly a_FNA = typeof(Game).Assembly;
		public readonly static Type t_FNAPlatform = a_FNA.GetType("Microsoft.Xna.Framework.FNAPlatform");

		public static void Hook(string name)
		{
			FieldInfo field = t_FNAPlatform.GetField(name);
			// Store the original delegate into fna_name.
			t_FNADroidPlatform.GetField($"fna_{name}")?.SetValue(null, field.GetValue(null));
			// Replace the value with the new method.
			field.SetValue(null, Delegate.CreateDelegate(a_FNA.GetType($"Microsoft.Xna.Framework.FNAPlatform+{name}Func"), t_FNADroidPlatform.GetMethod(name)));
		}

		public static MulticastDelegate fna_ApplyWindowChanges;
		public static void ApplyWindowChanges(
			IntPtr window,
			int clientWidth,
			int clientHeight,
			bool wantsFullscreen,
			string screenDeviceName,
			ref string resultDeviceName
		)
		{
			object[] args = { window, clientWidth, clientHeight, wantsFullscreen, screenDeviceName, resultDeviceName };

			// Real display size.
			Android.Graphics.Point size = new Android.Graphics.Point();
			MainActivity.SDL2DCS_Instance.WindowManager.DefaultDisplay.GetRealSize(size);
			args[1] = size.X;
			args[2] = size.Y;
			GraphicsDeviceManager gdm = Game?.Services.GetService(typeof(IGraphicsDeviceManager)) as GraphicsDeviceManager;
			if (gdm != null)
			{
				gdm.PreferredBackBufferWidth = size.X;
				gdm.PreferredBackBufferHeight = size.Y;
			}

			fna_ApplyWindowChanges.DynamicInvoke(args);
			resultDeviceName = (string) args[5];
		}

		public static MulticastDelegate fna_RunLoop;
		public static void RunLoop(Game game)
		{
			Initialize(game);

			fna_RunLoop.DynamicInvoke(game);
		}

	}
}