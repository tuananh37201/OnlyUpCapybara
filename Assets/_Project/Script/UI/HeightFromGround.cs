using UnityEngine;
using UnityEngine.UI;

public class HeightFromGround : MonoBehaviour
{
    public Text heightText;

    private float height;

    void Update()
    {
        height = (int)GameManager.Ins.player.transform.position.y;
        heightText.text = height + "m";
    }
}
