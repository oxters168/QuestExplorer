using UnityEngine;
using UnityHelpers;
// using UnityEngine.XR;
// using System.Collections.Generic;

public class CuffBoard : MonoBehaviour
{
    public CuffController leftCuff, rightCuff;
    [Tooltip("The length in seconds of the vibration that occurs during snapping")]
    /// <summary>
    /// The length in seconds of the vibration that occurs during snapping
    /// </summary>
    public float vibrationLength = 0.1f;
    [Range(0, 1), Tooltip("The strength of the vibration of the controller")]
    /// <summary>
    /// The strength of the vibration of the controller
    /// </summary>
    public float vibrationStrength = 0.5f;
    [Range(0, 1), Tooltip("How often the controller will vibrate within the length value given before")]
    /// <summary>
    /// How often the controller will vibrate within the length value given before
    /// </summary>
    public float vibrationFrequency = 0.01f;
    private Quaternion startRotL, startRotR;
    private Vector3 axisL, normalL, axisR, normalR;

    [Space(10), Tooltip("All the different glyphs this board supports")]
    public BoardGlyphCollection glyphs;

    public bool inputFieldFocused { get; private set; }
    #if (TextMeshPro)
    private TMPro.TMP_InputField inputField;
    #else
    private UnityEngine.UI.InputField inputField;
    #endif

    private Coroutine leftVibrator, rightVibrator;

    void OnEnable()
    {
        leftCuff.onSnap.AddListener(OnSnap);
        rightCuff.onSnap.AddListener(OnSnap);

        leftCuff.onClick.AddListener(OnClick);
        rightCuff.onClick.AddListener(OnClick);
    }
    void OnDisable()
    {
        leftCuff.onSnap.RemoveListener(OnSnap);
        rightCuff.onSnap.RemoveListener(OnSnap);

        leftCuff.onClick.RemoveListener(OnClick);
        rightCuff.onClick.RemoveListener(OnClick);
    }
    void Update()
    {
        GetCurrentInputField();

        leftCuff.gameObject.SetActive(inputFieldFocused);
        rightCuff.gameObject.SetActive(inputFieldFocused);

        ApplyInputToCuffs();
        // var inputDevices = new List<InputDevice>();
        // InputDevices.GetDevices(inputDevices);

        // foreach (var inputDevice in inputDevices)
        // {
        //     Vector3 position;
        //     inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out position);
        // }
    }

    private void OnClick(CuffController caller, string value, int clicks)
    {
        Debug.Log("Cuff Input: " + value);
        if (inputFieldFocused)
        {
            inputField.text += value;
        }
    }
    private void OnSnap(CuffController caller)
    {
        if (caller == leftCuff)
        {
            if (leftVibrator != null)
                StopCoroutine(leftVibrator);

            OculusInputBridge.SetControllerVibration(vibrationFrequency, vibrationStrength, OVRInput.Controller.LTouch);
            leftVibrator = StartCoroutine(CommonRoutines.WaitToDoAction((output) => { OculusInputBridge.SetControllerVibration(0, 0, OVRInput.Controller.LTouch); }, vibrationLength));
        }
        else if (caller == rightCuff)
        {
            if (rightVibrator != null)
                StopCoroutine(rightVibrator);

            OculusInputBridge.SetControllerVibration(vibrationFrequency, vibrationStrength, OVRInput.Controller.RTouch);
            rightVibrator = StartCoroutine(CommonRoutines.WaitToDoAction((output) => { OculusInputBridge.SetControllerVibration(0, 0, OVRInput.Controller.RTouch); }, vibrationLength));
        }
    }

    private void GetCurrentInputField()
    {
        #if (TextMeshPro)
        inputField = UnityEngine.EventSystems.EventSystem.current?.currentSelectedGameObject?.GetComponent<TMPro.TMP_InputField>();
        #else
        inputField = UnityEngine.EventSystems.EventSystem.current?.currentSelectedGameObject?.GetComponent<UnityEngine.UI.InputField>();
        #endif
        inputFieldFocused = inputField != null && inputField.isFocused;
    }
    private void ApplyInputToCuffs()
    {
        #region Set position
        leftCuff.transform.position = OculusInputBridge.ltouchPos;
        rightCuff.transform.position = OculusInputBridge.rtouchPos;
        #endregion

        #region Set clicking
        leftCuff.click = OculusInputBridge.triggerL;
        rightCuff.click = OculusInputBridge.triggerR;
        #endregion

        #region Setting cuff glyphs
        string[] currentLeft;
        string[] currentRight;
        if (OculusInputBridge.y && OculusInputBridge.x)
        {
            currentLeft = glyphs.altUpperLeft;
            currentRight = glyphs.altUpperRight;
        }
        else if (OculusInputBridge.x)
        {
            currentLeft = glyphs.altLeft;
            currentRight = glyphs.altRight;
        }
        else if (OculusInputBridge.y)
        {
            currentLeft = glyphs.upperLeft;
            currentRight = glyphs.upperRight;
        }
        else
        {
            currentLeft = glyphs.lowerLeft;
            currentRight = glyphs.lowerRight;
        }
        leftCuff.glyphs = currentLeft;
        rightCuff.glyphs = currentRight;
        #endregion

        #region Rotation of cuffs
        if (OculusInputBridge.gripL)
        {
            if (OculusInputBridge.gripLDown)
            {
                startRotL = OculusInputBridge.ltouchRot;
                axisL = startRotL * Vector3.forward;
                normalL = startRotL * Vector3.up;
            }

            float angle = (startRotL * Quaternion.Inverse(OculusInputBridge.ltouchRot)).Shorten().PollAxisSignedAngle(axisL, normalL);
            angle = Mathf.Clamp(angle * Mathf.Rad2Deg, -90, 90);
            leftCuff.spin = angle / 90;
        }
        else
        {
            leftCuff.transform.rotation = OculusInputBridge.ltouchRot;
        }

        if (OculusInputBridge.gripR)
        {
            if (OculusInputBridge.gripRDown)
            {
                startRotR = OculusInputBridge.rtouchRot;
                axisR = startRotR * Vector3.forward;
                normalR = startRotR * Vector3.up;
            }

            float angle = (startRotR * Quaternion.Inverse(OculusInputBridge.rtouchRot)).Shorten().PollAxisSignedAngle(axisR, normalR);
            angle = Mathf.Clamp(angle * Mathf.Rad2Deg, -90, 90);
            rightCuff.spin = angle / 90;
        }
        else
        {
            rightCuff.transform.rotation = OculusInputBridge.rtouchRot;
        }
        #endregion
    }
}
