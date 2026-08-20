using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class CameraFOV : MonoBehaviour
{
    [SerializeField, Min(0)] private float fovSmoothTime = 0.1f;
    [SerializeField, Min(0)] private float fovMultiplier = 2;
    [SerializeField, Min(0)] private float maxFov = 10;
    [SerializeField, Min(0)] private float targetSpeedThreshold = 0.01f;

    private CinemachineCamera camera;
    private float baseFov;
    private Vector3 targetPos;
    private float currentVelocity = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        camera = GetComponent<CinemachineCamera>();
        baseFov = camera.Lens.FieldOfView;
        targetPos = camera.Follow.position;
    }

    // Update is called once per frame
    private void Update()
    {
        var speed = Vector3.Dot(camera.Follow.position - targetPos, transform.forward) / Time.deltaTime;
        var lens = camera.Lens;

        lens.FieldOfView = Mathf.SmoothDamp(camera.Lens.FieldOfView, Mathf.Abs(speed) > targetSpeedThreshold ? baseFov + Mathf.Clamp(speed * fovMultiplier, -maxFov, maxFov) : baseFov, ref currentVelocity, fovSmoothTime);
        camera.Lens = lens;
        targetPos = camera.Follow.position;
    }
}
