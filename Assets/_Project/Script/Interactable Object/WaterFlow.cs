using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Direction flowDirection;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.AddForce(flowDirection.ToVector3() * force, ForceMode.Impulse);
            }
        }
    }
}