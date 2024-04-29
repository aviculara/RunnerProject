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
    public int speed = 10;
    public float side = 3;
    public Animator animator;
    public float jumpPower=2.2f;
    //public float jumpDur=0.9f;
    private bool moving = false;
    public bool jumping = false;
    

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
                gameObject.transform.DOLocalMoveY(transform.localPosition.y + jumpPower, 0.5f).SetEase(Ease.OutFlash);
                gameObject.transform.DOLocalMoveY(transform.localPosition.y, 0.75f).SetDelay(0.5f).SetEase(Ease.InFlash);
            }
        }
    }
        

    private void stopMove()
    {
        moving = false;
        animator.SetBool("Right", false);
        animator.SetBool("Left", false);
    }
    

}
