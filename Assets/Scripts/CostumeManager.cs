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
    [SerializeField] int equippedHeadCostume = -1;
    [SerializeField] int equippedBodyCostume = -1;
    private int headShopIndex = 0;
    private int bodyShopIndex = 0;

    // Start is called before the first frame update

    private void Awake()
    {
#if UNITY_EDITOR
        EditorPrices();
#endif
        foreach (Costume costume in headCostumes)
        {
            costume.Initialize();
        }
        foreach(Costume costume in bodyCostumes)
        {
            costume.Initialize();
        }
    }

    void Start()
    {
        equippedHeadCostume = PlayerPrefs.GetInt("EquippedHead", -1);
        equippedBodyCostume = PlayerPrefs.GetInt("EquippedBody", -1);

        if(equippedHeadCostume >=0 && equippedHeadCostume <= headCostumes.Length)
        {
            headCostumes[equippedHeadCostume].Equip();
        }
        if (equippedBodyCostume >= 0 && equippedBodyCostume <= bodyCostumes.Length)
        {
            bodyCostumes[equippedBodyCostume].Equip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        if(equippedHeadCostume<0)
        {
            headShopIndex = 0;
        }
        else
        {
            headShopIndex = equippedHeadCostume;
        }
        if(equippedBodyCostume < 0)
        {
            bodyShopIndex = 0;
        }
        else
        {
            bodyShopIndex = equippedBodyCostume;
        }
        ShowCostumeShop(CostumeType.head);
        ShowCostumeShop(CostumeType.body);
    }

    private void OnDisable()
    {
        headShopIndex = 0;
        bodyShopIndex = 0;
    }

    public enum CostumeType
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
        int shownIndex; //problem
        int equippedIndex;
        GameObject rightButton, leftButton, equipButton;
        TextMeshProUGUI priceText;
        Button priceButton;

        if (costumeType == CostumeType.head)
        {
            //headImageCenter.sprite = headCostumes[headShopIndex].shopImage;
            costumes = headCostumes;
            centerImage = headImageCenter;
            leftImage = headImageLeft;
            rightImage = headImageRight;
            shownIndex = headShopIndex;
            equippedIndex = equippedHeadCostume;
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
            equippedIndex = equippedBodyCostume;
            rightButton = bodyRightButton;
            leftButton = bodyLeftButton;
            equipButton = bodyEquipButton;
            priceText = bodyPrice;
        }
        priceButton = priceText.transform.parent.GetComponent<Button>();
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
                priceButton.gameObject.SetActive(false);
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
                priceButton.gameObject.SetActive(true);
                priceText.text = costumes[shownIndex].price.ToString();
                if(shopManager.totalStrawberries < costumes[shownIndex].price)
                {
                    priceButton.interactable = false;
                }
                else
                {
                    priceButton.interactable = true;
                }
            }

            if (shownIndex <= 0)
            {
                leftImage.transform.parent.gameObject.SetActive(false);
                leftButton.SetActive(false);
            }
            else
            {
                leftImage.transform.parent.gameObject.SetActive(true);
                leftButton.SetActive(true);
                leftImage.sprite = costumes[shownIndex - 1].shopImage;
            }

            if(shownIndex >= costumes.Length - 1)
            {
                rightImage.transform.parent.gameObject.SetActive(false);
                rightButton.SetActive(false);
            }
            else
            {
                rightImage.transform.parent.gameObject.SetActive(true);
                rightButton.SetActive(true);
                rightImage.sprite = costumes[shownIndex + 1].shopImage;
            }

        }

    }

    private void EditorPrices()
    {
        foreach(Costume costume in headCostumes)
        {
            costume.price = 1;
        }
        foreach(Costume costume in bodyCostumes)
        {
            costume.price = 2;
        }
        print("prices reduced for editor");
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

        shopManager.SubtractStrawberry(currentCostume.price);
        //headEquipButton.SetActive(false);
        currentCostume.Purchase();
        headPrice.transform.parent.gameObject.SetActive(false);
    }

    public void HeadEquipButton()
    {
        if(equippedHeadCostume >= 0 && equippedHeadCostume < headCostumes.Length)
        {
            headCostumes[equippedHeadCostume].Unequip();
        }
        equippedHeadCostume = headShopIndex;
        headCostumes[equippedHeadCostume].Equip();
        PlayerPrefs.SetInt("EquippedHead", equippedHeadCostume);
        headEquipButton.SetActive(false);
    }

    public void HeadUnequipButton()
    {
        headCostumes[headShopIndex].Unequip();
        equippedHeadCostume = -1;
        PlayerPrefs.SetInt("EquippedHead", equippedHeadCostume);
        headEquipButton.SetActive(true);
    }

    public void BodyButtonRight()
    {
        if (bodyShopIndex < bodyCostumes.Length - 1)
        {
            bodyShopIndex++;
            ShowCostumeShop(CostumeType.body);
        }
    }

    public void BodyButtonLeft()
    {
        if (bodyShopIndex > 0)
        {
            bodyShopIndex--;
            ShowCostumeShop(CostumeType.body);
        }
    }

    public void BodyPurchaseButton()
    {
        Costume currentCostume = bodyCostumes[bodyShopIndex];

        shopManager.SubtractStrawberry(currentCostume.price);
        //headEquipButton.SetActive(false);
        currentCostume.Purchase();
        bodyPrice.transform.parent.gameObject.SetActive(false);
    }

    public void BodyEquipButton()
    {
        if (equippedBodyCostume >= 0 && equippedBodyCostume < bodyCostumes.Length)
        {
            bodyCostumes[equippedBodyCostume].Unequip();
        }
        equippedBodyCostume = bodyShopIndex;
        bodyCostumes[equippedBodyCostume].Equip();
        PlayerPrefs.SetInt("EquippedBody", equippedBodyCostume);
        bodyEquipButton.SetActive(false);
    }
    public void BodyUnequipButton()
    {
        bodyCostumes[bodyShopIndex].Unequip();
        equippedBodyCostume = -1;
        PlayerPrefs.SetInt("EquippedBody", equippedBodyCostume);
        bodyEquipButton.SetActive(true);
    }

    public void UniversalRight(string costumeType)
    {
        if(costumeType == "head")
        {
            HeadButtonRight();
        }
    }

}
