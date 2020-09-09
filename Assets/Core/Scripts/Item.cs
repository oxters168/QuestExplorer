using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public bool isFile;
    public string itemName = "Item";

    public TextMeshProUGUI nameLabel;
    public GameObject fileObject;
    public GameObject directoryObect;

    void Update()
    {
        fileObject.SetActive(isFile);
        directoryObect.SetActive(!isFile);
        nameLabel.text = itemName;
    }
}
