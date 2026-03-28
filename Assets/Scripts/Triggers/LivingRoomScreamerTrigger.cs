using System.Collections;
using UnityEngine;

public class LivingRoomScreamerTrigger : MonoBehaviour
{
    public GameObject screamerLivingRoom;
    public AudioSource scareAudio;
    public float visibleTime = 1.5f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        StartCoroutine(PlayScreamer());
    }

    private IEnumerator PlayScreamer()
    {
        if (screamerLivingRoom != null)
            screamerLivingRoom.SetActive(true);

        if (scareAudio != null)
            scareAudio.Play();

        yield return new WaitForSeconds(visibleTime);

        if (screamerLivingRoom != null)
            screamerLivingRoom.SetActive(false);
    }
}
