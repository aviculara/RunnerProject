using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject losePanel;
    public TextMeshProUGUI scoreText;

    private GameManager gameManager;
    private ScoreManager scoreManager;
    GameObject player;
    PlayerMovement move;
    PlayerManager pScript;
    
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //birden fazla varsa ve liste olarak yapmak için FindGameObjectsWithTag
        move = player.GetComponent<PlayerMovement>();
        pScript = player.GetComponent<PlayerManager>();
        gameManager = gameObject.GetComponent<GameManager>();
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }
    void Start()
    {
        OpenPanel(startPanel);
        //Time.timeScale = 0; //animasyonlar da duruyor
        gameManager.gameInactive = true;
        pScript.animator.SetBool("Idle", true);
        scoreText.text = 0.ToString();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Button Functions
    public void TaptoStart()
    {
        startPanel.SetActive(false);
        //Time.timeScale = 1;
        gameManager.gameInactive = false;
        pScript.animator.SetBool("Idle", false);
        StartCoroutine(scoreManager.ScoreUpdate());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    void OpenPanel(GameObject panelObject)
    {
        startPanel.SetActive(false);
        losePanel.SetActive(false);
        if(panelObject != null)
        {
            panelObject.SetActive(true);
        }
            

    }

    public void LosePanel()
    {
        OpenPanel(losePanel);
    }
}

