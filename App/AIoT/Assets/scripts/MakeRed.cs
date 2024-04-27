using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;

public class MakeRed : MonoBehaviour
{
    [Header("Connected or not")] public ConnectionHandler conn;
    [Header("components")]
    public GameObject ipbutton;
    public GameObject lift;
    public GameObject camera;
    public GameObject plat; 
    [Header("red Matirials")] 
    public Sprite redipbutton;
    public Material redlift;
    public Material redplat;
    public Material redsky;
    [Header("green Matirials")]
    public Sprite greenipbutton;
    public Material greenlift;
    public Material greenplat;
    public Material greensky;
    
    private Image ipbut;
    private MeshRenderer platrend;
    private MeshRenderer liftrend;
    // Start is called before the first frame update
    void Start()
    {
        ipbut =  ipbutton.GetComponent<Image>();
        liftrend =  lift.GetComponent<MeshRenderer>();
        platrend = plat.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (conn.isConnected == false)
        {
            ipbut.sprite = redipbutton;
            liftrend.material = redlift;
            platrend.material = redplat;
            RenderSettings.skybox = redsky;
        }
        else
        {
            ipbut.sprite = greenipbutton;
            liftrend.material = greenlift;
            platrend.material = greenplat;
            RenderSettings.skybox = greensky;
        }
    }
}
