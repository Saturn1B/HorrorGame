using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public Slider progressBar; // Référence au Slider UI
    public float fillSpeed = 0.5f; // Vitesse de remplissage de la barre

    private bool isPlayerInRange = false;
    private bool isFilling = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKey(KeyCode.E))
        {
            isFilling = true;
        }
        else
        {
            isFilling = false;
        }

        if (isFilling)
        {
            progressBar.value += fillSpeed * Time.deltaTime;
            if (progressBar.value >= progressBar.maxValue)
            {
                Debug.Log("Player has completed the interaction!");
                // Réinitialisez la barre ou déclenchez l'événement souhaité
                progressBar.value = 0;
            }
        }
        else
        {
            // Arrêter de remplir la barre
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            isFilling = false;
        }
    }
}
