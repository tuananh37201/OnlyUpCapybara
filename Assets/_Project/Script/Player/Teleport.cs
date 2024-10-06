using UnityEngine;

public class Teleport : MonoBehaviour
{
    public void TeleportGameobject(GameObject gameObject, Vector3 endPosition)
    {
        gameObject.transform.position = new Vector3(endPosition.x, endPosition.y, endPosition.z);
    }
}
