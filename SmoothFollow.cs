using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed; //Lower = Slower and Smoother, Higher = Faster and Sharper

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; //Set a desired position offset from the target
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); //Interpolate between the current position and the desired position
    }
}
