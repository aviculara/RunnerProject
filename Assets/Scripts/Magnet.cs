using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float coinSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnTriggerStay(Collider other)
    {
        //GameObject collidedObject = other.gameObject;
        //print("colliding");
        //print(collidedObject.tag);
        //print(other.tag);
        if (other.CompareTag("Collectible"))
        {
            Vector3 playerDirection =  - other.transform.position + transform.position;
            
            other.transform.Translate(playerDirection.normalized * coinSpeed * Time.deltaTime , Space.World);
        }
        else
        {
            //print(collidedObject.tag);
        }
        //give transform to other
        
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        //print(other.tag);
        print(transform.position);
        Instantiate(sampleStrawberry, transform.position, Quaternion.identity);
    }
    */

    //public void MagnetPickup()
    //{
    //    coinSpeed = playerMovement.speed + 10;
    //}

    private void OnEnable()
    {
        //coinSpeed = playerMovement.speed + 10;
    }
    //IEnumerator MagnetOff()
    //{
    //    yield return new WaitForSeconds(magnetTime);
    //    //magnetOn = false;
    //    gameObject.SetActive(false);
    //    print("its off");
    //}
}
