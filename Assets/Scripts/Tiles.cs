using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEditor;

public class Tiles : MonoBehaviour, TilesListener
{
    private List<TilesBoxListener> listeners = new List<TilesBoxListener>();
    public GameObject tilePrefab;
    public Sprite[] tileImages;
    public Transform[] tileBoxs;
    public GameObject nextPanel;
    public GameObject losePanel;
    public TMPro.TextMeshProUGUI currentScoreValue;
    public TMPro.TextMeshProUGUI nextLevelValue;
    public AudioSource pickTileAudio;
    public AudioSource finishAudio;
    public AudioSource winAudio;
    public AudioSource loseAudio;
    private List<GameObject> tileInBoxs = new List<GameObject>();
    private List<GameObject> tileList = new List<GameObject>();
    private int currentBox = 0;
    private GameData data;
    private int tileNumber = 5;
    private int tileChose = 0;
    private int currentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.loadLevel();
        this.data = DataController.Instance.loadGame();
        for (int i = 0; i < this.tileNumber; i++)
        {
            this.createTile();
        }
        if(this.nextPanel.activeSelf == true)
        {
            this.nextPanel.SetActive(false);
        }
        if(this.losePanel.activeSelf == true)
        {
            this.losePanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void listenerAdd(TilesBoxListener listener)
    {
        this.listeners.Add(listener);
    }

    private void createTile()
    {
        int tileImageIndex = Random.Range(0, this.tileImages.Length);
        for (int i = 0; i < 3; i++)
        {
            float tilePosX = Random.Range(-200.0f, 200.0f);
            float tilePosY = Random.Range(-200.0f, 200.0f);
            GameObject newTile = Instantiate(tilePrefab, new Vector3(tilePosX / 128, tilePosY / 128, 0), Quaternion.identity, this.transform);
            newTile.name = tileImages[tileImageIndex].name;
            newTile.transform.Find("Main_Image").gameObject.GetComponent<Image>().sprite = tileImages[tileImageIndex];
            newTile.GetComponent<Tile>().listenerAdd(this);
            tileList.Add(newTile);
            this.getTileList();
        }
    }

    private void getTileList()
    {
        foreach (TilesBoxListener listener in this.listeners)
        {
            listener.getTileList();
        }
    }

    public void addTileToList(Tile tile)
    {
        this.pickTileAudio.Play();
        if (this.currentBox < this.tileBoxs.Length)
        {
            this.tileInBoxs.Add(tile.gameObject);
            tile.transform.position = this.tileBoxs[this.currentBox].position;
            this.tileChose++;
            this.checkFinishTile();
            if (this.checkFinish()){
                int nextLevel = this.data.level + 1;
                string resPath = "Levels/Level_" + nextLevel.ToString();
                LevelData levelData = Resources.Load<LevelData>(resPath);
                if (!levelData)
                {
                    nextLevel = 1;
                }
                int totalScore = this.data.score + this.currentScore;
                bool music = this.data.music;
                this.nextLevelValue.text = nextLevel.ToString();
                DataController.Instance.saveGame(nextLevel, totalScore, music);
                this.winGame();
            }
        }
        if (this.checkLose())
        {
            this.loseGame();
        }
    }

    private void checkFinishTile()
    {
        if (this.tileInBoxs.Count >= 3 && this.tileInBoxs[this.currentBox].name == this.tileInBoxs[this.currentBox - 1].name && this.tileInBoxs[this.currentBox].name == this.tileInBoxs[this.currentBox - 2].name)
        {
            this.tileInBoxs[this.currentBox].SetActive(false);
            this.tileInBoxs[this.currentBox - 1].SetActive(false);
            this.tileInBoxs[this.currentBox - 2].SetActive(false);
            this.tileInBoxs.Remove(this.tileInBoxs[this.currentBox]);
            this.tileInBoxs.Remove(this.tileInBoxs[this.currentBox - 1]);
            this.tileInBoxs.Remove(this.tileInBoxs[this.currentBox - 2]);
            
            this.currentScore++;
            this.currentScoreValue.text = this.currentScore.ToString();
            this.currentBox -= 2;
            
            this.finishAudio.Play();
        }
        else
        {
            this.currentBox++;
        }
    }

    private bool checkLose()
    {
        if (this.currentBox >= this.tileBoxs.Length || (this.tileChose >= this.tileList.Count && !this.checkFinish()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool checkFinish()
    {
        bool finish = true;
        foreach (GameObject tile in this.tileList)
        {
            if (tile.activeSelf == true)
            {
                finish = false;
            }
        }
        return finish;
    }

    private void loadLevel()
    {
        GameData newData = DataController.Instance.loadGame();
        string resPath = "Levels/Level_" + newData.level.ToString();
        LevelData levelData = Resources.Load<LevelData>(resPath);
        if (levelData)
        {
            bool hadTileImage = true;
            foreach (Sprite item in levelData.LevelsTile)
            {
                if (!item)
                {
                    hadTileImage = false;
                }
            }
            if (levelData.LevelsTile.Length > 0 && hadTileImage)
            {
                this.tileImages = levelData.LevelsTile;
            }
            this.tileNumber = levelData.TileNumber;
        }
        else
        {
            DataController.Instance.saveGame(1, newData.score, newData.music);
            this.loadLevel();
        }
    }

    private void winGame()
    {
        if(this.nextPanel.activeSelf == false)
        {
            this.winAudio.Play();
            this.nextPanel.SetActive(true);
        }
    }

    private void loseGame()
    {
        if(this.losePanel.activeSelf == false)
        {
            this.loseAudio.Play();
            this.losePanel.SetActive(true);
        }
    }
}
