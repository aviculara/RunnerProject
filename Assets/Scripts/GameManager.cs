using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameInactive = false;
    private ScoreManager scoreManager;

    [SerializeField] AudioSource[] musics;
    [SerializeField] AudioSource[] sounds; 

    [Header("PlayerPrefs")]
    public int playerStrawberries;
    public int sound;
    public int music;

    private void Awake()
    {
        playerStrawberries = PlayerPrefs.GetInt("Strawberries", 0);
        sound = PlayerPrefs.GetInt("Sound", 1);
        music = PlayerPrefs.GetInt("Music", 1);
        foreach (AudioSource musicSource in musics)
        {
            musicSource.mute = (music == 0);
        }
        foreach (AudioSource soundSource in sounds)
        {
            soundSource.mute = (sound == 0);
        }
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        gameInactive = false;
        
        StartCoroutine(scoreManager.ScoreUpdate());
    }

    public void PauseGame()
    {
        gameInactive = true; 
    }

    public void ResumeGame()
    {
        gameInactive = false;
    }

    //public void AddStrawberry(int amount=1)
    //{
    //    playerStrawberries += amount;
    //    PlayerPrefs.SetInt("Strawberries", playerStrawberries);
    //}

    public void ToggleMusic()
    {
        if (music == 0)
        {
            music = 1;
        }
        else
        {
            music = 0;
        }
        foreach(AudioSource musicSource in musics)
        {
            musicSource.mute = (music == 0);
        }
        PlayerPrefs.SetInt("Music", music);
    }

    public void ToggleSound()
    {
        if(sound == 0)
        {
            sound = 1;
        }
        else
        {
            sound = 0;
        }
        foreach(AudioSource soundSource in sounds)
        {
            soundSource.mute = (sound == 0);
        }
        PlayerPrefs.SetInt("Sound", sound);
    }
}
