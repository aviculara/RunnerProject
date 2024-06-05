using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private int bananaLevel;
    private int magnetLevel;
    private int starLevel;

    public int totalStrawberries;
    [SerializeField] TextMeshProUGUI strawberriesText;

    [SerializeField] Image[] bananaBars;
    [SerializeField] Image[] magnetBars;
    [SerializeField] Image[] starBars;


    [SerializeField] Button bananaButton, magnetButton, starButton;
    [SerializeField] TextMeshProUGUI bananaSecondsText, magnetSecondsText, starSecondsText;
    [SerializeField] TextMeshProUGUI bananaPriceText, magnetPriceText, starPriceText;

    public float[] seconds = { 2.5f, 3f, 4f, 6f, 10f };
    public int[] prices = { 30, 50, 150, 400 };

    PowerupManager powerupManager;

    // Start is called before the first frame update
    void Start()
    {
        powerupManager = gameObject.GetComponent<PowerupManager>();
        bananaLevel = PlayerPrefs.GetInt("BananaLevel", 0);
        powerupManager.bananaDuration = seconds[bananaLevel];
        magnetLevel = PlayerPrefs.GetInt("MagnetLevel", 0);
        powerupManager.magnetDuration = seconds[magnetLevel];
        starLevel = PlayerPrefs.GetInt("StarLevel", 0);
        powerupManager.magnetDuration = seconds[starLevel];
        totalStrawberries = PlayerPrefs.GetInt("TotalStrawberries", 0);
        strawberriesText.text = totalStrawberries.ToString();
        ShowBars();
        WritePrices();
        WriteSeconds();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowBars(Image[] barsList = null,int level = -1)
    {
        if (level < 0)
        {
            ShowBars(bananaBars,bananaLevel);
            ShowBars(magnetBars,magnetLevel);
            ShowBars(starBars,starLevel);
        }
        else
        {
            for(int i = 0; i< barsList.Length; i++)
            {
                if(i>level)
                {
                    barsList[i].color = new Color(0, 0, 0, 0.717f);
                    //not purchased = gray
                }
                else
                {
                    barsList[i].color = Color.white;
                }
                
            }
        }
    }

    private void WritePrices(TextMeshProUGUI priceText = null, Button button = null, int level = -1)
    {
        if( level <0)
        {
            WritePrices(starPriceText, starButton, starLevel);
            WritePrices(magnetPriceText, magnetButton, magnetLevel);
            WritePrices(bananaPriceText, bananaButton, bananaLevel);
        }
        else if(priceText == null || button == null)
        {
#if UNITY_EDITOR
            print("null object reference");
#endif
        }
        else
        {
            int price = prices[level];
            priceText.text = price.ToString();
            ShowButtons(button, level);          
        }
    }

    private void ShowButtons(Button button = null, int level = -1)
    {
        if (level < 0)
        {
            //show all
            ShowButtons(bananaButton, bananaLevel);
            ShowButtons(magnetButton, magnetLevel);
            ShowButtons(starButton, starLevel);
        }
        else if(button == null)
        {
            #if UNITY_EDITOR
            print("null object reference");
            #endif
        }
        else
        {
            if(level >= prices.Length)
            {
                button.gameObject.SetActive(false);
            }
            else
            {
                button.interactable = (prices[level] <= totalStrawberries);
            }
            
        }
    }

    private void WriteSeconds(TextMeshProUGUI secondsText = null, int level = -1)
    {
        if ( level < 0)
        {
            WriteSeconds(starSecondsText, starLevel);
            WriteSeconds(magnetSecondsText, magnetLevel);
            WriteSeconds(bananaSecondsText, bananaLevel);
        }
        else if(secondsText == null)
        {
#if UNITY_EDITOR
            print("null object reference");
#endif
        }
        else
        {
            secondsText.text = seconds[level].ToString("0.0") + " s";
        }
    }

    public void AddStrawberry()
    {
        totalStrawberries++;
        PlayerPrefs.SetInt("TotalStrawberries", totalStrawberries);
        strawberriesText.text = totalStrawberries.ToString();
    }

    public void SubtractStrawberry(int amount)
    {
        totalStrawberries -= amount;
        PlayerPrefs.SetInt("TotalStrawberries", totalStrawberries);
        strawberriesText.text = totalStrawberries.ToString();
    }

    public void PurchaseBanana()
    {
        SubtractStrawberry(prices[bananaLevel]);
        bananaLevel++;
        PlayerPrefs.SetInt("BananaLevel", bananaLevel);
        powerupManager.bananaDuration = seconds[bananaLevel];
        ShowBars(bananaBars, bananaLevel);
        WritePrices(bananaPriceText, bananaButton, bananaLevel);
        WriteSeconds(bananaSecondsText, bananaLevel);
        ShowButtons();
    }
    public void PurchaseMagnet()
    {
        SubtractStrawberry(prices[magnetLevel]);
        magnetLevel++;
        PlayerPrefs.SetInt("MagnetLevel", magnetLevel);
        powerupManager.magnetDuration = seconds[magnetLevel];
        ShowBars(magnetBars, magnetLevel);
        WritePrices(magnetPriceText, magnetButton, magnetLevel);
        WriteSeconds(magnetSecondsText, magnetLevel);
        ShowButtons();
    }
    public void PurchaseStar()
    {
        SubtractStrawberry(prices[starLevel]);
        starLevel++;
        PlayerPrefs.SetInt("StarLevel", starLevel);
        powerupManager.starDuration = seconds[starLevel];
        ShowBars(starBars, starLevel);
        WritePrices(starPriceText, starButton, starLevel);
        WriteSeconds(starSecondsText, starLevel);
        ShowButtons();
    }
}
