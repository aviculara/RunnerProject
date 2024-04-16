using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    
    public Positions position;
    public GameObject playerGroup;
    public GameManager gameManager;
    [Header("Editor Params")]
    public int speed = 10;
    public float side = 3;
    public Animator animator;
    private bool moving = false;
    

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
            playerGroup.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
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
                animator.SetTrigger("Left");
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
                animator.SetTrigger("Right");
                moving = true;
            }
        }
    }
        

    private void stopMove()
    {
        moving = false;
    }
    

}
