using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStart : MonoBehaviour
{
    public GameObject foxRenderer;
    public GameObject mainFox;
    public PlayerManager playerManager;
    private Rigidbody foxRB;
    public float bananaDuration = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BananaCollected()
    {
        print("banana start");
        foxRenderer.SetActive(false);
        playerManager.bananaOn = true;
        Time.timeScale = 2.5f;
        StartCoroutine(BananaTimer());
    } 
    
    private IEnumerator BananaTimer()
    {
        yield return new WaitForSeconds(bananaDuration * 2.5f);
        foxRenderer.SetActive(true);
        playerManager.bananaOn = false;
        Time.timeScale = 1;
        //mainFox.transform.Translate(mainFox.transform.position.x,            mainFox.transform.position.y + 4f, mainFox.transform.position.z);
        foxRB = mainFox.GetComponent<Rigidbody>();
        foxRB.AddForce(Vector3.up * 9, ForceMode.Impulse);
        print("banana end");
        gameObject.SetActive(false);
    }
}
