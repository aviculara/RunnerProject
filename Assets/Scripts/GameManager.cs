using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameInactive = false;
    private ScoreManager scoreManager;

    [Header("PlayerPrefs")]
    public int playerStrawberries;
    public int sound;
    public int music;

    private void Awake()
    {
        playerStrawberries = PlayerPrefs.GetInt("Strawberries", 0);
        sound = PlayerPrefs.GetInt("Sound", 1);
        music = PlayerPrefs.GetInt("Music", 1);
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

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

    public void SetSound(int value)
    {
        sound = value;
        
    }
}
