using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private Button button;
    void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }
    void Start()
    {
        button.onClick.AddListener(()=>SceneManager.LoadScene("TitleScene"));
    }
}
