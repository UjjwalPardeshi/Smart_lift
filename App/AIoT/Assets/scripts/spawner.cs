using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public GameObject yellow;

    private Vector3 placetobe;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.GetChild(0);
        Debug.Log(gameObject.transform.GetChild(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
