using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CameraAudio : MonoBehaviour
{
    [SerializeField, Min(0)] float audioSpeed = 1;
    [SerializeField, Min(0)] float speedThreshold = 0.01f;
    [SerializeField, Min(0)] float maxSpeed = 10;

    private AudioSource audio;
    private Vector3 position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var speed = (transform.position - position).magnitude / Time.deltaTime;

        audio.volume = Mathf.MoveTowards(audio.volume, speed > speedThreshold ? Mathf.Clamp01(speed / maxSpeed) : 0, audioSpeed * Time.deltaTime);
        position = transform.position;
    }
}
