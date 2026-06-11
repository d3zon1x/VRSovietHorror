using UnityEngine;

public class LockedDoorHandle : MonoBehaviour
{
    public SimpleDoor door;

    private bool opened = false;
    private bool lockedVoicePlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;
        if (!other.CompareTag("PlayerHand")) return;
        if (GameProgress.Instance == null) return;

        if (!GameProgress.Instance.hasHallKey)
        {
            if (!lockedVoicePlayed)
            {
                lockedVoicePlayed = true;

                if (VoiceManager.Instance != null)
                    VoiceManager.Instance.PlayLockedDoor();
            }

            return;
        }

        opened = true;
        door.OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
            opened = false;
    }
}