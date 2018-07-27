/*
 * Copyright (c) Simverge Software LLC - All Rights Reserved
 */

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace pdal
 {
	public class Pipeline : IDisposable
	{
		private const string PDALC_LIBRARY = "pdalc";
		private const int BUFFER_SIZE = 1024;

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALCreatePipeline")]
		private static extern IntPtr create(string json);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALDisposePipeline")]
		private static extern void dispose(IntPtr pipeline);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetPipelineAsString")]
		private static extern int asString(IntPtr pipeline, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetPipelineMetadata")]
		private static extern int getMetadata(IntPtr pipeline, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetPipelineSchema")]
		private static extern int getSchema(IntPtr pipeline, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetPipelineLog")]
		private static extern int getLog(IntPtr pipeline, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALSetPipelineLogLevel")]
		private static extern void setLogLevel(IntPtr pipeline, int level);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetPipelineLogLevel")]
		private static extern int getLogLevel(IntPtr pipeline);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALExecutePipeline")]
		private static extern long execute(IntPtr pipeline);

		[DllImport(PDALC_LIBRARY, EntryPoint="PDALValidatePipeline")]
		private static extern bool validate(IntPtr pipeline);

		//[DllImport(PDALC_LIBRARY, EntryPoint="PDALGetPointViews")]
		//private static extern PDALPointViewIteratorPtr PDALGetPointViews(IntPtr pipeline);

		/// The native C API PDAL pipeline pointer
		private IntPtr mNative = IntPtr.Zero;

		/**
		 * Creates an uninitialized and unexecuted pipeline.
		 * 
		 * @note Set the Json property to initialize the pipeline.
		 *       Once initialized, call the Execute method to execute the pipeline.
		 */
		public Pipeline()
		{
			// Do nothing
		}

		/**
		 * Creates a pipeline initialized with the provided JSON string.
		 
		 * @note Call the Execute method to execute the pipeline.
		 *
		 * @param json The JSON pipeline string
		 */
		public Pipeline(string json)
		{
			mNative = create(json);
		}

		/// Disposes the native PDAL pipeline object.
		public void Dispose()
		{
			dispose(mNative);
		}

		/// The JSON representation of the PDAL pipeline
		public string Json
		{
			get
			{
				StringBuilder buffer = new StringBuilder(BUFFER_SIZE);
				asString(mNative, buffer, buffer.Capacity);
				return buffer.ToString();
			}

			set
			{
				dispose(mNative);
				mNative = create(value);
			}
		}

		/// The pipeline's post-execution metadata
		public string Metadata
		{
			get
			{
				StringBuilder buffer = new StringBuilder(BUFFER_SIZE);
				getMetadata(mNative, buffer, buffer.Capacity);
				return buffer.ToString();
			}
		}

		/// The pipeline's post-execution schema
		public string Schema
		{
			get
			{
				StringBuilder buffer = new StringBuilder(BUFFER_SIZE);
				getSchema(mNative, buffer, buffer.Capacity);
				return buffer.ToString();
			}
		}

		/// The pipeline's execution log
		public string Log
		{
			get
			{
				StringBuilder buffer = new StringBuilder(BUFFER_SIZE);
				getLog(mNative, buffer, buffer.Capacity);
				return buffer.ToString();
			}
		}

		/// The pipeline's log level
		public int LogLevel
		{
			get { return getLogLevel(mNative); }
			set { setLogLevel(mNative, value); }
		}

		/// The pipeline's validation state
		public bool Valid
		{
			get { return validate(mNative); }
		}

		/**
		 * Executes the pipeline.
		 * 
		 * @return The total number of points produced by the pipeline
		 */
		public long Execute()
		{
			return execute(mNative);
		}
	}
 }