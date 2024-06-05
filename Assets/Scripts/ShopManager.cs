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

    [SerializeField] TextMeshProUGUI bananaSecondsText, magnetSecondsText, starSecondsText;
    [SerializeField] TextMeshProUGUI bananaPriceText, magnetPriceText, starPriceText;

    public float[] seconds = { 2.5f, 3f, 4f, 6f, 10f };
    public int[] prices = { 30, 50, 150, 400 };

    // Start is called before the first frame update
    void Start()
    {
        bananaLevel = PlayerPrefs.GetInt("BananaLevel", 0);
        magnetLevel = PlayerPrefs.GetInt("MagnetLevel", 0);
        starLevel = PlayerPrefs.GetInt("StarLevel", 0);
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
        if (barsList == null || level == -1)
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

    private void WritePrices(TextMeshProUGUI priceText = null, int level = -1)
    {
        if(priceText == null || level <0)
        {
            WritePrices(starPriceText, starLevel);
            WritePrices(magnetPriceText, magnetLevel);
            WritePrices(bananaPriceText, bananaLevel);
        }
        else
        {
            int price = prices[level];
            priceText.text = price.ToString();
            ShowButtons(priceText.gameObject.GetComponentInParent<Button>(), level);          
        }
    }

    private void ShowButtons(Button button = null, int level = -1)
    {
        if (level < 0 || button == null)
        {
            //show all
            ShowButtons(bananaPriceText.gameObject.GetComponentInParent<Button>(), bananaLevel);
            ShowButtons(magnetPriceText.gameObject.GetComponentInParent<Button>(), magnetLevel);
            ShowButtons(starPriceText.gameObject.GetComponentInParent<Button>(), starLevel);
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
        if (secondsText == null || level < 0)
        {
            WriteSeconds(starSecondsText, starLevel);
            WriteSeconds(magnetSecondsText, magnetLevel);
            WriteSeconds(bananaSecondsText, bananaLevel);
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
        ShowBars(bananaBars, bananaLevel);
        WritePrices(bananaPriceText, bananaLevel);
        WriteSeconds(bananaSecondsText, bananaLevel);
        ShowButtons();
    }
    public void PurchaseMagnet()
    {
        SubtractStrawberry(prices[magnetLevel]);
        magnetLevel++;
        ShowBars(magnetBars, magnetLevel);
        WritePrices(magnetPriceText, magnetLevel);
        WriteSeconds(magnetSecondsText, magnetLevel);
        ShowButtons();
    }
    public void PurchaseStar()
    {
        SubtractStrawberry(prices[starLevel]);
        starLevel++;
        ShowBars(starBars, starLevel);
        WritePrices(starPriceText, starLevel);
        WriteSeconds(starSecondsText, starLevel);
        ShowButtons();
    }
}
