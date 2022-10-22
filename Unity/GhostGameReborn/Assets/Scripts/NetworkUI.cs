using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;


    void Awake() {
        serverBtn.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartServer();
        });

        hostBtn.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartHost();
        });

        clientBtn.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartClient();
        });
    }

}
