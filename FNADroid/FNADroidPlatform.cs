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

		public static void Initialize()
		{
			RuntimeHelpers.RunClassConstructor(t_FNAPlatform.TypeHandle);

			// TODO Do we really not need to hook something?
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

	}
}