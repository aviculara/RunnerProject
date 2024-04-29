using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    public GameObject piece;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {

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
            Instantiate(piece, newpos, UnityEngine.Quaternion.identity, parent.transform);
            Destroy(other.gameObject);
        }
    }
    
}
