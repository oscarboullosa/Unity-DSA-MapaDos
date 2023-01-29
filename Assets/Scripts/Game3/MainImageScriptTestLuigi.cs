using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainImageScriptTestLuigi : MonoBehaviour
{
    // SerializeField allow us to see this value in the inespector
    [SerializeField] private GameObject Interrogante;
    [SerializeField] private GameControllerTestLuigi gameController;

    public void OnMouseDown()
    {
        if (Interrogante.activeSelf && gameController.canOpen)
        {
            Interrogante.SetActive(false);
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
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void Close()
    {
        Interrogante.SetActive(true); // HideImage
    }
}
