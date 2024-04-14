using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _intetactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

 

    // Update is called once per frame
    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_intetactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
        if(_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<I_Interactable>();
            if(interactable != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                interactable.Interact(this);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_intetactionPoint.position, _interactionPointRadius);
    }
}
