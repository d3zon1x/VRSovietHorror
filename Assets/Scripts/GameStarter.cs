using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStarter : MonoBehaviour
{
    [Header("Игрок")]
    public Transform xrOrigin;
    public Transform gameStartPoint;

    [Header("UI")]
    public GameObject menuCanvas;
    public CanvasGroup blackOverlay;

    [Header("Тайминги")]
    public float fadeInSpeed = 1.5f;    // скорость затемнения (нажатие кнопки)
    public float blackoutPause = 0.6f;  // пауза в темноте
    public float eyeOpenSpeed = 2.5f;   // скорость "открытия глаз"

    private bool _busy;

    // ── Кнопка "Начать игру" ──────────────────────────
    public void StartGame()
    {
        if (!_busy) StartCoroutine(StartSequence());
    }

    // ── Кнопка "Выйти" ───────────────────────────────
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ── Последовательность ───────────────────────────
    private IEnumerator StartSequence()
{
    _busy = true;

    // 1. Медленно темнеет (как смыкаются веки)
    yield return StartCoroutine(Fade(0f, 1f, 2.0f));

    // 2. Телепортируем пока темно
    Teleport();

    // 3. Пауза в темноте
    yield return new WaitForSeconds(1.0f);

    // 4. Медленно открываются глаза — сначала очень медленно, потом чуть быстрее
    yield return StartCoroutine(BlinkOpen(3.0f));

    _busy = false;
}

    private void Teleport()
    {
        if (xrOrigin != null && gameStartPoint != null)
        {
            xrOrigin.position = gameStartPoint.position;
            xrOrigin.rotation = Quaternion.Euler(0, gameStartPoint.eulerAngles.y, 0);
        }
        if (menuCanvas != null)
            menuCanvas.SetActive(false);
    }

    // Линейный fade
    private IEnumerator Fade(float from, float to, float duration)
{
    if (blackOverlay == null) yield break;
    float t = 0f;
    while (t < duration)
    {
        t += Time.deltaTime;
        blackOverlay.alpha = Mathf.Lerp(from, to, t / duration);
        yield return null;
    }
    blackOverlay.alpha = to;
}

    // Открытие глаз — нелинейная кривая (медленно-быстрее-медленно)
    private IEnumerator BlinkOpen(float duration)
{
    if (blackOverlay == null) yield break;
    float t = 0f;
    while (t < duration)
    {
        t += Time.deltaTime;
        float progress = t / duration;
        // EaseIn — начинает очень медленно
        float eased = progress * progress * progress;
        blackOverlay.alpha = 1f - eased;
        yield return null;
    }
    blackOverlay.alpha = 0f;
}
}
