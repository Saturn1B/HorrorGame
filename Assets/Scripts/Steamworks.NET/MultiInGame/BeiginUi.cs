using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeiginUi : MonoBehaviour
{
    public CanvasGroup Group;
    public TextMeshProUGUI roleText;


   
    void Start()
    {
        GameManager.OnGameStarted += OnGameStart;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStart; 
    }

    private void OnGameStart()
    {
        PlayerRole player = PlayerRole.local;
        roleText.text = "You are " + player.role.ToString().ToLower();

        StartCoroutine(DoFade());
    }

    private IEnumerator DoFade()
    {
        yield return new WaitForSeconds(2);

        float time = 0, duration = 2;

        while (time < duration)
        {
            Group.alpha = Mathf.Lerp(1, 0, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        Group.alpha = 0;
        Group.blocksRaycasts = false;
        Group.interactable = false;
    }
}
