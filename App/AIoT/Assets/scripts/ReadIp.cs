using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class ReadIp : MonoBehaviour
{
    public GameObject connecthandel;
    private string input;
    public string ipfromreadip;
    
    private ConnectionHandler hand; 
    // Start is called before the first frame update
    void Start()
    {
        hand = connecthandel.GetComponent<ConnectionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
        ipfromreadip = input;
        hand.ConnectToServer(); 
    }
}
