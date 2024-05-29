using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public Slider progressBar;
    public float fillSpeed;

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
                Debug.Log("Quest completed !");
            }
        }
        else
        {

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
