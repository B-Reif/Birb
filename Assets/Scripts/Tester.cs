using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tester : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject uiRoot;
    public string text;

    // Use this for initialization
    void Start()
    {
        Birb.Props props = new Birb.Props();
        props.Add("text", this.text);
        var button = Birb.CreateElement(buttonPrefab, props);
        Birb.Render(uiRoot, button);
    }
}
