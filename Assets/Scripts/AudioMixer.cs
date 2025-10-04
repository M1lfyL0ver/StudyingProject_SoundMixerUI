using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private string volumeParameter;
    [SerializeField] private float minVolumeDB = -80f;
    [SerializeField] private float volumeThreshold = 0.001f;

    private float currentVolume = 1f;
    private bool isMuted = false;

    private void Start()
    {
        InitializeUI();
    }

    private void OnEnable()
    {
        if (volumeSlider != null)
            volumeSlider.onValueChanged.AddListener(SetVolume);

        if (muteToggle != null)
            muteToggle.onValueChanged.AddListener(ToggleMute);
    }

    private void OnDisable()
    {
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(SetVolume);

        if (muteToggle != null)
            muteToggle.onValueChanged.RemoveListener(ToggleMute);
    }

    private void InitializeUI()
    {
        if (volumeSlider != null)
        {
            volumeSlider.SetValueWithoutNotify(currentVolume);
        }

        if (muteToggle != null)
        {
            muteToggle.SetIsOnWithoutNotify(isMuted);
        }

        ApplyVolumeToMixer(currentVolume);
    }

    public void SetVolume(float volume)
    {
        currentVolume = Mathf.Clamp01(volume);

        if (!isMuted)
        {
            ApplyVolumeToMixer(currentVolume);
        }

        if (muteToggle != null && currentVolume <= volumeThreshold && !isMuted)
        {
            muteToggle.SetIsOnWithoutNotify(true);
            isMuted = true;
        }
    }

    public void ToggleMute(bool muted)
    {
        isMuted = muted;

        if (isMuted)
        {
            audioMixer.SetFloat(volumeParameter, minVolumeDB);
        }
        else
        {
            ApplyVolumeToMixer(currentVolume);
        }

        if (volumeSlider != null)
        {
            volumeSlider.interactable = !isMuted;
        }
    }

    private void ApplyVolumeToMixer(float volume)
    {
        float volumeDB = volume > volumeThreshold ?
            Mathf.Log10(volume) * 20f :
            minVolumeDB;

        audioMixer.SetFloat(volumeParameter, volumeDB);
    }

    public void Mute() => ToggleMute(true);
    public void Unmute() => ToggleMute(false);
    public void SetVolumeParameter(string parameter) => volumeParameter = parameter;
}