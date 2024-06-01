using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleType ctype;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum CollectibleType
    { 
        strawberry,
        watermelon,
        cherry,
        pear,
        orange,
        star,
        magnet,
        banana
    }
}
