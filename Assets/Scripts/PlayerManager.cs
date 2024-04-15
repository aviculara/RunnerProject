using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    UIManager uiManager;
    //PlayerMovement move;
    public Animator animator;
    public Transform camFinalPos;
    public GameObject managerObject;
    private GameManager gameManager;
    private ScoreManager scoreManager;
    public bool watermelond = false;
    public GameObject shield;
    public GameObject explosionFX;
    
    // Start is called before the first frame update
    private void Awake()
    {
        managerObject = GameObject.Find("GameManager");
        uiManager = managerObject.GetComponent<UIManager>();
        gameManager = managerObject.GetComponent<GameManager>();
        scoreManager = managerObject.GetComponent<ScoreManager>();
        //move = gameObject.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
        
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
                    Destroy(other.gameObject);
                    
                    break;
                case Collectible.CollectibleType.cherry:
                    scoreManager.cherry = true;
                    other.gameObject.SetActive(false);
                    uiManager.igUIcherry.SetActive(true);
                    break;
                case Collectible.CollectibleType.banana:
                    scoreManager.banana = true;
                    other.gameObject.SetActive(false);
                    uiManager.igUIbanana.SetActive(true);
                    break;
                case Collectible.CollectibleType.orange:
                    scoreManager.orange = true;
                    other.gameObject.SetActive(false);
                    uiManager.igUIorange.SetActive(true);
                    break;
                case Collectible.CollectibleType.watermelon:
                    watermelond = true;
                    other.gameObject.SetActive(false);
                    shield.SetActive(true);
                    break;
                case Collectible.CollectibleType.star:
                    PowerUp pwrStr = other.gameObject.GetComponent<PowerUp>();
                    pwrStr.SeeknDestroy();
                    other.gameObject.GetComponent<MeshRenderer>().enabled = false;

                    break;
            }
            /*
            if(other.GetComponent<Collectible>().ctype == Collectible.CollectibleType.strawberry)
            {
                scoreManager.getStrawb();
                Destroy(other.gameObject);
            }
            else if(other.GetComponent<Collectible>().ctype == Collectible.CollectibleType.cherry)
            {
                scoreManager.cherry = true;
                other.gameObject.SetActive(false);
            }
            else if (other.GetComponent<Collectible>().ctype == Collectible.CollectibleType.banana)
            {
                scoreManager.banana = true;
                other.gameObject.SetActive(false);
            }
            else if (other.GetComponent<Collectible>().ctype == Collectible.CollectibleType.orange)
            {
                scoreManager.orange = true;
                other.gameObject.SetActive(false);
            }
            else if (other.GetComponent<Collectible>().ctype == Collectible.CollectibleType.watermelon)
            {
                watermelond = true;
                other.gameObject.SetActive(false);
                shield.SetActive(true);
            }
            */
        }
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        gameManager.gameInactive = true;
        Invoke("LosePanel", 2);
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
}
