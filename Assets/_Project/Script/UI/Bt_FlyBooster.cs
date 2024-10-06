using UnityEngine;

public class Bt_FlyBooster : MonoBehaviour
{
    public Boosters boosters;
    public float boosterDuration = 5f;

    void Start()
    {

    }

    public void OnButtonClick()
    {
        boosters.ActivateFlyBooster(boosterDuration);
    }
}
