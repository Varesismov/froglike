using System.Collections.Generic;

[System.Serializable]
public class PlayerProgressData
{
    public int totalEnemiesKilled = 0;
    public float totalXpCollected = 0f;
    public int totalRunsCompleted = 0;
    public List<int> unlockedItems = new List<int>();
}
