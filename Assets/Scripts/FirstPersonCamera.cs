using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // The object the camera should follow
    public Transform target;

    // The speed at which the camera should follow the target
    public float followSpeed = 10f;

    // The offset of the camera from the target
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}