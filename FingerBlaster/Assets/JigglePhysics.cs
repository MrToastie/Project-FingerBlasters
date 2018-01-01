using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglePhysics : MonoBehaviour {

    private Rigidbody ThisRigidbody;
    private Transform ThisParent;

    private Vector3 parentPosLastFrame = Vector3.zero;

    void Awake()
    {
        ThisParent = transform.parent;
        ThisRigidbody = transform.GetComponent<Rigidbody>();
    }
    void Update()
    {
        ThisRigidbody.AddForce((parentPosLastFrame - ThisParent.transform.position) * 100);
        parentPosLastFrame = ThisParent.position;
    }
}
