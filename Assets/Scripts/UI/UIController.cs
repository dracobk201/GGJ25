using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<BaseUI> uiPanels;

    private void Start()
    {
        ShowHidePanel(MenuOptions.MainMenu);
    }

    public void ShowHidePanel(MenuOptions targetPanel)
    {
        if (targetPanel.Equals(MenuOptions.None))
        {
            foreach (var uiPanel in uiPanels)
            {
                uiPanel.ShowHideCanvasGroup(false);
            }
        }
        else
        {
            int index = uiPanels.FindIndex(ui => ui.gameObject.name.Equals(targetPanel.ToString()));

            if (index == -1)
            {
                Debug.LogError($"Target Panel, {targetPanel}, doesn't exist");
                return;
            }

            foreach (var uiPanel in uiPanels)
            {
                uiPanel.ShowHideCanvasGroup(false);
            }

            uiPanels[index].ShowHideCanvasGroup(true);
        }
    }
}