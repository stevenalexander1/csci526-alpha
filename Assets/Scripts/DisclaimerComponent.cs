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
        Debug.Log("Showing disclaimer");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Showing disclaimer");
        StartCoroutine(FadeToFullAlpha(1.75f, _disclaimerText));
        yield return new WaitForSeconds(4.0f);
        Debug.Log("Removing disclaimer");
        StartCoroutine(FadeToZeroAlpha(1.75f, _disclaimerText));
        yield return new WaitForSeconds(2.25f);
        // load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator FadeToFullAlpha(float t, TMP_Text i)
    {
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 0);
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _img.color.a + (Time.deltaTime / t));
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    private IEnumerator FadeToZeroAlpha(float t, TMP_Text i)
    {
        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 1);
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _img.color.a - (Time.deltaTime / t));
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}