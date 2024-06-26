using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    UIManager uiManager;
    //PlayerMovement move;
    public Animator animator;
    public Transform camFinalPos;
    public GameObject managerObject;
    public bool watermelond = false;
    public bool magnetOn = false;
    public bool bananaOn = false;
    public bool invulnerable = false;
    public GameObject shield;
    public GameObject explosionFX;
    public GameObject mainCamera;

    public AudioSource strawberrySound, powerupSound, crashSound, watermelonSound, backgroundMusic;

    public float elevationDist = 5f;

    private GameManager gameManager;
    private ScoreManager scoreManager;
    private PlayerMovement playerMove;
    private PowerupManager powerManager;
    private ShopManager shopManager;
    private Transform camDefaultPos;

    private float invulnerableTimeLeft;


    // Start is called before the first frame update
    private void Awake()
    {
        managerObject = GameObject.Find("GameManager");
        uiManager = managerObject.GetComponent<UIManager>();
        gameManager = managerObject.GetComponent<GameManager>();
        scoreManager = managerObject.GetComponent<ScoreManager>();
        playerMove = GetComponent<PlayerMovement>();
        powerManager = managerObject.GetComponent<PowerupManager>();
        shopManager = managerObject.GetComponent<ShopManager>();
        //move = gameObject.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
        camDefaultPos = mainCamera.transform;        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerable == true)
        {
            if(invulnerableTimeLeft <=0)
            {
                invulnerable = false;
                powerManager.ghostFox.SetActive(false);
            }
            invulnerableTimeLeft -= Time.unscaledDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !bananaOn && !invulnerable)
        {
            if(watermelond)
            {
                watermelond = false;
                explosionFX.SetActive(true);
                StartCoroutine(ShieldOff(other.gameObject));
                watermelonSound.Play();
            }
            else
            {
                Death();
                crashSound.Play();
                backgroundMusic.Pause();
            }
            
        }
        else if (other.CompareTag("Finish"))
        {
            Win();
        }
        else if (other.CompareTag("Collectible"))
        {
            
            Collectible.CollectibleType collectibleType = other.GetComponent<Collectible>().ctype;
            switch (collectibleType)
            {
                case Collectible.CollectibleType.strawberry:
                    scoreManager.getStrawb();
                    shopManager.AddStrawberry();
                    strawberrySound.Play();
                    Destroy(other.gameObject);
                    break;
                case Collectible.CollectibleType.cherry:
                    scoreManager.cherry = true;
                    other.gameObject.SetActive(false);
                    uiManager.igUIcherry.GetComponent<Image>().color = Color.white;
                    powerupSound.Play();
                    break;
                case Collectible.CollectibleType.pear:
                    scoreManager.banana = true;
                    other.gameObject.SetActive(false);
                    uiManager.igUIbanana.GetComponent<Image>().color = Color.white;
                    powerupSound.Play();
                    break;
                case Collectible.CollectibleType.orange:
                    scoreManager.orange = true;
                    other.gameObject.SetActive(false);
                    uiManager.igUIorange.GetComponent<Image>().color = Color.white;
                    powerupSound.Play();
                    break;
                case Collectible.CollectibleType.watermelon:
                    watermelond = true;
                    other.gameObject.SetActive(false);
                    shield.SetActive(true);
                    powerupSound.Play();
                    break;
                case Collectible.CollectibleType.star:
                    powerManager.StarCollected();
                    Destroy(other.gameObject);
                    powerupSound.Play();
                    break;
                case Collectible.CollectibleType.magnet:
                    powerManager.MagnetCollected();
                    Destroy(other.gameObject);
                    powerupSound.Play();
                    break;
                case Collectible.CollectibleType.banana:                    
                    powerManager.BananaCollected();
                    Destroy(other.gameObject);
                    powerupSound.Play();
                    break;
            }
            
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.name);
        if(collision.collider.CompareTag("Ground"))
        {
            playerMove.jumping = false;
            //if camera was up, move it down
        }
        else if(collision.collider.CompareTag("Elevation"))
        {
            //move camera up            
            mainCamera.transform.DOLocalMoveY(mainCamera.transform.localPosition.y + elevationDist, 0.7f);
        }
    }
    
    private void Death()
    {
        animator.SetTrigger("Death");
        gameManager.gameInactive = true;
        //Invoke("LosePanel", 2);
        StartCoroutine(WinPanel());
    }

    private void Win() //unused
    {
        animator.SetTrigger("Win");
        gameManager.gameInactive = true;
        //Camera.main.transform.DOLocalRotate(camFinalPos.localEulerAngles, 1);
        //Camera.main.transform.DOLocalMove(camFinalPos.localPosition, 1);
        Camera.main.GetComponent<Animator>().SetBool("Win", true);
        StartCoroutine(WinPanel());

    }

    private void LosePanel()
    {
        uiManager.LosePanel();
    }

    IEnumerator WinPanel()
    {
        yield return new WaitForSeconds(3f);
        uiManager.WinPanel();
    }

    IEnumerator ShieldOff(GameObject collidedObject)
    {
        yield return new WaitForSeconds(0.2f);
        
        shield.SetActive(false);
        collidedObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        explosionFX.SetActive(false);
    }

    public void StartInvulnerableFor(float seconds)
    {
        if(!bananaOn)
        {
            powerManager.ghostFox.SetActive(true);
        }
        invulnerableTimeLeft = seconds;
        invulnerable = true;
    }
}
