using UnityEngine;

public class Swiper : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Direction rotateDirection;

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(rotateDirection.ToVector3(), rotateSpeed * Time.deltaTime);
    }
}