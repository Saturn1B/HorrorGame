using UnityEngine;
using Mirror;

public class PlayerLoader : NetworkBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    public void Start()
    {
        sceneLoader = GameObject.Find("sceneLoader").GetComponent<SceneLoader>();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Envoyer un message au serveur lorsque le joueur local est prêt
        sceneLoader.PlayerLoaded(connectionToServer);
    }
}