using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class FOVManager : MonoBehaviour
{
    [SerializeField] float fovSpeed = 2;
    [SerializeField] float fovMultiplier = 1;
    [SerializeField] float speedThreshold = 0.01f;

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
        float speed = Vector3.Dot(camera.Follow.position - targetPos, transform.forward) / Time.deltaTime;
        LensSettings lens = camera.Lens;

        lens.FieldOfView = Mathf.Lerp(camera.Lens.FieldOfView, speed > speedThreshold ? baseFov + Mathf.Pow(speed, 2) * fovMultiplier : baseFov, fovSpeed * Time.deltaTime);
        camera.Lens = lens;
        targetPos = camera.Follow.position;
    }
}
