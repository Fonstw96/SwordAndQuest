using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectonInput : MonoBehaviour {

    public EventSystem eventsystem;
    public GameObject selectObject;

    private bool buttonSelected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetAxisRaw("Vertical") != 0 && buttonSelected==false)
        {
            eventsystem.SetSelectedGameObject(selectObject);
            buttonSelected = true;
        }
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
