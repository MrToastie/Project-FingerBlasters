using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLimits : MonoBehaviour {
    private float moveDownY = 0.0f;
    private float moveDownX = 0.0f;
    private float sensitivity = .5f;

    private Vector2 fingerPos = Vector2.zero;
    private Vector2 lastFingerPos = Vector2.zero;
    private Vector2 fingerMoveBy = Vector2.zero;

    private float slideMagnitudeX = 0f;
    private float slideMagnitudeY = 0f;

    void Update ()
    {
        HandScreenLimits();
        MouseMovement();
        FingerMovement();
    }
    void HandScreenLimits()
    {
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 5.75f)
        {
            transform.position = new Vector3(5.75f, transform.position.y, transform.position.z);
        }
        if (transform.position.y > 5.38f)
        {
            transform.position = new Vector3(transform.position.x, 5.38f, transform.position.z);
        }
        else if (transform.position.y < -3.1f)
        {
            transform.position = new Vector3(transform.position.x, -3.1f, transform.position.z);
        }

    }
    void MouseMovement()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            moveDownY += Input.GetAxis("Mouse Y") * sensitivity;
            if (Input.GetAxis("Mouse Y") != 0)
            {
                transform.Translate(Vector3.up * moveDownY);
            }

            moveDownX += Input.GetAxis("Mouse X") * sensitivity;
            if (Input.GetAxis("Mouse X") != 0)
            {
                transform.Translate(Vector3.back * moveDownX);
            }
        }
        moveDownY = 0f;
        moveDownX = 0f;
    }
    void FingerMovement()
    {
        if(Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fingerPos = Vector2.zero;
                lastFingerPos = Vector2.zero;
                fingerMoveBy = Vector2.zero;
                slideMagnitudeX = 0f;
                slideMagnitudeY = 0f;

                //record start pos
                fingerPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                fingerMoveBy = touch.position - fingerPos;

                lastFingerPos = touch.position;
                fingerPos = touch.position;
                //slide hor
                slideMagnitudeX = fingerMoveBy.x / Screen.width;
                //slide ver
                slideMagnitudeY = fingerMoveBy.y / Screen.height;
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                lastFingerPos = fingerPos;
                fingerPos = touch.position;
                slideMagnitudeX = 0f;
                slideMagnitudeY = 0f;
            }
            else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                slideMagnitudeX = 0f;
                slideMagnitudeY = 0f;
            }
        }
    }
}
