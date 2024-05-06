//Thomas Gorman - Game Engine Scripting
//Assets provided by, presumably, Nintendo
//Lots of code and help provided by Zigurous https://www.youtube.com/@Zigurous/

using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");

    // Checks if the rigidbody is colliding with an object
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        //If physics control is turned off for object's rigidbody, return false for the raycast
        if (rigidbody.isKinematic) {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        //Checks in a circle to see if ray hits certain layers
        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        //Returns if not null and the rigidbody hit by ray is not the same rigidbody casting they ray
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    // Checks if the transform is facing another transform in a given direction
    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {

        //Gets vector pointing from mario/transform object to other object
        Vector2 direction = other.position - transform.position;
        //Checks if transform is moving towards object
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }

}
