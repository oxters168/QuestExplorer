using UnityEngine;
using UnityHelpers;
// using UnityEngine.XR;
// using System.Collections.Generic;

public class CuffBoard : MonoBehaviour
{
    public CuffController leftCuff, rightCuff;
    private Quaternion startRotL, startRotR;
    private Vector3 axisL, normalL, axisR, normalR;

    void Update()
    {
        leftCuff.transform.position = OculusInputBridge.ltouchPos;
        rightCuff.transform.position = OculusInputBridge.rtouchPos;

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
        // var inputDevices = new List<InputDevice>();
        // InputDevices.GetDevices(inputDevices);

        // foreach (var inputDevice in inputDevices)
        // {
        //     Vector3 position;
        //     inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out position);
        // }
    }
}
