using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostumeManager : MonoBehaviour
{
    [SerializeField] Costume[] headCostumes;
    [SerializeField] Costume[] bodyCostumes;
    [SerializeField] Image headImageLeft, headImageCenter, headImageRight;
    [SerializeField] Image bodyImageLeft, bodyImageCenter, bodyImageRight;
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

        if (costumeType == CostumeType.head)
        {
            //headImageCenter.sprite = headCostumes[headShopIndex].shopImage;
            costumes = headCostumes;
            centerImage = headImageCenter;
            leftImage = headImageLeft;
            rightImage = headImageRight;
            shownIndex = headShopIndex;
        }
        else
        {
            costumes = bodyCostumes;
            centerImage = bodyImageCenter;
            leftImage = bodyImageLeft;
            rightImage = bodyImageRight;
            shownIndex = bodyShopIndex;
        }
        print(shownIndex);
        
        if (shownIndex > costumes.Length - 1 || shownIndex < 0 )
        {
            print("invalid shop index");
        }
        else
        {
            centerImage.sprite = costumes[shownIndex].shopImage;
            if(shownIndex <= 0)
            {
                leftImage.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                if (shownIndex == 1)
                {
                    leftImage.transform.parent.gameObject.SetActive(true);
                }
                leftImage.sprite = costumes[shownIndex - 1].shopImage;
            }

            if(shownIndex >= costumes.Length - 1)
            {
                rightImage.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                if (shownIndex == costumes.Length - 2)
                {
                    rightImage.transform.parent.gameObject.SetActive(true);
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

}
