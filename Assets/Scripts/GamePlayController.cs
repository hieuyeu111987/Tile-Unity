using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI currentLevelValue;
    public GameObject pausePanel;
    
    public AudioSource backgroundAudio;
    public AudioSource buttonAudio;
    public AudioSource pickTileAudio;
    public AudioSource finishAudio;
    public AudioSource winAudio;
    public AudioSource loseAudio;
    // Start is called before the first frame update
    void Start()
    {
        GameData data = DataController.Instance.loadGame();
        string level = data.level.ToString();
        bool music = data.music;
        this.playMusic(music);
        currentLevelValue.text = level;
        if(this.pausePanel.activeSelf == true)
        {
            this.pausePanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPauseGame ()
    {
        if(this.pausePanel.activeSelf == false)
        {
            this.pausePanel.SetActive(true);
            this.buttonAudio.Play();
        }
    }

    public void onResumeGame ()
    {
        if(this.pausePanel.activeSelf == true)
        {
            this.pausePanel.SetActive(false);
            this.buttonAudio.Play();
        }
    }

    private void playMusic(bool play)
    {
        if (play == true)
        {
            this.backgroundAudio.volume = 0.5f;
            this.buttonAudio.volume = 1;
            this.pickTileAudio.volume = 1;
            this.finishAudio.volume = 1;
            this.winAudio.volume = 1;
            this.loseAudio.volume = 1;
        }
        else
        {
            this.backgroundAudio.volume = 0;
            this.buttonAudio.volume = 0;
            this.pickTileAudio.volume = 0;
            this.finishAudio.volume = 0;
            this.winAudio.volume = 0;
            this.loseAudio.volume = 0;
        }
    }
}
