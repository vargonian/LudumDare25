using UnityEngine;
using System.Collections;

public class MainGuiScript : MonoBehaviour 
{
    public float cash;
    public GUIStyle labelStyle;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 500/2, 0, 500, 100), "Funds: " + this.cash, this.labelStyle);
    }
}
