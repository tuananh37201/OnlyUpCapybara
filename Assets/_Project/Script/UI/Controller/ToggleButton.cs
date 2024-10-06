using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] List<ToggleConfigs> configs;

    Button button;

    bool isActive = false;

    public Action<bool> OnToggle;

    public static UnityAction<string, bool> OnToggleCallback;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void SetUp(bool isActive)
    {
        this.isActive = isActive;
        ResetApearance();
    }
    void OnClick()
    {
        isActive = !isActive;
        OnToggle?.Invoke(isActive);
        OnToggleCallback?.Invoke(gameObject.name, isActive);
        ResetApearance();
    }
    void ResetApearance()
    {
        foreach (var item in configs)
        {
            item.MainImage.sprite = isActive ? item.ActiveSprite : item.DisableSprite;
            if (item.SetNativeSize)
            {
                item.MainImage.SetNativeSize();
            }
            if (item.HasTitle)
            {
                item.TitleText.text = isActive ? item.ActiveTitle : item.DisableTitle;
            }
        }
    }
}

[Serializable]
public class ToggleConfigs
{
    [FoldoutGroup("Image")]
    public Image MainImage;

    [ShowIf("MainImage")]
    [FoldoutGroup("Image")]
    public bool SetNativeSize;

    [ShowIf("MainImage")]
    [FoldoutGroup("Image")]
    public Sprite DisableSprite;

    [ShowIf("MainImage")]
    [FoldoutGroup("Image")]
    public Sprite ActiveSprite;

    public bool HasTitle;

    [FoldoutGroup("Title")]
    [ShowIf("HasTitle")]
    public Text TitleText;

    [ShowIf("HasTitle")]
    [FoldoutGroup("Title")]
    public string DisableTitle;

    [ShowIf("HasTitle")]
    [FoldoutGroup("Title")]
    public string ActiveTitle;
}