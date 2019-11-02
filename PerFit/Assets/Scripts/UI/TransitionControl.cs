using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionControl : MonoBehaviour {

    public Animator animator;
    private int sceneToLoad;

    public static TransitionControl transitionManager { get; private set; }

    private void Awake()
    {
        if (transitionManager == null)
        {
            transitionManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void fadeToNextScene()
    {
        fadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void fadeToScene (int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    private void onFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
