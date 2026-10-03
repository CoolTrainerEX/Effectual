using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CameraAudio : MonoBehaviour
{
    [SerializeField] private CameraSettings settings;

    private AudioSource audio;
    private Vector3 position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        position = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        var speed = (transform.position - position).magnitude / Time.deltaTime;

        audio.volume = Mathf.MoveTowards(audio.volume, speed > settings.speedThreshold ? Mathf.Clamp01(speed / settings.maxSpeed) : 0, settings.audioSpeed * Time.deltaTime);
        position = transform.position;
    }
}
