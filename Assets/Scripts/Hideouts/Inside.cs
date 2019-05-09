using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inside : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Player").SendMessage("Inside");

        GameObject.Find("VisionCone").SendMessage("Inside");
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject.Find("Player").SendMessage("Outside");

        GameObject.Find("VisionCone").SendMessage("Outside");
    }


}
