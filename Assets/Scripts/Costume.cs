using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costume : MonoBehaviour
{
    public Sprite shopImage;
    [SerializeField] private GameObject costumeObject; //gerek olmayabilir bu kendisi
    public string shopName;
    public int price;
    public bool isEquipped;
    public bool bought = false;

    private void Awake()
    {
        gameObject.SetActive(false);
        print("set " + shopName + " inactive");
        isEquipped = false;
        bought = (PlayerPrefs.GetInt(shopName, 0) > 0);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unequip()
    {
        print("unequipped " + shopName);
        gameObject.SetActive(false);
        isEquipped = false;
    }
    
    public void Equip()
    {
        gameObject.SetActive(true);
        isEquipped = true;
        print("equipped " + shopName);
    }

    public void Purchase()
    {

        bought = true;
        PlayerPrefs.SetInt(shopName, 1);
    }
}
