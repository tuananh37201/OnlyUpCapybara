using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEffect", menuName = "ScriptableObject/PlayerEffect", order = 1)]
public class PlayerEffect : ScriptableObject
{
    public GameObject moveOnGroundEffect;
    public GameObject jumpEffect;
}
