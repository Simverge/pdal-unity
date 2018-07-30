using System;
using System.IO;
using UnityEngine;

public class PointCloudInspector : MonoBehaviour
{
	private void Start()
	{
        pdal.Config config = new pdal.Config();
        Debug.Log("GDAL Data Path: " + config.GdalData);
        Debug.Log("Proj4 Data Path: " + config.Proj4Data);

        Debug.Log("PDAL Version Integer: " + config.VersionInteger);
        Debug.Log("PDAL Version Major: " + config.VersionMajor);
        Debug.Log("PDAL Version Minor: " + config.VersionMinor);
        Debug.Log("PDAL Version Patch: " + config.VersionPatch);

        Debug.Log("PDAL Full Version: " + config.FullVersion);
        Debug.Log("PDAL Version: " + config.Version);
        Debug.Log("PDAL SHA1: " + config.Sha1);
        Debug.Log("PDAL Debug Info: " + config.DebugInfo);

        string path = "Assets/pdal/Examples/sort.json";
        string json = File.ReadAllText(path);

        pdal.Pipeline pipeline = new pdal.Pipeline(json);

        long pointCount = pipeline.Execute();
        Debug.Log("Executed pipeline - point count: " + pointCount);
        Debug.Log("Log Level: " + pipeline.LogLevel);
        Debug.Log("Pipeline JSON: " + json);
        Debug.Log("Result JSON: " + pipeline.Json);

        pdal.PointViewIterator views = pipeline.Views;
        pdal.PointView view = views.Next;

        while (view != null)
        {
            Debug.Log("View " + view.Id);
            Debug.Log("\tproj4: " + view.Proj4);
            Debug.Log("\tWKT: " + view.Wkt);
            Debug.Log("\tSize: " + view.Size);
            Debug.Log("\tEmpty? " + view.Empty);

            pdal.PointLayout layout = view.Layout;
            Debug.Log("\tHas layout? " + (layout != null));

    //        if (layout != null)
      //      {

        //    }

            view.Dispose();
            view = views.Next;
        }

        views.Dispose();
        pipeline.Dispose();
    }

}
