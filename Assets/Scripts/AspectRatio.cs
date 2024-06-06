using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    public float targetAspect = 9f / 16f;
    public void Adjust()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;
        Camera camera = GetComponent<Camera>();

        if(scaleHeight < 1f)
        {
            Rect rect = camera.rect;

            rect.width = 1f;
            rect.height = scaleHeight;

            rect.x = 0;
            rect.y = (1f - scaleHeight) / 2f;
            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0;
            camera.rect = rect;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Adjust();
        //will not update if screen size is changed
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
