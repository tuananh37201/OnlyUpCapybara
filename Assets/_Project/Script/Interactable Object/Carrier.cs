using UnityEngine;

public class Carrier : MonoBehaviour
{
    // Gán tag "Player" cho đối tượng player trong Unity Editor
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gán đối tượng player làm con của gameobject này
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Bỏ đối tượng player khỏi gameobject này
            other.transform.SetParent(null);
        }
    }
}
