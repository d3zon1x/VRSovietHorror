using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public static VoiceManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource voiceSource;

    [Header("Voice Lines")]
    public AudioClip firstScreamer;
    public AudioClip findElectro;
    public AudioClip missingFuse;
    public AudioClip lockedDoor;
    public AudioClip findingKey;
    public AudioClip findingFuse;
    public AudioClip lightsOn;

    private void Awake()
    {
        Instance = this;

        if (voiceSource == null)
            voiceSource = GetComponent<AudioSource>();
    }

    public void PlayVoice(AudioClip clip)
    {
        if (clip == null || voiceSource == null)
            return;

        voiceSource.Stop();
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    public void PlayFirstScreamer()
    {
        PlayVoice(firstScreamer);
    }

    public void PlayFindElectro()
    {
        PlayVoice(findElectro);
    }

    public void PlayMissingFuse()
    {
        PlayVoice(missingFuse);
    }

    public void PlayLockedDoor()
    {
        PlayVoice(lockedDoor);
    }

    public void PlayFindingKey()
    {
        PlayVoice(findingKey);
    }

    public void PlayFindingFuse()
    {
        PlayVoice(findingFuse);
    }

    public void PlayLightsOn()
    {
        PlayVoice(lightsOn);
    }
}