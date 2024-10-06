using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public GameObject[] skinPrefabs;
    private GameObject selectedSkin;
    public GameObject skinContainer;

    public void SelectSkin(int skinIndex)
    {
        if (skinIndex >= 0 && skinIndex < skinPrefabs.Length)
        {
            selectedSkin = skinPrefabs[skinIndex];
            ChangeSkin();
        }
        else
        {
            Debug.LogError("Skin index out of range.");
        }
    }

    private void ChangeSkin()
    {
        GameObject skin = GameObject.FindWithTag("Skin");

        if (skin != null)
        {
            GameObject newSkin = Instantiate(selectedSkin);
            newSkin.transform.SetPositionAndRotation(skin.transform.position, skin.transform.rotation);
            newSkin.transform.parent = skinContainer.transform;

            Destroy(skin);
        }
        else
        {
            Debug.LogError("Player object not found.");
        }
    }

}
