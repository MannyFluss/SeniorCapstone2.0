using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterEnvironment : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private string sceneName;
    [SerializeField] private Image BlackScreen;

    private float fadeRate = 1.8f;

    IEnumerator TransitionScene()
    {
        BlackScreen.color = new Color(1, 1, 1, 0);
        BlackScreen.enabled = true;
        player.GetComponent<PlayerMovement>().TogglePlayerInput();
        player.GetComponent<PlayerMovement>().moveSpeed = 0f;
        for (float alpha = 0f; alpha <= 1f; alpha += fadeRate * Time.deltaTime)
        {
            BlackScreen.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        BlackScreen.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(TransitionScene());
    }
}
