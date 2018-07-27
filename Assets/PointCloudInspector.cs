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

        string path = "Assets/pdal/Examples/stats.json";
        string json = File.ReadAllText(path);
        Debug.Log("Pipeline JSON: " + json);

        pdal.Pipeline pipeline = new pdal.Pipeline(json);

        int level = pipeline.LogLevel;
        Debug.Log("Log Level: " + level);

        long count = pipeline.Execute();
        Debug.Log("Executed pipeline - point count: " + count);
        Debug.Log("Result JSON: " + pipeline.Json);

        pipeline.Dispose();
    }

}
