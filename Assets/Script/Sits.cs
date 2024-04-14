using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Sits : MonoBehaviour,I_Interactable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Vector3 _position;
    [SerializeField] private Quaternion _rotation;
    private bool isSitting = false;
    Animator animator;

    public string InteractionPromp => _prompt;
    public UnityEvent OnSit;

    public bool Interact(Interactor interactor)
    {
        if(isSitting)
        {
            OnSit.Invoke(); // Raise the event

            isSitting = false;
        }
        else
        {
        
            Debug.Log("Sitting!");
            //animator.SetBool("isSitting", true);
            _position = transform.position;
            _position.y = _position.y - 0.2f;
            _position.z = _position.z - 0.3f;
           // _position.y = _position.y - 3; 
            OnSit.Invoke(); // Raise the event
            Debug.Log("after invoke");
            isSitting = true;
        }
        
        return true;
    }

    public Vector3 getChairPosition()
    {

        return _position;
    }
}
