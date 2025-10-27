using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    [SerializeField] GameObject Player;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextScene()
    {
        if (SceneManager.GetActiveScene().name == "Scene 1")
        {
            StartCoroutine(LoadLevel2());
        }
        else if (SceneManager.GetActiveScene().name == "Scene 2")
        {
            StartCoroutine(LoadLevel1());
        }
    }

    IEnumerator LoadLevel2()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Scene 2");
        transitionAnim.SetTrigger("Start");
        Player.transform.position = new Vector3(0, 1, 0);
    }

    IEnumerator LoadLevel1()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Scene 1");
        transitionAnim.SetTrigger("Start");
        Player.transform.position = new Vector3(0, 1, 0);
    }
}
