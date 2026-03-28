using System.Collections;
using UnityEngine;

public class EntranceScareTrigger : MonoBehaviour
{
    public GameObject creature;
    public Light[] lightsToControl;
    public AudioSource flickerAudio;
    public AudioSource PowerOffAudio;
    public AudioSource DemonicRoaring;
    public SimpleDoor entranceDoor;


    public float creatureVisibleTime = 1.5f;

    private bool triggered = false;
    private float[] originalIntensities;

    private void Start()
    {
        originalIntensities = new float[lightsToControl.Length];

        for (int i = 0; i < lightsToControl.Length; i++)
        {
            if (lightsToControl[i] != null)
                originalIntensities[i] = lightsToControl[i].intensity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        StartCoroutine(ScareSequence());
    }

    private IEnumerator ScareSequence()
    {
        if (entranceDoor != null)
            entranceDoor.ForceCloseAndLock();


        if (flickerAudio != null)
            flickerAudio.Play();

        yield return StartCoroutine(FlickerByIntensity());

        SetLightIntensityMultiplier(0f);
        yield return new WaitForSeconds(0.5f);

        if (creature != null)
            creature.SetActive(true);

        if (DemonicRoaring != null)
            DemonicRoaring.Play();

        SetLightIntensityMultiplier(0.12f);
        yield return new WaitForSeconds(0.08f);

        yield return StartCoroutine(FlickerWhileCreatureVisible());

        SetLightIntensityMultiplier(0f);

        if (flickerAudio != null)
            yield return StartCoroutine(FadeOutAudio(flickerAudio, 0.3f));

        if (creature != null)
            creature.SetActive(false);

        if (PowerOffAudio != null)
            PowerOffAudio.Play();
     
    }

    private IEnumerator FlickerByIntensity()
    {
        SetLightIntensityMultiplier(1.0f);
        yield return new WaitForSeconds(0.18f);

        SetLightIntensityMultiplier(0.55f);
        yield return new WaitForSeconds(0.10f);

        SetLightIntensityMultiplier(0.35f);
        yield return new WaitForSeconds(0.16f);

        SetLightIntensityMultiplier(0.01f);
        yield return new WaitForSeconds(0.17f);

        SetLightIntensityMultiplier(1.0f);
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator FlickerWhileCreatureVisible()
    {
        SetLightIntensityMultiplier(0.6f);
        yield return new WaitForSeconds(0.18f);

        SetLightIntensityMultiplier(0.15f);
        yield return new WaitForSeconds(0.14f);

        SetLightIntensityMultiplier(0.5f);
        yield return new WaitForSeconds(0.22f);

        SetLightIntensityMultiplier(0.08f);
        yield return new WaitForSeconds(0.16f);

        SetLightIntensityMultiplier(0.35f);
        yield return new WaitForSeconds(0.20f);

        SetLightIntensityMultiplier(0.02f);
        yield return new WaitForSeconds(0.18f);

        SetLightIntensityMultiplier(0.25f);
        yield return new WaitForSeconds(0.25f);

        SetLightIntensityMultiplier(0.6f);
        yield return new WaitForSeconds(0.18f);

        SetLightIntensityMultiplier(0.15f);
        yield return new WaitForSeconds(0.14f);

        SetLightIntensityMultiplier(0.5f);
        yield return new WaitForSeconds(0.22f);

        SetLightIntensityMultiplier(0.08f);
        yield return new WaitForSeconds(0.16f);

        SetLightIntensityMultiplier(0.35f);
        yield return new WaitForSeconds(0.20f);

        SetLightIntensityMultiplier(0.02f);
        yield return new WaitForSeconds(0.18f);

        SetLightIntensityMultiplier(0.25f);
        yield return new WaitForSeconds(0.25f);

        SetLightIntensityMultiplier(0.25f);
        yield return new WaitForSeconds(0.25f);
        
        SetLightIntensityMultiplier(0.6f);
        yield return new WaitForSeconds(0.18f);

        SetLightIntensityMultiplier(0.15f);
        yield return new WaitForSeconds(0.14f);

        SetLightIntensityMultiplier(0.5f);
        yield return new WaitForSeconds(0.22f);
    }

    private void SetLightIntensityMultiplier(float multiplier)
    {
        for (int i = 0; i < lightsToControl.Length; i++)
        {
            if (lightsToControl[i] != null)
            {
                lightsToControl[i].intensity = originalIntensities[i] * multiplier;
            }
        }
    }

    private IEnumerator FadeOutAudio(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }
}