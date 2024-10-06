using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCExtension;
using UCExtension.GUI;
using UnityEngine.UI;

public class HomeGUI : BaseGUI
{
    public Button openSettingButton;
    public Button skinShopButton;
    public Button mapButton;

    private void Start()
    {
        openSettingButton.onClick.AddListener(OnClickSettingButton);
        skinShopButton.onClick.AddListener(OnClickSkinShopButton);
        mapButton.onClick.AddListener(OnClickMapButton);
    }

    private void OnClickSettingButton()
    {
        GUIController.Ins.Open<SettingGUI>();
    }

    private void OnClickSkinShopButton()
    {
        GUIController.Ins.Open<SkinShopGUI>();
    }

    private void OnClickMapButton()
    {
        GUIController.Ins.Open<MapGUI>();
    }

    public void OpenCheckPointGUI()
    {
        GUIController.Ins.Open<CheckpointGUI>();
    }

}
