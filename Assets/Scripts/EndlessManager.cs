using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    //public GameObject piece;
    public GameObject parent;
    public ScoreManager scoreManager;
    [SerializeField] PlayerManager player;

    [SerializeField] GameObject[] pieces;
    [SerializeField] List<GameObject> collectibles = new List<GameObject>();
    public GameObject strawb; 
    public GameObject star, magnet, watermelon, banana, cherry, pear, orange;

    [Header("Chances")]
    public int minPowerupFrequency;
    public int maxPowerupFrequency;
    private int powerupPercent; //old
    public int strawbWeight;
    public int nothingWeight;
    public int magnetWeight;
    public int starWeight;
    public int bananaWeight;
    public int watermelonWeight;

    private int randomFrequency;
    private int collectibleCount = 0;
    //private float randomFrequency;

    [Header("Developer")]
    [SerializeField] bool prefabTest = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        randomFrequency = Random.Range(minPowerupFrequency, maxPowerupFrequency);
#if UNITY_EDITOR
        if(prefabTest)
        {
            PrefabTest();
        }
        else
        {
            FirstPlacement();
        }
#else
        FirstPlacement();
#endif

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
        if(other.CompareTag("Piece") && !prefabTest)
        {
            int randomint = Random.Range(0, pieces.Length);
            int randomRotation = Random.Range(0, 2);
            GameObject piece = pieces[randomint];
            //print(other.transform.position);
            Vector3 newpos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + (5 * 40));
            Quaternion newRotation = Quaternion.Euler(0, 180 * randomRotation, 0);
            GameObject newpiece = Instantiate(piece, newpos, newRotation, parent.transform);
            //Transform newParent = newpiece.transform.Find("Collectibles");
            //Transform posParent = newpiece.transform.Find("CollectiblePos");
            //collectibles: index 0
            //collectiblespos: index 1
            Transform newParent = newpiece.transform.GetChild(0);
            Transform posParent = newpiece.transform.GetChild(1);

            if (posParent != null)
            {
                PseudoRandomCollectibles(posParent,newParent);
            }
            
            
        }
        Destroy(other.gameObject);
    }

    private void PlaceCollectibles(Transform posParent, Transform newParent)
    {
        foreach (Transform childTransform in posParent)
        {
            int powerChance = Random.Range(1, 101);

            if(powerChance <= powerupPercent)
            {
                int rand = Random.Range(1, 101);
                if (rand <= magnetWeight)
                {
                    Instantiate(magnet, childTransform.position, magnet.transform.rotation, newParent);
                }
                else if (rand <= magnetWeight + starWeight)
                {
                    Instantiate(star, childTransform.position, star.transform.rotation, newParent);
                }
                else if (rand <= magnetWeight + starWeight + bananaWeight)
                {
                    Instantiate(banana, childTransform.position, banana.transform.rotation, newParent);
                }
                else if (rand <= magnetWeight + starWeight + bananaWeight + watermelonWeight)
                {
                    Instantiate(watermelon, childTransform.position, watermelon.transform.rotation, newParent);
                }
                else
                {

                }
            }
            else if(powerChance < strawbWeight + powerupPercent)
            {
                Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
            }

        }
    }

    private void PseudoRandomCollectibles(Transform posParent, Transform newParent)
    {
        foreach (Transform childTransform in posParent)
        {

            if (randomFrequency <= collectibleCount)
            {
                print("placed powerup after " + randomFrequency + "strawberries");
                int tempWatermelonWeight = watermelonWeight;
                if(player.watermelond)
                {
                    tempWatermelonWeight = 1;
                }

                int rand = Random.Range(1, magnetWeight + starWeight + bananaWeight + tempWatermelonWeight + 1);

                if (rand <= magnetWeight)
                {
                    Instantiate(magnet, childTransform.position, magnet.transform.rotation, newParent);
                }
                else if (rand <= magnetWeight + starWeight)
                {
                    Instantiate(star, childTransform.position, star.transform.rotation, newParent);
                }
                else if (rand <= magnetWeight + starWeight + bananaWeight)
                {
                    Instantiate(banana, childTransform.position, banana.transform.rotation, newParent);
                }
                //else if (rand <= magnetWeight + starWeight + bananaWeight + tempWatermelonWeight)
                //{
                //    Instantiate(watermelon, childTransform.position, watermelon.transform.rotation, newParent);
                //}
                else
                {
                    Instantiate(watermelon, childTransform.position, watermelon.transform.rotation, newParent);
                }
                randomFrequency = Random.Range(minPowerupFrequency, maxPowerupFrequency);
                collectibleCount = 0;
            }
            else
            {
                int rand = Random.Range(1, strawbWeight + nothingWeight +1);
                print(rand + " " + (strawbWeight + nothingWeight));
                if(rand <= strawbWeight)
                {
                    Instantiate(strawb, childTransform.position, strawb.transform.rotation, newParent);
                    print("placed strawberry");
                    collectibleCount += 1;
                }
                else
                {
                    print("skipped strawberry");
                }
                
            }

        }
    }

    private void FirstPlacement()
    {
        for(int i = 1; i<5; i++)
        {
            int randomint = Random.Range(0, pieces.Length);
            int randomRotation = Random.Range(0, 2);
            GameObject piece = pieces[randomint];
            //print(other.transform.position);
            Vector3 newpos = new Vector3(0, 0, 0 + (i * 40));
            Quaternion newRotation = Quaternion.Euler(0, 180 * randomRotation, 0);
            GameObject newpiece = Instantiate(piece, newpos, newRotation, parent.transform);
            //Transform newParent = newpiece.transform.Find("Collectibles");
            //Transform posParent = newpiece.transform.Find("CollectiblePos");
            //collectibles: index 0
            //collectiblespos: index 1
            Transform newParent = newpiece.transform.GetChild(0);
            Transform posParent = newpiece.transform.GetChild(1);

            if (posParent != null)
            {
                PseudoRandomCollectibles(posParent, newParent);
            }
        }
    }

    private void PrefabTest()
    {
        int i = 1;
        foreach(GameObject piece in pieces)
        {
            Vector3 newpos = new Vector3(0, 0, 0 + (i * 40));
            i += 1;
            Quaternion newRotation = Quaternion.Euler(0, 0, 0);
            GameObject newpiece = Instantiate(piece, newpos, newRotation, parent.transform);
            //Transform newParent = newpiece.transform.Find("Collectibles");
            //Transform posParent = newpiece.transform.Find("CollectiblePos");
            //collectibles: index 0
            //collectiblespos: index 1
            Transform newParent = newpiece.transform.GetChild(0);
            Transform posParent = newpiece.transform.GetChild(1);

            if (posParent != null)
            {
                PseudoRandomCollectibles(posParent, newParent);
            }

            newpos = new Vector3(0, 0, 0 + (i * 40));
            i += 1;
            newRotation = Quaternion.Euler(0, 180, 0);
            newpiece = Instantiate(piece, newpos, newRotation, parent.transform);
            //Transform newParent = newpiece.transform.Find("Collectibles");
            //Transform posParent = newpiece.transform.Find("CollectiblePos");
            //collectibles: index 0
            //collectiblespos: index 1
            newParent = newpiece.transform.GetChild(0);
            posParent = newpiece.transform.GetChild(1);

            if (posParent != null)
            {
                PseudoRandomCollectibles(posParent, newParent);
            }
        }
    }
    
}
