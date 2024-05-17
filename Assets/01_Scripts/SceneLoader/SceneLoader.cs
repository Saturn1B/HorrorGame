using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : NetworkBehaviour
{
    // Liste des joueurs pr�ts
    private List<NetworkConnection> readyPlayers = new List<NetworkConnection>();

    public GameObject cavasLoad;
    

    // Appel� par le client lorsque le chargement est termin�
    public void PlayerLoaded(NetworkConnection conn)
    {
        if (!readyPlayers.Contains(conn))
        {
            readyPlayers.Add(conn);

            // V�rifie si tous les joueurs sont pr�ts
            if (readyPlayers.Count == NetworkServer.connections.Count)
            {
                StartCoroutine(StartGame());
            }
        }
    }

    // Coroutine pour d�marrer le jeu apr�s un court d�lai
    private IEnumerator StartGame()
    {
        // Afficher un �cran de chargement pendant un court laps de temps
        // Peut-�tre utiliser SceneManager.LoadSceneAsync() pour charger une sc�ne de chargement sp�cifique
        cavasLoad.SetActive(true);

        // Attendre un court d�lai pour que les clients voient l'�cran de chargement
        yield return new WaitForSeconds(2f);

        // Charger la nouvelle sc�ne une fois que tous les joueurs sont pr�ts
        NetworkManager.singleton.ServerChangeScene("NouvelleScene");
    }
}
