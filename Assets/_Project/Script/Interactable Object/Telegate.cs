using UnityEngine;

public class Telegate : MonoBehaviour
{
    public Teleport teleport;
    public Transform endPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (endPosition != null)
            {
                teleport.TeleportGameobject(other.gameObject, endPosition.position);
            }
        }
    }
}
