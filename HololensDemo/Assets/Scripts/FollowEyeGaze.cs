// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using TMPro;

namespace ToolSuite
{
    /// <summary>
    /// Sample for allowing a GameObject to follow the user's eye gaze
    /// at a given distance of "DefaultDistanceInMeters".
    /// </summary>
    [AddComponentMenu("Scripts/MRTK/Examples/FollowEyeGaze")]
    public class FollowEyeGaze : MonoBehaviour
    {
        [Tooltip("Display the game object along the eye gaze ray at a default distance (in meters).")]
        [SerializeField]
        private float defaultDistanceInMeters = 2f;


        private IMixedRealityEyeGazeProvider eyeGazeProvider = null;
        
        private string note = "";
        private NotificationUpdate notificationUpdate = null;
        //private var spatialAwarenessService = null;
        

        private void Start()
        {
            eyeGazeProvider = CoreServices.InputSystem?.EyeGazeProvider;

            notificationUpdate = GameObject.Find("Notification 3D").GetComponent<NotificationUpdate>(); GetComponent<NotificationUpdate>();
        }
        private void Update()
        {
            string newNote = "";
            if (eyeGazeProvider != null)
            {
                gameObject.transform.position = eyeGazeProvider.HitInfo.point;
                //gameObject.transform.position = eyeGazeProvider.GazeOrigin + eyeGazeProvider.GazeDirection.normalized * defaultDistanceInMeters;
                //gameObject.transform.position = eyeGazeProvider.GazeOrigin + eyeGazeProvider.GazeDirection;
                EyeTrackingTarget lookedAtEyeTarget = EyeTrackingTarget.LookedAtEyeTarget;

                // Update GameObject to the current eye gaze position at a given distance
                if (lookedAtEyeTarget != null)
                {
                    // Show the object at the center of the currently looked at target.
                    if (lookedAtEyeTarget.EyeCursorSnapToTargetCenter)
                    {
                        newNote = "HIT eye target,  centered";
                        Debug.Log(newNote);
                        Ray rayToCenter = new Ray(CameraCache.Main.transform.position, lookedAtEyeTarget.transform.position - CameraCache.Main.transform.position);
                        RaycastHit hitInfo;
                        UnityEngine.Physics.Raycast(rayToCenter, out hitInfo);
                        gameObject.transform.position = hitInfo.point;
                    }
                    else
                    {
                        newNote = "HIT eye target,   but not centered";
                        Debug.Log(newNote);
                        // Show the object at the hit position of the user's eye gaze ray with the target.
                        //gameObject.transform.position = eyeGazeProvider.GazeOrigin + eyeGazeProvider.GazeDirection.normalized * defaultDistanceInMeters;
                        gameObject.transform.position = eyeGazeProvider.GazeOrigin + eyeGazeProvider.GazeDirection;
                    }
                }
                else
                {
                    if (eyeGazeProvider.HitInfo.distance == 0)
                    {
                        newNote = "Not hit, default " + eyeGazeProvider.HitInfo.distance.ToString("F2");
                        Debug.Log(newNote);
                        // If no target is hit, show the object at a default distance along the gaze ray.
                        gameObject.transform.position = eyeGazeProvider.GazeOrigin + eyeGazeProvider.GazeDirection.normalized * defaultDistanceInMeters;
                    }
                    else
                    {
                        newNote = "Not hit, wall? " + eyeGazeProvider.HitInfo.distance.ToString("F2");
                        Debug.Log(newNote);
                        // Hit a gameObject without lookedAtEyeTarget script attached
                        gameObject.transform.position = eyeGazeProvider.HitInfo.point;
                    }
                }
            }

            if (!newNote.Equals(note))
            {
                note = newNote;
                notificationUpdate.AddNote(note, Color.white);
            }
        }
    }
}
