using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boosters : MonoBehaviour
{
    public JettpackBoost flyBoostWhenFall;

    [Header("UI")]
    public Image coolDownCanvas;
    public Sprite doubleJumpBooster, flyBooster;
    public Image boosterIcon;
    public Image coolDownBar;
    public TMP_Text coolDownText;

    private bool canUse = true;
    private float currentCooldown;
    private float duration = 10f;

    private bool isDoubleJumpActive = false;
    private bool isFlyActive = false;

    public Button flyBoostButton;
    public Button doubleJumpBoostButton;

    private ToggleButton flyToggleButton;
    private ToggleButton doubleJumpToggleButton;


    private void Start()
    {
        flyBoostButton.onClick.AddListener(() => ActivateFlyBooster(duration));
        doubleJumpBoostButton.onClick.AddListener(() => ActivateDoubleJumpBooster(duration));

        flyToggleButton = flyBoostButton.GetComponent<ToggleButton>();
        doubleJumpToggleButton = doubleJumpBoostButton.GetComponent<ToggleButton>();
    }


    private void Update()
    {
        if (IsBoosting())
        {
            coolDownCanvas.gameObject.SetActive(true);

            currentCooldown -= Time.deltaTime;

            coolDownText.text = "" + (int)currentCooldown;
            coolDownBar.fillAmount -= 1.0f / duration * Time.deltaTime;
        }
        else
        {
            coolDownCanvas.gameObject.SetActive(false);
            currentCooldown = 0;
        }
    }

    public void ActivateDoubleJumpBooster(float duration)
    {
        if (canUse && !isFlyActive)
        {
            doubleJumpBoostButton.interactable = false;
            this.duration = duration;
            currentCooldown = duration;
            isDoubleJumpActive = true;
            StartCoroutine(DoubleJumpBoosterCoroutine(duration));
        }
    }

    public void ActivateFlyBooster(float duration)
    {
        if (canUse && !isDoubleJumpActive)
        {
            flyBoostButton.interactable = false;
            this.duration = duration;
            currentCooldown = duration;
            isFlyActive = true;
            StartCoroutine(FlyBoosterCoroutine(duration));
        }
    }

    private IEnumerator DoubleJumpBoosterCoroutine(float duration)
    {
        GameManager.Ins.player.isDoubleJumpActicved = true;
        canUse = false;
        doubleJumpToggleButton.SetUp(true); // Chuyển thành ActiveSprite

        yield return new WaitForSeconds(duration);

        GameManager.Ins.player.isDoubleJumpActicved = false;
        isDoubleJumpActive = false;
        canUse = true;
        doubleJumpToggleButton.SetUp(false); // Chuyển thành DisableSprite
        doubleJumpBoostButton.interactable = true;
    }

    private IEnumerator FlyBoosterCoroutine(float duration)
    {
        flyBoostWhenFall.canUse = false;
        GameManager.Ins.player.isFlyActicved = true;
        canUse = false;
        flyToggleButton.SetUp(true); // Chuyển thành ActiveSprite

        yield return new WaitForSeconds(duration);

        flyBoostWhenFall.canUse = true;
        GameManager.Ins.player.isFlyActicved = false;
        isFlyActive = false;
        canUse = true;
        flyToggleButton.SetUp(false); // Chuyển thành DisableSprite
        flyBoostButton.interactable = true;
    }


    private bool IsBoosting()
    {
        if (isDoubleJumpActive)
        {
            boosterIcon.sprite = doubleJumpBooster;
            return true;
        }
        else if (isFlyActive)
        {
            boosterIcon.sprite = flyBooster;
            return true;
        }
        else
        {
            coolDownBar.fillAmount = 1;
            boosterIcon.sprite = null;
            return false;
        }
    }
}
