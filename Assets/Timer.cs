using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base class for each timer
/// </summary>
[AddComponentMenu("IntroToUnity/Timer")]
public class Timer : MonoBehaviour 
{
    /// <summary>
    /// Timer duration
    /// </summary>
    public float TimerDuration;

    private float _remainingTime;
    public float RemainingTime
    {
        get
        {
            return (_remainingTime < .0f) ? .0f : _remainingTime;
        }
    }

    /// <summary>
    /// Event triggered when 
    /// </summary>
    public event Action Triggered;

    #region Unity callback

    protected void OnEnable()
    {
        _remainingTime = TimerDuration;
    }

    protected void Update()
    {
        if (!enabled) return;

        _remainingTime -= Time.deltaTime;
        if (_remainingTime <= .0f)
        {
            if (Triggered != null)
            {
                Triggered();
            }
            enabled = false;
        }
    }

    #endregion
}
