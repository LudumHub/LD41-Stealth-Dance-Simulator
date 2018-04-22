using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Fade : MonoBehaviour
{
    public static Fade instance;
    public float seconds = 2f;
    public Image image;

    private void Start()
    {
        if (instance != null)
        {
            if (instance != this)
                Destroy(gameObject);
        }
        else
        {
            instance = this;
            StartCoroutine(FadeOut());
            DontDestroyOnLoad(gameObject);
        }      
    }

    public void LoadNextLevel()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ResetLevel()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex));
    }

    public IEnumerator FadeIn(int SceneToLoadIndex)
    {
        var fader = Fade.instance;
        fader.image.color = Color.black;
        StandardAnimations.DoAppearanceUI(fader.gameObject, seconds, null, true);
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene(SceneToLoadIndex);
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        var fader = Fade.instance;
        image.gameObject.SetActive(true);

        fader.image.color = Color.black;
        StandardAnimations.DoVanishingUI(fader.gameObject, seconds, null, true);
        yield return new WaitForSeconds(seconds);
    }
}
