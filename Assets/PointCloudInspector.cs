using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCloudInspector : MonoBehaviour
{
	private void Start()
	{
        string cwd = Environment.CurrentDirectory;

        Environment.SetEnvironmentVariable("GDAL_DATA", cwd + "/Assets/gdal/Data");
        Environment.SetEnvironmentVariable("PROJ_LIB", cwd + "/Assets/proj4/Data");

        pdal.Config config = new pdal.Config();
        Debug.Log("PDAL Version Integer: " + config.VersionInteger);
        Debug.Log("PDAL Version Major: " + config.VersionMajor);
        Debug.Log("PDAL Version Minor: " + config.VersionMinor);
        Debug.Log("PDAL Version Patch: " + config.VersionPatch);

        Debug.Log("PDAL Full Version: " + config.FullVersion);
        Debug.Log("PDAL Version: " + config.Version);
        Debug.Log("PDAL SHA1: " + config.Sha1);
        Debug.Log("PDAL Debug Info: " + config.DebugInfo);
    }

}
