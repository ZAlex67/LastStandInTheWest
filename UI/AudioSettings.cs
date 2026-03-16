using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    private const float MinVolume = -80f;
    private const float MaxVolume = 0f;

    private const string MusicVolume = "MusicVolume";
    private const string MasterVolume = "MasterVolume";

    [SerializeField] private AudioMixerGroup _mixer;

    public void ToggleMusic(bool enabled)
    {
        float volume = enabled ? MaxVolume : MinVolume;

        _mixer.audioMixer.SetFloat(MusicVolume, volume);
    }

    public void ChangeMasterVolume(float value)
    {
        float volume = Mathf.Lerp(MinVolume, MaxVolume, value);

        _mixer.audioMixer.SetFloat(MasterVolume, volume);
    }
}