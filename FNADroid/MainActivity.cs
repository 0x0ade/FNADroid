using Android.App;
using Android.Widget;
using Android.OS;
using Org.Libsdl.App;
using Android.Views;
using Android.Content.Res;

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

		public static MainActivity SDL2DCS_Instance { get; protected set; }

		public override void LoadLibraries()
		{
			base.LoadLibraries();
			Java.Lang.JavaSystem.LoadLibrary("fnadroid-ext");
			// Give the main library something to call in Mono-Land.
			Bootstrap.SetMain(Bootstrap.SDL_Main);
		}

		protected override void OnStart()
		{
			base.OnStart();
			SDL2DCS_Instance = this;
			ActionBar.Hide();
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

	}
}
