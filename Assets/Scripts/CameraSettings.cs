using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Scriptable Objects/CameraSettings")]
public class CameraSettings : ScriptableObject
{
    [Header("Audio")]
    [Min(0)] public float audioSpeed = 1;
    [Min(0)] public float speedThreshold = 0.01f;
    [Min(0)] public float maxSpeed = 10;

    [Header("FOV")]
    [Min(0)] public float fovSmoothTime = 0.1f;
    [Min(0)] public float fovMultiplier = 2;
    [Min(0)] public float maxFov = 10;
    [Min(0)] public float targetSpeedThreshold = 0.01f;
}
