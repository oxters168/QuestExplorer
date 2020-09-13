using System.IO;

[System.Serializable]
public class FSData : GridItemData
{
    public bool isFile;
    public string fullPath = "Item";
    public Explorer refExp;

    public FSData(string path, bool isFile, Explorer referenceExplorer)
    {
        fullPath = path;
        this.isFile = isFile;
        refExp = referenceExplorer;
    }
    public string GetName()
    {
        return Path.GetFileName(fullPath);
    }
}
