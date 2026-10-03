using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Animation")]
    [Min(0)] public float animationDampTime = 0.05f;

    [Header("Audio")]
    [SerializeField] public Dictionary<string, List<AudioClip>> walkSounds;
    public List<AudioClip> jumpSounds;
    [Min(0)] public float footThreshold = 0.2f;

    [Header("Movement")]
    [Min(0)] public float moveSpeed = 2;
    [Min(0)] public float crouchMultiplier = 0.5f;
    [Min(0)] public float sprintMultiplier = 2f;
    [Min(0)] public float jumpHeight = 1;
    [Min(0)] public float crouchSpeed = 2;
    [Min(0)] public float rotationSpeed = 100;
    public float yGroundVelocity = -1;
}
