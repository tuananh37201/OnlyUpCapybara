using UCExtension.GUI;
using UnityEngine;

public class JettpackGUI : BasePopupGUI
{
    public override void Close()
    {
        Time.timeScale = 1.0f;
        base.Close();
    }
}
