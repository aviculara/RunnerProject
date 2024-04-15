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
                shield.SetActive(false);
                other.gameObject.SetActive(false);
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
}
