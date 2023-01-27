using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainImageScript : MonoBehaviour
{
    [SerializeField] private GameObject Image_Unknown;
    [SerializeField] private GameControllerScript gameController;

    public void OnMouseDown()
    {
        if (Image_Unknown.activeSelf && gameController.canOpen)
        {
            Image_Unknown.SetActive(false);
            gameController.imageOpened(this);
        }
    }

    private int _spriteId;
    public int spriteId
    {
        get { return _spriteId; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<SpriteRenderer>().sprite = image; //Gets the sprite renderer component to change the sprite.
    }

    public void Close()
    {
        Image_Unknown.SetActive(true); // Hide image
    }
}