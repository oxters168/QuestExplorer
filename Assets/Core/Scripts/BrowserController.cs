using UnityEngine;

public class BrowserController : MonoBehaviour
{
    public Explorer explorer;
    public Grid mainGrid;
    public Grid sideGrid;

    void Start()
    {
        explorer.onSomethingChanged += OnExplorerChanged;
    }

    private void OnExplorerChanged(Explorer caller)
    {
        Debug.Log("Browser changed");
        var ld = caller.GetLogicalDrives();
        var dirs = caller.GetDirectories();
        var files = caller.GetFiles();
        int ldCount = ld != null ? ld.Length : 0;
        int dirsCount = dirs != null ? dirs.Length : 0;
        int filesCount = files != null ? files.Length : 0;
        FSData[] mainData = new FSData[dirsCount + filesCount];
        FSData[] sideData = new FSData[ldCount];
        for (int i = 0; i < ldCount; i++)
        {
            var currentLD = new FSData(ld[i], FSData.FileType.LogicalDrive, explorer);
            sideData[i] = currentLD;
        }
        for (int i = 0; i < dirsCount; i++)
        {
            var currentDir = new FSData(dirs[i], FSData.FileType.Folder, explorer);
            mainData[i] = currentDir;
        }
        for (int i = 0; i < filesCount; i++)
        {
            var currentFile = new FSData(files[i], FSData.FileType.File, explorer);
            mainData[i + dirsCount] = currentFile;
        }

        mainGrid.SetData(mainData);
        sideGrid.SetData(sideData);
    }
}
