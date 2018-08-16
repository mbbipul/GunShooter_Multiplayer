using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetups : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentToDisable;

    Camera sceneCamera;
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i=0;i< componentToDisable.Length; i++)
            {
                componentToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
        
    }
    void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
