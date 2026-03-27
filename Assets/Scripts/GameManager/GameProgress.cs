using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    public bool hasHallKey = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}