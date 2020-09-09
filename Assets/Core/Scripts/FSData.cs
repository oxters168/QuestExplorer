using System.IO;

[System.Serializable]
public class FSData : GridItemData
{
    public bool isFile;
    public string fullPath = "Item";

    public string GetName()
    {
        return Path.GetFileName(fullPath);
    }
}
