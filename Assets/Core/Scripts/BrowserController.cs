using UnityEngine;

public class BrowserController : MonoBehaviour
{
    public Explorer explorer;
    public Grid grid;

    void Start()
    {
        explorer.onDirectoriesChanged += OnExplorerChanged;
        explorer.onFilesChanged += OnExplorerChanged;
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
        FSData[] allData = new FSData[ldCount + dirsCount + filesCount];
        for (int i = 0; i < ldCount; i++)
        {
            var currentLD = new FSData() { fullPath = ld[i], isFile = false };
            allData[i] = currentLD;
        }
        for (int i = 0; i < dirsCount; i++)
        {
            var currentDir = new FSData() { fullPath = dirs[i], isFile = false };
            allData[i + ldCount] = currentDir;
        }
        for (int i = 0; i < filesCount; i++)
        {
            var currentFile = new FSData() { fullPath = files[i], isFile = true };
            allData[i + ldCount + dirsCount] = currentFile;
        }

        grid.SetData(allData);
    }
}
