using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject loadingPanel;
    public Text loadingText;
    public GameObject logo;

    IEnumerator Start()
    {
        // Scale logo up and down in a continuous loop using LeanTween
        LeanTween.scale(logo, new Vector3(1.1f, 1.1f, 1), 1f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();

        // Animate loading text fading in and out
        LeanTween.alphaText(loadingText.rectTransform, 0f, 1f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();

        yield return new WaitForSeconds(2f);

        // Fade out the loading panel and deactivate it
        LeanTween.alphaCanvas(loadingPanel.GetComponent<CanvasGroup>(), 0f, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(() => {
            loadingPanel.SetActive(false);
        });
    }

    public void PlayButton()
    {
        // Animate the menuPanel moving off screen when Play is clicked
        LeanTween.moveY(menuPanel, -Screen.height, 1f).setEase(LeanTweenType.easeInBack).setOnComplete(() => {
            menuPanel.SetActive(false);
           // menuPanel.GetComponent<RectTransform>().an
            gameObject.GetComponent<PokerGame>().enabled = true;
        });
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void HomeButton()
    {
        // Fade out the menu and reload the scene with LeanTween
        loadingPanel.SetActive(true);
        LeanTween.alphaCanvas(menuPanel.GetComponent<CanvasGroup>(), 0f, 1f).setOnComplete(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}
