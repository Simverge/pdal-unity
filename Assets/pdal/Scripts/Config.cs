using System;
using System.Runtime.InteropServices;
using System.Text;

namespace pdal
{
	public class Config
	{
		private const string PDALC_LIBRARY = "pdalc";

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALFullVersionString")]
		private static extern void GetFullVersion([MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALVersionString")]
		private static extern void GetVersion([MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALVersionInteger")]
		private static extern int GetVersionInteger();

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALSha1")]
		private static extern void GetSha1([MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALVersionMajor")]
		private static extern int GetVersionMajor();

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALVersionMinor")]
		private static extern int GetVersionMinor();

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALVersionPatch")]
		private static extern int GetVersionPatch();

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALDebugInformation")]
		private static extern void GetDebugInfo([MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);

		public string FullVersion
		{
			get
            {
				StringBuilder buffer = new StringBuilder(64);
				GetFullVersion(buffer, buffer.Capacity);
				return buffer.ToString();
			}
		}

		public string Version
		{
			get
			{
				StringBuilder buffer = new StringBuilder(64);
				GetVersion(buffer, buffer.Capacity);
				return buffer.ToString();
			}
		}

		public int VersionInteger
		{
			get { return GetVersionInteger(); }
		}

		public string Sha1
		{
			get
			{
				StringBuilder buffer = new StringBuilder(64);
				GetSha1(buffer, buffer.Capacity);
				return buffer.ToString();
			}
		}

		public int VersionMajor
		{
			get { return GetVersionMajor(); }
		}

		public int VersionMinor
		{
			get { return GetVersionMinor(); }
		}

		public int VersionPatch
		{
			get { return GetVersionPatch(); }
		}

		public string DebugInfo
		{
			get
			{
				StringBuilder buffer = new StringBuilder(1024);
				GetDebugInfo(buffer, buffer.Capacity);
				return buffer.ToString();
			}
		}
	}
}
