using ScriptableObjectArchitecture;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup;
    public MenuOptionsGameEvent changeView;
    public AudioClip audioSFX;
    public AudioClipGameEvent soundToPlay;

    public void ShowHideCanvasGroup(bool toShow)
    {
        panelCanvasGroup.alpha = toShow ? 1 : 0;
        panelCanvasGroup.blocksRaycasts = toShow;
        panelCanvasGroup.interactable = toShow;
    }
}