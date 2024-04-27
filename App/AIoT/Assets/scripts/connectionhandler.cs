using UnityEngine;
using System.Net.Sockets;
using System.Text;
using TMPro;

public class ConnectionHandler : MonoBehaviour
{
    public GameObject numberText;
    public bool isConnected;
    public GameObject bitconnect;
    public spawner spa;

    private ReadIp ipgiver;
    public string ip = "localhost";
    public TMP_Text textw;

    public int sentint;
    TcpClient client;
    
    NetworkStream stream;

    void Start()
    {
        ipgiver = bitconnect.GetComponent<ReadIp>();
        textw = numberText.GetComponent<TMP_Text>();
        isConnected = false;
    }

    public void ConnectToServer()
    {
        spa.killbande(); 
        ip = ipgiver.ipfromreadip;
        if (ip == "" || ip == " ")
        {
            ip = "localhost";
        }
        Debug.Log("used ip: " + ip + 12345);
        // Replace with Android-specific socket initialization
        try
        {
            client = new TcpClient();
            client.Connect(ip, 12345);
            stream = client.GetStream();
            isConnected = true;
        }
        catch (SocketException e)
        {
            Debug.LogWarning("SocketException: " + e);
            isConnected = false;
            spa.SpawnLine(isConnected);
        }
    }

    void Update()
    {
        if (isConnected) { ReceiveNumber(); }

        if (isConnected == false)
        {
            textw.text = "Disconnected";
        }
    }

    void ReceiveNumber()
    {
        byte[] buffer = new byte[1024];

        // Replace with Android-specific stream reading
        int bytesRead = stream.Read(buffer, 0, buffer.Length);

        string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        int randomNumber;
        string wow = "Unknown";
        if (int.TryParse(receivedData, out randomNumber))
        {
            sentint = randomNumber;
            if (randomNumber <= 10)
            {
                wow = "Low";
            }
            else if (10 < randomNumber && randomNumber <= 20)
            {
                wow = "Medium";
            }
            else if (20 < randomNumber)
            {
                wow = "High";
            }

            string printToResult;
            printToResult = wow + "\n" + randomNumber;
            Debug.Log(printToResult);
            spa.SpawnLine(isConnected);
            textw.text = printToResult;
        }
    }

    void OnDestroy()
    {
        if (isConnected)
        {
            stream.Close();
            client.Close();
        }
    }
}
