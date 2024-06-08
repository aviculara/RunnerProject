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

    public GameObject ghostFox;
    public AudioSource tickingSound;
    private bool tickingPlaying = false;
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

    [Header("Star")]
    public GameObject starObject;
    private Star starScript;


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
        starScript = starObject.GetComponent<Star>();
        ghostFox.SetActive(false);
        bananaObject.SetActive(false);
        ResetIcons();
    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i = 0; i < activePowerups.Count; i++)
        {
            //set icons in order
            Collectible.CollectibleType activePowerup = activePowerups[i];
            GameObject powerIcon = powerIcons[i];
            
            Image iconImage = powerIcon.transform.GetChild(0).GetComponent<Image>();
            switch (activePowerup)
            {
                case Collectible.CollectibleType.banana:
                    if(bananaTimeLeft>=0)
                    {
                        iconImage.sprite = bananaSprite;
                        powerIcon.GetComponent<Image>().fillAmount = bananaTimeLeft / bananaDuration;
                        powerIcon.SetActive(true);
                        bananaActive = true;
                    }
                    break;
                case Collectible.CollectibleType.magnet:
                    if(magnetTimeLeft>=0)
                    {
                        iconImage.sprite = magnetSprite;
                        powerIcon.GetComponent<Image>().fillAmount = magnetTimeLeft / magnetDuration;
                        powerIcon.SetActive(true);
                        magnetActive = true;
                    }
                    break;
                case Collectible.CollectibleType.star:
                    if(starTimeLeft>=0)
                    {
                        iconImage.sprite = starSprite;
                        powerIcon.GetComponent<Image>().fillAmount = starTimeLeft / starDuration;
                        powerIcon.SetActive(true);
                        starActive = true;
                    }
                    break;
            }
        }
        if(Time.timeScale > 0)
        {
            //subtract times after iterating through list
            for (int i = activePowerups.Count - 1; i >= 0; i--)
            {
                //check if still active and subtract time
                Collectible.CollectibleType activePowerup = activePowerups[i];
                GameObject powerIcon = powerIcons[i];

                switch (activePowerup)
                {
                    case Collectible.CollectibleType.banana:                        
                        if (bananaTimeLeft < 0)
                        {
                            powerIcon.SetActive(false);
                            activePowerups.RemoveAt(i);
                            ResetIcons();
                            BananaEnd();
                        }
                        else if(bananaTimeLeft < 1)
                        {
                            PlayTicking();
                        }
                        bananaTimeLeft -= Time.unscaledDeltaTime;
                        break;
                    case Collectible.CollectibleType.magnet:
                        if (magnetTimeLeft < 0)
                        {
                            powerIcon.SetActive(false);
                            activePowerups.RemoveAt(i);
                            ResetIcons();
                            MagnetEnd();
                        }
                        else if (magnetTimeLeft < 1)
                        {
                            PlayTicking();
                        }
                        magnetTimeLeft -= Time.unscaledDeltaTime;
                        break;
                    case Collectible.CollectibleType.star:
                        if (starTimeLeft < 0)
                        {
                            powerIcon.SetActive(false);
                            activePowerups.RemoveAt(i);
                            ResetIcons();
                            StarEnd();
                        }
                        else if (starTimeLeft < 1)
                        {
                            PlayTicking();
                        }
                        starTimeLeft -= Time.unscaledDeltaTime;
                        break;
                }
            }
        }
    }

    public void BananaCollected()
    {
        bananaTimeLeft = bananaDuration;
        if (!bananaActive)
        {
            bananaObject.SetActive(true);
            foxRenderer.SetActive(false);
            Time.timeScale = 2f;
            playerManager.bananaOn = true;
            activePowerups.Add(Collectible.CollectibleType.banana);
        }
        bananaActive = true;
        //StartCoroutine(BananaTimer());
    }

    private void BananaEnd()
    {
        foxRenderer.SetActive(true);
        bananaActive = false;
        playerManager.bananaOn = false;
        bananaActive = false;
        Time.timeScale = 1;
        //mainFox.transform.Translate(mainFox.transform.position.x,            mainFox.transform.position.y + 4f, mainFox.transform.position.z);
        foxRB = mainFox.GetComponent<Rigidbody>();
        foxRB.AddForce(Vector3.up * 5, ForceMode.Impulse);
        bananaObject.SetActive(false);
        playerManager.StartInvulnerableFor(1.5f);
    }

    public void MagnetCollected()
    {
        magnetTimeLeft = magnetDuration;
        if (!magnetActive)
        {
            magnetObject.SetActive(true);
            activePowerups.Add(Collectible.CollectibleType.magnet);
            
        }
        magnetScript.coinSpeed = playerMovement.speed + 10f;
        magnetActive = true;
        //StartCoroutine(MagnetTimer());
    }

    private void MagnetEnd()
    {
        magnetObject.SetActive(false);
        magnetActive = false;
    }

    public void StarCollected()
    {
        starTimeLeft = starDuration;
        if(!starActive)
        {
            starObject.SetActive(true);
            activePowerups.Add(Collectible.CollectibleType.star);
        }
    }

    private void StarEnd()
    {
        starActive = false;
        starScript.StarEnd();
        starObject.SetActive(false);
        playerManager.StartInvulnerableFor(1.5f);
    }

    private void ResetIcons()
    {
        foreach (GameObject powericon in powerIcons)
        {
            powericon.SetActive(false);
        }
    }    

    private void PlayTicking()
    {
        if(!tickingPlaying)
        {
            tickingSound.Play();
            tickingPlaying = true;
            StartCoroutine(StopTicking());
        }
        
    }

    IEnumerator StopTicking()
    {
        yield return new WaitForSecondsRealtime(1f);
        tickingPlaying = false;
    }
}
