using UnityEngine;

[System.Serializable]
public class VFX
{
    public string Name;
    public GameObject vfxPrefab;
    public float destroyTime = 1f;
}

public class VFXManager : MonoBehaviour
{
    public VFX[] itemVFXs, playerVFXs;

    public static VFXManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnVFX(string name, Vector3 position)
    {
        VFX vfx = System.Array.Find(itemVFXs, vfx => vfx.Name == name);
        if (vfx == null)
        {
            Debug.LogWarning("VFX: " + name + " not found!");
            return;
        }
        else
        {
            GameObject vfxObj = Instantiate(vfx.vfxPrefab, position, Quaternion.identity);
            Destroy(vfxObj, vfx.destroyTime);
        }
    }

    public void SpawnPlayerVFX(string name, Vector3 position)
    {
        VFX vfx = System.Array.Find(playerVFXs, vfx => vfx.Name == name);
        if (vfx == null)
        {
            Debug.LogWarning("VFX: " + name + " not found!");
            return;
        }
        else
        {
            GameObject vfxObj = Instantiate(vfx.vfxPrefab, position, Quaternion.identity);
            Destroy(vfxObj, vfx.destroyTime);
        }
    }
}
