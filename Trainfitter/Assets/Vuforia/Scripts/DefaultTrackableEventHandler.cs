/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/


using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler


{
    const string TrackOption = "TrackValue";
    const string TrafficOption = "TrafficValue";
    const string CurveOption = "CurveValue";
    const string ElecOption = "ElecValue";

    int TrackType;
    int Curve;
    int Electricity;
    int TrafficType;

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        TrackType = PlayerPrefs.GetInt(TrackOption);
        Curve = PlayerPrefs.GetInt(CurveOption);
        Electricity = PlayerPrefs.GetInt(ElecOption);
        TrafficType = PlayerPrefs.GetInt(TrafficOption);

        Debug.Log("TrafficType: " + TrafficType + ", Electricity: " + Electricity + ", TrackType: " + TrackType);

        // Enable rendering:
        // Checks what settings are currently selected and renders the wanted ATU
        foreach (var component in rendererComponents)
        {

            if (TrafficType == 0 && Electricity == 0 && TrackType == 0 && component.name == "QuadSLPpr")
            {
                Debug.Log("TrafficType: " + TrafficType + ", Electricity: " + Electricity + ", TrackType: " + TrackType + " Should be: 0, 0, 0");
                component.enabled = true;
            }
            else if (TrafficType == 0 && Electricity == 0 && TrackType == 1 && component.name == "QuadSLPsr")
            {
                Debug.Log("TrafficType: " + TrafficType + ", Electricity: " + Electricity + ", TrackType: " + TrackType + " Should be: 0, 0, 1");
                component.enabled = true;
            }
            else if (TrafficType == 0 && Electricity == 1 && TrackType == 0 && component.name == "QuadESLPpr")
            {
                Debug.Log("TrafficType: " + TrafficType + ", Electricity: " + Electricity + ", TrackType: " + TrackType + " Should be: 0, 1, 0");
                component.enabled = true;
            }
            else if (TrafficType == 0 && Electricity == 1 && TrackType == 1 && component.name == "QuadESLPsr")
            {
                Debug.Log("TrafficType: " + TrafficType + ", Electricity: " + Electricity + ", TrackType: " + TrackType + " Should be: 0, 1, 1");
                component.enabled = true;
            }
            else if (TrafficType == 1 && Electricity == 0 && TrackType == 0 && component.name == "QuadSLpr")
            {
                Debug.Log("TrafficType: " + TrafficType + ", Electricity: " + Electricity + ", TrackType: " + TrackType + " Should be: 1, 0, 0");
                component.enabled = true;
            }
            else if (TrafficType == 1 && Electricity == 0 && TrackType == 1 && component.name == "QuadSLsr")
            {
                Debug.Log("TrafficType: " + TrafficType + ", Electricity: " + Electricity + ", TrackType: " + TrackType + " Should be: 1, 0, 1");
                component.enabled = true;
            }
            else if (TrafficType == 1 && Electricity == 1 && TrackType == 0 && component.name == "QuadESLpr")
            {
                Debug.Log("TrafficType: " + TrafficType + ", Electricity: " + Electricity + ", TrackType: " + TrackType + " Should be: 1, 1, 0");
                component.enabled = true;
            }
            else if (TrafficType == 1 && Electricity == 1 && TrackType == 1 && component.name == "QuadESLsr")
            {
                Debug.Log("TrafficType: " + TrafficType + ", Electricity: " + Electricity + ", TrackType: " + TrackType + " Should be: 1, 1, 1");
                component.enabled = true;
            }
        }

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PROTECTED_METHODS
}
