using UnityEngine;

public class Bomb : MonoBehaviour, IInteractable
{
    public float bounceForce = 15f;
    public float inactiveTime = 5f; // Thời gian không hoạt động

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Interact(collision.gameObject);
        }
    }

    public void Interact(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
        Explode();
    }

    private void Explode()
    {
        VFXManager.Instance.SpawnVFX("Explosion", transform.position);
        gameObject.SetActive(false);
        Invoke("Reactivate", inactiveTime);
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
}