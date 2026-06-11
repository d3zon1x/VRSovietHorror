using System.Collections;
using UnityEngine;

public class VRMenuManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform xrOrigin;

    [Header("Spawn Points")]
    [SerializeField] private Transform menuSpawnPoint;
    [SerializeField] private Transform gameSpawnPoint;

    [Header("Menu Objects")]
    [SerializeField] private GameObject menuRoot;

    [Header("Fade")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeOutTime = 0.35f;
    [SerializeField] private float blackScreenTime = 0.25f;
    [SerializeField] private float fadeInTime = 0.45f;

    private bool isTransitioning;

    public void PlayGame()
    {
        if (isTransitioning)
            return;

        StartCoroutine(PlayGameRoutine());
    }

    private IEnumerator PlayGameRoutine()
    {
        isTransitioning = true;

        yield return Fade(0f, 1f, fadeOutTime);

        TeleportToGame();

        if (menuRoot != null)
            menuRoot.SetActive(false);

        yield return new WaitForSeconds(blackScreenTime);

        yield return Fade(1f, 0f, fadeInTime);

        isTransitioning = false;
    }

    private void TeleportToGame()
    {
        if (xrOrigin == null || gameSpawnPoint == null)
            return;

        Transform cam = Camera.main.transform;

        Vector3 cameraOffset = cam.position - xrOrigin.position;
        cameraOffset.y = 0f;

        xrOrigin.position = gameSpawnPoint.position - cameraOffset;
        xrOrigin.rotation = gameSpawnPoint.rotation;
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        if (fadeCanvasGroup == null)
            yield break;

        float timer = 0f;
        fadeCanvasGroup.alpha = from;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(from, to, timer / duration);
            yield return null;
        }

        fadeCanvasGroup.alpha = to;
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ReturnToMenu()
    {
        if (xrOrigin == null || menuSpawnPoint == null)
            return;

        xrOrigin.position = menuSpawnPoint.position;
        xrOrigin.rotation = menuSpawnPoint.rotation;

        if (menuRoot != null)
            menuRoot.SetActive(true);
    }
}