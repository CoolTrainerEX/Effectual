using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
[RequireComponent(typeof(AudioSource))]
public class CameraAudio : MonoBehaviour
{
    [SerializeField] float audioSpeed = 1;
    [SerializeField] float targetSpeedThreshold = 0.01f;
    [SerializeField] float maxTargetSpeed = 10;

    private CinemachineCamera camera;
    private AudioSource audio;
    private Vector3 targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<CinemachineCamera>();
        audio = GetComponent<AudioSource>();
        targetPos = camera.Follow.position;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Mathf.Clamp((camera.Follow.position - targetPos).magnitude / Time.deltaTime, 0, maxTargetSpeed);

        audio.volume = Mathf.MoveTowards(audio.volume, speed > targetSpeedThreshold ? Mathf.Log(speed, maxTargetSpeed) : 0, audioSpeed * Time.deltaTime);
        targetPos = camera.Follow.position;
    }
}
