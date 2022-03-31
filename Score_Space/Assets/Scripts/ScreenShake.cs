using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    private float startX;
    private float startY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }

    public void shake()
    {
        /*
        float x = this.gameObject.GetComponent<Transform>().x;
        float y = this.gameObject.GetComponent<Transform>().y;
        startX = x;
        startY = y;
        x += Random.Range(-5, 5);
        y += Random.Range(-5, 5);
        transform.Translate(new Vector3(x, y, 0));
        Time.wait(.25);
        transform.Translate(new Vector3(startX, startY, 0));
        */
    }
}
