using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;
using FileData;

public class SceneChanger : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private GameObject loadingPrefab;

    public static SceneChanger Instance;
    private GameObject loadscreen_UI;
    private SceneTransition transition;
    public void Awake()
    {
        Instance = this;
    }
    public void ChangeScene(string name)
    {
        ChangeScene(name, null);
    }
    public void ChangeScene(string name, Action DoAfterLoad)
    {
        SETFALSE();
        DataManager.Instance.SaveGame(() =>
        {
            loadscreen_UI = Instantiate(loadingPrefab, this.transform);
            transition = loadscreen_UI.GetComponentInChildren<SceneTransition>();
            StartCoroutine(Load(name, DoAfterLoad));
        });
    }
    IEnumerator Load(string sceneName, Action DoAfterLoad)
    {
        DontDestroyOnLoad(this.gameObject);
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        yield return transition.AnimateTransitionIn();
        while (!scene.isDone) yield return null;
        DoAfterLoad?.Invoke();
        yield return transition.AnimateTransitionOut();
        Destroy(loadscreen_UI);
        Destroy(gameObject);
    }
    public void SETFALSE()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
