using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public float magnetDuration = 5f;
    public float bananaDuration = 5f;
    public float starDuration = 5f;

    public float magnetTimeLeft, bananaTimeLeft, starTimeLeft;

    public bool magnetActive;
    public bool bananaActive;
    public bool starActive;

    private PlayerManager playerManager;

    [Header("Banana Head Start")]
    public GameObject foxRenderer;
    public GameObject mainFox;
    public GameObject bananaObject;
    private Rigidbody foxRB;

    [Header("Magnet")]
    public GameObject magnetObject;
    private Magnet magnetScript;
    private PlayerMovement playerMovement;

    [Header("Icons")]
    public GameObject[] powerIcons;
    public Sprite magnetSprite;
    public Sprite bananaSprite;
    public Sprite starSprite;

    private List<Collectible.CollectibleType> activePowerups = new List<Collectible.CollectibleType>();


    // Start is called before the first frame update
    void Start()
    {
        playerManager = mainFox.GetComponent<PlayerManager>();
        magnetScript = magnetObject.GetComponent<Magnet>();
        playerMovement = mainFox.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i = 0; i < activePowerups.Count; i++)
        {
            //check if still active and subtract time
            Collectible.CollectibleType activePowerup = activePowerups[i];
            GameObject powerIcon = powerIcons[i];
            
            Image iconImage = powerIcon.transform.GetChild(0).GetComponent<Image>();
            switch (activePowerup)
            {
                case Collectible.CollectibleType.banana:
                    iconImage.sprite = bananaSprite;
                    bananaTimeLeft -= Time.unscaledDeltaTime;
                    if(bananaTimeLeft > 0)
                    {
                        powerIcon.SetActive(true);
                    }
                    //fill
                    break;
                case Collectible.CollectibleType.magnet:
                    iconImage.sprite = magnetSprite;
                    magnetTimeLeft -= Time.unscaledDeltaTime;
                    break;
                case Collectible.CollectibleType.star:
                    iconImage.sprite = starSprite;
                    starTimeLeft -= Time.unscaledDeltaTime;
                    break;
            }
        }
        //subtract times after iterating through list

    }

    public void BananaCollected()
    {

        print("banana start");
        bananaObject.SetActive(true);
        foxRenderer.SetActive(false);
        bananaActive = true;
        bananaTimeLeft = bananaDuration;
        playerManager.bananaOn = true;

        Time.timeScale = 2.5f;
        //StartCoroutine(BananaTimer());
    }

    private IEnumerator BananaTimer()
    {
        yield return new WaitForSeconds(bananaDuration * 2.5f);
        foxRenderer.SetActive(true);
        bananaActive = false;
        playerManager.bananaOn = false;
        Time.timeScale = 1;
        //mainFox.transform.Translate(mainFox.transform.position.x,            mainFox.transform.position.y + 4f, mainFox.transform.position.z);
        foxRB = mainFox.GetComponent<Rigidbody>();
        foxRB.AddForce(Vector3.up * 5, ForceMode.Impulse);
        print("banana end");
        bananaObject.SetActive(false);
    }

    public void MagnetCollected()
    {
        magnetObject.SetActive(true);
        magnetScript.coinSpeed = playerMovement.speed + 10f;
        StartCoroutine(MagnetTimer());
    }

    IEnumerator MagnetTimer()
    {
        yield return new WaitForSeconds(magnetDuration);
        //magnetOn = false;
        magnetObject.SetActive(false);
        print("its off");
    }

}
