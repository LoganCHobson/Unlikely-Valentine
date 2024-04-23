using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakInteract : MonoBehaviour
{
    DialogManager dialogManager;

    public Object script;

    private void Start()
    {
        dialogManager = DialogManager.instance;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                dialogManager.dialogDataFile = script;
                dialogManager.InitiateConversation();
            }
        }
    }
}
