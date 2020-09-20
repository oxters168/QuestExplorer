using UnityEngine;
using UnityHelpers;

[System.Serializable]
public class ClickEvent : UnityEngine.Events.UnityEvent<CuffController, string, int> {}
[System.Serializable]
public class SnapEvent : UnityEngine.Events.UnityEvent<CuffController> {}

public class CuffController : MonoBehaviour
{
    [Tooltip("The part of the object whose spin represents the input")]
    /// <summary>
    /// The part of the object whose spin represents the input
    /// </summary>
    public Transform innerModule;
    [Tooltip("The part of the object that covers the inner module")]
    /// <summary>
    /// The part of the object that covers the inner module
    /// </summary>
    public Transform coverModule;
    [Tooltip("The parent of all the UI")]
    /// <summary>
    /// The parent of all the UI
    /// </summary>
    public Transform glyphCanvas;
    [Tooltip("The prefab that will be cloned to display the different symbols on the cuff")]
    /// <summary>
    /// The prefab that will be cloned to display the different symbols on the cuff
    /// </summary>
    public Glyph glyphPrefab;

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
    [Tooltip("Inverts the spin")]
    /// <summary>
    /// Inverts the spin
    /// </summary>
    public bool inverted = true;

    [Space(10), Tooltip("The percent distance from the spin value where it will snap to the value")]
    /// <summary>
    /// The percent distance from the spin value where it will snap to the value
    /// </summary>
    public float snapPercent = 0.1f;
    [Tooltip("The minimum angle in degrees the inner module can be on the z-axis")]
    /// <summary>
    /// The minimum angle in degrees the inner module can be on the z-axis
    /// </summary>
    public float minAngle = -90;
    [Tooltip("The maximum angle in degrees the inner module can be on the z-axis")]
    /// <summary>
    /// The maximum angle in degrees the inner module can be on the z-axis
    /// </summary>
    public float maxAngle = 90;
    [Tooltip("The angle to offset on the x-axis after the spin angle is applied on the z-axis")]
    /// <summary>
    /// The angle to offset on the x-axis after the spin angle is applied on the z-axis
    /// </summary>
    public float horizontalOffsetAngle = 45;
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

    [Space(10), Tooltip("The distance from the center the glyphs will be placed")]
    /// <summary>
    /// The distance from the center the glyphs will be placed
    /// </summary>
    public float cuffUIRadius = 612;
    [Tooltip("The symbols that will appear on the cuff within the min and max angle given")]
    /// <summary>
    /// The symbols that will appear on the cuff within the min and max angle given
    /// </summary>
    public string[] glyphs;
    private string[] shownGlyphs;

    private ObjectPool<Glyph> _glyphPool;
    private ObjectPool<Glyph> GlyphPool { get { if (_glyphPool == null) _glyphPool = new ObjectPool<Glyph>(glyphPrefab, 5, false, true, glyphCanvas); return _glyphPool; } }
    

    [Space(10)]
    public ClickEvent onClick;
    public ClickEvent onRelease;
    public SnapEvent onSnap;

    private float lastClickTime = -1;
    private int spamCount = 0;
    private int clicksSent = 0;
    private int prevIndex;
    private bool snapEnd;

    void Update()
    {
        if (glyphs != null && (shownGlyphs == null || !GlyphsAreEqual(glyphs, shownGlyphs)))
        {
            shownGlyphs = (string[])glyphs.Clone();
            //Refresh
            float totalAngle = maxAngle - minAngle;
            float offsetAngle = totalAngle / (shownGlyphs.Length - 1);
            GlyphPool.ReturnAll();
            for (int i = 0; i < shownGlyphs.Length; i++)
            {
                var glyph = GlyphPool.Get();
                float currentAngle = offsetAngle * i;
                Vector2 position = Vector2.up.Rotate(currentAngle + minAngle) * cuffUIRadius;
                glyph.transform.localPosition = position;
                glyph.transform.localRotation = Quaternion.Euler(0, 0, (totalAngle - currentAngle) + minAngle);
                glyph.transform.localScale = Vector3.one;
                glyph.value = shownGlyphs[i];
            }
        }

        HandleInput();
    }

    private static bool GlyphsAreEqual(string[] first, string[] second)
    {
        if (first.Length != second.Length)
            return false;

        for (int i = 0; i < first.Length; i++)
            if (!first[i].Equals(second[i]))
                return false;

        return true;
    }
    private void HandleInput()
    {
        spin = Mathf.Clamp(spin, -1, 1);
        var actualSpin = inverted ? -spin : spin;
        #region Snap to value
        if (snapPercent > 0 && shownGlyphs != null)
        {
            int glyphCount = shownGlyphs.Length;
            float offsetAmount = 2f / (glyphCount - 1);
            float percentDiff = snapPercent * offsetAmount;
            float valueIndex = (actualSpin + 1) / offsetAmount;
            int roundedIndex = Mathf.RoundToInt(valueIndex);
            float decimalPortion = Mathf.Abs(valueIndex - roundedIndex);
            if (decimalPortion <= percentDiff)
            {
                actualSpin = offsetAmount * roundedIndex + (-1);

                if (prevIndex != roundedIndex)
                {
                    //OculusInputBridge.SetControllerVibration(1, 1);
                    onSnap?.Invoke(this);
                    // snapEnd = false;
                }
                // else if (!snapEnd)
                // {
                //     //OculusInputBridge.SetControllerVibration(0, 0);
                //     onEndSnap?.Invoke(this);
                //     snapEnd = true;
                // }
                
                prevIndex = roundedIndex;
            }
        }
        #endregion

        innerModule.localRotation = Quaternion.Euler(horizontalOffsetAngle, 0, (maxAngle - minAngle) * ((actualSpin + 1) / 2) + minAngle);
        coverModule.localRotation = Quaternion.AngleAxis(horizontalOffsetAngle, Vector3.right);

        if (click && lastClickTime < 0)
        {
            //Fire onClick
            lastClickTime = Time.time;
            clicksSent = 1;
            onClick?.Invoke(this, shownGlyphs[prevIndex], clicksSent);
        }
        else if (click && Time.time - lastClickTime >= longPressTime)
        {
            //Fire onLongPress
            int expectedSpam = Mathf.FloorToInt(((Time.time - lastClickTime) - longPressTime) / spamTime);
            if (expectedSpam > spamCount)
            {
                spamCount = expectedSpam;
                clicksSent++;
                onClick?.Invoke(this, shownGlyphs[prevIndex], clicksSent);
            }
        }
        else if (!click && lastClickTime >= 0)
        {
            //Fire onUp
            lastClickTime = -1;
            spamCount = 0;
            onRelease?.Invoke(this, shownGlyphs[prevIndex], clicksSent);
        }
    }
}
