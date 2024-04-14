using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform followTarget;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5;
    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;
    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    float rotationY;
    float rotationX;

    float invertXVal;
    float invertYVal;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        invertXVal = (invertX) ? 1 : -1;
        invertYVal = (invertY) ? -1 : 1;
        rotationX += Input.GetAxis("Mouse Y")* invertYVal *rotationSpeed;
        rotationX = Mathf.Clamp(rotationX,minVerticalAngle,maxVerticalAngle);
        rotationY += Input.GetAxis("Mouse X")* invertXVal*rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.position = followTarget.position - targetRotation * new Vector3(0, -2, distance);
        transform.rotation = targetRotation;

    }
    public Quaternion PlanerRotation => Quaternion.Euler(0, rotationY, 0);

 
}
