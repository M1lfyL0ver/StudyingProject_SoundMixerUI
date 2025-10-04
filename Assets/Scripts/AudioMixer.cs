using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private string _volumeParameter;
    [SerializeField] private float _minVolumeDB = -80f;
    [SerializeField] private float _volumeThreshold = 0.001f;
    [SerializeField] private float _volumeMultiplier = 20f;

    private float _currentVolume = 1f;
    private bool _isMuted = false;

    private void Start()
    {
        InitializeUI();
    }

    private void OnEnable()
    {
        if (_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    private void OnDisable()
    {
        if (_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }
    }

    private void InitializeUI()
    {
        if (_volumeSlider != null)
        {
            _volumeSlider.SetValueWithoutNotify(_currentVolume);
        }

        ApplyVolumeToMixer(_currentVolume);
    }

    public void SetVolume(float volume)
    {
        _currentVolume = Mathf.Clamp01(volume);

        if (!_isMuted)
        {
            ApplyVolumeToMixer(_currentVolume);
        }
    }

    public void ToggleMute(bool muted)
    {
        _isMuted = muted;

        if (_isMuted)
        {
            _audioMixer.SetFloat(_volumeParameter, _minVolumeDB);
        }
        else
        {
            ApplyVolumeToMixer(_currentVolume);
        }

        if (_volumeSlider != null)
        {
            _volumeSlider.interactable = !_isMuted;
        }
    }

    private void ApplyVolumeToMixer(float volume)
    {
        float volumeDB = volume > _volumeThreshold ?
            Mathf.Log10(volume) * _volumeMultiplier :
            _minVolumeDB;

        _audioMixer.SetFloat(_volumeParameter, volumeDB);
    }

    public void SetVolumeParameter(string parameter)
    {
        _volumeParameter = parameter;
    }
}