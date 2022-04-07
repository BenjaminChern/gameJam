using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkScreen : MonoBehaviour
{
    private GameObject canvas;
    public Texture2D black;
    private GameObject blackSquare;
    private RectTransform trans;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject;

        blackSquare = new GameObject("black");
        trans = blackSquare.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0, 0); // setting position, will be on center
        trans.sizeDelta = new Vector2(2000, 2000); // custom size

        image = blackSquare.AddComponent<Image>();
        image.sprite = Sprite.Create(black, new Rect(0, 0, black.width, black.height), new Vector2(0.5f, 0.5f));

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

        // blackSquare.GetComponent<MeshRenderer>().material.color.Seta = .1f;
    }

    // Update is called once per frame
    public void darken()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, .675f);
    }
    public void undarken()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }
}
