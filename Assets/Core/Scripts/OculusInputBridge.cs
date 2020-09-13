using UnityEngine;

public class OculusInputBridge : MonoBehaviour
{
    public Transform leftRay;
    public Transform rightRay;

    public float gripThreshold = 0.01f;
    public float triggerThreshold = 0.01f;
    
    public float gripValueL { get; private set; }
    public float gripValueR { get; private set; }
    public float trigValueL { get; private set; }
    public float trigValueR { get; private set; }
    public bool a { get; private set; }
    public bool b { get; private set; }

    private bool _gripL;
    public bool gripL { get { return _gripL; } }
    private bool _gripLUp;
    public bool gripLUp { get { return _gripLUp; } }
    private bool _gripLDown;
    public bool gripLDown { get { return _gripLDown; } }
    private bool _gripR;
    public bool gripR { get { return _gripR; } }
    private bool _gripRUp;
    public bool gripRUp { get { return _gripRUp; } }
    private bool _gripRDown;
    public bool gripRDown { get { return _gripRDown; } }
    private bool _triggerL;
    public bool triggerL { get { return _triggerL; } }
    private bool _triggerLUp;
    public bool triggerLUp { get { return _triggerLUp; } }
    private bool _triggerLDown;
    public bool triggerLDown { get { return _triggerLDown; } }
    private bool _triggerR;
    public bool triggerR { get { return _triggerR; } }
    private bool _triggerRUp;
    public bool triggerRUp { get { return _triggerRUp; } }
    private bool _triggerRDown;
    public bool triggerRDown { get { return _triggerRDown; } }
    private bool _aUp;
    public bool aUp { get { return _aUp; } }
    private bool _aDown;
    public bool aDown { get { return _aDown; } }
    private bool _bUp;
    public bool bUp { get { return _bUp; } }
    private bool _bDown;
    public bool bDown { get { return _bDown; } }

    private bool prevB;
    private bool prevA;
    private bool prevGripL;
    private bool prevGripR;
    private bool prevTriggerL;
    private bool prevTriggerR;
    
    void Update()
    {
        gripValueL = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger, OVRInput.Controller.All);
        gripValueR = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger, OVRInput.Controller.All);
        trigValueL = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger, OVRInput.Controller.All);
        trigValueR = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger, OVRInput.Controller.All);
        b = OVRInput.Get(OVRInput.RawButton.B, OVRInput.Controller.All);
        a = OVRInput.Get(OVRInput.RawButton.A, OVRInput.Controller.All);

        _gripL = gripValueL >= gripThreshold;
        _gripR = gripValueR >= gripThreshold;
        _triggerL = trigValueL >= triggerThreshold;
        _triggerR = trigValueR >= triggerThreshold;

        SetUpAndDown(a, ref _aUp, ref _aDown, ref prevA);
        SetUpAndDown(b, ref _bUp, ref _bDown, ref prevB);
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
