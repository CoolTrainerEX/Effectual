using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class CameraFov : MonoBehaviour
{
    [SerializeField] private CameraSettings settings;

    private CinemachineCamera camera;
    private float baseFov;
    private Vector3 targetPos;
    private float currentVelocity = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<CinemachineCamera>();
        baseFov = camera.Lens.FieldOfView;
        targetPos = camera.Follow.position;
    }

    // Update is called once per frame
    void Update()
    {
        var speed = Vector3.Dot(camera.Follow.position - targetPos, transform.forward) / Time.deltaTime;
        var lens = camera.Lens;

        lens.FieldOfView = Mathf.SmoothDamp(camera.Lens.FieldOfView, Mathf.Abs(speed) > settings.targetSpeedThreshold ? baseFov + Mathf.Clamp(speed * settings.fovMultiplier, -settings.maxFov, settings.maxFov) : baseFov, ref currentVelocity, settings.fovSmoothTime);
        camera.Lens = lens;
        targetPos = camera.Follow.position;
    }
}
