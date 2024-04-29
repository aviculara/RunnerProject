using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    public GameObject piece;
    public GameObject parent;
    public ScoreManager scoreManager;
    [Header("Collectibles")]
    public GameObject strawb; 
    public GameObject star, magnet, watermelon, cherry, banana, orange;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if(collision.gameObject.CompareTag("Piece"))
        {
            Destroy(collision.gameObject);
        }
        
    }
    */
    
    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name);
        if(other.CompareTag("Piece"))
        {
            //print(other.transform.position);
            Vector3 newpos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + (3 * 40));
            GameObject newpiece = Instantiate(piece, newpos, UnityEngine.Quaternion.identity, parent.transform);
            Transform newParent = newpiece.transform.Find("Collectibles");
            Transform posParent = newpiece.transform.Find("CollectiblePos");
            if (posParent != null)
            {
                PlaceCollectibles(posParent,newParent);
            }
            
            Destroy(other.gameObject);
        }
    }

    private void PlaceCollectibles(Transform posParent, Transform newParent)
    {
        foreach (Transform childTransform in posParent)
        {
            int rand = Random.Range(1, 100);
            if(rand <=55)
            {
                Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
            }
            else if(rand <= 60)
            {
                if(scoreManager.cherry)
                {
                    Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
                }
                else
                {
                    Instantiate(cherry, childTransform.position, cherry.transform.rotation, newParent);
                }
            }
            else if (rand <= 65)
            {
                if (scoreManager.banana)
                {
                    Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
                }
                else
                {
                    Instantiate(banana, childTransform.position, banana.transform.rotation, newParent);
                }
            }
            else if (rand <= 70)
            {
                if (scoreManager.orange)
                {
                    Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
                }
                else
                {
                    Instantiate(orange, childTransform.position, orange.transform.rotation, newParent);
                }
            }
            else if(rand <= 75)
            {
                Instantiate(star, childTransform.position, star.transform.rotation, newParent);
            }
            else if(rand <= 90)
            {
                Instantiate(magnet, childTransform.position, magnet.transform.rotation, newParent);
            }
            else
            {
                Instantiate(watermelon, childTransform.position, watermelon.transform.rotation, newParent);
            }
        }
    }
    
}
