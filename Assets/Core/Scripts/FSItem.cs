using UnityEngine;
using TMPro;

public class FSItem : MonoBehaviour, IGridItem
{
    public FSData data;

    [Space(10)]
    public TextMeshProUGUI nameLabel;
    public GameObject fileObject;
    public GameObject directoryObect;

    void Update()
    {
        fileObject.SetActive(data.isFile);
        directoryObect.SetActive(!data.isFile);
        nameLabel.text = data.GetName();
    }

    public void SetSize(float value)
    {

    }
    public void SetData(GridItemData data)
    {
        this.data = (FSData)data;
    }
}
