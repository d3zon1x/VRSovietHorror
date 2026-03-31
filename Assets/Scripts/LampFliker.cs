using UnityEngine;

public class LampFlicker : MonoBehaviour
{
    [Header("Источник света")]
    public Light lampLight;

    [Header("Интенсивность")]
    public float baseIntensity = 2f;
    public float minIntensity = 0f;

    [Header("Мерцание")]
    public float flickerSpeed = 10f;
    public float flickerAmount = 0.5f;

    [Header("Просадки яркости")]
    public float dipChance = 0.2f;       // шанс в секунду
    public float dipMinValue = 0.3f;
    public float dipMaxValue = 0.8f;
    public float dipRecovery = 5f;

    [Header("Полное выключение")]
    public float blackoutChance = 0.04f; // шанс в секунду
    public float blackoutDuration = 0.1f;

    // приватные
    private float _dip = 1f;
    private float _dipTarget = 1f;
    private float _blackoutTimer;
    private bool _blackout;

    void Start()
    {
        // Если поле не заполнено — ищем Light на этом же объекте
        if (lampLight == null)
            lampLight = GetComponent<Light>();
    }

    void Update()
    {
        // --- Полное выключение ---
        if (blackoutChance > 0 && !_blackout)
        {
            if (Random.value < blackoutChance * Time.deltaTime)
            {
                _blackout = true;
                _blackoutTimer = blackoutDuration;
            }
        }

        if (_blackout)
        {
            lampLight.intensity = 0f;
            _blackoutTimer -= Time.deltaTime;
            if (_blackoutTimer <= 0f) _blackout = false;
            return;
        }

        // --- Случайная просадка ---
        if (Random.value < dipChance * Time.deltaTime)
            _dipTarget = Random.Range(dipMinValue, dipMaxValue);

        _dip = Mathf.Lerp(_dip, _dipTarget, Time.deltaTime * dipRecovery);
        _dipTarget = Mathf.Lerp(_dipTarget, 1f, Time.deltaTime * dipRecovery * 0.5f);

        // --- Мелкое синусоидальное мерцание ---
        float flicker = Mathf.Sin(Time.time * flickerSpeed) * flickerAmount;

        // --- Итоговая яркость ---
        lampLight.intensity = Mathf.Max(minIntensity, (baseIntensity + flicker) * _dip);
    }
}
