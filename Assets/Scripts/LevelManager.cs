using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Singleton Pattern
    public static LevelManager Instance { get; private set; }

    [Header("XP and Level Settings")]
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int nextLevelXP = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetXP(int amount)
    {
        currentXP += amount;
        Debug.Log($"XP earned: +{amount} | Total XP: {currentXP}/{nextLevelXP}");

        if (currentXP >= nextLevelXP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentXP -= nextLevelXP;
        currentLevel++;
        nextLevelXP = Mathf.RoundToInt(nextLevelXP * 1.5f); //XP %50 increase each level

        Debug.Log($"<color=green>CONGRATULATIONS! LEVEL UPGRADED! New Level: {currentLevel}</color>");
    }
}