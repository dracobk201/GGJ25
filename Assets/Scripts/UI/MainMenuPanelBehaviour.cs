using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanelBehaviour : BaseUI
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private TMP_Text prestigeLabel;
    private int timesBeated;
    private int bubblesPopped;
    private float timer;

    private void OnEnable()
    {
        startGameButton.onClick.AddListener(StartGame);
        creditsButton.onClick.AddListener(OnCreditsButton);
        optionsButton.onClick.AddListener(OnOptionsButton);
    }

    private void OnDisable()
    {
        startGameButton.onClick.RemoveListener(StartGame);
        creditsButton.onClick.RemoveListener(OnCreditsButton);
        optionsButton.onClick.RemoveListener(OnOptionsButton);
    }

    private void Start()
    {
        timesBeated = PlayerPrefs.GetInt("timesBeated", 0);
        prestigeLabel.text = timesBeated > 0 ? $"Prestige: {timesBeated}" : "";
    }

    public void StartGame() 
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && panelCanvasGroup.alpha < 1)
        {
            bubblesPopped = 0;
            panelCanvasGroup.alpha = 1;
            panelCanvasGroup.interactable = true;
        }
    }

    private void OnCreditsButton()
    {
        soundToPlay.Raise(audioSFX);
        changeView.Raise(MenuOptions.Credits);
    }

    private void OnOptionsButton()
    {
        soundToPlay.Raise(audioSFX);
        changeView.Raise(MenuOptions.Options);
    }

    public void ChangeBubblesCounter(int value)
    {
        timer = 3f;
        bubblesPopped += value;
        float tValue = (float)bubblesPopped / 20;
        panelCanvasGroup.alpha = Mathf.Lerp(1, 0.1f, tValue);
        if (panelCanvasGroup.alpha == 0.1f)
        {
            panelCanvasGroup.interactable = false;
        }
    }
}
