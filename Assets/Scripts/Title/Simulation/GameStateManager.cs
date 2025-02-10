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
        this.isLoad = isLoad;
        this.isStoryMode = isStoryMode;
        this.playtime = playtime;
        this.lastDay = lastDay;
        this.initialMoney = initialMoney;

        Debug.Log($"Game Mode Set: Load({isLoad}), Story({isStoryMode}), Playtime({playtime}), LastDay({lastDay}), InitialMoney({initialMoney})");
    }

    // Getter methods to retrieve values
    public bool IsLoad => isLoad;
    public bool IsStoryMode => isStoryMode;
    public int Playtime => playtime;
    public int LastDay => lastDay;
    public int InitialMoney => initialMoney;
}
