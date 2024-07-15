using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostumeManager : MonoBehaviour
{
    [SerializeField] ShopManager shopManager;

    [SerializeField] Costume[] headCostumes;
    [SerializeField] Costume[] bodyCostumes;
    [SerializeField] Image headImageLeft, headImageCenter, headImageRight;
    [SerializeField] GameObject headRightButton, headLeftButton, headEquipButton;
    [SerializeField] TextMeshProUGUI headPrice;
    [SerializeField] Image bodyImageLeft, bodyImageCenter, bodyImageRight;
    [SerializeField] GameObject bodyRightButton, bodyLeftButton, bodyEquipButton;
    [SerializeField] TextMeshProUGUI bodyPrice;
    [SerializeField] int activeHeadCostume = -1;
    [SerializeField] int activeBodyCostume = -1;
    private int headShopIndex = 0;
    private int bodyShopIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        if(activeHeadCostume<0)
        {
            headShopIndex = 0;
        }
        else
        {
            headShopIndex = activeHeadCostume;
        }
        if(activeBodyCostume < 0)
        {
            bodyShopIndex = 0;
        }
        else
        {
            bodyShopIndex = activeBodyCostume;
        }
        ShowCostumeShop(CostumeType.head);
        //ShowCostumeShop(CostumeType.body);
    }

    private enum CostumeType
    {
        head,
        body
    }

    private void ShowCostumeShop(CostumeType costumeType)
    {
        Costume[] costumes;
        Image centerImage;
        Image leftImage;
        Image rightImage;
        int shownIndex;
        int equippedIndex;
        GameObject rightButton, leftButton, equipButton;
        TextMeshProUGUI priceText;

        if (costumeType == CostumeType.head)
        {
            //headImageCenter.sprite = headCostumes[headShopIndex].shopImage;
            costumes = headCostumes;
            centerImage = headImageCenter;
            leftImage = headImageLeft;
            rightImage = headImageRight;
            shownIndex = headShopIndex;
            equippedIndex = activeHeadCostume;
            rightButton = headRightButton;
            leftButton = headLeftButton;
            equipButton = headEquipButton;
            priceText = headPrice;
        }
        else
        {
            costumes = bodyCostumes;
            centerImage = bodyImageCenter;
            leftImage = bodyImageLeft;
            rightImage = bodyImageRight;
            shownIndex = bodyShopIndex;
            equippedIndex = activeBodyCostume;
            rightButton = bodyRightButton;
            leftButton = bodyLeftButton;
            equipButton = bodyEquipButton;
            priceText = bodyPrice;
        }
        //print(shownIndex);
        
        if (shownIndex > costumes.Length - 1 || shownIndex < 0 )
        {
            print("invalid shop index");
        }
        else
        {
            centerImage.sprite = costumes[shownIndex].shopImage;
            if(costumes[shownIndex].bought)
            {
                priceText.transform.parent.gameObject.SetActive(false);
                if(shownIndex == equippedIndex)
                {
                    equipButton.SetActive(false);
                }
                else
                {
                    equipButton.SetActive(true);
                }
            }
            else
            {
                priceText.transform.parent.gameObject.SetActive(true);
                priceText.text = costumes[shownIndex].price.ToString();
            }

            if (shownIndex <= 0)
            {
                leftImage.transform.parent.gameObject.SetActive(false);
                leftButton.SetActive(false);
            }
            else
            {
                if (shownIndex == 1)
                {
                    leftImage.transform.parent.gameObject.SetActive(true);
                    leftButton.SetActive(true);
                }
                leftImage.sprite = costumes[shownIndex - 1].shopImage;
            }

            if(shownIndex >= costumes.Length - 1)
            {
                rightImage.transform.parent.gameObject.SetActive(false);
                rightButton.SetActive(false);
            }
            else
            {
                if (shownIndex == costumes.Length - 2)
                {
                    rightImage.transform.parent.gameObject.SetActive(true);
                    rightButton.SetActive(true);
                }
                rightImage.sprite = costumes[shownIndex + 1].shopImage;
            }

        }

    }

    public void HeadButtonRight()
    {
        if(headShopIndex<headCostumes.Length -1)
        {
            headShopIndex++;
            ShowCostumeShop(CostumeType.head);
        }
    }

    public void HeadButtonLeft()
    {
        if(headShopIndex>0)
        {
            headShopIndex--;
            ShowCostumeShop(CostumeType.head);
        }
    }

    public void HeadPurchaseButton()
    {
        Costume currentCostume = headCostumes[headShopIndex];
        //shopManager.SubtractStrawberry(currentCostume.price);
        print("pretend bought " + currentCostume.shopName);
        currentCostume.Purchase();
        headPrice.transform.parent.gameObject.SetActive(false);
    }
}
