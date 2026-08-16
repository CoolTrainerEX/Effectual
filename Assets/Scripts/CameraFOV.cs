using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class CameraFOV : MonoBehaviour
{
    [SerializeField] private float fovSpeed = 2;
    [SerializeField] private float fovMultiplier = 1;
    [SerializeField] private float targetSpeedThreshold = 0.01f;
    [SerializeField] private float maxTargetSpeed = 10;

    private CinemachineCamera camera;
    private float baseFov;
    private Vector3 targetPos;

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
        float speed = Mathf.Clamp(Vector3.Dot(camera.Follow.position - targetPos, transform.forward) / Time.deltaTime, 0, maxTargetSpeed);

        LensSettings lens = camera.Lens;

        lens.FieldOfView = Mathf.Lerp(camera.Lens.FieldOfView, speed > targetSpeedThreshold ? baseFov + Mathf.Pow(speed, 2) * fovMultiplier : baseFov, fovSpeed * Time.deltaTime);
        camera.Lens = lens;
        targetPos = camera.Follow.position;
    }
}
