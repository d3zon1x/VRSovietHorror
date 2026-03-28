using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private bool pickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;
        if (!other.CompareTag("PlayerHand")) return;

        pickedUp = true;

        if (GameProgress.Instance != null)
        {
            GameProgress.Instance.hasHallKey = true;
            Debug.Log("Hall key picked up");
        }
        else
        {
            Debug.LogError("GameProgress.Instance is NULL");
        }

        transform.parent.gameObject.SetActive(false);
    }
}