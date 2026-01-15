using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen1 : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("isOpen", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("isOpen", false);
    }
}

