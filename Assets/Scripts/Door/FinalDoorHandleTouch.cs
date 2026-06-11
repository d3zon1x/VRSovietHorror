using System.Collections;
using UnityEngine;
using TMPro;

public class FinalDoorHandleTouch : MonoBehaviour
{
    [Header("Door")]
    public SimpleDoor door;

    [Header("Monster")]
    public GameObject finalMonster;
    public Animator finalMonsterAnimator;
    public string scareTriggerName = "Scream";

    [Header("Audio")]
    public AudioSource finalScreamAudio;

    [Header("Ending UI")]
    public CanvasGroup blackScreenCanvasGroup;
    public TMP_Text endingText;
    public string endingMessage = "YOU NEVER LEFT";

    [Header("Timing")]
    public float monsterAppearDelay = 0.25f;
    public float blackScreenDelay = 0.9f;
    public float fadeToBlackTime = 0.25f;
    public float textDelay = 0.8f;
    public float quitDelay = 4f;

    private bool finalEnabled = false;
    private bool triggered = false;

    private void Start()
    {
        if (finalMonster != null)
            finalMonster.SetActive(false);

        if (blackScreenCanvasGroup != null)
            blackScreenCanvasGroup.alpha = 0f;

        if (endingText != null)
        {
            endingText.text = endingMessage;
            endingText.gameObject.SetActive(false);
        }
    }

    public void EnableFinal()
    {
        finalEnabled = true;
        Debug.Log("Final door scare enabled");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("FINAL DOOR TRIGGER ENTER: " + other.name + " | tag=" + other.tag);

        if (!finalEnabled) return;
        if (triggered) return;
        if (!other.CompareTag("PlayerHand")) return;

        triggered = true;
        StartCoroutine(FinalSequence());
    }

    private IEnumerator FinalSequence()
    {
        if (door != null)
            door.OpenDoor();

        yield return new WaitForSeconds(monsterAppearDelay);

        if (finalMonster != null)
            finalMonster.SetActive(true);

        if (finalMonsterAnimator != null && !string.IsNullOrEmpty(scareTriggerName))
            finalMonsterAnimator.SetTrigger(scareTriggerName);

        if (finalScreamAudio != null)
            finalScreamAudio.Play();

        yield return new WaitForSeconds(blackScreenDelay);

        yield return StartCoroutine(FadeToBlack());

        yield return new WaitForSeconds(textDelay);

        if (endingText != null)
            endingText.gameObject.SetActive(true);

        yield return new WaitForSeconds(quitDelay);

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private IEnumerator FadeToBlack()
    {
        if (blackScreenCanvasGroup == null)
            yield break;

        float timer = 0f;

        while (timer < fadeToBlackTime)
        {
            timer += Time.deltaTime;
            blackScreenCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeToBlackTime);
            yield return null;
        }

        blackScreenCanvasGroup.alpha = 1f;
    }
}