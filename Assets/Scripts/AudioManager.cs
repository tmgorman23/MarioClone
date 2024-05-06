//Thomas Gorman - Game Engine Scripting
//Assets provided by, presumably, Nintendo
//Lots of code and help provided by Zigurous https://www.youtube.com/@Zigurous/

using System.Collections;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    Player player;
    public static AudioManager instance;
    public FMOD.Studio.EventInstance fModInstance;
    public FMODUnity.EventReference fModEvent;
    public FMODUnity.EventReference a_Jump;
    public FMODUnity.EventReference a_HitBlockEmpty;
    public FMODUnity.EventReference a_HitBlock;
    public FMODUnity.EventReference a_PowerUp;
    public FMODUnity.EventReference a_HitEntity;

    private void Awake()
    {
        player = GetComponent<Player>();
        instance = this;
    }

    public void Play_Jump()
    {
        fModInstance = FMODUnity.RuntimeManager.CreateInstance(fModEvent);
        fModInstance.start();
        if (player.big)
        {
            fModInstance.setParameterByName("Size", .75f);
        }
        else
        {
            fModInstance.setParameterByName("Size", .25f);
        }
        FMODUnity.RuntimeManager.PlayOneShot(a_Jump, transform.position);
    }
    public void Play_HitBlock()
    {
        FMODUnity.RuntimeManager.PlayOneShot(a_HitBlock, transform.position);
    }
    public void Play_HitBlockEmpty()
    {
        FMODUnity.RuntimeManager.PlayOneShot(a_HitBlockEmpty, transform.position);
    }
    public void Play_PowerUp()
    {
        FMODUnity.RuntimeManager.PlayOneShot(a_PowerUp, transform.position);
    }
    public void Play_Hit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(a_HitEntity, transform.position);
    }
}
