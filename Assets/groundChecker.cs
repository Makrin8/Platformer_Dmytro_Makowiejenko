using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundChecker : MonoBehaviour
{
    public bool isGrounded;
    public BoxCollider2D collider;
    public void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //Empty
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}