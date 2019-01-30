using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecisKontrol : MonoBehaviour {

    public Animator animator;
    private int sceneToLoad;

    public void oyunuBaslat()
    {
        fadeToNextScene();
    }

    public void fadeToNextScene()
    {
        fadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void fadeToScene (int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
