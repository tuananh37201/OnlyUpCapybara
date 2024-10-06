using UnityEngine;

public class Bt_DoubleJumpBooster : MonoBehaviour
{
    public Boosters boosters;
    public float boosterDuration = 5f;

    public void OnButtonClick()
    {
        boosters.ActivateDoubleJumpBooster(boosterDuration);
    }
}
