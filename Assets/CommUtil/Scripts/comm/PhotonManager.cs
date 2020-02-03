using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

namespace CommUtil.Scripts.comm
{
    public class PhotonManager : MonoBehaviour, IPhotonPeerListener
    {
        public static PhotonManager Instance;
        
        private PhotonPeer _photonPeer;

        private void Awake()
        {
            print("===PhotonManager======Awake==========");
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _photonPeer = new PhotonPeer(this, ConnectionProtocol.Tcp);
            _photonPeer.Connect("10.10.114.37:4530", "MyTestServer");
        }

        // Update is called once per frame
        void Update()
        {
            _photonPeer.Service();
            if (Input.GetKeyDown(KeyCode.H))
            {
                SendMsg(33, "hello ni hao wa gaga");
            }
        }

        private void OnDestroy()
        {
            _photonPeer.Disconnect();
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            print("==========level:" + level + "    message:" + message);
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            print("===OnStatusChanged=====status:" + statusCode);
            if (statusCode == StatusCode.Connect)
            {
                print("====连接服务器成功====status:" + statusCode);
            }
        }

        public void OnEvent(EventData eventData)
        {
        }

        public void SendMsg(int code, String msg)
        {
            print("========sendMsg:" + msg);
            Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
            dictionary.Add(3, msg);
            _photonPeer.OpCustom(5, dictionary, true);
        }
    }
}