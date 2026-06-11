using UnityEngine;

public class VoiceTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip voiceLine;
    [SerializeField] private bool playOnce = true;
    [SerializeField] private float delay = 0f;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (playOnce && hasPlayed) return;
        if (!other.CompareTag("Player")) return;

        hasPlayed = true;

        if (delay > 0f)
            Invoke(nameof(Play), delay);
        else
            Play();
    }

    private void Play()
    {
        if (VoiceManager.Instance != null)
            VoiceManager.Instance.PlayVoice(voiceLine);
    }
}