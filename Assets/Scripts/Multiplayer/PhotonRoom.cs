using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{

    public int  multiplayerScene;
    public int currentScene;

    //paths
    public string path = "PhotonPrefabs";
    public string playerPath = "PhotonNetworkedPlayer";

    public static PhotonRoom room;
    private PhotonView PV;

    private void Awake()
    {
        if (PhotonRoom.room == null) 
        {
            PhotonRoom.room = this;
        }
        else 
        { 
            if (PhotonRoom.room != this) 
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
        StartGame();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;

    }
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();




    }
    public void StartGame() 
    {
        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.LoadLevel(multiplayerScene);
    }
    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == multiplayerScene) 
        {
            //CreatePlayer();
        }
    }
    private void CreatePlayer() 
    {
        PhotonNetwork.Instantiate(Path.Combine(path, playerPath), new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
    }
}
