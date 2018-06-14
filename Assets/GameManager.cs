using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    Text txtPerso;

    [SerializeField]
    Text txtDiscussion;

    [SerializeField]
    GameObject stuffHolder;

    [SerializeField]
    GameObject currStuff;

    [SerializeField]
    GameObject panelText;
    
    private bool nextTalk;
    private bool stopErrorGaz;
    private bool canTakeHache;
    private bool canExtincteur;
    
    private int nbFireDown;

    private Dictionary<string, string> obj;

    private List<string> lstPart;

    List<Part> lstParts;

    private int indicePart;
    private int indiceNumDiscToPlay;


    #region Accesseurs
    public Text TxtPerso
    {
        get
        {
            return txtPerso;
        }

        set
        {
            txtPerso = value;
        }
    }

    public Text TxtDiscussion
    {
        get
        {
            return txtDiscussion;
        }

        set
        {
            txtDiscussion = value;
        }
    }

    public bool CanTakeHache
    {
        get
        {
            return canTakeHache;
        }

        set
        {
            canTakeHache = value;
        }
    }
    
    public bool CanExtincteur
    {
        get
        {
            return canExtincteur;
        }

        set
        {
            canExtincteur = value;
        }
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        // Set properties
        if (instance == null)
            instance = this;
        indiceNumDiscToPlay = 0;
        indicePart = 0;
        lstPart = new List<string>();
        nbFireDown = 0;
        stopErrorGaz = false;
        CanTakeHache = false;

        // Load texts
        TextAsset targetFile = Resources.Load<TextAsset>("discussions");

        XDocument doc = XDocument.Parse(targetFile.text);
        lstParts = (from r in doc.Root.Elements("Part")
         select new Part
         {
             disc = (from e in r.Elements("disc")
                         select new Discussion
                         {
                             perso = (string)e.Element("perso"),
                             text = (string)e.Element("text")
                         }
                    ).ToList(),
         }).ToList();

        // Start game
        nextTalk = true;
        NextTalk();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (nextTalk)
            {
                NextTalk();
            } else if (stopErrorGaz)
            {
                stopErrorGaz = false;
                panelText.SetActive(false);
            }
        }
    }

    public void NextTalk()
    {
        if (lstParts[indicePart].disc.Count == indiceNumDiscToPlay)
        {
            nextTalk = false;
            panelText.SetActive(false);
            if (indicePart == 2)
            {
                Application.Quit();
            }
            return;
        }

        panelText.SetActive(true);
        TxtPerso.text = lstParts[indicePart].disc[indiceNumDiscToPlay].perso;
        TxtDiscussion.text = lstParts[indicePart].disc[indiceNumDiscToPlay].text;
        indiceNumDiscToPlay++;
    }

    public void FireDown()
    {
        nbFireDown++;
        if(nbFireDown >= 3)
        {
            nextTalk = true;
            indicePart++;
            indiceNumDiscToPlay = 0;
            NextTalk();
            CanTakeHache = true;
        }
    }
    public void GazDown()
    {
        nextTalk = true;
        indicePart++;
        indiceNumDiscToPlay = 0;
        NextTalk();
    }

    public void DisplayGazError()
    {
        stopErrorGaz = true;
        panelText.SetActive(true);
        TxtPerso.text = "Pompier";
        TxtDiscussion.text = "Il semblerait que l'eau ne puisse venir à bout du gaz ! Trouve un autre moyen de l'éteindre.";
    }

    public void SetStuff(GameObject stuff)
    {
        if(currStuff != null)
            Destroy(currStuff.gameObject);
        currStuff = stuff;
        stuff.transform.parent = stuffHolder.transform;
        stuff.transform.localPosition = Vector3.zero;
        stuff.transform.localRotation = Quaternion.identity;
    }
}
