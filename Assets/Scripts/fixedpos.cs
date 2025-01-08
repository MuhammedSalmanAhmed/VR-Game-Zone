using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedpos : MonoBehaviour
{

  public Transform target; // Reference to the "Score" or "Next Level" text object

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position; // Match position
            transform.rotation = target.rotation; // Match rotation (if needed)
        }
    }
}


