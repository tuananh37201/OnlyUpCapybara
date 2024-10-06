using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offSet;
    private Vector3 velociy = Vector3.zero;

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offSet;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velociy, smoothTime);
        }
    }
}
