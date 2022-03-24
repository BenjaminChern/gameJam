using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour
{
    public GameObject canvas;
    public Texture2D heart;
    public Texture2D halfHeart;
    public Texture2D noHeart;

    private int leftMost = -375;
    private int height = 150;
    private int spacing = 30;

    public GameObject[] hearts;
    //public Sprite refSprite;

    // Use this for initialization
    void Start()
    {
        int count = 0;
        hearts = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            makeHeart(leftMost + spacing*i, height, heart, "heart"+i, i, hearts);
        }
        for(int i = 0; i< hearts.Length; i++)
        {
            print(hearts[i]);
        }



    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void healthSet(int health)
    {
        print("test for child in method:  " + canvas.gameObject.transform.GetChild(0).gameObject);
        print("health is:       " + health);
        for (int i = 0; i < hearts.Length; i++)
        {
            print("inside method:    " + hearts[i]);
        }
        for (int i = 0; i < 3; i++)
        {
            print(hearts);
            /*if (hearts.Length > 0)
            {
                Destroy(hearts[i]);
            }
            */
            Destroy(canvas.gameObject.transform.GetChild(i).gameObject);


            if (health >= i * 2 + 2)
            {
                makeHeart(leftMost + spacing * i, height, heart, "heart" + i + "test", i, hearts);
            } else if (health >= i * 2 + 1)
            {
                makeHeart(leftMost + spacing * i, height, halfHeart, "heart" + i + "test", i, hearts);
            } else
            {
                makeHeart(leftMost + spacing * i, height, noHeart, "heart" + i + "test", i, hearts);
            }
        }
    }

    void makeHeart(int x, int y, Texture2D tex, string name, int position, GameObject[] hearts)
    {
        GameObject imgObject = new GameObject(name);

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(x, y); // setting position, will be on center
        trans.sizeDelta = new Vector2(30, 30); // custom size

        Image image = imgObject.AddComponent<Image>();
        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        imgObject.transform.SetParent(canvas.transform);
        hearts[position] = imgObject;
        print("here" + hearts[position] + "   " + hearts.Length);
        print("test for child:  " + canvas.gameObject.transform.GetChild(position).gameObject);
    }
}
