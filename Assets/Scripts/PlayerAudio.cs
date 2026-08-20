using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// In Unity 6.6, use <see cref="Dictionary{TKey, TValue}" />.
/// </summary>
[Serializable]
internal struct WalkSounds
{
    public List<AudioClip> sounds;
    public string tag;
}

/// <summary>
/// Data on foot for animations.
/// </summary>
internal class FootData
{
    public Transform Transform { get; }
    public bool Played = true;

    public FootData(Transform transform)
    {
        Transform = transform;
    }
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private List<WalkSounds> walkSounds;
    [SerializeField] private List<AudioClip> jumpSounds;
    [SerializeField] private float footThreshold = 0.2f;

    private AudioSource audio;
    private PlayerMovement movement;
    private readonly Dictionary<HumanBodyBones, FootData> feet = new();
    private bool playedJump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        movement = GetComponent<PlayerMovement>();

        var animator = GetComponent<Animator>();

        foreach (var humanBodyBone in new[] { HumanBodyBones.LeftFoot, HumanBodyBones.RightFoot })
        {
            var transform = animator.GetBoneTransform(humanBodyBone);

            feet.Add(humanBodyBone, new(transform));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var foot in feet.Values)
        {
            var raycast = Physics.Raycast(foot.Transform.position, Vector3.down, out RaycastHit hitInfo, footThreshold);

            Play(raycast, ref foot.Played, raycast ? walkSounds.First(walkSound => hitInfo.transform.CompareTag(walkSound.tag)).sounds : walkSounds[0].sounds);
        }

        Play(movement.IsJumping, ref playedJump, jumpSounds);
    }

    private void Play(bool condition, ref bool toggle, List<AudioClip> clips)
    {
        if (condition && !toggle) audio.PlayOneShot(clips[Random.Range(0, clips.Count)]);

        toggle = condition;
    }
}
