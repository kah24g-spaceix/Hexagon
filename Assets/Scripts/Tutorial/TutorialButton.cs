using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    private Button tutorial;

    void Awake()
    {
        tutorial = gameObject.GetComponent<Button>();
    }
    void Start()
    {
        tutorial.onClick.AddListener(() => LoadingSceneManager.LoadScene("TutorialScene"));
    }

}
