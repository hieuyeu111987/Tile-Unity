using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level = 0;
    public int score = 0;
    public bool music = true;

    public GameData (int level, int score, bool music)
    {
        this.level = level;
        this.score = score;
        this.music = music;
    }
}
