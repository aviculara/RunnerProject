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

    private void Awake()
    {
        gameObject.SetActive(false);
        isEquipped = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void unEquip()
    {
        gameObject.SetActive(false);
        isEquipped = false;
    }
    
    public void Equip()
    {
        gameObject.SetActive(true);
        isEquipped = true;
    }
}
