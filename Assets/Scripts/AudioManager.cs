using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private FloatReference bgmVolume;
    [SerializeField] private FloatReference sfxVolume;
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    const string BGM_TAG = "MusicVolume";
    const string SFX_TAG = "SFXVolume";

    private void Awake()
    {
        bgmVolume.Value = Mathf.Pow(10, PlayerPrefs.GetFloat(BGM_TAG, 1) / 20); 
        sfxVolume.Value = Mathf.Pow(10, PlayerPrefs.GetFloat(SFX_TAG, 1) / 20); 
    }

    private void Start()
    {
        audioMixer.SetFloat(BGM_TAG, bgmVolume.Value);
        audioMixer.SetFloat(SFX_TAG, sfxVolume.Value);
    }

    public void MusicVolumeRefreshed(float value)
    {
        bgmVolume.Value = value;
        audioMixer.SetFloat(BGM_TAG, value);
        PlayerPrefs.SetFloat(BGM_TAG, value);
    }

    public void SFXVolumeRefreshed(float value)
    {
        sfxVolume.Value = value;
        audioMixer.SetFloat(SFX_TAG, value);
        PlayerPrefs.SetFloat(SFX_TAG, value);
    }

    public void PlaySFX(AudioClip targetAudio)
    {
        float randomPitch = Random.Range(.95f, 1.05f);
        sfxAudioSource.pitch = randomPitch;
        sfxAudioSource.clip = targetAudio;
        sfxAudioSource.loop = false;
        sfxAudioSource.pitch = Random.Range(0.4f, 1f);
        sfxAudioSource.Play();
    }

    public void PlayBGM(AudioClip targetAudio)
    {
        bgmAudioSource.clip = targetAudio;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
}