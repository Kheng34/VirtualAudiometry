using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAndTrigger : MonoBehaviour
{
    // Işınlanacağı hedef pozisyon (Birinci nesne için)
    public Transform teleportTarget;

    // İkinci nesnenin hedef pozisyonu
    public Transform secondObjectTarget;

    // İkinci nesnenin GameObject'i
    public GameObject secondObject;

    // Birinci nesnenin tag'i
    public string playerTag = "karakter";

    // Birinci nesne ışınlandıktan sonra ikinci nesnenin içeri girmesi için geçici bir flag
    private bool isFirstObjectTeleported = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // Birinci nesneyi hedef pozisyona ışınla
            other.transform.position = teleportTarget.position;

            // Birinci nesne ışınlandıktan sonra ikinci nesnenin hareketini başlat
            isFirstObjectTeleported = true;
        }
    }

    private void Update()
    {
        if (isFirstObjectTeleported)
        {
            MoveSecondObject();
            isFirstObjectTeleported = false; // Tek seferlik hareket için
        }
    }

    void MoveSecondObject()
    {
        if (secondObject != null)
        {
            secondObject.SetActive(true); // İkinci nesneyi görünür yap
            secondObject.transform.position = secondObjectTarget.position;
        }
    }
}
