using UnityEngine;

public class QuickTest : MonoBehaviour
{
    public TMPro.TMP_InputField browserBar;
    public Explorer explorer;

    public void Go()
    {
        explorer.Goto(browserBar.text);
    }
}
