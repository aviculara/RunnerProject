using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    
    public Positions position;
    public GameObject playerGroup;
    public GameManager gameManager;
    public GameObject all;
    [Header("Editor Params")]
    public float speed = 5;
    [SerializeField] float side = 3;    
    [SerializeField] float jumpPower=2.2f;
    [SerializeField] float acceleration;

    //public float jumpDur=0.9f;
    private bool moving = false;
    public bool jumping = false;

    private Tween jumpUpTween;
    private Tween jumpDownTween;

    Animator animator;


    // Start is called before the first frame update
    private void Awake()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    public enum Positions
    {
        onMid,
        onLeft,
        onRight
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameInactive)
        {
            //playerGroup.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
            all.transform.Translate(-speed * Time.deltaTime * new Vector3(0, 0, 1));
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && position != Positions.onLeft && !moving)
            {
                if (position == Positions.onMid)
                {
                    position = Positions.onLeft;
                }
                else if (position == Positions.onRight)
                {
                    position = Positions.onMid;
                }
                transform.DOMoveX(transform.position.x - side, 0.25f).OnComplete(stopMove);
                animator.SetBool("Left",true);
                moving = true;
            }
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && position != Positions.onRight && !moving)
            {
                if (position == Positions.onMid)
                {
                    position = Positions.onRight;
                }
                else if (position == Positions.onLeft)
                {
                    position = Positions.onMid;
                }
                transform.DOMoveX(transform.position.x + side, 0.25f).OnComplete(stopMove);
                animator.SetBool("Right",true);
                moving = true;
            }
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !jumping)
            {
                animator.SetTrigger("Jump");
                jumping = true;
                //Vector3 currentpos = new Vector3(transform.position);
                //gameObject.transform.DOLocalJump(transform.localPosition,jumpPower,1,jumpDur);
                //DOLocalJump(Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping)
                jumpUpTween = gameObject.transform.DOLocalMoveY(transform.localPosition.y + jumpPower, 0.5f).SetEase(Ease.OutFlash);
                jumpDownTween = gameObject.transform.DOLocalMoveY(transform.localPosition.y, 0.75f).SetDelay(0.5f).SetEase(Ease.InFlash);
            }
            if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && jumping)
            {
                if(jumpUpTween != null)
                {
                    jumpUpTween.Kill();
                }
                if(jumpDownTween != null)
                {
                    jumpDownTween.Kill();
                }
                transform.DOMoveY(0.5f,0.25f);
                //jumping = false;
            }
        }
    }
        

    private void stopMove()
    {
        moving = false;
        animator.SetBool("Right", false);
        animator.SetBool("Left", false);
    }
    
    public void StartGame()
    {
        gameManager.StartGame();
        StartCoroutine(increaseSpeed());
        print("started");
    }

    IEnumerator increaseSpeed()
    {
        yield return new WaitForSeconds(1f);
        speed += acceleration;
        StartCoroutine(increaseSpeed());
    }

    #region Swipe Functions
    public void Jump()
    {
        if(!jumping && !gameManager.gameInactive)
        {
            animator.SetTrigger("Jump");
            jumping = true;
            //Vector3 currentpos = new Vector3(transform.position);
            //gameObject.transform.DOLocalJump(transform.localPosition,jumpPower,1,jumpDur);
            //DOLocalJump(Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping)
            jumpUpTween = gameObject.transform.DOLocalMoveY(transform.localPosition.y + jumpPower, 0.5f).SetEase(Ease.OutFlash);
            jumpDownTween = gameObject.transform.DOLocalMoveY(transform.localPosition.y, 0.75f).SetDelay(0.5f).SetEase(Ease.InFlash);
        }
    }

    public void RightStrafe()
    {
        if(position != Positions.onRight && !moving && !gameManager.gameInactive)
        {
            if (position == Positions.onMid)
            {
                position = Positions.onRight;
            }
            else if (position == Positions.onLeft)
            {
                position = Positions.onMid;
            }
            transform.DOMoveX(transform.position.x + side, 0.25f).OnComplete(stopMove);
            animator.SetBool("Right", true);
            moving = true;
        }
    }

    public void LeftStrafe()
    {
        if(position != Positions.onLeft && !moving && !gameManager.gameInactive)
        {
            if (position == Positions.onMid)
            {
                position = Positions.onLeft;
            }
            else if (position == Positions.onRight)
            {
                position = Positions.onMid;
            }
            transform.DOMoveX(transform.position.x - side, 0.25f).OnComplete(stopMove);
            animator.SetBool("Left", true);
            moving = true;
        }
    }

    public void StopJump()
    {
        if(jumping && !gameManager.gameInactive)
        {
            if (jumpUpTween != null)
            {
                jumpUpTween.Kill();
            }
            if (jumpDownTween != null)
            {
                jumpDownTween.Kill();
            }
            transform.DOMoveY(0.5f, 0.25f);
        }
    }

    #endregion

}
