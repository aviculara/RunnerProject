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
    public GameObject settingsPanel;
    public GameObject shopPanel;
    public GameObject deleteWarningPanel;
    public GameObject outfitsPanel;

    [Header("Score Texts")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI strawbsText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highscoreText;

    [Header("Sprites")]
    public GameObject cherrySprite;
    public GameObject bananaSprite;
    public GameObject orangeSprite;

    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOn;
    public Sprite musicOff;

    //public Image soundButton, musicButton;
    public Image[] soundButtons, musicButtons;

    public GameObject igUIcherry, igUIbanana, igUIorange;
    public GameObject newHighscoreText;
    public AudioSource buttonSound,strawberrySound;

    [Header("Other Functions")]
    public GameObject mainCamera;
    public GameObject[] fruitsToEat;

    private GameManager gameManager;
    private ScoreManager scoreManager;
    private float multiplierBonus=0.5f;

    private bool firstStart = false;
    public bool getNewHighscore = false;

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

        //igUIcherry.GetComponent<Image>().color = Color.black; 
        //igUIbanana.GetComponent<Image>().color = Color.black;
        //igUIorange.GetComponent<Image>().color = Color.black;

    }
    void Start()
    {
        Time.timeScale = 1;
        OpenPanel(startPanel);
        inGamePanel.SetActive(false);
        pausePanel.SetActive(false);
        warningPanel.SetActive(false);
        getNewHighscore = false;
        newHighscoreText.SetActive(false);
        //Time.timeScale = 0; //animasyonlar da duruyor
        gameManager.gameInactive = true;
        //pScript.animator.SetBool("Idle", true);
        scoreText.text = 0.ToString("0000");
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
            pScript.StartInvulnerableFor(1.5f);
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

    public void SettingsOpen()
    {
        OpenPanel(settingsPanel);
    }

    public void BackToMainMenu()
    {
        OpenPanel(startPanel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShopOpen()
    {
        OpenPanel(shopPanel);
        outfitsPanel.SetActive(false);
    }

    public void DeleteWarning()
    {
        deleteWarningPanel.SetActive(true);
    }

    public void DeleteYes()
    {
        PlayerPrefs.DeleteAll();
        //BackToMainMenu();
        //deleteWarningPanel.SetActive(false);
        Restart();
    }
    public void DeleteNo()
    {
        deleteWarningPanel.SetActive(false);
    }

    public void OutfitsButton()
    {
        outfitsPanel.SetActive(true);
    }

    public void UpgradesButton()
    {
        outfitsPanel.SetActive(false);
    }

    #endregion

    #region Sound Buttons
    private void SoundIcon()
    {
        if (gameManager.sound == 0)
        {
            foreach (Image soundButton in soundButtons)
            {
                soundButton.sprite = soundOff;
            }
        }
        else
        {
            foreach (Image soundButton in soundButtons)
            {
                soundButton.sprite = soundOn;
            }
        }
    }

    public void ToggleSound()
    {
        gameManager.ToggleSound();
        SoundIcon();
    }

    private void MusicIcon()
    {
        if (gameManager.music == 0)
        {
            foreach (Image musicButton in musicButtons)
            {
                musicButton.sprite = musicOff;
            }
        }
        else
        {
            foreach (Image musicButton in musicButtons)
            {
                musicButton.sprite = musicOn;
            }
        }
    }

    public void ToggleMusic()
    {
        gameManager.ToggleMusic();
        MusicIcon();
    }

    #endregion
    void OpenPanel(GameObject panelObject)
    {
        startPanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        shopPanel.SetActive(false);
        settingsPanel.SetActive(false);
        deleteWarningPanel.SetActive(false);
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
        multiplierText.text = "x 1";
        if(getNewHighscore)
        {
            WriteHighscore(scoreManager.highscore);
            newHighscoreText.SetActive(true);
        }
        
        //StartCoroutine(WriteFruitBonus());
        //multiplierText.gameObject.SetActive(false);
    }

    public void ButtonSound()
    {
        buttonSound.Play();
    }

    IEnumerator EatFruit()
    {
        yield return new WaitForSeconds(0.40f);
        for (int i = 0; i< fruitsToEat.Length; i++)
        {
            fruitsToEat[i].SetActive(false);
            strawberrySound.Play();
            yield return new WaitForSeconds(0.40f);
        }
        
    }

    public void WriteHighscore(int score)
    {
        highscoreText.text = score.ToString("0000");
        //NewHighscore();
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
        if(mult > 1)
        {
            finalScoreText.color = new Color(1, 0.812f, 0.224f, 1);
        }
        finalScoreText.text = "Score: " + finalScore.ToString();
    }
}


