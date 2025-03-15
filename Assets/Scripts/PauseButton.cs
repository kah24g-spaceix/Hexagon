using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject pause;
    Button button;
    bool isPause;
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }
    private void Start()
    {
        isPause = false;
        play.SetActive(true);
        pause.SetActive(false);
        button.onClick.AddListener(Pause);
    }
    public void Pause()
    {
        isPause = !isPause;
        play.SetActive(!isPause);
        pause.SetActive(isPause);
    }

}
