using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (_audioSource != null && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
}