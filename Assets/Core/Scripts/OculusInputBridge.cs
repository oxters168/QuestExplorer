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
    public static bool x { get; private set; }
    public static bool y { get; private set; }
    public static bool l3 { get; private set; }
    public static bool r3 { get; private set; }

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
    public static bool xUp { get { return _xUp; } }
    public static bool xDown { get { return _xDown; } }
    public static bool yUp { get { return _yUp; } }
    public static bool yDown { get { return _yDown; } }
    public static bool l3Up { get { return _l3Up; } }
    public static bool l3Down { get { return _l3Down; } }
    public static bool r3Up { get { return _r3Up; } }
    public static bool r3Down { get { return _r3Down; } }

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
    private static bool _xUp;
    private static bool _xDown;
    private static bool _yUp;
    private static bool _yDown;
    private static bool _l3Up;
    private static bool _l3Down;
    private static bool _r3Up;
    private static bool _r3Down;

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
    private static bool prevX;
    private static bool prevY;
    private static bool prevL3;
    private static bool prevR3;
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
        x = OVRInput.Get(OVRInput.RawButton.X, OVRInput.Controller.All);
        y = OVRInput.Get(OVRInput.RawButton.Y, OVRInput.Controller.All);
        l3 = OVRInput.Get(OVRInput.RawButton.LThumbstick, OVRInput.Controller.All);
        r3 = OVRInput.Get(OVRInput.RawButton.RThumbstick, OVRInput.Controller.All);

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
        SetUpAndDown(x, ref _xUp, ref _xDown, ref prevX);
        SetUpAndDown(y, ref _yUp, ref _yDown, ref prevY);
        SetUpAndDown(l3, ref _l3Up, ref _l3Down, ref prevL3);
        SetUpAndDown(r3, ref _r3Up, ref _r3Down, ref prevR3);
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
