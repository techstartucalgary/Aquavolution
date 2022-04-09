using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinTakesDamage : MonoBehaviour
{
    public SpriteRenderer SkinSprite;

    // Start is called before the first frame update
    void Start()
    {
        SkinSprite = GetComponent<SpriteRenderer>();
    }

    public void ChangeSkinColor()
    {
        SkinSprite.color = new Color (1, 0, 0, 1);
        StartCoroutine("ReverToOriginalColor");
    }

    IEnumerator ReverToOriginalColor()
    {
        yield return new WaitForSeconds(0.15f);
        SkinSprite.color = new Color (1, 1, 1, 1);
    }
}
