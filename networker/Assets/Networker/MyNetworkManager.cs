using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {


	// Use this for initialization
	void Start () {
        print("myNetworkManager has started");
	}
	
    public void MyStartHost()
    {
        Debug.Log(Time.timeSinceLevelLoad + " starting Host.");
        StartHost();
    }

    public override void OnStartHost()
    {
        Debug.Log(Time.timeSinceLevelLoad + " host started.");
    }

    public override void OnStartClient(NetworkClient myClient)
    {
        Debug.Log(Time.timeSinceLevelLoad + " Client started.");
        InvokeRepeating("printLoading", 0f, 0.1f);
    }

    public override void OnClientConnect(NetworkConnection connection)
    {
       Debug.Log(Time.timeSinceLevelLoad + " Client is connected to IP:" + connection.address);
        CancelInvoke();
    }

    private void printLoading()
    {
        print(".");
    }
}
