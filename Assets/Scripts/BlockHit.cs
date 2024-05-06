//Thomas Gorman - Game Engine Scripting
//Assets provided by, presumably, Nintendo
//Lots of code and help provided by Zigurous https://www.youtube.com/@Zigurous/

using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    //If negative, block can be hit an infinite amount of times, set per prefab or block in editor
    public int maxHits = -1;
    private bool animating;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            //If Mario collides with block while jumping (moving up), Mario cannot hit block from falling
            if (collision.transform.DotTest(transform, Vector2.up)) {
                Hit();
            }
        }
        else
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                AudioManager.instance.Play_HitBlockEmpty();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true; // show if hidden

        maxHits--;

        if (maxHits == 0) {
            //If block can be empty after hit, set to empty sprite, default sprite is invisible
            spriteRenderer.sprite = emptyBlock;
        }

        if (item != null) {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());
        AudioManager.instance.Play_HitBlock();
    }

    private IEnumerator Animate()
    {
        //Stops block from being hit while in hit animation coroutine
        animating = true;

        //Move it up and down by seting normal position and peak position, then moving between.
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition, 0.125f);
        yield return Move(animatedPosition, restingPosition, 0.125f);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to, float duration)
    {
        //Sets animation time, I love animating in code and not touching the animation window in Unity. I'll leave that to the animators.
        //I think this Move method can be refactored into its own class but would require pasing in the game object as well, so i didn't do it. See the extra duration argument here. I tried something. Didn't like it.
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }

}
