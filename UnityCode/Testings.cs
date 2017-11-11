using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System.Threading;
using UnityEngine.SceneManagement;
//using Hashtable = ExitGames.Client.Photon.Hashtable;


public class Testings : MonoBehaviour {

    PhotonList phList;
    public byte fighter;
    byte arena = 0;

    public delegate void ReciveInic(Dictionary<byte, object> x);
    public ReciveInic ret;

    public delegate void ReciveAttack(Dictionary<byte, object> x);
    public ReciveAttack recAt;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Application.runInBackground = true;
        phList = new PhotonList();
        phList.peer.DisconnectTimeout = 2000;
        phList.Connect();
    }

    void Update()
    {
        //получен ответ от сервера для инициализации
        if(phList.recivedHP)
        {
            phList.recivedHP = false;
            ret(phList.tempHPINI);
        }

        //получен ответ от сервера для атаки
        if(phList.reciveAttack)
        {
            phList.reciveAttack = false;
            recAt(phList.tempAttack);
        }

        if(phList.deliteRec)
        {
            phList.deliteRec = false;
            GetComponent<FileSave>().ResetStat();
        }
    }

    //Начало боя с выбранным бойцом
    public void ButonStart()
    {
        if (phList.peer.PeerState == PeerStateValue.Connected)
        {
            ChoosFighter temp = FindObjectOfType<ChoosFighter>();
            fighter = temp.currChar;
            arena = temp.arena;
            switch (arena)
            {
                case 0:
                    SceneManager.LoadScene("FirstScene");
                    break;
                case 1:
                    SceneManager.LoadScene("SecondScene");
                    break;
                case 2:
                    SceneManager.LoadScene("ThirdScene");
                    break;
            }
        }
    }


    void OnApplicationQuit()
    {
        phList.peer.Disconnect();
        Debug.Log("Exit Disconnect");
    }

    //Отправка запроса на инициализацию боя
    public void FightStarting(ServerContacts serv)
    {
        ret = new ReciveInic(serv.Inicil);
        phList.peer.OpCustom(1,new Dictionary<byte, object>(),true);
        Debug.Log("Send inic recuest");
    }

    public void WantsAttack(ServerContacts serv)
    {
        recAt = new ReciveAttack(serv.ReciveAtcReq);
        phList.peer.OpCustom(2, new Dictionary<byte, object>(), true);
        Debug.Log("Send attack request");
    }
}


public class PhotonList : IPhotonPeerListener
{
    public Dictionary<byte, object> tempHPINI = new Dictionary<byte, object>();
    public bool recivedHP = false;

    public Dictionary<byte, object> tempAttack = new Dictionary<byte, object>();
    public bool reciveAttack = false;

    bool connected = false;
    public Thread thread;

    public bool deliteRec = false;

    public PhotonList()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
    }

    public PhotonPeer peer;

    public void Connect()
    {        
        bool con = peer.Connect("127.0.0.1:5055", "PhotonIntro");
        Debug.Log("PeerID:" + peer.PeerID);
        Debug.Log("Peer state:" + peer.PeerState);
        Run();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
    }

    public void OnEvent(EventData eventData)
    {
        if(eventData.Code.Equals(0))
        {
            deliteRec = true;
            Debug.Log("Евент получен!");
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        //ответ: инициализация боя
        if (operationResponse.OperationCode == 1)
        {
            Debug.Log("ParametersRecived");
            tempHPINI = operationResponse.Parameters;
            recivedHP = true;
        }
        else if(operationResponse.OperationCode == 2)
        {
            Debug.Log("Attack recived");
            tempAttack = operationResponse.Parameters;
            reciveAttack = true;
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("Connect Status:" + statusCode);
        Debug.Log("Type:" + this.peer.UsedProtocol);

        if (statusCode == StatusCode.Connect)
        {
            connected = true;
            Debug.Log(peer.ConnectionTime);
        }
    }

    private void UpdateLoop()
    {
        while (true)
        {
            peer.Service();
            Thread.Sleep(25);
        }
    }

    public void Run()
    {
        thread = new Thread(UpdateLoop);
        thread.IsBackground = true;
        thread.Start();
    }
}
