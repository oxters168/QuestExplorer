using UnityEngine;
using UnityHelpers;

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
            var currentLD = new FSData(ld[i], false, explorer);
            allData[i] = currentLD;
        }
        for (int i = 0; i < dirsCount; i++)
        {
            var currentDir = new FSData(dirs[i], false, explorer);
            allData[i + ldCount] = currentDir;
        }
        for (int i = 0; i < filesCount; i++)
        {
            var currentFile = new FSData(files[i], true, explorer);
            allData[i + ldCount + dirsCount] = currentFile;
        }

        var items = grid.SetData(allData);
        for (int i = 0; i < items.Length; i++)
        {
            var pt = items[i].GetComponent<PhysicsTransform>();
            if (pt != null)
            {
                var lp = items[i].localPosition;
                var lr = items[i].localRotation;
                pt.parent = items[i].parent;
                pt.localPosition = lp;
                pt.localRotation = lr;
                items[i].SetParent(null);
            }
        }
    }
}
