using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class MenuController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI currentLevelValue;
    public TMPro.TextMeshProUGUI currentScoreValue;
    public GameObject settingPanel;
    public AudioSource backgroundAudio;
    public AudioSource buttonAudio;
    public Image musicButton;
    private GameData data;

    // Start is called before the first frame update
    void Start()
    {
        GameData data = DataController.Instance.loadGame();
        string level = data.level.ToString();
        string score = data.score.ToString();
        bool music = data.music;
        currentLevelValue.text = level;
        currentScoreValue.text = score;
        this.playMusic(music);
        if(this.settingPanel.activeSelf == true)
        {
            this.settingPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayGame ()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void onOpenSetting ()
    {
        if(this.settingPanel.activeSelf == false)
        {
            this.settingPanel.SetActive(true);
            buttonAudio.Play();
        }
    }

    public void onCloseSetting ()
    {
        if(this.settingPanel.activeSelf == true)
        {
            this.settingPanel.SetActive(false);
            buttonAudio.Play();
        }
    }

    public void onClickMusic()
    {
        GameData data = DataController.Instance.loadGame();
        int nextLevel = data.level;
        int totalScore = data.score;
        bool music = data.music;
        if (music)
        {
            this.playMusic(false);
            music = false;
        }
        else
        {
            this.playMusic(true);
            music = true;
        }
        DataController.Instance.saveGame(nextLevel, totalScore, music);
    }

    private void playMusic(bool play)
    {
        if (play == true)
        {
            this.backgroundAudio.volume = 0.5f;
            this.buttonAudio.volume = 1;
            musicButton.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            this.backgroundAudio.volume = 0;
            this.buttonAudio.volume = 0;
            musicButton.color = new Color32(0, 0, 0, 255);
        }
    }
}
