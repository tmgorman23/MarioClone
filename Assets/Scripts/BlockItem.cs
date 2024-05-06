//Thomas Gorman - Game Engine Scripting
//Assets provided by, presumably, Nintendo
//Lots of code and help provided by Zigurous https://www.youtube.com/@Zigurous/

using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        //Defines bodies and colliders for new object to make sure animation looks right since item and block occupy same space
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        //Disables physics simulation on rigidbody
        rigidbody.isKinematic = true;
        //Disables all colliders on object
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        //When item spawns, the block is animating and will reveal item waiting underneath... looks bad, so disable item's sprite
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);
        //Sprite reenabled after block animation
        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Vector3.up;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        //Resets to default, correct settings for Item object
        rigidbody.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
    }

}
