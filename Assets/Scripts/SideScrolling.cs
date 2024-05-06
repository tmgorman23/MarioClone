//Thomas Gorman - Game Engine Scripting
//Assets provided by, presumably, Nintendo
//Lots of code and help provided by Zigurous https://www.youtube.com/@Zigurous/

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrolling : MonoBehaviour
{
    private new Camera camera;
    private Transform player;

    public float height = 6.5f;
    public float undergroundHeight = -9.5f;
    public float undergroundThreshold = 0f;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        player = GameObject.FindWithTag("Player").transform;
    }

    //Using LateUpdate so camera calculations happen after new position information is available
    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        //Will always use max value, so camera will never move back to the left since the camera position will always be greater
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }

}
