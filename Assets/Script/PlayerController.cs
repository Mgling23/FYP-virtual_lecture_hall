using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 300f;
    [SerializeField] float jumpSpeed=5;

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] Vector3 chairPosition;
    [SerializeField] LayerMask groundLayer;

    bool isGrounded;
    bool isJumping;
    bool isFalling;
    bool isSitting;

    private float originalStepOffset;
    float ySpeed;

    Quaternion targetRotation;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;
    Sits[] sits = new Sits[3];

    // Start is called before the first frame update
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        sits = FindObjectsOfType<Sits>();

        if (sits != null)
        {
            foreach (Sits s in sits)
            {
                s.OnSit.AddListener(OnSitEvent); // Subscribe to the OnSit event

            }
            //sits.OnSit.AddListener(OnSitEvent); // Subscribe to the OnSit event
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isSitting)
        {
            return;
        }
        animator.SetBool("isSitting", false);


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        var moveInput = new Vector3(h, 0, v).normalized;

        var moveDir = cameraController.PlanerRotation * moveInput;

        GroundCheck();
        //Debug.Log("Isgrounded = " + isGrounded);
        if (isGrounded)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            //if (Input.GetButtonDown("Jump")) {
            if (Keyboard.current.spaceKey.wasPressedThisFrame) {
                ySpeed = jumpSpeed;        
            }

            if (isSitting)
            {
                animator.SetBool("isSitting", true);
                //chairPosition = sits.getChairPosition();
                //transform.position = chairPosition;
                //Sits.Interact(interactor);
            }

            
        }
        else
        {
            characterController.stepOffset = 0;
            ySpeed += Physics.gravity.y*Time.deltaTime;
        }

        


        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);
        //characterController.Move(velo)

        if (moveAmount > 0)
        {
            //transform.position += moveDir * moveSpeed * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(moveDir);

        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, 
            targetRotation, rotationSpeed * Time.deltaTime);
       
        animator.SetFloat("moveAmount",moveAmount,0.2f,Time.deltaTime);

    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
        //Debug.Log(isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);

    }

    private void OnSitEvent()
    {
        if (!isSitting)
        {

            var targetRotation = Quaternion.Euler(0, 180, 0);
            isSitting = true;
            animator.SetBool("isSitting", true);

            Debug.Log("onsitevent");

            if (sits != null)
            {
                // Set the player's position to the position of the sits object
                //transform.position = sits[1].getChairPosition();
                transform.rotation = targetRotation;
            }
        }
        else
        {

            isSitting = false;
            animator.SetBool("isSitting", false);
        }
       
        // Additional actions when the player sits
    }
}
