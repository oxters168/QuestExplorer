using UnityEngine;
using TMPro;

public class FSItem : MonoBehaviour, IGridItem
{
    public FSData data;

    [Space(10)]
    public TextMeshProUGUI nameLabel;
    [Tooltip("0: File\n1: Folder\n2: Logical Drive")]
    public GameObject[] objectIcons;
    // public GameObject fileObject;
    // public GameObject directoryObect;

    private void Refresh()
    {
        for (int i = 0; i < objectIcons.Length; i++)
            objectIcons[i].SetActive(i == (int)data.fileType);

        if (data.fileType != FSData.FileType.LogicalDrive)
            nameLabel.text = data.GetName();
        else
            nameLabel.text = data.fullPath;
    }

    public void SetSize(float value)
    {

    }
    public void SetData(GridItemData data)
    {
        this.data = (FSData)data;
        Refresh();
    }
}
