using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelBehaviour : BaseUI
{
    [SerializeField] private Button restartProgressButton;
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Text restartProgressLabel;
    [Header("Audio")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private FloatReference bgmVolume;
    [SerializeField] private FloatReference sfxVolume;
    [SerializeField] private FloatGameEvent bgmVolumeChange;
    [SerializeField] private FloatGameEvent sfxVolumeChange;

    private void Start()
    {
        musicSlider.value = bgmVolume.Value;
        sfxSlider.value = sfxVolume.Value;
    }

    private void OnEnable()
    {
        restartProgressButton.onClick.AddListener(RestartProgress);
        backButton.onClick.AddListener(OnBackButton);
        musicSlider.onValueChanged.AddListener(OnMusicSliderChange);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChange);
    }

    private void OnDisable()
    {
        restartProgressButton.onClick.RemoveListener(RestartProgress);
        backButton.onClick.RemoveListener(OnBackButton);
        musicSlider.onValueChanged.RemoveListener(OnMusicSliderChange);
        sfxSlider.onValueChanged.RemoveListener(OnSFXSliderChange);
    }

    private void RestartProgress()
    {
        restartProgressLabel.text = "Done!";
        restartProgressButton.interactable = false;
        PlayerPrefs.SetInt("actualLevelIndex", 0);
    }

    private void OnBackButton()
    {
        soundToPlay.Raise(audioSFX);
        changeView.Raise(MenuOptions.MainMenu);
    }

    private void OnMusicSliderChange(float value)
    {
        bgmVolumeChange.Raise(Mathf.Log10(value) * 20);
    }

    private void OnSFXSliderChange(float value)
    {
        sfxVolumeChange.Raise(Mathf.Log10(value) * 20);
    }
}
