using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour, I_Interactable
{
    [SerializeField] private string _prompt;
    public string InteractionPromp => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening whiteboard!");
        return true;
    }
}
