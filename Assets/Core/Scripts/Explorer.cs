﻿using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class Explorer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI pathLabel;
    
    public delegate void ContentsChangedCallback(Explorer caller);
    public event ContentsChangedCallback onDirectoriesChanged;
    public event ContentsChangedCallback onFilesChanged;
    public event ContentsChangedCallback onLogicalDrivesChanged;
    public event ContentsChangedCallback onSomethingChanged;

    private List<string> pathHistory = new List<string>();
    public string currentPath;
    public float refreshTime = 10;
    public string[] logicalDrives;
    public string[] directories;
    public string[] files;
    private float lastRefresh = float.MinValue;

    void Update()
    {
        if (Time.time - lastRefresh >= refreshTime)
            Refresh();

        pathLabel.text = currentPath;
    }

    public string[] GetLogicalDrives()
    {
        return (string[])logicalDrives?.Clone();
    }
    public string[] GetDirectories()
    {
        return (string[])directories?.Clone();
    }
    public string[] GetFiles()
    {
        return (string[])files?.Clone();
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

        bool logicalDrivesChanged = SetLogicalDrives(GrabPathsSafely((unnecessary) => { return Directory.GetLogicalDrives(); }, null));
        
        if (string.IsNullOrEmpty(currentPath) || !Directory.Exists(currentPath))
            GoBack();
        if (pathHistory.Count <= 0 || !PathEquals(pathHistory[pathHistory.Count - 1], currentPath))
            pathHistory.Add(currentPath);
        
        //These are kept outside the second if in case there have been changes in the directory
        bool directoriesChanged = SetDirectories(GrabPathsSafely(Directory.GetDirectories, currentPath));
        bool filesChanged = SetFiles(GrabPathsSafely(Directory.GetFiles, currentPath));

        if (logicalDrivesChanged || directoriesChanged || filesChanged)
            onSomethingChanged?.Invoke(this);
    }
    private static string[] GrabPathsSafely(Func<string, string[]> pathGetter, string path)
    {
        string[] grabbedPaths = new string[0];
        try
        {
            grabbedPaths = pathGetter(path);
        }
        catch (Exception e)
        {
            Debug.LogError("Caught exception: " + e);
        }
        return grabbedPaths;
    }

    private bool SetLogicalDrives(string[] newLogicalDrives)
    {
        bool foundChange = ComparePaths(logicalDrives, newLogicalDrives);
        if (foundChange)
        {
            logicalDrives = newLogicalDrives;
            onLogicalDrivesChanged?.Invoke(this);
        }
        return foundChange;
    }
    private bool SetDirectories(string[] newDirectories)
    {
        bool foundChange = ComparePaths(directories, newDirectories);
        if (foundChange)
        {
            directories = newDirectories;
            onDirectoriesChanged?.Invoke(this);
        }
        return foundChange;
    }
    private bool SetFiles(string[] newFiles)
    {
        bool foundChange = ComparePaths(files, newFiles);
        if (foundChange)
        {
            files = newFiles;
            onFilesChanged?.Invoke(this);
        }
        return foundChange;
    }

    public static bool ComparePaths(string[] first, string[] second)
    {
        bool foundChange = false;
        if (first != null && second != null && first.Length == second.Length)
        {
            for (int i = 0; i < first.Length; i++)
            {
                if (!PathEquals(first[i], second[i]))
                {
                    foundChange = true;
                    break;
                }
            }
        }
        else
            foundChange = true;
        
        return foundChange;
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
