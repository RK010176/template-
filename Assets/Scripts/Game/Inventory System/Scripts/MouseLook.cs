using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouesSensitivity= 100f;
    public Transform PlayerBody;
    private float _xRotation= 0;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouesSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouesSensitivity * Time.deltaTime;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_xRotation,0,0);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
