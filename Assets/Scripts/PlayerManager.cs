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
    public GameObject shield;
    public GameObject explosionFX;
    public GameObject mainCamera;

    public float magnetTime = 5f;
    public float elevationDist = 5f;

    private GameManager gameManager;
    private ScoreManager scoreManager;
    private PlayerMovement playerMove;
    private Transform camDefaultPos;
    public GameObject magnetRange;
    public GameObject starRange;
    

    // Start is called before the first frame update
    private void Awake()
    {
        managerObject = GameObject.Find("GameManager");
        uiManager = managerObject.GetComponent<UIManager>();
        gameManager = managerObject.GetComponent<GameManager>();
        scoreManager = managerObject.GetComponent<ScoreManager>();
        playerMove = GetComponent<PlayerMovement>();
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if(watermelond)
            {
                watermelond = false;
                explosionFX.SetActive(true);
                //shield.SetActive(false);
                //other.gameObject.SetActive(false);
                StartCoroutine(ShieldOff(other.gameObject));
            }
            else
            {
                Death();
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
                    gameManager.AddStrawberry();
                    Destroy(other.gameObject);
                    
                    break;
                case Collectible.CollectibleType.cherry:
                    scoreManager.cherry = true;
                    other.gameObject.SetActive(false);
                    uiManager.igUIcherry.GetComponent<Image>().color = Color.white;
                    break;
                case Collectible.CollectibleType.banana:
                    scoreManager.banana = true;
                    other.gameObject.SetActive(false);
                    uiManager.igUIbanana.GetComponent<Image>().color = Color.white;
                    break;
                case Collectible.CollectibleType.orange:
                    scoreManager.orange = true;
                    other.gameObject.SetActive(false);
                    uiManager.igUIorange.GetComponent<Image>().color = Color.white ;
                    
                    break;
                case Collectible.CollectibleType.watermelon:
                    watermelond = true;
                    other.gameObject.SetActive(false);
                    shield.SetActive(true);
                    break;
                case Collectible.CollectibleType.star:
                    //PowerUp pwrStr = other.gameObject.GetComponent<PowerUp>();
                    //pwrStr.SeeknDestroy();
                    starRange.SetActive(true);
                    starRange.GetComponent<PowerUp>().StarCollected();
                    other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    Destroy(other.gameObject);

                    break;
                case Collectible.CollectibleType.magnet:
                    if(!magnetOn)
                    {
                        magnetRange.SetActive(true);
                        StartCoroutine(MagnetOff());
                    }
                    //destroy magnet
                    Destroy(other.gameObject);

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

    private void Win()
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
    }

    IEnumerator MagnetOff()
    {
        yield return new WaitForSeconds(magnetTime);
        magnetOn = false;
        magnetRange.SetActive(false);
        print("its off");
    }
}
