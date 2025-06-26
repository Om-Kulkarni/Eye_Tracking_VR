using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

public class updateRightPupil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyePupilData(out XrSingleEyePupilDataHTC[] out_pupils);
        XrSingleEyePupilDataHTC rightPupil = out_pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];

        if (rightPupil.isDiameterValid)
        {
            float rightPupilDiameter = rightPupil.pupilDiameter;
            //print("Right Pupil Diameter: " + rightPupilDiameter);
        }
        if (rightPupil.isPositionValid)
        {
            XrVector2f rightPupilPosition = rightPupil.pupilPosition;
            //print("Right Pupil Position: " + rightPupilPosition.x + ", " + rightPupilPosition.y);
        }
    }
}
