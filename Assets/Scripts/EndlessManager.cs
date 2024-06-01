using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    //public GameObject piece;
    public GameObject parent;
    public ScoreManager scoreManager;

    [SerializeField] GameObject[] pieces;
    [SerializeField] List<GameObject> collectibles = new List<GameObject>();
    public GameObject strawb; 
    public GameObject star, magnet, watermelon, banana, cherry, pear, orange;


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
            int randomint = Random.Range(0, pieces.Length);
            int randomRotation = Random.Range(0, 2);
            GameObject piece = pieces[randomint];
            //print(other.transform.position);
            Vector3 newpos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + (5 * 40));
            Quaternion newRotation = Quaternion.Euler(0, 180 * randomRotation, 0);
            GameObject newpiece = Instantiate(piece, newpos, newRotation, parent.transform);
            Transform newParent = newpiece.transform.Find("Collectibles");
            Transform posParent = newpiece.transform.Find("CollectiblePos");
            if (posParent != null)
            {
                PlaceCollectibles(posParent,newParent);
            }
            
            
        }
        Destroy(other.gameObject);
    }

    private void PlaceCollectibles(Transform posParent, Transform newParent)
    {
        foreach (Transform childTransform in posParent)
        {
            
                int rand = Random.Range(1, 101);
                if (rand <= 80)
                {

                }
                else if (rand <= 90)
                {
                    Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
                }

                //else if (rand <= 75)
                //{
                //    if (scoreManager.cherry)
                //    {
                //        Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
                //    }
                //    else
                //    {
                //        Instantiate(cherry, childTransform.position, cherry.transform.rotation, newParent);
                //    }
                //}
                //else if (rand <= 80)
                //{
                //    if (scoreManager.banana)
                //    {
                //        Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
                //    }
                //    else
                //    {
                //        Instantiate(banana, childTransform.position, banana.transform.rotation, newParent);
                //    }
                //}
                //else if (rand <= 85)
                //{
                //    if (scoreManager.orange)
                //    {
                //        Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
                //    }
                //    else
                //    {
                //        Instantiate(orange, childTransform.position, orange.transform.rotation, newParent);
                //    }
                //}
                else if (rand <= 92)
                {
                    Instantiate(star, childTransform.position, star.transform.rotation, newParent);
                }
                else if (rand <= 95)
                {
                    Instantiate(magnet, childTransform.position, magnet.transform.rotation, newParent);
                }
                else if (rand <= 98)
            {
                Instantiate(banana, childTransform.position, banana.transform.rotation, newParent);
            }
                else
                {
                    Instantiate(watermelon, childTransform.position, watermelon.transform.rotation, newParent);
                }
            
            
        }
    }
    
}
