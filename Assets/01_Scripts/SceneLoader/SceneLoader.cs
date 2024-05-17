using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : NetworkBehaviour
{
    // Liste des joueurs prêts
    private List<NetworkConnection> readyPlayers = new List<NetworkConnection>();

    public GameObject cavasLoad;
    

    // Appelé par le client lorsque le chargement est terminé
    public void PlayerLoaded(NetworkConnection conn)
    {
        if (!readyPlayers.Contains(conn))
        {
            readyPlayers.Add(conn);

            // Vérifie si tous les joueurs sont prêts
            if (readyPlayers.Count == NetworkServer.connections.Count)
            {
                StartCoroutine(StartGame());
            }
        }
    }

    // Coroutine pour démarrer le jeu après un court délai
    private IEnumerator StartGame()
    {
        // Afficher un écran de chargement pendant un court laps de temps
        // Peut-être utiliser SceneManager.LoadSceneAsync() pour charger une scène de chargement spécifique
        cavasLoad.SetActive(true);

        // Attendre un court délai pour que les clients voient l'écran de chargement
        yield return new WaitForSeconds(2f);

        // Charger la nouvelle scène une fois que tous les joueurs sont prêts
        NetworkManager.singleton.ServerChangeScene("NouvelleScene");
    }
}
