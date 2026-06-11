using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FuseBoxSlot : MonoBehaviour
{
    public GameObject fuseInsertedVisual;
    public Light[] lightsToTurnOn;
    public float[] targetIntensities;
    public AudioSource turnOnElectricity;

    [Header("Final Setup")]
    public FinalDoorHandleTouch finalDoorHandleTouch;
    public FinalDoorCloser finalDoorCloser;
    public float finalDoorCloseDelay = 1f;

    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated) return;
        if (!other.CompareTag("Fuse")) return;

        isActivated = true;

        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.enabled = false;
        }

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        other.gameObject.SetActive(false);

        if (fuseInsertedVisual != null)
        {
            fuseInsertedVisual.SetActive(true);
        }

        if (turnOnElectricity != null)
            turnOnElectricity.Play();

        for (int i = 0; i < lightsToTurnOn.Length; i++)
        {
            if (lightsToTurnOn[i] != null)
            {
                lightsToTurnOn[i].enabled = true;

                if (targetIntensities != null && i < targetIntensities.Length)
                {
                    lightsToTurnOn[i].intensity = targetIntensities[i];
                }
            }
        }

        if (VoiceManager.Instance != null)
            VoiceManager.Instance.PlayLightsOn();

        Invoke(nameof(PrepareFinalDoor), finalDoorCloseDelay);

        Debug.Log("Fuse inserted, lights turned on");
    }

    private void PrepareFinalDoor()
    {
        if (finalDoorCloser != null)
            finalDoorCloser.CloseDoorOnly();

        if (finalDoorHandleTouch != null)
            finalDoorHandleTouch.EnableFinal();

        Debug.Log("Final door prepared");
    }
}