using UnityEngine;

public class GameModeScript : MonoBehaviour
{
    public enum GameMode
    {
        Normal,
        Hardcore
    }

    public static GameModeScript Instance { get; private set; }

    public GameMode currentGameMode = GameMode.Normal;

    public float xpLossOnDeath_Normal = 0f;
    public float xpLossOnDeath_Hardcore = 1000f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public float GetXpLossOnDeath()
    {
        switch (currentGameMode)
        {
            case GameMode.Hardcore:
                return xpLossOnDeath_Hardcore;
            case GameMode.Normal:
            default:
                return xpLossOnDeath_Normal;
        }
    }
}
