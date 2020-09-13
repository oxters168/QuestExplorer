using UnityEngine;

public class OculusInputBridge : MonoBehaviour
{
    public Transform leftRay;
    public Transform rightRay;

    public float joystickThreshold = 0.01f;
    public float gripThreshold = 0.01f;
    public float triggerThreshold = 0.01f;
    
    public Vector2 joystickL { get; private set; }
    public Vector2 joystickR { get; private set; }
    public float gripAxisL { get; private set; }
    public float gripAxisR { get; private set; }
    public float triggerAxisL { get; private set; }
    public float triggerAxisR { get; private set; }
    public bool a { get; private set; }
    public bool b { get; private set; }

    public bool joystickL_PH_Up { get { return _joystickL_PH_Up; } }
    public bool joystickL_PH_Down { get { return _joystickL_PH_Down; } }
    public bool joystickL_NH_Up { get { return _joystickL_NH_Up; } }
    public bool joystickL_NH_Down { get { return _joystickL_NH_Down; } }
    public bool joystickL_PV_Up { get { return _joystickL_PV_Up; } }
    public bool joystickL_PV_Down { get { return _joystickL_PV_Down; } }
    public bool joystickL_NV_Up { get { return _joystickL_NV_Up; } }
    public bool joystickL_NV_Down { get { return _joystickL_NV_Down; } }
    public bool joystickR_PH_Up { get { return _joystickR_PH_Up; } }
    public bool joystickR_PH_Down { get { return _joystickR_PH_Down; } }
    public bool joystickR_NH_Up { get { return _joystickR_NH_Up; } }
    public bool joystickR_NH_Down { get { return _joystickR_NH_Down; } }
    public bool joystickR_PV_Up { get { return _joystickR_PV_Up; } }
    public bool joystickR_PV_Down { get { return _joystickR_PV_Down; } }
    public bool joystickR_NV_Up { get { return _joystickR_NV_Up; } }
    public bool joystickR_NV_Down { get { return _joystickR_NV_Down; } }
    public bool joystickL_PH { get { return _joystickL_PH; } } //positive horizontal
    public bool joystickL_NH { get { return _joystickL_NH; } } //negative horizontal
    public bool joystickL_PV { get { return _joystickL_PV; } } //positive vertical
    public bool joystickL_NV { get { return _joystickL_NV; } } //negative vertical
    public bool joystickR_PH { get { return _joystickR_PH; } } //positive horizontal
    public bool joystickR_NH { get { return _joystickR_NH; } } //negative horizontal
    public bool joystickR_PV { get { return _joystickR_PV; } } //positive vertical
    public bool joystickR_NV { get { return _joystickR_NV; } } //negative vertical
    public bool gripL { get { return _gripL; } }
    public bool gripLUp { get { return _gripLUp; } }
    public bool gripLDown { get { return _gripLDown; } }
    public bool gripR { get { return _gripR; } }
    public bool gripRUp { get { return _gripRUp; } }
    public bool gripRDown { get { return _gripRDown; } }
    public bool triggerL { get { return _triggerL; } }
    public bool triggerLUp { get { return _triggerLUp; } }
    public bool triggerLDown { get { return _triggerLDown; } }
    public bool triggerR { get { return _triggerR; } }
    public bool triggerRUp { get { return _triggerRUp; } }
    public bool triggerRDown { get { return _triggerRDown; } }
    public bool aUp { get { return _aUp; } }
    public bool aDown { get { return _aDown; } }
    public bool bUp { get { return _bUp; } }
    public bool bDown { get { return _bDown; } }

    private bool _joystickL_PH; //positive horizontal
    private bool _joystickL_PH_Up;
    private bool _joystickL_PH_Down;
    private bool _joystickL_NH; //negative horizontal
    private bool _joystickL_NH_Up;
    private bool _joystickL_NH_Down;
    private bool _joystickL_PV; //positive vertical
    private bool _joystickL_PV_Up;
    private bool _joystickL_PV_Down;
    private bool _joystickL_NV; //negative vertical
    private bool _joystickL_NV_Up;
    private bool _joystickL_NV_Down;
    private bool _joystickR_PH; //positive horizontal
    private bool _joystickR_PH_Up;
    private bool _joystickR_PH_Down;
    private bool _joystickR_NH; //negative horizontal
    private bool _joystickR_NH_Up;
    private bool _joystickR_NH_Down;
    private bool _joystickR_PV; //positive vertical
    private bool _joystickR_PV_Up;
    private bool _joystickR_PV_Down;
    private bool _joystickR_NV; //negative vertical
    private bool _joystickR_NV_Up;
    private bool _joystickR_NV_Down;
    private bool _gripL;
    private bool _gripLUp;
    private bool _gripLDown;
    private bool _gripR;
    private bool _gripRUp;
    private bool _gripRDown;
    private bool _triggerL;
    private bool _triggerLUp;
    private bool _triggerLDown;
    private bool _triggerR;
    private bool _triggerRUp;
    private bool _triggerRDown;
    private bool _aUp;
    private bool _aDown;
    private bool _bUp;
    private bool _bDown;

    private bool prevJoystickL_PH; //positive horizontal
    private bool prevJoystickL_NH; //negative horizontal
    private bool prevJoystickL_PV; //positive vertical
    private bool prevJoystickL_NV; //negative vertical
    private bool prevJoystickR_PH; //positive horizontal
    private bool prevJoystickR_NH; //negative horizontal
    private bool prevJoystickR_PV; //positive vertical
    private bool prevJoystickR_NV; //negative vertical
    private bool prevB;
    private bool prevA;
    private bool prevGripL;
    private bool prevGripR;
    private bool prevTriggerL;
    private bool prevTriggerR;
    
    void Update()
    {
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
}
