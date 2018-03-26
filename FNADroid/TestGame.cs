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
	class TestGame : Game
	{

		public readonly GraphicsDeviceManager GraphicsDeviceManager;

		public TestGame()
		{
			GraphicsDeviceManager = new GraphicsDeviceManager(this);
		}

		protected override void Initialize()
		{
			Window.Title = "XnaToFna ContentHelper Game (ignore me!)";
			base.Initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White * (0.5f + 0.5f * (float) Math.Sin(gameTime.TotalGameTime.TotalSeconds)));

			base.Draw(gameTime);
		}

	}
}
