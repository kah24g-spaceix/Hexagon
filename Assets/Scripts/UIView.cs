using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [Header("Option UI")]
    [SerializeField] private GameObject OptionUI;
    private bool option;
    [Header("InGame UI")]
    [SerializeField] private GameObject TechTreeUI;
    // Start is called before the first frame update
    private void Start()
    {
        HideUI(OptionUI);
        HideUI(TechTreeUI);
        option = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            option = !option;
            CurrentActive(OptionUI, option);
            Pause(option);
        }
    }




    private void HideUI(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(false);
    }
    private void ShowUI(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(true);
    }
    private void CurrentActive(GameObject gameObject, bool active)
    {
        gameObject.gameObject.SetActive(active);
    }
    private void Pause(bool active)
    {
        if (active)
        {
            Time.timeScale = 0; // 게임 정지
        }
        else
        {
            Time.timeScale = 1; // 게임 재개
        }
    }
}
