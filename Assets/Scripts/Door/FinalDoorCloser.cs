using System.Collections;
using UnityEngine;

public class FinalDoorCloser : MonoBehaviour
{
    public Transform doorTransform;
    public float closeSpeed = 8f;
    public AudioSource audioSource;
    public AudioClip closeSound;

    private Quaternion closedRotation;
    private Coroutine closeRoutine;

    private void Start()
    {
        if (doorTransform != null)
            closedRotation = doorTransform.localRotation;
    }

    public void CloseDoorOnly()
    {
        if (doorTransform == null)
            return;

        if (closeRoutine != null)
            StopCoroutine(closeRoutine);

        if (audioSource != null && closeSound != null)
            audioSource.PlayOneShot(closeSound);

        closeRoutine = StartCoroutine(RotateDoor(closedRotation));
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(doorTransform.localRotation, targetRotation) > 0.5f)
        {
            doorTransform.localRotation = Quaternion.Slerp(
                doorTransform.localRotation,
                targetRotation,
                Time.deltaTime * closeSpeed
            );

            yield return null;
        }

        doorTransform.localRotation = targetRotation;
    }
}