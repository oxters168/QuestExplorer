using UnityEngine;
using UnityHelpers;

public class ApplyInputToBrowser : MonoBehaviour
{
    public OculusInputBridge oInput;

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

        if (oInput.aDown)
        {
            explorerR?.GoBack();
        }
        if (oInput.bDown)
        {
            explorerR?.GoUp();
        }

        grabberL.grab = oInput.gripL;
        grabberR.grab = oInput.gripR;

        if (oInput.triggerLDown)
        {
            if (itemL != null)
            {
                itemL.data.refExp.Goto(itemL.data.fullPath);
            }
        }
        if (oInput.triggerRDown)
        {
            if (itemR != null)
            {
                itemR.data.refExp.Goto(itemR.data.fullPath);
            }
        }

        if (oInput.joystickL_NH_Down || oInput.joystickL_PH_Down)
        {
            Debug.Log("Next/Prev page " + (gridL != null));
            if (gridL != null)
            {
                if (oInput.joystickL.x > 0)
                    gridL.NextPage();
                else
                    gridL.PrevPage();
            }
        }
        if (oInput.joystickR_NH_Down || oInput.joystickR_PH_Down)
        {
            Debug.Log("Next/Prev page " + (gridR != null));
            if (gridR != null)
            {
                if (oInput.joystickR.x > 0)
                    gridR.NextPage();
                else
                    gridR.PrevPage();
            }
        }
    }

    public void CheckPointers()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(oInput.leftRay.position, oInput.leftRay.forward, out rayHit))
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
        
        if (Physics.Raycast(oInput.rightRay.position, oInput.rightRay.forward, out rayHit))
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
