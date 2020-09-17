using UnityEngine;
using UnityHelpers;

public class ApplyInputToBrowser : MonoBehaviour
{
    public Transform leftRay, rightRay;
    
    private Grid gridL;
    private Explorer explorerL;
    private FSItem itemL;
    private Grid gridR;
    private Explorer explorerR;
    private FSItem itemR;

    [Space(10)]
    public Grabber grabberL;
    public Grabber grabberR;

    void Update()
    {
        CheckPointers();

        if (OculusInputBridge.aDown)
        {
            explorerR?.GoBack();
        }
        if (OculusInputBridge.bDown)
        {
            explorerR?.GoUp();
        }

        grabberL.grab = OculusInputBridge.gripL;
        grabberR.grab = OculusInputBridge.gripR;

        if (OculusInputBridge.triggerLDown)
        {
            if (itemL != null)
            {
                itemL.data.refExp.Goto(itemL.data.fullPath);
            }
        }
        if (OculusInputBridge.triggerRDown)
        {
            if (itemR != null)
            {
                itemR.data.refExp.Goto(itemR.data.fullPath);
            }
        }

        if (OculusInputBridge.joystickL_NH_Down || OculusInputBridge.joystickL_PH_Down)
        {
            Debug.Log("Next/Prev page " + (gridL != null));
            if (gridL != null)
            {
                if (OculusInputBridge.joystickL.x > 0)
                    gridL.NextPage();
                else
                    gridL.PrevPage();
            }
        }
        if (OculusInputBridge.joystickR_NH_Down || OculusInputBridge.joystickR_PH_Down)
        {
            Debug.Log("Next/Prev page " + (gridR != null));
            if (gridR != null)
            {
                if (OculusInputBridge.joystickR.x > 0)
                    gridR.NextPage();
                else
                    gridR.PrevPage();
            }
        }
    }

    public void CheckPointers()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(leftRay.position, leftRay.forward, out rayHit))
        {
            gridL = rayHit.transform.GetComponentInParent<Grid>();
            explorerL = rayHit.transform.GetComponentInParent<Explorer>();
            itemL = rayHit.transform.GetComponentInParent<FSItem>();
            if (itemL != null && explorerL == null)
            {
                explorerL = itemL.data.refExp;
            }
        }
        else
        {
            gridL = null;
            explorerL = null;
            itemL = null;
        }
        
        if (Physics.Raycast(rightRay.position, rightRay.forward, out rayHit))
        {
            gridR = rayHit.transform.GetComponentInParent<Grid>();
            explorerR = rayHit.transform.GetComponentInParent<Explorer>();
            itemR = rayHit.transform.GetComponentInParent<FSItem>();
            if (itemR != null && explorerR == null)
            {
                explorerR = itemR.data.refExp;
            }
        }
        else
        {
            gridR = null;
            explorerR = null;
            itemR = null;
        }
    }
}
