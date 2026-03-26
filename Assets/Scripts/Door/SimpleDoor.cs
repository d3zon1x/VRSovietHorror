using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SimpleDoor : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public float closeDelay = 4f;
    public bool openPositive = true;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private Coroutine moveRoutine;
    private Coroutine closeRoutine;

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
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        if (closeRoutine != null)
            StopCoroutine(closeRoutine);

        moveRoutine = StartCoroutine(RotateDoor(openRotation));
        closeRoutine = StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(closeDelay);

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(RotateDoor(closedRotation));
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.5f)
        {
            transform.localRotation = Quaternion.Slerp(
                transform.localRotation,
                targetRotation,
                Time.deltaTime * openSpeed
            );

            yield return null;
        }

        transform.localRotation = targetRotation;
    }
}