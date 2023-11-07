using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisclaimerComponent : MonoBehaviour
{
    [SerializeField] private GameObject _disclaimerPanel;
    [SerializeField] private TMP_Text _disclaimerText;
    [SerializeField] private TMP_Text _disclaimerOSText;

    [SerializeField] private GameObject mouseImage;

    private Image _img;
    // Start is called before the first frame update

    private void Awake()
    {
        _img = mouseImage.GetComponent<Image>();
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 0);
        _disclaimerText.color = new Color(_disclaimerText.color.r, _disclaimerText.color.g, _disclaimerText.color.b, 0);
    }

    void Start()
    {
        StartCoroutine(ShowDisclaimer());
    }

    private IEnumerator ShowDisclaimer()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeToFullAlpha(1.5f, _disclaimerText));
        StartCoroutine(FadeToFullAlphaIMG(1.5f, _img));
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(FadeToZeroAlpha(1.5f, _disclaimerText));
        StartCoroutine(FadeToZeroAlphaIMG(1.5f, _img));
        yield return new WaitForSeconds(1.75f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //StartCoroutine(FadeToFullAlpha(1.0f, _disclaimerOSText));
       // yield return new WaitForSeconds(5.0f);
        //StartCoroutine(FadeToZeroAlpha(1.0f, _disclaimerOSText));
        //yield return new WaitForSeconds(2f);
        // load next scene
    }
    
    private IEnumerator FadeToFullAlphaIMG(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
    
    private IEnumerator FadeToZeroAlphaIMG(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    private IEnumerator FadeToFullAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    private IEnumerator FadeToZeroAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}