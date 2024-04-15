using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject obstacleParent;
    public GameObject sampleStrawb;
    public GameObject[] obstacles;
    public GameObject destroyParent;

    public float powerDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SeeknDestroy()
    {
        obstacleParent.SetActive(false);
        foreach (GameObject obs in obstacles)
        {
            print("doin it");
            Vector3 v3 = new Vector3(obs.transform.position.x, sampleStrawb.transform.position.y,
                obs.transform.position.z);

            Instantiate(sampleStrawb, v3, sampleStrawb.transform.rotation, destroyParent.transform);
        }

        LaterDestroy(destroyParent);
    }

    IEnumerator LaterDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(powerDuration);
        Destroy(destroyParent);
        obstacleParent.SetActive(true);
    }
}
