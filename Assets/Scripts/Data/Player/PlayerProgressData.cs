using System.Collections.Generic;

[System.Serializable]
public class PlayerProgressData
{
    public int totalEnemiesKilled = 0;
    public float totalXpCollected = 0f;
    public int totalRunsCompleted = 0;
    public float goldCollected = 0f;
    public int playerLevel = 0;

    public List<int> unlockedItemsIds = new List<int>();
    public void UnlockItem(int id)
    {
        if (!unlockedItemsIds.Contains(id))
        {
            unlockedItemsIds.Add(id);
        }
    }

    public bool IsItemUnlocked(int id)
    {
        return unlockedItemsIds.Contains(id);
    }
}
