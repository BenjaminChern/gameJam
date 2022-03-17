using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour
{
    public Texture2D fullHeart;
    public Image halfHeart;
    public Image noHeart;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        GameObject imgObject = new GameObject("fullHeart");

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(this.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0f, 0f); // setting position, will be on center
        trans.sizeDelta = new Vector2(150, 200); // custom size

        Image image = imgObject.AddComponent<Image>();
        Texture2D tex = Resources.Load<Texture2D>(fullHeart);
        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        imgObject.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
