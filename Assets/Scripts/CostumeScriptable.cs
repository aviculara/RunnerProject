using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Costume")]
public class CostumeScriptable : ScriptableObject
{
    public Sprite image;
    public CostumeType type;
    public GameObject costumeObject;
    public int price;
    public string nameString;
    public string hierarchyName;

    public enum CostumeType
    {
        Head,
        Body
    }

    public bool FindCostume()
    {
        costumeObject = GameObject.Find(hierarchyName);
        if(costumeObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
