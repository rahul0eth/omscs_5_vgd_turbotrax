using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float movSmoothing;
    public float rotSmoothing;

    void Start()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, movSmoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotSmoothing);
    }
}
