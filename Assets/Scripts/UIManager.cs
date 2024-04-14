using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    GameObject player;
    PlayerMovement pScript;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //birden fazla varsa ve liste olarak yapmak için FindGameObjectsWithTag
        pScript = player.GetComponent<PlayerMovement>();
    }
    void Start()
    {
        startPanel.SetActive(true);
        //Time.timeScale = 0; //animasyonlar da duruyor
        pScript.speed = 0;
        pScript.animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaptoStart()
    {
        startPanel.SetActive(false);
        //Time.timeScale = 1;
        player.GetComponent<PlayerMovement>().speed = 6;
        pScript.animator.SetBool("Idle", false);
    }
}
