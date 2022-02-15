using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Text text1;
    public float timeToAppear = 2f;
    private float timeWhenDisappear;


    // Start is called before the first frame update
    void Start()
    {
        EnableText();
    }

    // Update is called once per frame
    void Update()
    {
        float position = GetComponent<Rigidbody2D>().position.x;
        //add positional calls
        if(text1.enabled && Time.time>= timeWhenDisappear)
        {
            text1.enabled = false;
        }
    }

    public void EnableText()
    {
        text1.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }
}
