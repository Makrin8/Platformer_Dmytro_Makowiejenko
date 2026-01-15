using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    [SerializeField]
   private Transform target;
    [SerializeField]
   private Vector3 offset;

   void LateUpdate()
   {
    transform.position = target.position + offset;
   }
}
