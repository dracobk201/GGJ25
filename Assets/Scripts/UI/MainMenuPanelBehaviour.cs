using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanelBehaviour : MonoBehaviour
{
    [SerializeField] private Button restartProgressButton;
    [SerializeField] private TMP_Text restartProgressLabel;
    [SerializeField] private TMP_Text prestigeLabel;
    private int timesBeated;

    private void Start()
    {
        timesBeated = PlayerPrefs.GetInt("timesBeated", 0);
    }

    public void StartGame () 
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        prestigeLabel.text = timesBeated > 0 ? $"Prestige: {timesBeated}" : "";
    }

    public void RestartProgress()
    {
        restartProgressLabel.text = "Done!";
        restartProgressButton.interactable = false;
        PlayerPrefs.SetInt("actualLevelIndex", 0);
    }
}
