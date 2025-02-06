using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public class GameStateData
    {
        public bool IsLoad { get; set; }
        public bool IsStoryMode { get; set; }
        public int DailyPlaytime { get; set; }
        public int LastDay { get; set; }
        public int InitialMoney { get; set; }
    }

    private GameStateData gameStateData;

    public void SetGameState(bool isLoad, bool isStoryMode, int dailyPlaytime, int lastDay, int initialMoney)
    {
        gameStateData = new GameStateData
        {
            IsLoad = isLoad,
            IsStoryMode = isStoryMode,
            DailyPlaytime = dailyPlaytime,
            LastDay = lastDay,
            InitialMoney = initialMoney
        };

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("MainGameScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGameScene")
        {
            GameObject gameManagerObject = GameObject.Find("GameManager");
            if (gameManagerObject == null)
            {
                Debug.LogError("GameManager GameObject를 찾을 수 없습니다.");
                return;
            }

            GameModel gameManager = gameManagerObject.GetComponent<GameModel>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager GameObject에 GameModel 컴포넌트가 없습니다.");
                return;
            }

            ApplyGameState(gameManager, gameStateData);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void ApplyGameState(GameModel gameModel, GameStateData data)
    {
        gameModel.isLoad = data.IsLoad;
        gameModel.isStoryMode = data.IsStoryMode;
        gameModel.dailyPlaytime = data.DailyPlaytime;
        gameModel.lastDay = data.LastDay;
        gameModel.initialMoney = data.InitialMoney;
    }
}
