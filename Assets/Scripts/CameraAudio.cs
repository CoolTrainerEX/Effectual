using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CameraAudio : MonoBehaviour
{
    [SerializeField] float audioSpeed = 1;
    [SerializeField] float speedThreshold = 0.01f;
    [SerializeField] float maxSpeed = 10;

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
        float speed = (transform.position - position).magnitude / Time.deltaTime;

        audio.volume = Mathf.MoveTowards(audio.volume, speed > speedThreshold ? Mathf.Clamp01(speed / maxSpeed) : 0, audioSpeed * Time.deltaTime);
        position = transform.position;
    }
}
