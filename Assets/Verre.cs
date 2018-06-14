using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verre : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.name == "default")
        {
            Destroy(gameObject);
        }
    }
}
