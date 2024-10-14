using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnCollide : MonoBehaviour
{
    private int CurrScnPtr = 0;

    void Start()
    {
        CurrScnPtr = SceneManager.GetActiveScene().buildIndex;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (0 < CurrScnPtr) { CurrScnPtr--; SceneManager.LoadScene(CurrScnPtr, LoadSceneMode.Single); }
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

}
