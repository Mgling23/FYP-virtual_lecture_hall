using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}
    Animator animator;

    public float moveSpeed = 7f;
    //public float jumpSpeed;
    // Update is called once per frame

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetBool("isSitting"))
        {
            Debug.Log(animator.GetBool("isSitting"));
            return;
        }
        else
        {



            Vector2 inputVector = new Vector2(0, 0);
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Pressing");
            }
            if (Input.GetKey(KeyCode.W))
            {
                inputVector.y = +1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputVector.y = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputVector.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputVector.x = +1;
            }


            inputVector = inputVector.normalized;
            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            Debug.Log(inputVector);
        }
    }
}
