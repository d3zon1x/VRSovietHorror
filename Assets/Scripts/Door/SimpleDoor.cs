using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SimpleDoor : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public float closeSpeed = 8f;
    public float closeDelay = 4f;
    public bool openPositive = true;

    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private Coroutine moveRoutine;
    private Coroutine closeRoutine;

    private bool isOpen = false;
    private bool isLockedForever = false;

    void Start()
    {
        closedRotation = transform.localRotation;

        float angle = openPositive ? openAngle : -openAngle;
        openRotation = closedRotation * Quaternion.Euler(0f, angle, 0f);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.oKey.wasPressedThisFrame)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (isOpen) return;
        if (isLockedForever) return;

        isOpen = true;

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        if (closeRoutine != null)
            StopCoroutine(closeRoutine);

        PlaySound(openSound);

        moveRoutine = StartCoroutine(RotateDoor(openRotation, openSpeed));
        closeRoutine = StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(closeDelay);

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        PlaySound(closeSound);

        moveRoutine = StartCoroutine(RotateDoor(closedRotation, closeSpeed));
        isOpen = false;
    }

    public void ForceCloseAndLock()
    {
        isLockedForever = true;
        isOpen = false;

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        if (closeRoutine != null)
            StopCoroutine(closeRoutine);

        PlaySound(closeSound);

        moveRoutine = StartCoroutine(RotateDoor(closedRotation, closeSpeed));
    }

    private IEnumerator RotateDoor(Quaternion targetRotation, float speed)
    {
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.5f)
        {
            transform.localRotation = Quaternion.Slerp(
                transform.localRotation,
                targetRotation,
                Time.deltaTime * speed
            );

            yield return null;
        }

        transform.localRotation = targetRotation;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.pitch = Random.Range(0.97f, 1.03f);
            audioSource.PlayOneShot(clip);
            audioSource.pitch = 1f;
        }
    }
}