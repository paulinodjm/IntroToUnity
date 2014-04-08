using UnityEngine;
using UnityEditor;
using System.Collections;

[AddComponentMenu("IntroToUnity/CameraController")]
public class CameraController : MonoBehaviour
{
    #region Unity Settings

    // view target
    public Transform Target;

    // desired distance between the camera and the target
    public float Distance = 2;

    // pitch limitation
    public float PitchMin = -60f;
    public float PitchMax = 60f;

    // sensitivity
    public float VerticalSensitivity = 15f;
    public float HorizontalSensitivity = 15f;

    #endregion

    private float _pitch;

    void Start()
    {
        _pitch = transform.localEulerAngles.x;
    }

    void Update()
    {
        float yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * HorizontalSensitivity;
        
        _pitch += Input.GetAxis("Mouse Y") * VerticalSensitivity;
        _pitch = Mathf.Clamp(_pitch, PitchMin, PitchMax);

        transform.localEulerAngles = new Vector3(-_pitch, yaw, 0);        
    }

    void LateUpdate()
    {
        if (Target == null) return;

        transform.position = Target.position;
        transform.Translate(0, 0, -Distance);
    }
}