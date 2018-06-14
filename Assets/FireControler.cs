using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControler : MonoBehaviour {

    private int life;

	// Use this for initialization
	void Start () {
        life = 10;
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<WaterParticle>() != null)
        {
            life--;
            if (life <= 0)
            {
                Destroy(gameObject);
                GameManager.instance.FireDown();
            }
        }
    }
}
