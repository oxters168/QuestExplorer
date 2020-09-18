using UnityEngine;

[ExecuteAlways]
public class Glyph : MonoBehaviour
{
    public TMPro.TextMeshProUGUI label;
    public string value;

    void Update()
    {
        if (label != null)
            label.text = value;
    }
}
