using UnityEngine;

public class LockedDoorHandle : MonoBehaviour
{
    public SimpleDoor door;

    private bool opened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (opened) return;
        if (!other.CompareTag("PlayerHand")) return;
        if (GameProgress.Instance == null) return;
        if (!GameProgress.Instance.hasHallKey) return;

        opened = true;
        door.OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
            opened = false;
    }
}