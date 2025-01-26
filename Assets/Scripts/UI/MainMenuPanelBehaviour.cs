using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanelBehaviour : MonoBehaviour
{
    [SerializeField] private Button restartProgressButton;
    [SerializeField] private TMP_Text restartProgressLabel;

    public void StartGame () 
    {
        SceneManager.LoadScene(0);
    }

    public void RestartProgress()
    {
        restartProgressLabel.text = "Done!";
        restartProgressButton.interactable = false;
        PlayerPrefs.SetInt("actualLevelIndex", 0);
    }
}
