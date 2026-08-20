using UnityEngine;

public class PlayerCameraTarget : MonoBehaviour
{
    private CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        controller = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, controller.height * 0.75f, transform.localPosition.z);
    }
}
