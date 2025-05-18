using UnityEngine;
using System.IO;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance;

    public PlayerProgressData data;
    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);

            savePath = Application.persistentDataPath + "/progress.json";

            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        LoadProgress();
        Debug.Log("You've killed total: " + data.totalEnemiesKilled);
    }


    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadProgress()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<PlayerProgressData>(json);
        }
        else
        {
            data = new PlayerProgressData();
        }
    }

    public void ResetProgress()
    {
        data = new PlayerProgressData();
        SaveProgress();
    }
}
