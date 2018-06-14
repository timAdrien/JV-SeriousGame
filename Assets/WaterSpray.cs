using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpray : MonoBehaviour {

    private bool playing;
    private bool displayTakeMessage;
    GUIStyle style;

    // Use this for initialization
    void Start ()
    {
        displayTakeMessage = false;
        playing = false;
        style = new GUIStyle();
        style.normal.textColor = Color.black;
        gameObject.transform.Find("WaterParticles").GetComponent<ParticleSystem>().Stop();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!playing)
            {
                gameObject.transform.Find("WaterParticles").GetComponent<ParticleSystem>().Play();
                playing = true;
            }
        }
        else
        {
            if (playing)
            {
                gameObject.transform.Find("WaterParticles").GetComponent<ParticleSystem>().Stop();
                playing = false;
            }
        }

        if (displayTakeMessage && Input.GetKeyDown(KeyCode.H))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            displayTakeMessage = false;
            GameManager.instance.SetStuff(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            displayTakeMessage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            displayTakeMessage = false;
        }
    }


    public void OnGUI()
    {
        if (displayTakeMessage)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200 / 2, Screen.height / 2, 200, 20), "Appuyer sur H pour prendre le spray.", style);
        }
    }
}
