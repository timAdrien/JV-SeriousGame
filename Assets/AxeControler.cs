using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeControler : MonoBehaviour {

    private bool displayTakeMessage;
    Animator anim;
    GUIStyle style;

    // Use this for initialization
    void Start () {
        displayTakeMessage = false;
        //style.normal.background = Color.black;
        style = new GUIStyle();
        style.normal.textColor = Color.black;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("attack");
        } 

        if (displayTakeMessage && GameManager.instance.CanTakeHache && Input.GetKeyDown(KeyCode.H))
        {
            anim.enabled = true;
            displayTakeMessage = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            GameManager.instance.CanTakeHache = false;
            GameManager.instance.CanExtincteur = true;
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
        if (displayTakeMessage && GameManager.instance.CanTakeHache)
        {
            GUI.Label(new Rect(Screen.width / 2 - 200 / 2, Screen.height / 2, 200, 20), "Appuyer sur H pour prendre la hache.", style);
        }
    }
}
