using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hider : MonoBehaviour
{

    public AudioSource door;
    private bool inside = false;
    private bool outside = true;

    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (inside)
        {
            Debug.Log("Inside");

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (rend.enabled == true)
                {
                    rend.enabled = false;
                    GetComponent<PlayerMovement>().enabled = false;
                    door.Play();
                }
                else
                {
                    rend.enabled = true;
                    GetComponent<PlayerMovement>().enabled = true;
                    door.Play();
                }
            }
        }
        if (outside)
        {
            Debug.Log("Outside");
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
