using UnityEngine;
using Image = UnityEngine.UI.Image;

[CreateAssetMenu(menuName ="ScriptableObject/Levels")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private string levelsName;
    [SerializeField]
    private int tileNumber;
    [SerializeField]
    private Sprite[] levelsTile;

    public string LevelsName
    {
        get
        {
            return levelsName;
        }
        set
        {
            levelsName = value;
        }
    }

    public int TileNumber
    {
        get
        {
            return tileNumber;
        }
    }

    public Sprite[] LevelsTile
    {
        get
        {
            return levelsTile;
        }
        set
        {
            levelsTile = value;
        }
    }
}
