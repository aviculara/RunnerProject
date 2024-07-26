using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int points;
    UIManager uiManager;
    GameObject player;
    
    public float timeSpeed = 0.25f;
    public int levelScore, increaseScoreAmount = 1;
    //int strawbScore = 10;
    public int levelStrawbs;
    public int highscore;

    public bool cherry = false;
    public bool banana = false;
    public bool orange = false;
    private GameManager gameManager;
    private ShopManager shopManager;
    private bool accHalved = false;
    private bool powerupsHalved = false;
    //private int speed;
    public PlayerMovement move;
    public PlayerManager playerManager;
    public EndlessManager endlessManager;
    
    //int multipliersCollected = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        uiManager = gameObject.GetComponent<UIManager>();
        player = GameObject.Find("Fox");
        move = player.GetComponent<PlayerMovement>();
        playerManager = player.GetComponent<PlayerManager>();
        gameManager = gameObject.GetComponent<GameManager>();
        shopManager = GetComponent<ShopManager>();
        highscore = PlayerPrefs.GetInt("Highscore", 0);
    }
    void Start()
    {
        uiManager.WriteHighscore(highscore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ScoreUpdate()
    {
        yield return new WaitForSeconds(timeSpeed);
        levelScore += increaseScoreAmount;
        if(!powerupsHalved && levelScore > 380)
        {
            //endlessManager.powerupPercent /= 2;
            endlessManager.maxPowerupFrequency *= 3;
            endlessManager.minPowerupFrequency *=3 ;
            powerupsHalved = true;
        }
        if(!accHalved && levelScore > 480)
        {
            move.acceleration /= 2;
            accHalved = true;
        }
        uiManager.scoreText.text = levelScore.ToString("0000");
        uiManager.finalScoreText.text = "Score: " + levelScore.ToString();
        if(levelScore > highscore)
        {
            highscore = levelScore;
            PlayerPrefs.SetInt("Highscore", highscore);
            uiManager.getNewHighscore = true;
        }
        if(!gameManager.gameInactive)
        {
            StartCoroutine(ScoreUpdate());
        }
    }

    public void getStrawb()
    {
        levelStrawbs++;
        shopManager.AddStrawberry();
        uiManager.strawbsText.text = levelStrawbs.ToString("000");
    }

    public void getMultiplier()
    {
        //multipliersCollected += 1;
    }

    /*
    public void getMultiplier(GameObject collidedObject)
    {
        Collectible.CollectibleType collectibleType = collidedObject.GetComponent<Collectible>().ctype;
        switch (collectibleType)
        {
            case Collectible.CollectibleType.strawberry:
                getStrawb();
                Destroy(collidedObject.gameObject);

                break;
            case Collectible.CollectibleType.cherry:
                cherry = true;
                collidedObject.gameObject.SetActive(false);
                uiManager.igUIcherry.SetActive(true);
                break;
            case Collectible.CollectibleType.banana:
                banana = true;
                collidedObject.gameObject.SetActive(false);
                uiManager.igUIbanana.SetActive(true);
                break;
            case Collectible.CollectibleType.orange:
                orange = true;
                collidedObject.gameObject.SetActive(false);
                uiManager.igUIorange.SetActive(true);
                break;
            case Collectible.CollectibleType.watermelon:
                playerManager.watermelond = true;
                collidedObject.gameObject.SetActive(false);
                playerManager.shield.SetActive(true);
                break;
            case Collectible.CollectibleType.star:
                PowerUp pwrStr = collidedObject.gameObject.GetComponent<PowerUp>();
                pwrStr.SeeknDestroy();
                collidedObject.gameObject.GetComponent<MeshRenderer>().enabled = false;

                break;
        }
   
    }
    */
}
