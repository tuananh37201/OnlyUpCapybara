using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public Direction rotateDirection;
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * rotateDirection.ToVector3());
    }

}
