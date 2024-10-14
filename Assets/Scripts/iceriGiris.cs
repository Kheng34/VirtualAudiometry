using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAndTriggerOther : MonoBehaviour
{
    // Işınlanacağı hedef pozisyon
    public Transform teleportTarget;

    // Tetiklenecek olan diğer nesne
    public GameObject otherObject;

    // Karakterin Tag'ini belirleyin
    public string playerTag = "turkGenci";

    // Diğer nesnenin hedef pozisyonu
    public Transform otherObjectTargetPosition;

    // Trigger'a giriş yapıldığında tetiklenecek
    public void OnTriggerEnter(Collider other)
    {
        // Eğer çarpan obje karakterse (Player tagine sahip bir obje ise)
        
            // Karakteri hedef pozisyona ışınla
            other.transform.position = teleportTarget.position;

            // Diğer nesneyi aktif hale getir
            otherObject.SetActive(true);

            // Diğer nesneyi hedef pozisyona doğru hareket ettir
            otherObject.transform.position = otherObjectTargetPosition.position;
        
    }
}

