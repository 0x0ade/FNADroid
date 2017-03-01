using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FNADroid
{
	/// <summary>
	/// External helper functions.
	/// </summary>
	public static class FNADroidExtern
	{

		private const string nativeLibName = "fnadroid-ext";

		[DllImport(nativeLibName)]
		public static extern IntPtr GetJavaVM();

		[DllImport(nativeLibName)]
		public static extern IntPtr GetJNIEnv();

		[DllImport(nativeLibName)]
		public static extern bool AttachThreadToJavaVM();

		[DllImport(nativeLibName)]
		public static extern bool DetachThreadToJavaVM();

	}
}
