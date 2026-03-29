using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    public Light spotLight;
    public Light glowLight;
    public AudioSource audioSource;
    public AudioClip toggleSound;

    public bool isOn = true;

    [Header("Flicker")]
    public bool useSubtleFlicker = true;
    public float baseIntensity = 25f;
    public float flickerAmount = 5f;
    public float flickerSpeed = 12f;

    [Header("Rare Power Dip")]
    public bool useRandomDip = true;
    public float dipChancePerSecond = 0.15f;
    public float dipMinMultiplier = 0.75f;
    public float dipMaxMultiplier = 0.9f;
    public float dipRecoverSpeed = 8f;

    private float currentDipMultiplier = 1f;
    private float targetDipMultiplier = 1f;

    private void Start()
    {
        ApplyStateImmediate();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleFlashlight();
        }

        if (isOn && spotLight != null)
        {
            UpdateFlicker();
        }
    }

    public void ToggleFlashlight()
    {
        isOn = !isOn;
        ApplyStateImmediate();

        if (audioSource != null && toggleSound != null)
        {
            audioSource.pitch = Random.Range(0.97f, 1.03f);
            audioSource.PlayOneShot(toggleSound);
            audioSource.pitch = 1f;
        }
    }

    private void ApplyStateImmediate()
    {
        if (spotLight != null)
            spotLight.enabled = isOn;

        if (glowLight != null)
            glowLight.enabled = isOn;

        if (isOn && spotLight != null)
            spotLight.intensity = baseIntensity;
    }

    private void UpdateFlicker()
    {
        float subtle = 0f;

        if (useSubtleFlicker)
            subtle = Mathf.Sin(Time.time * flickerSpeed) * flickerAmount;

        if (useRandomDip)
        {
            if (Random.value < dipChancePerSecond * Time.deltaTime)
                targetDipMultiplier = Random.Range(dipMinMultiplier, dipMaxMultiplier);

            currentDipMultiplier = Mathf.Lerp(currentDipMultiplier, targetDipMultiplier, Time.deltaTime * dipRecoverSpeed);
            targetDipMultiplier = Mathf.Lerp(targetDipMultiplier, 1f, Time.deltaTime * dipRecoverSpeed * 0.75f);
        }
        else
        {
            currentDipMultiplier = 1f;
        }

        float finalIntensity = (baseIntensity + subtle) * currentDipMultiplier;
        spotLight.intensity = Mathf.Max(0f, finalIntensity);
    }
}