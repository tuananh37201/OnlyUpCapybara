using UnityEngine;

public class WindZoneArea : MonoBehaviour
{
    [SerializeField] private float blowForce;
    [SerializeField] private Direction directionBlow;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.AddForce(directionBlow.ToVector3() * blowForce, ForceMode.Impulse);
            }
        }
    }
}