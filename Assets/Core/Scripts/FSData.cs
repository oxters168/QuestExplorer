using System.IO;

[System.Serializable]
public class FSData : GridItemData
{
    public enum FileType { File, Folder, LogicalDrive, }
    public FileType fileType;
    // public bool isFile;
    public string fullPath = "Item";
    public Explorer refExp;

    public FSData(string path, FileType fileType, Explorer referenceExplorer)
    {
        fullPath = path;
        this.fileType = fileType;
        // this.isFile = isFile;
        refExp = referenceExplorer;
    }
    public string GetName()
    {
        return Path.GetFileName(fullPath);
    }
}
