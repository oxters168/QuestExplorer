using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class Explorer : MonoBehaviour
{
    private List<string> pathHistory = new List<string>();
    public string currentPath { get; private set; }
    public float refreshTime = 10;
    private string[] logicalDrives;
    private string[] directories;
    private string[] files;
    private float lastRefresh = float.MinValue;

    void Update()
    {
        if (Time.time - lastRefresh >= refreshTime)
            Refresh();
    }

    public string[] GetLogicalDrives()
    {
        return (string[])logicalDrives.Clone();
    }
    public string[] GetDirectories()
    {
        return (string[])directories.Clone();
    }
    public string[] GetFiles()
    {
        return (string[])files.Clone();
    }
    
    public void Goto(string path)
    {
        currentPath = path;
        Refresh();
    }
    public void GoBack()
    {
        if (pathHistory.Count > 0)
        {
            if (pathHistory.Count > 1 && PathEquals(pathHistory[pathHistory.Count - 1], currentPath))
            {
                currentPath = pathHistory[pathHistory.Count - 2];
                pathHistory.RemoveAt(pathHistory.Count - 1);
            }
            else
            {
                currentPath = pathHistory[pathHistory.Count - 1];
            }
        }
        else
            ResetPath();

        Refresh();
    }
    public void GoUp()
    {
        string upPath = Directory.GetParent(currentPath).FullName;
        if (Directory.Exists(upPath))
        {
            currentPath = upPath;
            Refresh();
        }
    }
    private void Refresh()
    {
        lastRefresh = Time.time;

        logicalDrives = Directory.GetLogicalDrives();

        if (string.IsNullOrEmpty(currentPath) || !Directory.Exists(currentPath))
            GoBack();
        if (pathHistory.Count <= 0 || !PathEquals(pathHistory[pathHistory.Count - 1], currentPath))
            pathHistory.Add(currentPath);

        //These are kept outside the second if in case there have been changes in the directory
        directories = Directory.GetDirectories(currentPath);
        files = Directory.GetFiles(currentPath);
    }
    private void ResetPath()
    {
        currentPath = Application.persistentDataPath;
    }

    public static bool PathEquals(string path1, string path2)
    {
        return Path.GetFullPath(path1).TrimEnd('\\').TrimEnd('/').Equals(
        Path.GetFullPath(path2).TrimEnd('\\').TrimEnd('/'), 
        StringComparison.InvariantCultureIgnoreCase);
    }
}
