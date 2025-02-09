using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditsPanelBehaviour : BaseUI
{
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Text creditsLabel;

    const string PROGRAMMING = "<a href='https://www.ericksubero.com' target='_blank'>Erick Subero</a>\n";
    const string ART = "<a href='https://www.instagram.com/kyuurocks/' target='_blank'>Kyuurocks</a>\n";
    const string SOUND = "<a href='https://open.spotify.com/artist/4TUOvfgACDqpVK4yoMeuIV 'target='_blank'>Malbanyat</a>\n";
    const string MUSIC = "from <a href='https://elements.envato.com/' target='_blank'>Envato</a>\n";

    private void Start()
    {
        creditsLabel.text = "Programming \n";
        creditsLabel.text += PROGRAMMING;
        creditsLabel.text += "\n";
        creditsLabel.text += "ART \n";
        creditsLabel.text += ART;
        creditsLabel.text += "\n";
        creditsLabel.text += "Music \n";
        creditsLabel.text += MUSIC;
        creditsLabel.text += "\n";
        creditsLabel.text += "Sound Effects \n";
        creditsLabel.text += SOUND;
        creditsLabel.text += "\n";
        creditsLabel.text += "Game Design \n";
        creditsLabel.text += PROGRAMMING;
        creditsLabel.text += ART;
    }

    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackButton);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(OnBackButton);
    }

    private void OnBackButton()
    {
        soundToPlay.Raise(audioSFX);
        changeView.Raise(MenuOptions.MainMenu);
    }

}
