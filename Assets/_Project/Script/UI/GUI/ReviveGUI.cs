using System;
using UCExtension;
using UCExtension.GUI;
using UnityEngine.UI;
using UnityEngine;

public class ReviveGUI : BasePopupGUI
{
    public Button reviveButton;

    public float reviveTime = 5f;

    public GameObject lastTriggeredCheckpoint;

    private void Start()
    {
        gameObject.SetActive(false);
        reviveButton.onClick.AddListener(OnReviveButtonClick);
        
    }

    private void OnReviveButtonClick()
    {
        GameManager.Ins.player.animator.SetTrigger("Revive");
        gameObject.SetActive(false);
        GameManager.Ins.player.canJump = true;
        GameManager.Ins.player.canMove = true;

        GameManager.Ins.player.transform.position = lastTriggeredCheckpoint.transform.position;
    }

    public override void Close()
    {
        base.Close();
    }
}
