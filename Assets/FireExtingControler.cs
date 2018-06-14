using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtingControler : MonoBehaviour {

    private bool displayTakeMessage;
    private bool tookExtinct;
    GUIStyle style;
    private bool playing;

    // Use this for initialization
    void Start()
    {
        displayTakeMessage = false;
        playing = false;
        tookExtinct = false;
        style = new GUIStyle();
        style.normal.textColor = Color.black;
        gameObject.transform.Find("ExtingParticles").GetComponent<ParticleSystem>().Stop();
        gameObject.transform.Find("ExtingzParticles").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tookExtinct && GameManager.instance.CanExtincteur && Input.GetKey(KeyCode.Mouse0))
        {
            if (!playing)
            {
                gameObject.transform.Find("ExtingParticles").GetComponent<ParticleSystem>().Play();
                gameObject.transform.Find("ExtingzParticles").gameObject.SetActive(true);
                playing = true;
            }
        }
        else
        {
            if (playing)
            {
                gameObject.transform.Find("ExtingParticles").GetComponent<ParticleSystem>().Stop();
                gameObject.transform.Find("ExtingzParticles").gameObject.SetActive(false);
                playing = false;
            }
        }

        if (displayTakeMessage && GameManager.instance.CanExtincteur && Input.GetKeyDown(KeyCode.H))
        {
            displayTakeMessage = false;
            tookExtinct = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
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
        if (displayTakeMessage && GameManager.instance.CanExtincteur)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200 / 2, Screen.height / 2, 200, 20), "Appuyer sur H pour prendre l'exctincteur.", style);
        }
    }
}
