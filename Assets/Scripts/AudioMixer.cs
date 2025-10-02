using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [Header("Audio Mixer Reference")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Slider References")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider buttonSlider;

    [Header("Mixer Parameters Names")]
    [SerializeField] private string masterVolumeParam = "Master";
    [SerializeField] private string musicVolumeParam = "Music";
    [SerializeField] private string buttonsVolumeParam = "Buttons";

    private void Start()
    {
        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetButtonVolume(buttonSlider.value);
    }

    private void OnEnable()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        buttonSlider.onValueChanged.AddListener(SetButtonVolume);
    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveAllListeners();
    }

    public void SetMasterVolume(float volume)
    {
        SetVolume(masterVolumeParam, volume);
    }

    public void SetMusicVolume(float volume)
    {
        SetVolume(musicVolumeParam, volume);
    }

    public void SetButtonVolume(float volume)
    {
        SetVolume(buttonsVolumeParam, volume);
    }

    private void SetVolume(string parameterName, float volume)
    {
        float volumeDB = volume > 0.001f ? Mathf.Log10(volume) * 20 : -80f;
        audioMixer.SetFloat(parameterName, volumeDB);
    }
}