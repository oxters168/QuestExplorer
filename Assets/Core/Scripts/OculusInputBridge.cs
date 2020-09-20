using UnityEngine;
using UnityHelpers;

public class OculusInputBridge : MonoBehaviour
{
    public Transform trackingSpace;

    [Space(10)]
    public float joystickThreshold = 0.01f;
    public float gripThreshold = 0.01f;
    public float triggerThreshold = 0.01f;
    
    public static Vector3 ltouchPos { get; private set; }
    public static Quaternion ltouchRot { get; private set; }
    public static Vector3 rtouchPos { get; private set; }
    public static Quaternion rtouchRot { get; private set; }
    public static Vector2 joystickL { get; private set; }
    public static Vector2 joystickR { get; private set; }
    public static float gripAxisL { get; private set; }
    public static float gripAxisR { get; private set; }
    public static float triggerAxisL { get; private set; }
    public static float triggerAxisR { get; private set; }
    public static bool a { get; private set; }
    public static bool b { get; private set; }

    public static bool joystickL_PH_Up { get { return _joystickL_PH_Up; } }
    public static bool joystickL_PH_Down { get { return _joystickL_PH_Down; } }
    public static bool joystickL_NH_Up { get { return _joystickL_NH_Up; } }
    public static bool joystickL_NH_Down { get { return _joystickL_NH_Down; } }
    public static bool joystickL_PV_Up { get { return _joystickL_PV_Up; } }
    public static bool joystickL_PV_Down { get { return _joystickL_PV_Down; } }
    public static bool joystickL_NV_Up { get { return _joystickL_NV_Up; } }
    public static bool joystickL_NV_Down { get { return _joystickL_NV_Down; } }
    public static bool joystickR_PH_Up { get { return _joystickR_PH_Up; } }
    public static bool joystickR_PH_Down { get { return _joystickR_PH_Down; } }
    public static bool joystickR_NH_Up { get { return _joystickR_NH_Up; } }
    public static bool joystickR_NH_Down { get { return _joystickR_NH_Down; } }
    public static bool joystickR_PV_Up { get { return _joystickR_PV_Up; } }
    public static bool joystickR_PV_Down { get { return _joystickR_PV_Down; } }
    public static bool joystickR_NV_Up { get { return _joystickR_NV_Up; } }
    public static bool joystickR_NV_Down { get { return _joystickR_NV_Down; } }
    public static bool joystickL_PH { get { return _joystickL_PH; } } //positive horizontal
    public static bool joystickL_NH { get { return _joystickL_NH; } } //negative horizontal
    public static bool joystickL_PV { get { return _joystickL_PV; } } //positive vertical
    public static bool joystickL_NV { get { return _joystickL_NV; } } //negative vertical
    public static bool joystickR_PH { get { return _joystickR_PH; } } //positive horizontal
    public static bool joystickR_NH { get { return _joystickR_NH; } } //negative horizontal
    public static bool joystickR_PV { get { return _joystickR_PV; } } //positive vertical
    public static bool joystickR_NV { get { return _joystickR_NV; } } //negative vertical
    public static bool gripL { get { return _gripL; } }
    public static bool gripLUp { get { return _gripLUp; } }
    public static bool gripLDown { get { return _gripLDown; } }
    public static bool gripR { get { return _gripR; } }
    public static bool gripRUp { get { return _gripRUp; } }
    public static bool gripRDown { get { return _gripRDown; } }
    public static bool triggerL { get { return _triggerL; } }
    public static bool triggerLUp { get { return _triggerLUp; } }
    public static bool triggerLDown { get { return _triggerLDown; } }
    public static bool triggerR { get { return _triggerR; } }
    public static bool triggerRUp { get { return _triggerRUp; } }
    public static bool triggerRDown { get { return _triggerRDown; } }
    public static bool aUp { get { return _aUp; } }
    public static bool aDown { get { return _aDown; } }
    public static bool bUp { get { return _bUp; } }
    public static bool bDown { get { return _bDown; } }

    private static bool _joystickL_PH; //positive horizontal
    private static bool _joystickL_PH_Up;
    private static bool _joystickL_PH_Down;
    private static bool _joystickL_NH; //negative horizontal
    private static bool _joystickL_NH_Up;
    private static bool _joystickL_NH_Down;
    private static bool _joystickL_PV; //positive vertical
    private static bool _joystickL_PV_Up;
    private static bool _joystickL_PV_Down;
    private static bool _joystickL_NV; //negative vertical
    private static bool _joystickL_NV_Up;
    private static bool _joystickL_NV_Down;
    private static bool _joystickR_PH; //positive horizontal
    private static bool _joystickR_PH_Up;
    private static bool _joystickR_PH_Down;
    private static bool _joystickR_NH; //negative horizontal
    private static bool _joystickR_NH_Up;
    private static bool _joystickR_NH_Down;
    private static bool _joystickR_PV; //positive vertical
    private static bool _joystickR_PV_Up;
    private static bool _joystickR_PV_Down;
    private static bool _joystickR_NV; //negative vertical
    private static bool _joystickR_NV_Up;
    private static bool _joystickR_NV_Down;
    private static bool _gripL;
    private static bool _gripLUp;
    private static bool _gripLDown;
    private static bool _gripR;
    private static bool _gripRUp;
    private static bool _gripRDown;
    private static bool _triggerL;
    private static bool _triggerLUp;
    private static bool _triggerLDown;
    private static bool _triggerR;
    private static bool _triggerRUp;
    private static bool _triggerRDown;
    private static bool _aUp;
    private static bool _aDown;
    private static bool _bUp;
    private static bool _bDown;

    private static bool prevJoystickL_PH; //positive horizontal
    private static bool prevJoystickL_NH; //negative horizontal
    private static bool prevJoystickL_PV; //positive vertical
    private static bool prevJoystickL_NV; //negative vertical
    private static bool prevJoystickR_PH; //positive horizontal
    private static bool prevJoystickR_NH; //negative horizontal
    private static bool prevJoystickR_PV; //positive vertical
    private static bool prevJoystickR_NV; //negative vertical
    private static bool prevB;
    private static bool prevA;
    private static bool prevGripL;
    private static bool prevGripR;
    private static bool prevTriggerL;
    private static bool prevTriggerR;
    
    void Update()
    {
        ltouchPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        ltouchRot = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        rtouchPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        rtouchRot = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        if (trackingSpace != null)
        {
            ltouchPos = trackingSpace.TransformPoint(ltouchPos);
            ltouchRot = trackingSpace.TransformRotation(ltouchRot);
            rtouchPos = trackingSpace.TransformPoint(rtouchPos);
            rtouchRot = trackingSpace.TransformRotation(rtouchRot);
        }

        joystickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick, OVRInput.Controller.All);
        joystickR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick, OVRInput.Controller.All);
        gripAxisL = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger, OVRInput.Controller.All);
        gripAxisR = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger, OVRInput.Controller.All);
        triggerAxisL = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger, OVRInput.Controller.All);
        triggerAxisR = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger, OVRInput.Controller.All);
        b = OVRInput.Get(OVRInput.RawButton.B, OVRInput.Controller.All);
        a = OVRInput.Get(OVRInput.RawButton.A, OVRInput.Controller.All);

        _joystickL_NH = joystickL.x <= -joystickThreshold;
        _joystickL_PH = joystickL.x >= joystickThreshold;
        _joystickL_NV = joystickL.y <= -joystickThreshold;
        _joystickL_PV = joystickL.y >= joystickThreshold;
        _joystickR_NH = joystickR.x <= -joystickThreshold;
        _joystickR_PH = joystickR.x >= joystickThreshold;
        _joystickR_NV = joystickR.y <= -joystickThreshold;
        _joystickR_PV = joystickR.y >= joystickThreshold;
        _gripL = gripAxisL >= gripThreshold;
        _gripR = gripAxisR >= gripThreshold;
        _triggerL = triggerAxisL >= triggerThreshold;
        _triggerR = triggerAxisR >= triggerThreshold;

        SetUpAndDown(a, ref _aUp, ref _aDown, ref prevA);
        SetUpAndDown(b, ref _bUp, ref _bDown, ref prevB);
        SetUpAndDown(_joystickL_NH, ref _joystickL_NH_Up, ref _joystickL_NH_Down, ref prevJoystickL_NH);
        SetUpAndDown(_joystickL_PH, ref _joystickL_PH_Up, ref _joystickL_PH_Down, ref prevJoystickL_PH);
        SetUpAndDown(_joystickL_NV, ref _joystickL_NV_Up, ref _joystickL_NV_Down, ref prevJoystickL_NV);
        SetUpAndDown(_joystickL_PV, ref _joystickL_PV_Up, ref _joystickL_PV_Down, ref prevJoystickL_PV);
        SetUpAndDown(_joystickR_NH, ref _joystickR_NH_Up, ref _joystickR_NH_Down, ref prevJoystickR_NH);
        SetUpAndDown(_joystickR_PH, ref _joystickR_PH_Up, ref _joystickR_PH_Down, ref prevJoystickR_PH);
        SetUpAndDown(_joystickR_NV, ref _joystickR_NV_Up, ref _joystickR_NV_Down, ref prevJoystickR_NV);
        SetUpAndDown(_joystickR_PV, ref _joystickR_PV_Up, ref _joystickR_PV_Down, ref prevJoystickR_PV);
        SetUpAndDown(_gripL, ref _gripLUp, ref _gripLDown, ref prevGripL);
        SetUpAndDown(_gripR, ref _gripRUp, ref _gripRDown, ref prevGripR);
        SetUpAndDown(_triggerL, ref _triggerLUp, ref _triggerLDown, ref prevTriggerL);
        SetUpAndDown(_triggerR, ref _triggerRUp, ref _triggerRDown, ref prevTriggerR);
    }

    private static void SetUpAndDown(bool current, ref bool up, ref bool down, ref bool prev)
    {
        if (current && !prev)
        {
            down = true;
            prev = true;
        }
        else if (current && prev)
        {
            down = false;
        }
        else if (!current && prev)
        {
            up = true;
            prev = false;
        }
        else if (!current && !prev)
        {
            up = false;
        }
    }

    public static void SetControllerVibration(float frequency, float amplitude, OVRInput.Controller mask = OVRInput.Controller.Active)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, mask);
    }
}
