using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject startPanel;
    public GameObject inGamePanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject pausePanel;
    public GameObject warningPanel;

    [Header("Score Texts")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI strawbsText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI finalScoreText;

    [Header("Sprites")]
    public GameObject cherrySprite;
    public GameObject bananaSprite;
    public GameObject orangeSprite;

    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOn;
    public Sprite musicOff;

    public Image soundButton, musicButton;

    public GameObject igUIcherry, igUIbanana, igUIorange;

    [Header("Other Functions")]
    public GameObject mainCamera;
    public GameObject[] fruitsToEat;

    private GameManager gameManager;
    private ScoreManager scoreManager;
    private float multiplierBonus=0.5f;

    private bool firstStart = false;
    

    GameObject player;
    PlayerMovement move;
    PlayerManager pScript;
    Animator camAnimator;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //birden fazla varsa ve liste olarak yapmak için FindGameObjectsWithTag
        move = player.GetComponent<PlayerMovement>();
        pScript = player.GetComponent<PlayerManager>();
        gameManager = gameObject.GetComponent<GameManager>();
        scoreManager = gameObject.GetComponent<ScoreManager>();

        camAnimator = mainCamera.GetComponent<Animator>();

        igUIcherry.GetComponent<Image>().color = Color.black; 
        igUIbanana.GetComponent<Image>().color = Color.black;
        igUIorange.GetComponent<Image>().color = Color.black;

    }
    void Start()
    {
        Time.timeScale = 1;
        OpenPanel(startPanel);
        inGamePanel.SetActive(false);
        pausePanel.SetActive(false);
        warningPanel.SetActive(false);
        //Time.timeScale = 0; //animasyonlar da duruyor
        gameManager.gameInactive = true;
        //pScript.animator.SetBool("Idle", true);
        scoreText.text = 0.ToString("00000");
        strawbsText.text = 0.ToString("000");
        SoundIcon();
        MusicIcon();
        firstStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(startPanel.activeSelf)
            {
                Application.Quit();
            }
            //else if(shoppanel)
            else if(warningPanel.activeSelf)
            {
                WarningNo();                                                                                                        
            }
            else if (pausePanel.activeSelf)
            {
                HomeWarningPanel();
            }
            else
            {
                PausePanel();
            }
        }
        if(firstStart && !gameManager.gameInactive)
        {
            inGamePanel.SetActive(true);
            firstStart = false;
        }
    }

    #region Button Functions
    public void TaptoStart()
    {
        startPanel.SetActive(false);
        //inGamePanel.SetActive(true);
        //Time.timeScale = 1;
        //gameManager.gameInactive = false;
        //pScript.animator.SetBool("Idle", false);
        StartCoroutine(EatFruit());
        pScript.animator.SetBool("GameStart", true);
        camAnimator.SetBool("Start", true);
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PausePanel()
    {
        if (gameManager.gameInactive && pausePanel.activeSelf)
        {
            gameManager.ResumeGame();
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (!gameManager.gameInactive)
        {
            gameManager.PauseGame();
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void HomeWarningPanel()
    {
        warningPanel.SetActive(true);
    }

    public void WarningYes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WarningNo()
    {
        warningPanel.SetActive(false);
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


    IEnumerator EatFruit()
    {
        yield return new WaitForSeconds(0.40f);
        for (int i = 0; i< fruitsToEat.Length; i++)
        {
            fruitsToEat[i].SetActive(false);
            yield return new WaitForSeconds(0.40f);
        }
        
    }

    #region Sound Buttons
    private void SoundIcon()
    {
        if(gameManager.sound == 0)
        {
            soundButton.sprite = soundOff;
        }
        else
        {
            soundButton.sprite = soundOn;
        }
    }

    public void ToggleSound()
    {
        if(gameManager.sound == 0)
        {
            gameManager.sound = 1;
        }
        else
        {
            gameManager.sound = 0;
        }
        PlayerPrefs.SetInt("Sound", gameManager.sound);
        SoundIcon();
    }

    private void MusicIcon()
    {
        if (gameManager.music == 0)
        {
            musicButton.sprite = musicOff;
        }
        else
        {
            musicButton.sprite = musicOn;
        }
    }

    public void ToggleMusic()
    {
        if (gameManager.music == 0)
        {
            gameManager.music = 1;
        }
        else
        {
            gameManager.music = 0;
        }
        PlayerPrefs.SetInt("Music", gameManager.music);
        MusicIcon();
    }

    #endregion

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
        if(mult > 1)
        {
            finalScoreText.color = new Color(1, 0.812f, 0.224f, 1);
        }
        finalScoreText.text = "Score: " + finalScore.ToString();
    }
}


