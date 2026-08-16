using UnityEngine;

public class CameraTargetPosition : MonoBehaviour
{
    private CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, controller.height * 0.75f, transform.localPosition.z);
    }
}
