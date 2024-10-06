using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    [SerializeField] private float destroyTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyEffectObject", destroyTime);
    }

    private void DestroyEffectObject()
    {
        Destroy(gameObject);
    }
}
