using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// This is an improved diagnostic script. It continuously checks for an eye-tracking
/// device and tries to read the modern 'eyesData' feature usage.
/// </summary>
public class EyeTrackingDeviceCheck_Continuous : MonoBehaviour
{
    [Header("Device Status")]
    [Tooltip("This will be true if a valid eye tracking device is found.")]
    public bool isDeviceFound = false;

    [Tooltip("Live gaze direction from the device, if found.")]
    public Vector3 gazeDirection;

    private InputDevice eyeTrackingDevice;

    void Update()
    {
        // If we haven't found the device yet, keep trying.
        if (!isDeviceFound)
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.EyeTracking, devices);

            if (devices.Count > 0)
            {
                // We found it!
                eyeTrackingDevice = devices[0];
                isDeviceFound = true;
                Debug.Log("SUCCESS: Eye tracking device was found by the Input System: " + eyeTrackingDevice.name);
            }
        }

        // Only try to get data if the device has been found and is still valid.
        if (isDeviceFound && eyeTrackingDevice.isValid)
        {
            // --- UPDATED LOGIC ---
            // We now try to get the 'eyesData' feature, which is a dedicated
            // container for all eye-tracking information.

            bool found_eyes_data = eyeTrackingDevice.TryGetFeatureValue(CommonUsages.eyesData, out Eyes eyesData);
            Debug.Log("Eyes Data Found: " + found_eyes_data);
            bool found_fixation_point = eyesData.TryGetFixationPoint(out Vector3 fixationPoint);
            Debug.Log("Fixation Point Found: " + found_fixation_point + " at " + fixationPoint.ToString("F3"));

            // The 'eyesData' struct contains gaze data for left, right, and a combined 'fixation' point.
            // We'll try to get the rotation of the left eye's gaze.
            if (eyesData.TryGetLeftEyeRotation(out Quaternion leftEyeRotation))
            {
                gazeDirection = leftEyeRotation * Vector3.forward;

                // To show it's working, we'll log the data every 100 frames.
                if (Time.frameCount % 100 == 0)
                {
                    Debug.Log("Live Gaze Data Found via eyesData! Direction: " + gazeDirection.ToString("F3"));
                }
            }
           
        }
    }
}
