using UnityEngine;
using UnityHelpers;

public class ApplyInputToBrowser : MonoBehaviour
{
    public OculusInputBridge oInput;

    private Explorer explorerL;
    private FSItem itemL;
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
    }

    public void CheckPointers()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(oInput.leftRay.position, oInput.leftRay.forward, out rayHit))
        {
            explorerL = rayHit.transform.GetComponentInParent<Explorer>();
            itemL = rayHit.transform.GetComponentInParent<FSItem>();
            if (itemL != null && explorerL == null)
            {
                explorerL = itemL.data.refExp;
            }
        }
        else
        {
            explorerL = null;
            itemL = null;
        }
        
        if (Physics.Raycast(oInput.rightRay.position, oInput.rightRay.forward, out rayHit))
        {
            explorerR = rayHit.transform.GetComponentInParent<Explorer>();
            itemR = rayHit.transform.GetComponentInParent<FSItem>();
            if (itemR != null && explorerR == null)
            {
                explorerR = itemR.data.refExp;
            }
        }
        else
        {
            explorerR = null;
            itemR = null;
        }
    }
}
