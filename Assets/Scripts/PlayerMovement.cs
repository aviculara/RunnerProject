using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    
    public Positions position;
    public GameObject playerGroup;

    [Header("Editor Params")]
    public int speed = 10;
    public float side = 3;

    private bool moving = false;
    public Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
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
            moving = true;
        }
    }

    private void stopMove()
    {
        moving = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            Death();
        }
        else if (other.CompareTag("Finish"))
        {
            Win();
        }
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        speed = 0;
        //open Lose screen etc
    }

    private void Win()
    {
        animator.SetTrigger("Win");
        speed = 0;
        //UI etc
    }
}
