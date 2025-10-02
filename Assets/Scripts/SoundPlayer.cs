using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip soundClip;
    private Button _button;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();

        _audioSource.clip = soundClip;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnButtonClick()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
    }
}