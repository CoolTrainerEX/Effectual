using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSound;

    private AudioSource audio;
    private PlayerMovement movement;
    private bool played = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.IsJumping != played)
        {
            played = !played;

            if (played) audio.PlayOneShot(jumpSound);
        }
    }
}
