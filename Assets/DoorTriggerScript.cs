using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    public GameObject door;
    public string requiredTag = "Key";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            door.SetActive(false);
        }
    }
}