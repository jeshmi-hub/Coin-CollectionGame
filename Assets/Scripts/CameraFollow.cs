using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float mouseSensitivity = 3f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Rotate the camera based on mouse input
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX += Input.GetAxis("Mouse Y") * -1 * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -45f, 89f); // Clamp vertical rotation

        // Calculate the desired rotation based on the car's position
        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 targetPosition = target.position - (targetRotation * Vector3.forward * 10f);

        // Update the camera's position and rotation
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }


}
