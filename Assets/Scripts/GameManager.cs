using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SocketIOClient;
using TMPro;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public SocketIO client;
    private Scene scene;

    public string theme = "orange";
    public float volume = 0.15f;

    public string roomId;
    
    public string username;
    public bool isValidWaiting;
    string currentOnlinePlayer;
    public dynamic allRooms;
    public GameObject eachRoomTab;
    public bool justFindRoom;

    public Ship[] ships;
    public string[] shipsTiles;
    public bool isReady = false;

    public GameObject cursor;
    public Tiles[] playerTiles;
    public EnemyTiles[] enemyTiles;
    public ChatManager chatManager;
    public GameObject pauseScreen;
    public Timer timer;
    public int time;
    public string enemyName;
    public int playerGameScore;
    public int enemyGameScore;
    public int currentRound;
    public int rounds;
    public bool isMyTurn;
    public string currentEmote = "";
    public bool justResume;
    public bool justPaused;
    public bool canPause = true;
    public bool isPaused;

    public bool roundEnded;
    public bool gameEnded;
    public bool justEnded;
    public GameObject matchResult;
    public bool enemySurrendered;
    public string winner;
    public int playerScore;
    public int enemyScore;

    public string sceneToLoad;

    //Hello
    private async void Awake()
    {
        DontDestroyOnLoad(gameObject);

        client = new SocketIO("http://battleship.southeastasia.azurecontainer.io:3031");
        SceneManager.sceneLoaded += OnSceneLoaded;
        isMyTurn = false;
        await client.ConnectAsync();

        client.On("ready", response =>
        {
            enemyName = response.GetValue<string>();
            currentRound = response.GetValue<int>(1);
            rounds = response.GetValue<int>(2);
        });

        client.On("checkReady", response =>
        {
            string turn = response.GetValue<string>();
            int time = response.GetValue<int>(1) / 1000;
            isMyTurn = (turn == username);
            this.time = time;
            sceneToLoad = "GameScene";
        });

        client.On("timeOut", response =>
        {
            Debug.Log("TimeOut");
            string turn = response.GetValue<string>();
            isMyTurn = (turn == username);
            timer.RestartTimer();
        });

        client.On("chatBack", response =>
        {
            string username = response.GetValue<string>();
            string message = response.GetValue<string>(1);

            chatManager.ShowMessage(username, message);
        });

        client.On("emoteResponse", response =>
        {
            string username = response.GetValue<string>();
            string emote = response.GetValue<string>(1);

            currentEmote = emote;
        });

        client.On("pauseResponse", response =>
        {
            bool isConfirmed = response.GetValue<bool>();

            isPaused = isConfirmed;
            if(isPaused)
            {
                justPaused = true;
            }
        });

        client.On("checkReset", response =>
        {
            bool isConfirmed = response.GetValue<bool>();

            if(isConfirmed && scene.name == "GameScene")
            {
                ResetRoom();
                sceneToLoad = "SetupScene";
            };
        });

        client.On("quitRoomResponse", response =>
        {
            if(!gameEnded)
            {
                winner = username;
                playerScore = response.GetValue<int>();
                enemyScore = response.GetValue<int>(1);
                enemySurrendered = true;
                gameEnded = true;
                justEnded = true;
            }
        });

        client.On("resumeResponse", response =>
        {
            float remainingTime = response.GetValue<float>();
            timer.timeRemaining = remainingTime / 1000;
            canPause = true;
            justResume = true;
        });

        client.On("attack", response =>
        {
            string isHit = response.GetValue<string>();
            string coordinate = response.GetValue<string>(1);
            string turn = response.GetValue<string>(2);
            playerGameScore = response.GetValue<int>(3);
            GetComponent<Attack>().canAttack = true;

            if (isHit == "Hit")
            {
                foreach (EnemyTiles tile in enemyTiles)
                {
                    if ("" + tile.row + tile.col == coordinate)
                    {
                        tile.isAttacked = true;
                        tile.isHit = true;
                    }
                }
            }

            if (isHit == "Missed")
            {
                foreach (EnemyTiles tile in enemyTiles)
                {
                    if ("" + tile.row + tile.col == coordinate)
                    {
                        tile.isAttacked = true;
                        tile.isMissed = true;
                    }
                }
            }

            isMyTurn = turn == username;
            timer.RestartTimer();
        });

        client.On("attacked", response =>
        {
            string isHit = response.GetValue<string>();
            string coordinate = response.GetValue<string>(1);
            string turn = response.GetValue<string>(2);
            enemyGameScore = response.GetValue<int>(3);

            if (isHit == "Hit")
            {
                foreach (Tiles tile in playerTiles)
                {
                    if ("" + tile.row + tile.col == coordinate)
                    {
                        tile.isAttacked = true;
                        tile.isHit = true;
                    }
                }
            }

            if (isHit == "Missed")
            {
                foreach (Tiles tile in playerTiles)
                {
                    if ("" + tile.row + tile.col == coordinate)
                    {
                        tile.isAttacked = true;
                        tile.isMissed = true;
                    }
                }
            }

            isMyTurn = turn == username;
            timer.RestartTimer();
        });

        client.On("gameEnds", response =>
        {
            winner = response.GetValue<string>();
            playerScore = response.GetValue<int>(1);
            enemyScore = response.GetValue<int>(2);
            gameEnded = true;
            justEnded = true;
        });
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.scene = scene;

        if (scene.name == "SetupScene")
        {
            gameObject.GetComponent<Attack>().enabled = false;
            ships = FindObjectsOfType<Ship>();
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if(audioManager)
            {
                 audioManager.ChangeAudio(audioManager.relaxSong);
            }
        }
        if (scene.name == "GameScene")
        {
            gameObject.GetComponent<ClickAndDrag>().enabled = false;
            gameObject.GetComponent<Attack>().enabled = true;
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.ChangeAudio(audioManager.fightSong);

            timer = FindObjectOfType<Timer>();

            chatManager = FindObjectOfType<ChatManager>();

            playerTiles = FindObjectsOfType<Tiles>();
            enemyTiles = FindObjectsOfType<EnemyTiles>();
        }

        if (scene.name == "Landing")
        {
            client.On("checkUsername", response =>
            {
                bool isValid = response.GetValue<bool>(0);
                string confirmedUsername = response.GetValue<string>(1);

                if (isValid)
                {
                    username = confirmedUsername;

                    sceneToLoad = "MainMenu";

                }
            });
        }

        if (scene.name == "MainMenu")
        {
            GameObject.Find("Lobby").GetComponent<Lobby>().currentUsername.text = username;
        }

        if (scene.name == "JoinGame")
        {
            client.EmitAsync("findRoom");
            client.On("onlineNum", response =>
            {
                int currentOnline = response.GetValue<int>(0);
                currentOnlinePlayer = currentOnline.ToString();
           
            });
            client.On("findRoom", response =>
            {
                string jsonData = response.GetValue<string>(0);
                var rooms = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonData);
                allRooms = rooms;

                //Debug.Log(allRooms[0].pName.p1);
                justFindRoom = true;


            });
            client.On("joinGameJoin", response =>
            {
                bool isValid = response.GetValue<bool>(0);
                string roomIdFromTab = response.GetValue<string>(1);
                roomId = roomIdFromTab;

                if (isValid)
                {
                    sceneToLoad = "SetupScene";
                }
            });
        }


        // Boom code
        if (scene.name == "CreateGame")
        {
            client.On("roomCode", response =>
            {
                string roomIdBoom = response.GetValue<string>(0);
                roomId = roomIdBoom;

                sceneToLoad = "Waiting";

                //Debug.Log(roomIdCreateGame);
            });
        }

        if (scene.name == "Waiting")
        {
            client.On("joinGameCreate", response =>
            {
                bool isValid = response.GetValue<bool>(0);
                isValidWaiting = isValid;

                if (isValid)
                {
                    sceneToLoad = "SetupScene";
                }
            });
        }
        // End Boom code
    }

    // Update is called once per frame
    void Update()
    {
        if (scene.name == "SetupScene")
        {
            if(!gameEnded)
            {
                UpdateShipsTiles();
            }

            if (justEnded)
            {
                GameObject resultInstance = Instantiate(matchResult);
                AudioManager audioManager = FindObjectOfType<AudioManager>();
                audioManager.ChangeAudio(audioManager.endSong);
                justEnded = false;
            }
        }

        if (scene.name == "GameScene")
        {
            if (justEnded)
            {
                GameObject resultInstance = Instantiate(matchResult);
                Destroy(FindObjectOfType<MouseCursor>().gameObject);
                Instantiate(cursor);
                AudioManager audioManager = FindObjectOfType<AudioManager>();
                audioManager.ChangeAudio(audioManager.endSong);
                justEnded = false;
            }

            if(justPaused)
            {
                Instantiate(pauseScreen);
                GetComponent<Attack>().enabled = false;
                justPaused = false;
            }
            if(justResume)
            {
                Destroy(FindObjectOfType<PauseScreen>().gameObject);
                GetComponent<Attack>().enabled = true;
                isPaused = false;
                justResume = false;
            }
        }

        if (scene.name == "JoinGame")
        {
            GameObject.Find("SearchTab").GetComponent<SearchRoom>().onlinePlayerCount.text = "online : " + currentOnlinePlayer;

            if (justFindRoom)
            {
                GameObject content = GameObject.Find("Content");
                foreach (Transform child in content.transform)
                {
                    Destroy(child.gameObject);
                }

                foreach (var r in allRooms)
                {
                    GameObject instance = Instantiate(eachRoomTab, content.transform);
                    EachRoomtab eachRoomtab = instance.GetComponent<EachRoomtab>();
                    eachRoomtab.roomId = r.roomId;
                    instance.GetComponentInChildren<TMP_Text>().text = r.pName.p1;

                }
                justFindRoom = false;
            }

        }
    }

    private void UpdateShipsTiles()
    {
        List<string> currentTiles = new List<string>();

        bool isSnapped = true;

        for (int i = 0; i < ships.Length; i++)
        {
            if (!ships[i].isSnapped)
            {
                isSnapped = false;
            }
        }

        for (int i = 0; i < ships.Length; i++)
        {
            if (ships[i].isSnapped)
            {
                for (int j = 0; j < ships[i].currentTiles.Count; j++)
                {
                    currentTiles.Add(ships[i].currentTiles[j]);
                }

            }
        }

        shipsTiles = currentTiles.ToArray();

        if (isSnapped && shipsTiles.Length == 16 && client.Connected)
        {
            isReady = true;
        }
        else
        {
            isReady = false;
        }
    }

    public void Reset()
    {
        foreach (Ship ship in ships)
        {
            Destroy(ship.gameObject);
        }

        Destroy(FindObjectOfType<PlayerBoard>().gameObject);

        gameObject.GetComponent<ClickAndDrag>().enabled = true;
        gameObject.GetComponent<Attack>().enabled = false;
        isValidWaiting = false;
        ships = null;
        shipsTiles = null;
        isReady = false;
        playerTiles = null;
        enemyTiles = null;
        time = 0;
        enemyName = null;
        playerGameScore = 0;
        enemyGameScore = 0;
        currentRound = 0;
        rounds = 0;
        canPause = true;
        isPaused = false;
        isMyTurn = false;
        enemySurrendered = false;
        gameEnded = false;
        justEnded = false;
        winner = null;
        playerScore = 0;
        enemyScore = 0;
    }

    public void ResetRoom()
    {
        foreach (Ship ship in ships)
        {
            Destroy(ship.gameObject);
        }

        Destroy(FindObjectOfType<PlayerBoard>().gameObject);

        gameObject.GetComponent<ClickAndDrag>().enabled = true;
        gameObject.GetComponent<Attack>().enabled = false;
        isValidWaiting = false;
        ships = null;
        shipsTiles = null;
        isReady = false;
        playerTiles = null;
        enemyTiles = null;
        enemyName = null;
        playerGameScore = 0;
        enemyGameScore = 0;
        canPause = true;
        isPaused = false;
        isMyTurn = false;
        enemySurrendered = false;
        gameEnded = false;
        justEnded = false;
        winner = null;
        playerScore = 0;
        enemyScore = 0;
    }

    public void Ready()
    {
        if (isReady)
        {
            gameObject.GetComponent<ClickAndDrag>().enabled = false;
            client.EmitAsync("ready", roomId, username, string.Join(",", shipsTiles));
        }
    }

    public async void Enter()
    {
        GameObject usernamePanel = GameObject.Find("UsernamePanel");
        string inputUsername = usernamePanel.GetComponent<UserInput>().UsernameInput.text;
        if (client.Connected)
        {
            await client.EmitAsync("userData", inputUsername);
        }

    }

    public void Attack(string coordinate)
    {
        if (client.Connected)
        {
            client.EmitAsync("attack", roomId, username, coordinate);
        }
    }

    public void Pause()
    {
         if (client.Connected && canPause)
        {
            canPause = false;
            client.EmitAsync("pause", roomId);
        }
    }

    public void Resume()
    {
        if (client.Connected)
        {
            client.EmitAsync("resume", roomId);
        }
    }

    public void SendMessageToServer(string message)
    {
        if (client.Connected)
        {
            client.EmitAsync("chat", roomId, username, message);
        }
    }

    public void SendEmote(string emote)
    {
        if (client.Connected)
        {
            client.EmitAsync("emote", roomId, username, emote);
        }
    }
    public async void JoinRoom()
    {
        GameObject searchTab = GameObject.Find("SearchTab");
        roomId = searchTab.GetComponent<SearchRoom>().inputRoomID.text;
        if (client.Connected)
        {
            await client.EmitAsync("joinGame", roomId, username);
        }
    }

    // Boom code
    public async void CreateGame()
    {
        GameObject roundTimeInput = GameObject.Find("RoundTimeInput");
        GameObject roundInput = GameObject.Find("RoundInput");

        int roundTime = int.Parse(roundTimeInput.GetComponent<TMP_InputField>().text);
        int round = int.Parse(roundInput.GetComponent<TMP_InputField>().text);
        string roomStatus = FindObjectOfType<CreateGame>().roomStatus;

        //Debug.Log(roundTime);
        //Debug.Log(round);
        //Debug.Log(roomStatus);

        if (client.Connected)
        {
            await client.EmitAsync("createGame", username, roundTime, round, roomStatus);
        }
    }

    // End Boom code

    public async void Refresh()
    {
        if (client.Connected)
        {
            await client.EmitAsync("findRoom");
        }

    }
    public async void JoinRoomWithId(string roomId)
    {
        if (client.Connected)
        {
            await client.EmitAsync("joinGame", roomId, username);
        }
    }

    public void QuitRoom()
    {
        if (client.Connected)
        {
            client.EmitAsync("quitRoom", roomId, username);
        }
    }

}
