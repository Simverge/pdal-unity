/*
 * Copyright (c) Simverge Software LLC - All Rights Reserved
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

 namespace pdal
 {
	public class PointLayout
	{
		private const string PDALC_LIBRARY = "pdalc";

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetPointLayoutDimTypes")]
		private static extern IntPtr getDimTypeList(IntPtr layout);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALFindDimType")]
		[return:MarshalAs(UnmanagedType.Struct)]
		private static extern DimType.NativeDimType findDimType(IntPtr layout, string name);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetDimSize")]
		private static extern uint getDimSize(IntPtr layout, string name);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetDimPackedOffset")]
		private static extern uint getDimPackedOffset(IntPtr layout, string name);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetPointSize")]
		private static extern uint getPointSize(IntPtr layout);

		private IntPtr mNative = IntPtr.Zero;
		private DimTypeList mTypes = null;
		private Dictionary<string, DimType> mCache = new Dictionary<string, DimType>();

		public PointLayout(IntPtr nativeLayout)
		{
			mNative = nativeLayout;
			IntPtr nativeTypes = (mNative != IntPtr.Zero) ? getDimTypeList(mNative) : IntPtr.Zero;

			if (nativeTypes != IntPtr.Zero)
			{
				mTypes = new DimTypeList(nativeTypes);
			}
		}

		public DimTypeList Types
		{
			get { return mTypes; }
		}

		public uint PointSize
		{
			get { return getPointSize(mNative); }
		}

		public DimType FindDimType(string name)
		{
			DimType type = null;
			
			if (!mCache.TryGetValue(name, out type))
			{
				type = new DimType(findDimType(mNative, name));
				mCache.Add(name, type);
			}

			return type;
		}

		public uint GetDimSize(string name)
		{
			return getDimSize(mNative, name);
		}

		public uint GetDimPackedOffset(string name)
		{
			return getDimPackedOffset(mNative, name);
		}
	}
 }