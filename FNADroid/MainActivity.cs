using Android.App;
using Android.Widget;
using Android.OS;
using Org.Libsdl.App;
using Android.Views;
using Android.Content.Res;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;

namespace FNADroid
{
	[Activity(
		Label = "FNADroid",
		MainLauncher = true,
		Icon = "@drawable/icon",
		HardwareAccelerated = true,
		ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape
	)]
	public class MainActivity : SDLActivity
	{

		// TODO: UNHARDCODE!
		public const string Game = "Axiom Verge/AxiomVerge.exe";
		public string GamePath;

		public static MainActivity Instance { get; protected set; }

		public override void LoadLibraries()
		{
			base.LoadLibraries();
			Java.Lang.JavaSystem.LoadLibrary("fnadroid-ext");
			// Give the main library something to call in Mono-Land.
			SetMain(SDL_Main);
		}

		protected override void OnStart()
		{
			base.OnStart();
			Instance = this;
			ActionBar.Hide();

			// Load stub Steamworks.NET
			Steamworks.SteamAPI.Init();

			GamePath = null;
			foreach (Java.IO.File root in GetExternalFilesDirs(null))
			{
				string path = Path.Combine(root.AbsolutePath, Game);
				if (!File.Exists(path))
					continue;
				GamePath = path;
				break;
			}

			if (!string.IsNullOrEmpty(GamePath))
			{
				System.Environment.CurrentDirectory = Path.GetDirectoryName(GamePath);
			}
			System.Environment.SetEnvironmentVariable("FNADROID", "1");

			// Load our copy of FNA before the game gets a chance to run its copy.
			RuntimeHelpers.RunClassConstructor(typeof(Game).TypeHandle);
		}

		public override void OnWindowFocusChanged(bool hasFocus)
		{
			base.OnWindowFocusChanged(hasFocus);
			if (hasFocus)
			{
				Window.DecorView.SystemUiVisibility = (StatusBarVisibility) (
					SystemUiFlags.LayoutStable |
					SystemUiFlags.LayoutHideNavigation |
					SystemUiFlags.LayoutFullscreen |
					SystemUiFlags.HideNavigation |
					SystemUiFlags.Fullscreen |
					SystemUiFlags.ImmersiveSticky
				);
			}
		}

		public static void SDL_Main()
		{
			if (string.IsNullOrEmpty(Instance.GamePath))
			{
				AlertDialog dialog = null;
				Instance.RunOnUiThread(() =>
				{
					using (AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(Instance))
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append("Game not found: ").AppendLine(Game);
						foreach (Java.IO.File root in Instance.GetExternalFilesDirs(null))
						{
							stringBuilder.AppendLine();
							stringBuilder.AppendLine(Path.Combine(root.AbsolutePath, Game));
						}

						dialogBuilder.SetMessage(stringBuilder.ToString());
						dialogBuilder.SetCancelable(false);
						dialog = dialogBuilder.Show();
					}
				});

				while (dialog == null || dialog.IsShowing)
				{
					System.Threading.Thread.Sleep(0);
				}
				dialog.Dispose();
				return;
			}

			// Replace the following with whatever was in your Program.Main method.

			/*
			using (TestGame game = new TestGame())
			{
				game.Run();
			}
			*/
			Assembly.LoadFrom(Instance.GamePath).EntryPoint.Invoke(null, new object[] { new string[] { /*args*/ } });
		}

		[DllImport("main")]
		public static extern void SetMain(System.Action main);

	}
}
