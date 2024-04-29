using System;
using UnityEngine;

public static class ScreenshotFolderInfo
{
    public static string FOLDER_NAME = "Screenshots";
}
public static class ScoreScreenshot
{
    public static void CreateScreenshot()
    {
        #if !UNITY_EDITOR
            ScreenCapture.CaptureScreenshot(Application.dataPath + $"/../{ScreenshotFolderInfo.FOLDER_NAME}/Score{DateTime.Now:hh-mm}.png", 4); 
        #endif
    }
}
