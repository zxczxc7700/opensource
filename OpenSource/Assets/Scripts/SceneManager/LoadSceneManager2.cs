using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager2 : MonoBehaviour
{
    public Slider slider;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingScene());
    }
    
    IEnumerator LoadingScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("stage2");

        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            time = +Time.time;
            slider.value = time / 2f;

            if(time > 2)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
