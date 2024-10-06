using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float force;
    private Vector3 bounceDirection;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.collider.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                bounceDirection = transform.position - collision.collider.transform.position;
                playerRb.AddExplosionForce(force, transform.position, 100f);
            }
        }
    }
}