using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayonE : MonoBehaviour
{

    public AudioSource door;
    private bool inside = false;
    private bool outside = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            door.Play();
        }
    }

    public void Inside()
    {
        inside = true;
        outside = false;
    }

    public void Outside()
    {
        outside = true;
        inside = false;
    }
}
