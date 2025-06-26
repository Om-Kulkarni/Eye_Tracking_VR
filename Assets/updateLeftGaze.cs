using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

/// <summary>
/// This script gets true eye-tracking data and uses it to position a separate
/// visualizer object a fixed distance in front of the user's camera.
/// </summary>
public class updateLeftGaze : MonoBehaviour
{
    [Header("Visualization")]
    [Tooltip("Assign a GameObject here (like a small sphere) to visualize the gaze point.")]
    public Transform gazeVisualizer; // Public field to assign your sphere in the Inspector

    [Tooltip("How far in front of the camera the visualizer should float (in meters).")]
    public float visualizerDistance = 0.5f; // The fixed distance for our cursor

    // Update is called once per frame
    void Update()
    {
        // This array will be populated with the gaze data.
        XrSingleEyeGazeDataHTC[] out_gazes;

        // Check if the function successfully returns data before using it.
        if (XR_HTC_eye_tracker.Interop.GetEyeGazeData(out out_gazes))
        {
            // Get the data for the left eye from the returned array.
            XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];

            // Check if the gaze data for this frame is valid.
            if (leftGaze.isValid)
            {
                // Ensure a visualizer has been assigned in the Inspector.
                if (gazeVisualizer != null)
                {
                    // --- CORRECTED LOGIC ---
                    // This script is on the Main Camera, so 'transform.position' is our head's origin.
                    Vector3 headOrigin = transform.position;

                    // Get the direction of the gaze from the eye tracker.
                    Vector3 gazeDirection = leftGaze.gazePose.orientation.ToUnityQuaternion() * Vector3.forward;

                    // Calculate the new position for the visualizer.
                    // It will start from the head and move along the gaze direction.
                    gazeVisualizer.position = headOrigin + (gazeDirection * visualizerDistance);

                    // We can also make the visualizer always face the user (optional but nice).
                    gazeVisualizer.LookAt(headOrigin);

                    // --- END CORRECTED LOGIC ---
                }
            }
        }
    }
}
