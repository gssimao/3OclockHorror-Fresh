using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedBookLineCntrl : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("SetScale");
    }

    IEnumerator SetScale()
    {
        yield return new WaitForEndOfFrame();
        //Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        //Vector2 S = gameObject.GetComponent<Image>().sprite.bounds.size;
        Vector2 S = new Vector2(gameObject.GetComponent<RectTransform>().rect.size.x, gameObject.GetComponent<RectTransform>().rect.size.y);
        gameObject.GetComponent<BoxCollider2D>().size = S;
        //gameObject.GetComponent<BoxCollider2D>().offset = new Vector2((S.x / 2), (S.y/2));
    }
}
