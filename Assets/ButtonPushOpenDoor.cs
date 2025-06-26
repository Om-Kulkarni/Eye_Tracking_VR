using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPushOpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator doorAnimator;
    public string boolName = "open";

    void Start()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => toggleDoorOpen());
    }

    public void toggleDoorOpen()
    {   
        bool isOpen = doorAnimator.GetBool(boolName);
        doorAnimator.SetBool(boolName, !isOpen);
    }
}
