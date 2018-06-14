using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazControler : MonoBehaviour {

    private int life;

    // Use this for initialization
    void Start()
    {
        life = 10;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<ExtingParticle>() != null)
        {
            life--;
            if (life <= 0)
            {
                Destroy(gameObject);
                GameManager.instance.GazDown();
            }
        } else
        {
            GameManager.instance.DisplayGazError();
        }
    }
}
