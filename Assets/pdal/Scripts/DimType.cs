/*
 * Copyright (c) Simverge Software LLC - All Rights Reserved
 */

using System;
using System.Runtime.InteropServices;
using System.Text;

 namespace pdal
 {
	public class DimType
	{
		private const string PDALC_LIBRARY = "pdalc";
		private const int BUFFER_SIZE = 1024;

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct NativeDimType
		{
			public UInt32 id;
			public UInt32 interpretation;
			public double scale;
			public double offset;
		};

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetInvalidDimType")]
		[return:MarshalAs(UnmanagedType.Struct)]
		private static extern NativeDimType getInvalidDimType();

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetDimTypeIdName")]
		private static extern int getDimTypeIdName([MarshalAs(UnmanagedType.Struct)] NativeDimType type, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, uint size);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetDimTypeInterpretationName")]
		private static extern int getDimTypeInterpretationName([MarshalAs(UnmanagedType.Struct)] NativeDimType type, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, uint size);

		private NativeDimType mType = getInvalidDimType();

		public DimType()
		{
			// Do nothing
		}

		public DimType(NativeDimType nativeType)
		{
			mType = nativeType;
		}

		public DimType(uint id, uint interpretation, double scale = 1.0, double offset = 0.0)
		{
			mType.id = id;
			mType.interpretation = interpretation;
			mType.scale = scale;
			mType.offset = offset;
		}

		public uint Id
		{
			get { return mType.id; }
			set { mType.id = value; }
		}

		public string IdName
		{
			get
			{
				StringBuilder buffer = new StringBuilder(256);
				getDimTypeIdName(mType, buffer, (uint) buffer.Capacity);
				return buffer.ToString();
			}
		}

		public uint Interpretation
		{
			get { return mType.interpretation; }
			set { mType.interpretation = value; }
		}

		public string InterpretationName
		{
			get
			{
				StringBuilder buffer = new StringBuilder(256);
				getDimTypeInterpretationName(mType, buffer, (uint) buffer.Capacity);
				return buffer.ToString();
			}
		}

        public double Scale
        {
            get { return mType.scale; }
        }

        public double Offset
        {
            get { return mType.offset; }
        }
    }
 }