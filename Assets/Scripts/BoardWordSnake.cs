using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardWordSnake : MonoBehaviour
{
    // Borders
    public Double rightCol = 6.5;
    public Double leftCol = -6.5;
    public Double topRows = 9;
    public Double bottomRows = -11;

    //Food Tiles
    public GameObject foodTileA;
    Sprite[] LetterASprite;
    public GameObject foodTileB;
    Sprite[] LetterBSprite;
    public GameObject foodTileC;
    Sprite[] LetterCSprite;
    public GameObject foodTileD;
    Sprite[] LetterDSprite;
    public GameObject foodTileE;
    Sprite[] LetterESprite;
    public GameObject foodTileF;
    Sprite[] LetterFSprite;
    public GameObject foodTileG;
    Sprite[] LetterGSprite;
    public GameObject foodTileH;
    Sprite[] LetterHSprite;
    public GameObject foodTileI;
    Sprite[] LetterISprite;
    public GameObject foodTileJ;
    Sprite[] LetterJSprite;
    public GameObject foodTileK;
    Sprite[] LetterKSprite;
    public GameObject foodTileL;
    Sprite[] LetterLSprite;
    public GameObject foodTileM;
    Sprite[] LetterMSprite;
    public GameObject foodTileN;
    Sprite[] LetterNSprite;
    public GameObject foodTileO;
    Sprite[] LetterOSprite;
    public GameObject foodTileP;
    Sprite[] LetterPSprite;
    public GameObject foodTileQ;
    Sprite[] LetterQSprite;
    public GameObject foodTileR;
    Sprite[] LetterRSprite;
    public GameObject foodTileS;
    Sprite[] LetterSSprite;
    public GameObject foodTileT;
    Sprite[] LetterTSprite;
    public GameObject foodTileU;
    Sprite[] LetterUSprite;
    public GameObject foodTileV;
    Sprite[] LetterVSprite;
    public GameObject foodTileW;
    Sprite[] LetterWSprite;
    public GameObject foodTileX;
    Sprite[] LetterXSprite;
    public GameObject foodTileY;
    Sprite[] LetterYSprite;
    public GameObject foodTileZ;
    Sprite[] LetterZSprite;
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
    public GameManagerWordSnake gm;
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
        LetterASprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterASprites");
        LetterBSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterBSprites");
        LetterCSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterCSprites");
        LetterDSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterDSprites");
        LetterESprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterESprites");
        LetterFSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterFSprites");
        LetterGSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterGSprites");
        LetterHSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterHSprites");
        LetterISprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterISprites");
        LetterJSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterJSprites");
        LetterKSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterKSprites");
        LetterLSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterLSprites");
        LetterMSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterMSprites");
        LetterNSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterNSprites");
        LetterOSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterOSprites");
        LetterPSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterPSprites");
        LetterQSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterQSprites");
        LetterRSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterRSprites");
        LetterSSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterSSprites");
        LetterTSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterTSprites");
        LetterUSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterUSprites");
        LetterVSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterVSprites");
        LetterWSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterWSprites");
        LetterXSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterXSprites");
        LetterYSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterYSprites");
        LetterZSprite = Resources.LoadAll<Sprite>("CharacterSprites/LetterZSprites");

        headSprites = Resources.LoadAll<Sprite>("CharacterSprites/Head");
        bodySprites = Resources.LoadAll<Sprite>("CharacterSprites/Body");
    }
    void SpriteInitilizer()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = (Sprite)headSprites[PlayerPrefs.GetInt("CurHead")];

        foodTileA.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterASprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileB.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterBSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileC.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterCSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileD.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterDSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileE.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterESprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileF.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterFSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileG.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterGSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileH.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterHSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileI.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterISprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileJ.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterJSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileK.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterKSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileL.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterLSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileM.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterMSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileN.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterNSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileO.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterOSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileP.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterPSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileQ.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterQSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileR.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterRSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileS.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterSSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileT.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterTSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileU.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterUSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileV.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterVSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileW.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterWSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileX.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterXSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileY.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterYSprite[PlayerPrefs.GetInt("CurHead") / 2];
        foodTileZ.GetComponent<SpriteRenderer>().sprite = (Sprite)LetterZSprite[PlayerPrefs.GetInt("CurHead") / 2];
        snakeBody.GetComponent<SpriteRenderer>().sprite = (Sprite)bodySprites[PlayerPrefs.GetInt("CurHead") / 2];
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
        gm = GetComponent<GameManagerWordSnake>();
        DD = GetComponent<DontDestroy>();
        ach = GetComponent<Achievements>();
        
        AudioSource[] homeSound = GameObject.Find("Home").GetComponents<AudioSource>();

        audio1 = homeSound[0];
        audio2 = homeSound[1];

        stick = GameObject.Find("Pane");
        dpad = GameObject.Find("DPad");

        if (PlayerPrefs.GetString("Controls") == "Swipe")
        {
            stick.SetActive(false);
            dpad.SetActive(false);
        }
        else if (PlayerPrefs.GetString("Controls") == "Joystick")
        {
            stick.SetActive(true);
            dpad.SetActive(false);
        }
        else if (PlayerPrefs.GetString("Controls") == "DPad")
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
        for(int n = 0; n < 9; n++)
        {
            AddLetter();
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
            playerMovement.moving = false ;
            clearTail();
            transform.position = new Vector2(0, 0);
            audio1.Play();
            GameObject.Find("Player").GetComponent<PlayerMovement>().mDir = Vector3.zero;
            gm.subLife();
        }
        else if (coll.name.Equals("SafeEdges"))
        {
            playerMovement.moving = true;
            clearTail();
            audio2.Play();
            playerMovement.upDir = false;
            playerMovement.downDir = false;
            playerMovement.leftDir = false;
            playerMovement.rightDir = false;
            transform.position = new Vector2(0, 0);
            GameObject.Find("Player").GetComponent<PlayerMovement>().mDir = Vector3.zero;
        }
        else if (coll.name.StartsWith("Trash"))
        {
            clearTail();
        }
        else if (coll.name.StartsWith("Home"))
        {
            sendTail();
            gm.checkIfWord();     
        }   
        else if (coll.name.StartsWith("Food"))
        {
        word.Add(coll.gameObject);
        tail.Add(coll.gameObject);
        coll.gameObject.tag = "TailSplay";
        GameObject wordDisplay = Instantiate(coll.gameObject, new Vector2(-4.5f + count - blocks, 12), Quaternion.identity);
        GetComponent<AudioSource>().Play();
        AddLetter();
        }
    }

    public int blocks = 0;
    public void sendTail()
    {
        char[] x = new char[tail.Count];
        char[] y = new char[tail.Count - blocks];
        for(int i = 0; i < tail.Count; i++) {

            if (tail[i].name.StartsWith("SnakeBody"))
            {
                x[i] = '-';
            }
            else if (tail[i].name.StartsWith("FoodTileBlank"))
            {
                x[i] = ' ';
                wpc = wpc + 0;
            }
            else if (tail[i].name.StartsWith("FoodTileA"))
            {
                x[i] = 'A';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileB"))
            {
                x[i] = 'B';
                wpc = wpc + 3;
            }
            else if (tail[i].name.StartsWith("FoodTileC"))
            {
                x[i] = 'C';
                wpc = wpc + 3;
            }
            else if (tail[i].name.StartsWith("FoodTileD"))
            {
                x[i] = 'D';
                wpc = wpc + 2;
            }
            else if (tail[i].name.StartsWith("FoodTileE"))
            {
                x[i] = 'E';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileF"))
            {
                x[i] = 'F';
                wpc = wpc + 4;
            }
            else if (tail[i].name.StartsWith("FoodTileG"))
            {
                x[i] = 'G';
                wpc = wpc + 2;
            }
            else if (tail[i].name.StartsWith("FoodTileH"))
            {
                x[i] = 'H';
                wpc = wpc + 4;
            }
            else if (tail[i].name.StartsWith("FoodTileI"))
            {
                x[i] = 'I';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileJ"))
            {
                x[i] = 'J';
                wpc = wpc + 8;
            }
            else if (tail[i].name.StartsWith("FoodTileK"))
            {
                x[i] = 'K';
                wpc = wpc + 5;
            }
            else if (tail[i].name.StartsWith("FoodTileL"))
            {
                x[i] = 'L';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileM"))
            {
                x[i] = 'M';
                wpc = wpc + 3;
            }
            else if (tail[i].name.StartsWith("FoodTileN"))
            {
                x[i] = 'N';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileO"))
            {
                x[i] = 'O';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileP"))
            {
                x[i] = 'P';
                wpc = wpc + 3;
            }
            else if (tail[i].name.StartsWith("FoodTileQ"))
            {
                x[i] = 'Q';
                wpc = wpc + 10;
            }
            else if (tail[i].name.StartsWith("FoodTileR"))
            {
                x[i] = 'R';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileS"))
            {
                x[i] = 'S';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileT"))
            {
                x[i] = 'T';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileU"))
            {
                x[i] = 'U';
                wpc = wpc + 1;
            }
            else if (tail[i].name.StartsWith("FoodTileV"))
            {
                x[i] = 'V';
                wpc = wpc + 4;
            }
            else if (tail[i].name.StartsWith("FoodTileW"))
            {
                x[i] = 'W';
                wpc = wpc + 4;
            }
            else if (tail[i].name.StartsWith("FoodTileX"))
            {
                x[i] = 'X';
                wpc = wpc + 8;
            }
            else if (tail[i].name.StartsWith("FoodTileY"))
            {
                x[i] = 'Y';
                wpc = wpc + 4;
            }
            else if (tail[i].name.StartsWith("FoodTileZ"))
            {
                x[i] = 'Z';
                wpc = wpc + 10;
            }
        }
        for(int i = 0; i < tail.Count - blocks; i++)
        {
            y[i] = x[i + blocks];
        }
        finalWord = new string(y);
    }
    
    public void AddBlocks()
    {
        word.Clear();
        tail.Clear();
        playerMovement.pointsHistory.Clear();
        GameObject[] letters = GameObject.FindGameObjectsWithTag("TailSplay");
        for (int i = 0; i < letters.Length; i++)
        {
            GameObject.Destroy(letters[i]);
        }
        for (int i = 0; i < blocks; i++)
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
    }

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

        for (int i = 0; i < blocks; i++)
        {
            GameObject block = Instantiate(snakeBody, new Vector2(0, -25), Quaternion.identity);
            block.gameObject.tag = "TailSplay";
            if (!PlayerPrefs.HasKey("CurHead"))
            {
                block.GetComponent<SpriteRenderer>().sprite = (Sprite)bodySprites[0];
            }
            else {
                block.GetComponent<SpriteRenderer>().sprite = (Sprite)bodySprites[PlayerPrefs.GetInt("CurHead") / 2];
            }
            tail.Add(block);
        }
        multi = 1;   
}
    //Clears the Board of all food Objects
    void ClearBoard()
    {
        int k = foodAlive.Count;
        for(int n = 0; n < k; n++)
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
		if (randomIndex == 122) {
			randomIndex = randomIndex + Random.Range (1, 20);
		}
		Vector2 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        GameObject gameObj;
        int randomLetter = Random.Range(2, 99);
        Char letter = new char();
        int Points = 0;
        if(randomLetter >= 2 && randomLetter <= 10)
        {
            gameObj = foodTileA;
        }
        else if (randomLetter >= 11 && randomLetter <= 12)
        {
            gameObj = foodTileB;
        }
        else if (randomLetter >= 13 && randomLetter <= 14)
        {
            gameObj = foodTileC;
        }
        else if (randomLetter >= 15 && randomLetter <= 18)
        {
            gameObj = foodTileD;
        }
        else if (randomLetter >= 19 && randomLetter <= 30)
        {
            gameObj = foodTileE;
        }
        else if (randomLetter >= 31 && randomLetter <= 32)
        {
            gameObj = foodTileF;
        }
        else if (randomLetter >= 33 && randomLetter <= 35)
        {
            gameObj = foodTileG;
        }
        else if (randomLetter >= 36 && randomLetter <= 37)
        {
            gameObj = foodTileH;
        }
        else if (randomLetter >= 38 && randomLetter <= 46)
        {
            gameObj = foodTileI;
        }
        else if (randomLetter == 47)
        {
            gameObj = foodTileJ;
        }
        else if (randomLetter == 48)
        {
            gameObj = foodTileK;
        }
        else if (randomLetter >= 49 && randomLetter <= 52)
        {
            gameObj = foodTileL;
        }
        else if (randomLetter >= 53 && randomLetter <= 54)
        {
            gameObj = foodTileM;
        }
        else if (randomLetter >= 55 && randomLetter <= 60)
        {
            gameObj = foodTileN;
        }
        else if (randomLetter >= 61 && randomLetter <= 68)
        {
            gameObj = foodTileO;
        }
        else if (randomLetter >= 69 && randomLetter <= 70)
        {
            gameObj = foodTileP;
        }
        else if (randomLetter == 71)
        {
            gameObj = foodTileQ;
        }
        else if (randomLetter >= 72 && randomLetter <= 77)
        {
            gameObj = foodTileR;
        }
        else if (randomLetter >= 78 && randomLetter <= 81)
        {
            gameObj = foodTileS;
        }
        else if (randomLetter >= 82 && randomLetter <= 87)
        {
            gameObj = foodTileT;
        }
        else if (randomLetter >= 88 && randomLetter <= 91)
        {
            gameObj = foodTileU;
        }
        else if (randomLetter >= 92 && randomLetter <= 93)
        {
            gameObj = foodTileV;
        }
        else if (randomLetter >= 94 && randomLetter <= 95)
        {
            gameObj = foodTileW;
        }
        else if (randomLetter == 96)
        {
            gameObj = foodTileX;
        }
        else if (randomLetter >= 97 && randomLetter <= 98)
        {
            gameObj = foodTileY;
        }
        else
        {
            gameObj = foodTileZ;
        }

        return new LetterPlaced(randomPosition, randomIndex, letter, gameObj, Points);
    }

    // Spawn one piece of food
    public void AddLetter()
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
            if (playerMovement.pointsHistory.Count > 2)
            {
                tail[i].transform.position = playerMovement.pointsHistory[playerMovement.pointsHistory.Count - (2 + i)];
            }
        }

    }

    void Update()
    {
        followTail();
    }
}