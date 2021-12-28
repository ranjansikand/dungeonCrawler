using System.Collections;
using UnityEngine;

public class StatusSprite : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    WaitForSeconds _delay = new WaitForSeconds(1.5f);

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void UpdateSprite(Sprite newSprite)
    {
        if (_spriteRenderer != null) _spriteRenderer.sprite = newSprite;

        StartCoroutine(IClearSprite());
    }

    IEnumerator IClearSprite()
    {
        yield return _delay;

        _spriteRenderer.sprite = null;
    }
}
