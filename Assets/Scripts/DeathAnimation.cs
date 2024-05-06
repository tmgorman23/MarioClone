//Thomas Gorman - Game Engine Scripting
//Assets provided by, presumably, Nintendo
//Lots of code and help provided by Zigurous https://www.youtube.com/@Zigurous/

using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// When death script is enabled, update sprite to death sprite, disable physics to allow animation and falling through floor
    /// then animate
    /// </summary>
    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void UpdateSprite()
    {
        //Make sure sprite is enabled before updating
        spriteRenderer.enabled = true;
        //High number = Forces sprite to be at higher layer than background so you can see mario fall
        spriteRenderer.sortingOrder = 10;

        if (deadSprite != null) {
            spriteRenderer.sprite = deadSprite;
        }
    }

    private void DisablePhysics()
    {
        //Gets all colliders in scene in an array
        Collider2D[] colliders = GetComponents<Collider2D>();

        //Disables all colliders
        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
        }

        //Stops body from interacting with physics
        GetComponent<Rigidbody2D>().isKinematic = true;

        //Gets scripts for movement
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if (playerMovement != null) {
            playerMovement.enabled = false;
        }

        if (entityMovement != null) {
            entityMovement.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}
