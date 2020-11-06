using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardClassic : MonoBehaviour
{
    // Borders
    public Double rightCol = 6.5;
    public Double leftCol = -6.5;
    public Double topRows = 9;
    public Double bottomRows = -11;

    //Food Tiles
    public GameObject FoodTileBlank;
    public GameObject snakeBody;

    //Sprites
    Sprite[] headSprites;
    Sprite[] bodySprites;
    Sprite[] backgroundSprites;

    //Lists used for word display and tail
    public List<GameObject> word = new List<GameObject>();
    public List<GameObject> tail = new List<GameObject>();

    //Other Class information
    public PlayerMovement playerMovement;
    public GameManagerClassic gm;
    private DontDestroy DD;
    public FadeManager fm;
    private GameObject stick;
    private GameObject dpad;
    public Achievements ach;

    //Grid Positions for possible Spawn Locations
    private List<Vector2> gridPositions = new List<Vector2>();

    //List of Letters Currently On the Board
    private List<GameObject> foodAlive = new List<GameObject>();

    public string finalWord = "";
    public int wpc = 0;
    public int multi = 1;

    //Class for containing information about Tile Location and Letter
    [Serializable]
    public class LetterPlaced
    {
        public Vector2 randomPosition;
        public int indexLocation;
        public Char letter;
        public GameObject foodTile;
        public int points;
        public int index;

        public LetterPlaced(Vector2 rP, int iL, Char Letter, GameObject tile, int Points)
        {
            randomPosition = rP;
            indexLocation = iL;
            letter = Letter;
            foodTile = tile;
            points = Points;
        }

    }
    public AudioSource audio1;
    public AudioSource audio2;

    void SpriteLoader()
    {
        headSprites = Resources.LoadAll<Sprite>("CharacterSprites/Head");
        bodySprites = Resources.LoadAll<Sprite>("CharacterSprites/Body");
    }
    void SpriteInitilizer()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)headSprites[PlayerPrefs.GetInt("CurHead")];
    }

    void Start()
    {
        fm = GameObject.Find("Fade Manager").GetComponent<FadeManager>();

        StartCoroutine(Fading());
        SpriteLoader();
        SpriteInitilizer();
        InitializeList();
        SetupBoard();

        playerMovement = GetComponent<PlayerMovement>();
        gm = GetComponent<GameManagerClassic>();
        DD = GetComponent<DontDestroy>();

        AudioSource homeSound = GameObject.Find("Canvas").GetComponent<AudioSource>();

        audio1 = homeSound;

        stick = GameObject.Find("Pane");
        dpad = GameObject.Find("DPad");

        if (PlayerPrefs.GetString("Controls") == "Swipe")
        {
            stick.SetActive(false);
            dpad.SetActive(false);
        } 
        else if(PlayerPrefs.GetString("Controls") == "Joystick")
        {
            stick.SetActive(true);
            dpad.SetActive(false);
        }
        else if(PlayerPrefs.GetString("Controls") == "DPad")
        {
            dpad.SetActive(true);
            stick.SetActive(false);
        }

        VolCheck();
        playerMovement.moveEnabled = true;
    }
    void VolCheck()
    {

        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            GetComponent<AudioSource>().volume = 0;
            audio1.volume = 0.0f;
            audio2.volume = 0.0f;
        }
        else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            GetComponent<AudioSource>().volume = 0.8f;
            audio1.volume = 0.8f;
            audio2.volume = 0.8f;
        }
    }
    System.Collections.IEnumerator Fading()
    {
        yield return new WaitForSeconds(1);
        fm.Fade(false, 3.0f);
        yield return new WaitForSeconds(3);
        Destroy(GameObject.Find("Fade Manager"));
    }

    //creates a List of al possible array positions
    public void InitializeList()
    {
        gridPositions.Clear();
        for (int x = (int)leftCol; x < (int)rightCol + 1; x++)
        {
            for (int y = (int)bottomRows + 1; y < topRows; y++)
            {
                gridPositions.Add(new Vector2(x, y));
            }
        }
    }

    // Use this for initialization
    public void SetupBoard()
    {
        InitializeList();
        for (int n = 0; n < 4; n++)
        {
            AddFood();
        }
    }
    //Removes 
    void RemoveLetter(GameObject caughtFood)
    {
        //Comment out next two lines and uncomment the variable in order to have it remove specific one.
        Destroy(caughtFood);
        foodAlive.Remove(caughtFood);
    }
    void pointCreate()
    {
        Vector4 newPoint = transform.position;
        newPoint.w = Time.time;
        playerMovement.pointsHistory.Add(newPoint);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        int count = tail.Count;
        if (coll.name.Equals("Edges") || coll.tag.StartsWith("TailSplay"))
        {
            playerMovement.moving = false;
            clearTail();
            transform.position = new Vector2(0, 0);
            audio1.Play();
            GameObject.Find("Player").GetComponent<PlayerMovement>().mDir = Vector3.zero;
            gm.subLife();
            gm.HUDUpdate();
            gm.wordCount = 0;
            blocks = 0;
            multi = 1;
        }
        else if (coll.name.StartsWith("Food"))
        {
            Destroy(coll.gameObject);
            GameObject[] letters = GameObject.FindGameObjectsWithTag("TailSplay");
            blocks++;
            coll.gameObject.tag = "TailSplay";
            GetComponent<AudioSource>().Play();
            AddFood();
            gm.addPts();
            gm.SetScore();
            gm.Count.text = "x "+blocks;

            for (int i = 0; i < 1; i++)
            {
                GameObject block = Instantiate(snakeBody, new Vector2(0, -25), Quaternion.identity);
                block.gameObject.tag = "TailSplay";
                if (!PlayerPrefs.HasKey("CurHead"))
                {
                    block.GetComponent<SpriteRenderer>().sprite = (Sprite)bodySprites[0];
                }
                else 
                { 
                    block.GetComponent<SpriteRenderer>().sprite = (Sprite)bodySprites[PlayerPrefs.GetInt("CurHead") / 2];
                }
                tail.Add(block);
            }
            multi = blocks / 10;
        }

    }
    public int blocks = 0;
    
    public void clearTail()
    {
        word.Clear();
        tail.Clear();

        playerMovement.pointsHistory.Clear();
        GameObject[] letters = GameObject.FindGameObjectsWithTag("TailSplay");
        for (int i = 0; i < letters.Length; i++)
        {
            GameObject.Destroy(letters[i]);
        }

        
        multi = blocks / 5;
    }
    
    //Clears the Board of all food Objects
    void ClearBoard()
    {
        int k = foodAlive.Count;
        for (int n = 0; n < k; n++)
        {
            GameObject foodObject = foodAlive[0];
            Destroy(foodObject);
            foodAlive.Remove(foodObject);
        }
    }

    //Helper Function to choose random positions
    LetterPlaced RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        if (randomIndex == 122)
        {
            randomIndex = randomIndex + Random.Range(1, 20);
        }
        Vector2 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        GameObject gameObj;
        int randomLetter = Random.Range(0, 99);
        Char letter = new char();
        int Points = 0;
        if (randomLetter == 0)
        {
            gameObj = FoodTileBlank;
        }
        else
        {
            gameObj = FoodTileBlank;
        }


        return new LetterPlaced(randomPosition, randomIndex, letter, gameObj, Points);
    }

    // Spawn one piece of food
    public void AddFood()
    {
        // Instantiate the food at (x, y)
        LetterPlaced newLetter = RandomPosition();
        GameObject newFood = Instantiate(newLetter.foodTile, newLetter.randomPosition, Quaternion.identity);
        // default rotation
        foodAlive.Add(newFood);
    }

    void followTail()
    {
        if (tail.Count > 0)
        {
            if (playerMovement.pointsHistory.Count > 2)
            {
                tail[0].transform.position = playerMovement.pointsHistory[playerMovement.pointsHistory.Count - 2];
            }
        }
        for (int i = 1; i < tail.Count; i++)
        {
            tail[i].transform.position = playerMovement.pointsHistory[playerMovement.pointsHistory.Count - (2 + i)]; 
        }
        if(tail.Count > PlayerPrefs.GetInt("Longest_Tail"))
        {
            PlayerPrefs.SetInt("Longest_Tail", tail.Count);
            ach.AchEighteen(tail.Count);
            ach.AchNineteen(tail.Count);
            ach.AchTwenty(tail.Count);
            ach.AchTwentyOne(tail.Count);
        }
    }

    void Update()
    {
        followTail();
    }
}