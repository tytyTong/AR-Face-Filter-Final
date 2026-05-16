using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GoblinFaceFilter : MonoBehaviour
{
    [Header("Goblin Effects")]
    public ParticleSystem greenSparkles;
    public Light goblinEyeGlow;

    [Header("Settings")]
    public float smileThreshold = 0.5f;

    private ARFace arFace;
    private bool isSmiling = false;

    void Awake()
    {
        arFace = GetComponent<ARFace>();
    }

    void OnEnable()
    {
        ARFaceManager faceManager = FindObjectOfType<ARFaceManager>();
        if (faceManager != null)
            arFace.updated += OnFaceUpdated;
    }

    void OnDisable()
    {
        if (arFace != null)
            arFace.updated -= OnFaceUpdated;
    }

    void OnFaceUpdated(ARFaceUpdatedEventArgs args)
    {
        UpdateGoblinEffects();
    }

    void UpdateGoblinEffects()
    {
        // Simulate smile detection using face tracking state
        if (arFace == null) return;

        // Toggle sparkles when face is tracked
        if (arFace.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            if (!isSmiling)
            {
                isSmiling = true;
                if (greenSparkles != null) greenSparkles.Play();
                if (goblinEyeGlow != null) goblinEyeGlow.intensity = 2f;
            }
        }
        else
        {
            isSmiling = false;
            if (greenSparkles != null) greenSparkles.Stop();
            if (goblinEyeGlow != null) goblinEyeGlow.intensity = 0f;
        }
    }
}