/*
 * Copyright (c) Simverge Software LLC - All Rights Reserved
 */

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

 namespace pdal
 {
	public class DimTypeList : IDisposable
	{
		private const string PDALC_LIBRARY = "pdalc";

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetDimTypeListSize")]
		private static extern uint getSize(IntPtr list);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetDimType")]
		[return:MarshalAs( UnmanagedType.Struct)]
		private static extern DimType.NativeDimType getType(IntPtr list, uint index);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALDisposeDimTypeList")]
		private static extern void dispose(IntPtr list);

		private IntPtr mNative = IntPtr.Zero;
		private Dictionary<uint, DimType> mCache = new Dictionary<uint, DimType>();

		public DimTypeList(IntPtr nativeList)
		{
			mNative = nativeList;
		}

		public void Dispose()
		{
			dispose(mNative);
		}

		public DimType at(uint index)
		{
			DimType type = null;
			
			if (!mCache.TryGetValue(index, out type))
			{
				type = new DimType(getType(mNative, index));
				mCache.Add(index, type);
			}

			return type;
		}

		public uint Size
		{
			get { return getSize(mNative); }
		}
	}
 }
