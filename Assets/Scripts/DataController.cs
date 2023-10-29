using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private static DataController instance;
    public static DataController Instance { get => instance; }
    // Start is called before the first frame update
    private void Awake()
    {
        DataController.instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveGame(int level, int score, bool music)
    {
        GameData data = new GameData(level, score, music);
        string gameData = JsonUtility.ToJson(data);
        string filePath = Application.persistentDataPath + "/GameData.json";
        System.IO.File.WriteAllText(filePath, gameData);
    }

    public GameData loadGame()
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        if (System.IO.File.Exists(filePath))
        {
            string gameData = System.IO.File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(gameData);
            return data;
        }
        else
        {
            this.saveGame(1, 0, true);
            return this.loadGame();
        }
    }
}
