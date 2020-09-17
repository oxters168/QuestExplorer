using UnityEngine;

[System.Serializable]
public class ClickEvent : UnityEngine.Events.UnityEvent<CuffController, int> {}

public class CuffController : MonoBehaviour
{
    [Tooltip("The part of the object whose spin represents the input")]
    /// <summary>
    /// The part of the object whose spin represents the input
    /// </summary>
    public Transform innerModule;

    [Space(10), Tooltip("When set to true, fires events based on settings")]
    /// <summary>
    /// When set to true, fires events based on settings
    /// </summary>
    public bool click;
    [Range(-1, 1), Tooltip("A value between -1 and 1 that represents the inner modules angle along the z-axis")]
    /// <summary>
    /// A value between -1 and 1 that represents the inner modules angle along the z-axis
    /// </summary>
    public float spin;

    [Space(10), Tooltip("The minimum angle in degrees the inner module can be on the z-axis")]
    /// <summary>
    /// The minimum angle in degrees the inner module can be on the z-axis
    /// </summary>
    public float minAngle = -180;
    [Tooltip("The maximum angle in degrees the inner module can be on the z-axis")]
    /// <summary>
    /// The maximum angle in degrees the inner module can be on the z-axis
    /// </summary>
    public float maxAngle = 180;
    [Space(10), Tooltip("The time in seconds before click is considered a long press")]
    /// <summary>
    /// The time in seconds before click is considered a long press
    /// </summary>
    public float longPressTime = 0.7f;
    [Tooltip("The frequency with which the events fire when click is held after long press has occured")]
    /// <summary>
    /// The frequency with which the events fire when click is held after long press has occured
    /// </summary>
    public float spamTime = 0.1f;

    [Space(10)]
    public ClickEvent onClick;
    public ClickEvent onRelease;

    private float lastClickTime = -1;
    private int spamCount = 0;
    private int clicksSent = 0;

    void Update()
    {
        spin = Mathf.Clamp(spin, -1, 1);

        innerModule.localRotation = Quaternion.AngleAxis((maxAngle - minAngle) * ((spin + 1) / 2) + minAngle, Vector3.forward);

        if (click && lastClickTime < 0)
        {
            //Fire onClick
            lastClickTime = Time.time;
            clicksSent = 1;
            onClick?.Invoke(this, clicksSent);
        }
        else if (click && Time.time - lastClickTime >= longPressTime)
        {
            //Fire onLongPress
            int expectedSpam = Mathf.FloorToInt(((Time.time - lastClickTime) - longPressTime) / spamTime);
            if (expectedSpam > spamCount)
            {
                spamCount = expectedSpam;
                clicksSent++;
                onClick?.Invoke(this, clicksSent);
            }
        }
        else if (!click && lastClickTime >= 0)
        {
            //Fire onUp
            lastClickTime = -1;
            spamCount = 0;
            onRelease?.Invoke(this, clicksSent);
        }
    }
}
