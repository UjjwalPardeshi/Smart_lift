using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ipbutton : MonoBehaviour
{
    public GameObject ippage; 

    public void ipbuttonPress()
    {
        if (ippage.activeInHierarchy)
        {
            Debug.Log("No, IP Page is active");
            ippage.SetActive(false);
        }
        else
        {
            ippage.SetActive(true);
        }
    }
}
