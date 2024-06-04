using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    //public GameObject obstacleParent;
    public GameObject sampleStrawb;
    public GameObject[] obstacles;
    public GameObject destroyParent;
    public Transform allParent;
    public GameObject starIcon;

    public float powerDuration = 5f;

    private float remainingDuration;
    private List<GameObject> createdStrawbs = new List<GameObject>();
    private List<GameObject> inactiveObstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //obstacles = GameObject.FindGameObjectsWithTag("Obstacle");


        //starIcon.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Old Star
    public void SeeknDestroy()
    {
        remainingDuration = powerDuration;
        starIcon.SetActive(true);
        //obstacleParent.SetActive(false);
        foreach (GameObject obs in obstacles)
        {
            Vector3 v3 = new Vector3(obs.transform.position.x, sampleStrawb.transform.position.y,
                obs.transform.position.z);
            obs.SetActive(false);
            Instantiate(sampleStrawb, v3, sampleStrawb.transform.rotation, destroyParent.transform);
            StartCoroutine(LaterActivate(obs));
        }

        StartCoroutine(LaterDestroy(destroyParent));
        StartCoroutine(StarFlash());
    }

    IEnumerator LaterDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(powerDuration);
        Destroy(destroyParent);
        //obstacleParent.SetActive(true);
    }

    IEnumerator LaterActivate(GameObject obj)
    {
        yield return new WaitForSeconds(powerDuration);
        if(obj != null)
        {
            obj.SetActive(true);
        }
        
    }

    IEnumerator StarFlash()
    {
        if (remainingDuration <= 0.6f)
        {
            starIcon.SetActive(false); 
        }
        else if(remainingDuration <= 1.8f)
        {

            starIcon.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            starIcon.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            remainingDuration -= 0.6f;
            StartCoroutine(StarFlash());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            remainingDuration -= 0.6f;
            StartCoroutine(StarFlash());
        }
        
    }
    IEnumerator LaterDestroyList()
    {
        yield return new WaitForSeconds(powerDuration);
        foreach (GameObject strawb in createdStrawbs)
        {
            if (strawb != null)
            {
                Destroy(strawb);
            }
        }
        createdStrawbs.Clear();
    }

    IEnumerator LaterActivateList()
    {
        yield return new WaitForSeconds(powerDuration);
        foreach (GameObject obs in inactiveObstacles)
        {
            if (obs != null)
            {
                obs.SetActive(true);
            }

        }
        inactiveObstacles.Clear();
    }

    IEnumerator LaterInactivateSelf()
    {
        yield return new WaitForSeconds(powerDuration);
        gameObject.SetActive(false);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        GameObject obs = other.gameObject;

        if(obs.CompareTag("Obstacle"))
        {
            Vector3 v3 = new Vector3(obs.transform.position.x, 1.4f,
                obs.transform.position.z);
            obs.SetActive(false);
            inactiveObstacles.Add(obs);
            GameObject newStrawb = Instantiate(sampleStrawb, v3, sampleStrawb.transform.rotation, allParent);
            createdStrawbs.Add(newStrawb);
        }
    }

    private void OnEnable()
    {
        
        //powerup sure gostergeleri degisecek
    }

    public void StarCollected()
    {
        print("im active");
        starIcon.SetActive(true);
        print(starIcon.activeSelf);
        print(starIcon.name);
        remainingDuration = powerDuration;
        StartCoroutine(StarFlash());
        StartCoroutine(LaterDestroyList());
        StartCoroutine(LaterActivateList());
        StartCoroutine(LaterInactivateSelf());
    }
}
