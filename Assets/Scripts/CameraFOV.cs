using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class CameraFOV : MonoBehaviour
{
    [SerializeField] private float fovSmoothTime = 0.1f;
    [SerializeField] private float fovMultiplier = 2;
    [SerializeField] private float maxFov = 10;
    [SerializeField] private float targetSpeedThreshold = 0.01f;

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
        float speed = Vector3.Dot(camera.Follow.position - targetPos, transform.forward) / Time.deltaTime;

        LensSettings lens = camera.Lens;

        lens.FieldOfView = Mathf.SmoothDamp(camera.Lens.FieldOfView, Mathf.Abs(speed) > targetSpeedThreshold ? baseFov + Mathf.Clamp(speed * fovMultiplier, -maxFov, maxFov) : baseFov, ref currentVelocity, fovSmoothTime);
        camera.Lens = lens;
        targetPos = camera.Follow.position;
    }
}
