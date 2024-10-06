using UnityEngine;

public class DisappearObject : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact(other.gameObject);
        }
    }

}
