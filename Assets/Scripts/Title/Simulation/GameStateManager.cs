using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private bool isLoad;
    private bool isStoryMode;
    private int playtime;
    private int lastDay;
    private int initialMoney;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameState(bool isLoad, bool isStoryMode, int playtime, int lastDay, int initialMoney)
    {
        //Debug.Log($"SetGameState called with: isLoad={isLoad}, isStoryMode={isStoryMode}, playtime={playtime}, lastDay={lastDay}, initialMoney={initialMoney}");
        this.isLoad = isLoad;
        this.isStoryMode = isStoryMode;
        this.playtime = playtime;
        this.lastDay = lastDay;
        this.initialMoney = initialMoney;
    }

    public bool IsLoad => isLoad;
    public bool IsStoryMode => isStoryMode;
    public int Playtime => playtime;
    public int LastDay => lastDay;
    public int InitialMoney => initialMoney;
}
