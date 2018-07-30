/*
 * Copyright (c) Simverge Software LLC - All Rights Reserved
 */

using System;
using System.Runtime.InteropServices;
using System.Text;

 namespace pdal
 {
	public class PointViewIterator : IDisposable
	{
		private const string PDALC_LIBRARY = "pdalc";

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALHasNextPointView")]
		private static extern bool hasNext(IntPtr itr);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetNextPointView")]
		private static extern IntPtr next(IntPtr itr);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALResetPointViewIterator")]
		private static extern void reset(IntPtr itr);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALDisposePointViewIterator")]
		private static extern void dispose(IntPtr itr);

		private IntPtr mNative = IntPtr.Zero;

		public PointViewIterator(IntPtr nativeIterator)
		{
			mNative = nativeIterator;
		}

		public void Dispose()
		{
			dispose(mNative);
			mNative = IntPtr.Zero;
		}

		public PointView Next
		{
			get
			{
				PointView view = null;

				IntPtr nativeView = next(mNative);

				if (nativeView != IntPtr.Zero)
				{
					view = new PointView(nativeView);
				}

				return view;
			}
		}

		public bool HasNext()
		{
			return hasNext(mNative);
		}

		public void Reset()
		{
			reset(mNative);
		}
	}
 }