using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public TextMeshProUGUI scoreText;

    public GameObject cherrySprite;
    public GameObject bananaSprite;
    public GameObject orangeSprite;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI finalScoreText;

    public GameObject igUIcherry, igUIbanana, igUIorange;

    private GameManager gameManager;
    private ScoreManager scoreManager;
    private float multiplierBonus=0.5f;
    GameObject player;
    PlayerMovement move;
    PlayerManager pScript;
    
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //birden fazla varsa ve liste olarak yapmak i�in FindGameObjectsWithTag
        move = player.GetComponent<PlayerMovement>();
        pScript = player.GetComponent<PlayerManager>();
        gameManager = gameObject.GetComponent<GameManager>();
        scoreManager = gameObject.GetComponent<ScoreManager>();

        igUIcherry.SetActive(false);
        igUIbanana.SetActive(false);
        igUIorange.SetActive(false);

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
        winPanel.SetActive(false);
        if(panelObject != null)
        {
            panelObject.SetActive(true);
        }
            

    }

    public void LosePanel()
    {
        OpenPanel(losePanel);
    }

    public void WinPanel()
    {
        OpenPanel(winPanel);
        /*
        if(scoreManager.cherry || scoreManager.banana || scoreManager.orange)
        {

        }
        */
        multiplierText.text = "x 1";
        StartCoroutine(WriteFruitBonus());
        //multiplierText.gameObject.SetActive(false);
    }

    IEnumerator WriteFruitBonus()
    {
        yield return new WaitForSeconds(0.7f);
        float mult = 1;
        if (scoreManager.cherry)
        {
            mult = 1 + multiplierBonus;
            multiplierText.text = "x " + mult.ToString();
            multiplierBonus *= 2;
            cherrySprite.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.7f);
        }

        if (scoreManager.banana)
        {
            mult = 1 + multiplierBonus;
            multiplierText.text = "x " + mult.ToString();
            multiplierBonus *= 2;
            bananaSprite.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.7f);
        }
        if (scoreManager.orange)
        {
            mult = 1 + multiplierBonus;
            multiplierText.text = "x " + mult.ToString();
            orangeSprite.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //multiplierBonus *= 2;
            yield return new WaitForSeconds(0.6f);
        }
        int finalScore = (int)(scoreManager.levelScore * mult);
        finalScoreText.text = "Score: " + finalScore.ToString();
    }
}

