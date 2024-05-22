using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
	public static CustomNetworkManager Instance { get; private set; }

	private AsyncOperation asyncOperation;
    private bool sceneLoaded;
	private string multiSceneName = "MultiScene";

	public override void Awake()
	{
		base.Awake();
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	public override void OnStartServer()
	{
		base.OnStartServer();

		StartCoroutine(LoadSceneInBackground(multiSceneName));
	}

	private IEnumerator LoadSceneInBackground(string sceneName)
	{
		yield return new WaitForSeconds(2);

		asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		asyncOperation.allowSceneActivation = false;

		while (!asyncOperation.isDone)
		{
			if(asyncOperation.progress >= .9f)
			{
				sceneLoaded = true;
				yield break;
			}

			yield return null;
		}
	}

	private IEnumerator ActivateLoadedScene()
	{
		asyncOperation.allowSceneActivation = true;

		yield return new WaitUntil(() => asyncOperation.isDone);

		Scene newScene = SceneManager.GetSceneByName(multiSceneName);
		if (newScene.IsValid())
		{
			SceneManager.SetActiveScene(newScene);
		}

		Scene lobbyScene = SceneManager.GetActiveScene();
		if (lobbyScene.name != multiSceneName)
		{
			AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(lobbyScene);
			yield return unloadOperation;
		}

		ServerChangeScene(multiSceneName);
	}

	public void ActivateLoadedSceneManually()
	{
		if (sceneLoaded)
		{
			StartCoroutine(ActivateLoadedScene());
		}
		else
		{
			Debug.LogWarning("Scene is not yet loaded.");
		}
	}
}
