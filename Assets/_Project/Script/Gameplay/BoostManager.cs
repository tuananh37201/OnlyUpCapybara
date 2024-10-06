using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoostManager : MonoBehaviour
{

    [Space]
    public Sprite doubleJumpSprite;
    public Sprite flySprite;

    [Space]
    public Button flyBoostButton;
    public Button doubleJumpBoostButton;

    private ToggleButton flyToggleButton;
    private ToggleButton doubleJumpToggleButton;

    [Space]
    private bool isBoosting;
    //public bool isJettpack;

    [Space]
    public float duration;

    //private JettpackBoost jettpackBoost;

    public static Action<Sprite, float> OnStartBooter;

    private void Start()
    {
        //jettpackBoost = GetComponent<JettpackBoost>();

        flyBoostButton.onClick.AddListener(() => ActivateBoost("fly"));
        doubleJumpBoostButton.onClick.AddListener(() => ActivateBoost("doubleJump"));
        //jettpackBoost.getNowButton.onClick.AddListener(() => ActivateBoost("jettpack"));


        flyToggleButton = flyBoostButton.GetComponent<ToggleButton>();
        doubleJumpToggleButton = doubleJumpBoostButton.GetComponent<ToggleButton>();
    }

    private void Update()
    {
        //if (!isBoosting && !jettpackBoost.isFallDetectionDisabled)
        //{
        //    //jettpackBoost.CheckFallingStatus();
        //    if (jettpackBoost.isFalling && jettpackBoost.fallTime >= jettpackBoost.fallThreshold)
        //    {
        //        if (GameManager.Ins.player.isDoubleJumpActicved)
        //        {
        //            DeactivateDoubleJumpBoost();
        //        }
        //        jettpackBoost.ActivateJettpackBoostPanel();
        //    }
        //}
    }

    private void ActivateBoost(string boostType)
    {
        if (!isBoosting)
        {
            isBoosting = true;

            Sprite icon = flySprite;
            if (boostType == "fly")
            {
                GameManager.Ins.player.isFlyActicved = true;
                flyToggleButton.SetUp(true);
                icon = flySprite;
                StartCoroutine(DeactivateFlyBoostAfterDuration());
            }
            else if (boostType == "doubleJump")
            {
                GameManager.Ins.player.isDoubleJumpActicved = true;
                doubleJumpToggleButton.SetUp(true);
                icon = doubleJumpSprite;
                StartCoroutine(DeactivateDoubleJumpBoostAfterDuration());

            }
            //else if (boostType == "jettpack")
            //{
            //    GameManager.Ins.player.isFlyActicved = true;
            //    flyBoostButton.interactable = false;
            //    icon = flySprite;
            //    StartCoroutine(DeactivateFlyBoostAfterDuration());
            //    jettpackBoost.OnButtonClick();
            //}

            OnStartBooter?.Invoke(icon, duration);
            flyBoostButton.interactable = false;
            doubleJumpBoostButton.interactable = false;
        }
    }

    private IEnumerator DeactivateFlyBoostAfterDuration()
    {
        float remainingTime = duration;
        while (remainingTime > 0)
        {
            while (GameManager.Ins.player.canMove)
            {
                yield return null;
            }

            remainingTime -= Time.deltaTime;
            yield return null;
        }

        DeactivateFlyBoost();
    }

    private IEnumerator DeactivateDoubleJumpBoostAfterDuration()
    {
        float remainingTime = duration;
        while (remainingTime > 0)
        {
            while (GameManager.Ins.player.canMove)
            {
                yield return null;
            }

            remainingTime -= Time.deltaTime;
            yield return null;
        }

        DeactivateDoubleJumpBoost();
    }

    private void DeactivateFlyBoost()
    {
        isBoosting = false;
        GameManager.Ins.player.isFlyActicved = false;
        flyBoostButton.interactable = true;
        doubleJumpBoostButton.interactable = true;
        flyToggleButton.SetUp(false);
        //jettpackBoost.isFallDetectionDisabled = false; // Bắt đầu kiểm tra rơi lại
    }

    private void DeactivateDoubleJumpBoost()
    {
        isBoosting = false;
        GameManager.Ins.player.isDoubleJumpActicved = false;
        doubleJumpBoostButton.interactable = true;
        flyBoostButton.interactable = true;
        doubleJumpToggleButton.SetUp(false);
    }

    private void DeactivateAllBoosts()
    {
        if (isBoosting)
        {
            StopAllCoroutines();
            DeactivateFlyBoost();
            DeactivateDoubleJumpBoost();
        }
    }
}