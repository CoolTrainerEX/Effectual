using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
[RequireComponent(typeof(AudioSource))]
public class FOVManager : MonoBehaviour
{
    [SerializeField] float fovSpeed = 2;
    [SerializeField] float fovMultiplier = 1;
    [SerializeField] float speedThreshold = 0.01f;
    [SerializeField] float maxSpeed = 10;

    private CinemachineCamera camera;
    private AudioSource audio;
    private float baseFov;
    private Vector3 targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<CinemachineCamera>();
        audio = GetComponent<AudioSource>();
        baseFov = camera.Lens.FieldOfView;
        targetPos = camera.Follow.position;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Mathf.Clamp(Vector3.Dot(camera.Follow.position - targetPos, transform.forward) / Time.deltaTime, 0, maxSpeed);
        LensSettings lens = camera.Lens;

        audio.volume = Mathf.MoveTowards(audio.volume, Mathf.Log(speed, maxSpeed), fovSpeed * Time.deltaTime);
        lens.FieldOfView = Mathf.Lerp(camera.Lens.FieldOfView, speed > speedThreshold ? baseFov + Mathf.Pow(speed, 2) * fovMultiplier : baseFov, fovSpeed * Time.deltaTime);
        camera.Lens = lens;
        targetPos = camera.Follow.position;
    }
}
