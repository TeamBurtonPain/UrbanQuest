﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject loadingTxt;
    public GameObject errorTxt;
    public GameObject errorBtn;

    private IEnumerator Start()
    {
        errorTxt.SetActive(false);
        loadingTxt.SetActive(true);
        errorBtn.SetActive(false);
        while (! (GeoManager.Instance.IsLoaded() || GeoManager.Instance.HasFailed()) )
        {
            yield return null;
        }
        if (GeoManager.Instance.HasFailed())
        {
            Debug.Log("Loading failed.");
            errorTxt.SetActive(true);
            loadingTxt.SetActive(false);
            errorBtn.SetActive(true);
        }
        else
        {
            // TODO 
            // if lien avec compte en ligne trouvé
            //     SceneManager.LoadScene("MapScene");
            // else if compte local trouvé 
            //     SceneManager.LoadScene("Login");
            // else
            SceneManager.LoadScene("Pseudo");
        }
    }

    public void TryAgain()
    {
        StartCoroutine(GeoManager.Instance.Init());
        StartCoroutine(Start());
    }
}
