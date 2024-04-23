using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //public GameObject obstacleParent;
    public GameObject sampleStrawb;
    public GameObject[] obstacles;
    public GameObject destroyParent;
    public GameObject starIcon;

    public float powerDuration = 5f;

    private float remainingDuration;
    // Start is called before the first frame update
    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        starIcon.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Star
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
        obj.SetActive(true);
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
    #endregion


}
