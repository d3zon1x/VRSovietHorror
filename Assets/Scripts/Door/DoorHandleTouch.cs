using UnityEngine;

public class DoorHandleTouch : MonoBehaviour
{
    public SimpleDoor door;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTER: " + other.name + " | tag=" + other.tag);

        if (other.CompareTag("PlayerHand"))
        {
            Debug.Log("PLAYER HAND DETECTED");

            if (door == null)
            {
                Debug.LogError("Door reference is NULL");
                return;
            }

            door.OpenDoor();
        }
    }
}