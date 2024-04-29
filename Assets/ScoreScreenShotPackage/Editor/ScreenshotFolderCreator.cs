using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;


public class ScreenshotFolderCreator : IPostprocessBuildWithReport
{
    public int callbackOrder { get; }
    public void OnPostprocessBuild(BuildReport report)
    {
        Directory.CreateDirectory(report.summary.outputPath + $"/../{ScreenshotFolderInfo.FOLDER_NAME}");
        Debug.Log(report.summary.outputPath + $"/../{ScreenshotFolderInfo.FOLDER_NAME}");
    }
}
