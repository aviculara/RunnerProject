using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int points;
    UIManager uiManager;
    public float timeSpeed = 0.25f;
    int levelScore, increaseScoreAmount = 1;
    private GameManager gameManager;
    //private int speed;
    public PlayerMovement move;
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
        if(!gameManager.gameInactive)
        {
            StartCoroutine(ScoreUpdate());
        }
    }
}
