using UnityEngine;
using System.Net.Sockets;
using System.Text;
using TMPro;

public class ConnectionHandler : MonoBehaviour
{
    public GameObject numberText;
    public bool isConnected;
    public GameObject bitconnect;

    private ReadIp ipgiver;
    public string ip = "localhost";
    public TMP_Text textw;

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
        }
    }

    void Update()
    {
        if (isConnected) { ReceiveNumber(); }
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
            if (randomNumber <= 10)
            {
                wow = "low";
            }
            else if (10 < randomNumber && randomNumber <= 20)
            {
                wow = "medium";
            }
            else if (20 < randomNumber)
            {
                wow = "high";
            }

            string printToResult;
            printToResult = wow + " " + randomNumber;
            Debug.Log(printToResult);
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
