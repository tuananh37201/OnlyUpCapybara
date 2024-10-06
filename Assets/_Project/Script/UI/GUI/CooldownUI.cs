using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public GameObject coolDownGUI;
    public Image boostIcon;
    public Image cooldownFillBar;
    public Text cooldownRemainingText;

    void Start()
    {
        coolDownGUI.gameObject.SetActive(false);
        BoostManager.OnStartBooter += StartBooster;
    }


    void StartBooster(Sprite icon, float duration)
    {
        boostIcon.sprite = icon;
        coolDownGUI.gameObject.SetActive(true);
        StartCoroutine(CooldownRoutine(duration));
    }

    private IEnumerator CooldownRoutine(float time)
    {
        float remainingTime = time;

        while (remainingTime > 0)
        {
            cooldownRemainingText.text = remainingTime.ToString("F0") + "s";

            cooldownFillBar.fillAmount = remainingTime / time;

            if (remainingTime <= 3)
            {
                cooldownRemainingText.color = Color.red;
            }
            else
            {
                cooldownRemainingText.color = Color.white;
            }

            while (!GameManager.Ins.player.canMove)
            {
                yield return null;
            }

            remainingTime -= Time.deltaTime;

            yield return null;
        }

        cooldownRemainingText.text = "0s";
        cooldownFillBar.fillAmount = 0;
        coolDownGUI.gameObject.SetActive(false);
    }

}
