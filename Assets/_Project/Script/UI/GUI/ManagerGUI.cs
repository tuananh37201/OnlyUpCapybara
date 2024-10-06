using UnityEngine.UI;
using UCExtension.GUI;

public class ManagerGUI : BaseGUI
{
    private void Start()
    {
        GUIController.Ins.Open<HomeGUI>();
    }
}
