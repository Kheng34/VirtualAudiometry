using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPositionFixer : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < 0)
        {
            // Pozisyonu düzeltecek kod
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
