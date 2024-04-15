using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int points;
    UIManager uiManager;
    public float timeSpeed = 0.25f;
    public int levelScore, increaseScoreAmount = 1;
    int strawbScore = 10;
    public bool cherry = false;
    public bool banana = false;
    public bool orange = false;
    private GameManager gameManager;
    //private int speed;
    public PlayerMovement move;
    //int multipliersCollected = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        uiManager = gameObject.GetComponent<UIManager>();
        move = GameObject.Find("Fox").GetComponent<PlayerMovement>();
        gameManager = gameObject.GetComponent<GameManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ScoreUpdate()
    {
        yield return new WaitForSeconds(timeSpeed);
        levelScore += increaseScoreAmount;
        uiManager.scoreText.text = levelScore.ToString();
        uiManager.finalScoreText.text = levelScore.ToString();
        if(!gameManager.gameInactive)
        {
            StartCoroutine(ScoreUpdate());
        }
    }

    public void getStrawb()
    {
        levelScore += strawbScore;
    }

    public void getMultiplier()
    {
        //multipliersCollected += 1;
    }
}
