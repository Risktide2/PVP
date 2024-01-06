using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public GameObject camera;

    public Movement movement;
    
    public void IsLocalPlayer()
    {
        movement.enabled = true;
        camera.gameObject.SetActive(true);
        
    }
}
