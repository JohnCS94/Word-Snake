using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IPointerDownHandler
{
    public GameObject field;
    public Button up;
    public Button down;
    public Button left;
    public Button right;

    Button upBtn;
    Button downBtn;
    Button leftBtn;
    Button rightBtn;

    //Speed
    public float unitsPerFrame = 1;
    public float snakeSpeed = 0.2f;

    //Tail
    private GameObject[] wordTiles;  

    //Direction
    public bool upDir = false;
    public bool downDir = false;
    public bool rightDir = false;
    public bool leftDir = false;

    public bool moveEnabled = true;
    public bool moving = false;

    // Borders
    public Double rightCol = 6.5;
    public Double leftCol = -6.5;
    public Double topRows = 9;
    public Double bottomRows = -11;

    private Vector2 touchOrigin = -Vector2.one;
    private Vector2 joyOrigin = -Vector2.one;
    public List<Vector3> pointsHistory;

    public VirtualJoyStick joystick;

    public Vector3 inputVector;
    public Vector2 mDir;

    public void Start()
    {
        upBtn = up.GetComponent <Button>();
        downBtn = down.GetComponent<Button>();
        leftBtn = left.GetComponent<Button>();
        rightBtn = right.GetComponent<Button>();
    }

    public void Awake()
    {
        InvokeRepeating("Move", snakeSpeed, snakeSpeed);
        Time.timeScale = 1;
        pointsHistory = new List<Vector3>();
        field = GetComponent<GameObject>();
    }

    public void OnPointerDown(PointerEventData ped)
    {

    }

    // Update is called once per frame
    void Update() {

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (moveEnabled == true && Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.UpArrow) || joystick.up())
            {
                if (downDir == false)
                {
                    transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
                    mDir = Vector2.up * unitsPerFrame;

                    upDir = true;
                    downDir = false;
                    rightDir = false;
                    leftDir = false;
                    setMovement();
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow) || joystick.down())
            {
                if (upDir == false)
                {
                    transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 180);
                    mDir = Vector2.down * unitsPerFrame;

                    upDir = false;
                    downDir = true;
                    rightDir = false;
                    leftDir = false;
                    setMovement();
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || joystick.left())
            {
                if (rightDir == false)
                {
                    transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 90);
                    mDir = Vector2.left * unitsPerFrame;

                    upDir = false;
                    downDir = false;
                    rightDir = false;
                    leftDir = true;
                    setMovement();
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) || joystick.right())
            {
                if (leftDir == false)
                {
                    transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, -90);
                    mDir = Vector2.right * unitsPerFrame;

                    upDir = false;
                    downDir = false;
                    rightDir = true;
                    leftDir = false;
                    setMovement();
                }
            }
        }

#endif

#if UNITY_ANDROID || UNITY_IPHONE
        if (moveEnabled == true && Time.timeScale != 0)
        {
            if (PlayerPrefs.GetString("Controls").Equals("Swipe"))
            {
                if (Input.touchCount > 0)
                {
                    Touch myTouch = Input.touches[0];

                    if (myTouch.phase == TouchPhase.Began)
                    {
                        touchOrigin = myTouch.position;
                    }
                    else if (myTouch.phase == TouchPhase.Ended)
                    {
                    
                        Vector2 touchEnd = myTouch.position;
                        float x = touchEnd.x - touchOrigin.x;
                        float y = touchEnd.y - touchOrigin.y;

                        //PositionX.text = "X: " + touchOrigin.x + " " + touchEnd.x + "|" + x + "";
                        //PositionY.text = "Y: " + touchOrigin.y + " " + touchEnd.y + "|" + y + "";

                        touchOrigin = Vector2.zero;
                        if (Mathf.Abs(x) > Mathf.Abs(y))
                        {
                            if (x < 0 && rightDir == false)
                            {
                                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 90);
                                mDir = Vector2.left * unitsPerFrame;

                                upDir = false;
                                downDir = false;
                                rightDir = false;
                                leftDir = true;
                                setMovement();
                            }
                            if (x > 0 && leftDir == false)
                            {
                                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, -90);
                                mDir = Vector2.right * unitsPerFrame;

                                upDir = false;
                                downDir = false;
                                rightDir = true;
                                leftDir = false;
                                setMovement();
                            }
                        }
                        if (Mathf.Abs(y) > Mathf.Abs(x))
                        {
                            if (y < 0 && upDir == false)
                            {
                                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 180);
                                mDir = Vector2.down * unitsPerFrame;

                                upDir = false;
                                downDir = true;
                                rightDir = false;
                                leftDir = false;
                                setMovement();
                            }

                            if (y > 0 && downDir == false)
                            {
                                transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
                                mDir = Vector2.up * unitsPerFrame;

                                upDir = true;
                                downDir = false;
                                rightDir = false;
                                leftDir = false;
                                setMovement();
                            }
                        
                        }
                    }
                }
            }

            else if (Time.timeScale == 1)
            {
                if (PlayerPrefs.GetString("Controls").Equals("Joystick"))
                {

                    if (joystick.up() && downDir == false)
                    {
                        transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
                        mDir = Vector2.up * unitsPerFrame;

                        upDir = true;
                        downDir = false;
                        rightDir = false;
                        leftDir = false;
                        setMovement();
                    }
                    if (joystick.right() && leftDir == false)
                    {
                        transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, -90);
                        mDir = Vector2.right * unitsPerFrame;

                        upDir = false;
                        downDir = false;
                        rightDir = true;
                        leftDir = false;
                        setMovement();
                    }
                    if (joystick.down() && upDir == false)
                    {

                        transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 180);
                        mDir = Vector2.down * unitsPerFrame;

                        upDir = false;
                        downDir = true;
                        rightDir = false;
                        leftDir = false;
                        setMovement();
                    }
                    if (joystick.left() && rightDir == false)
                    {

                        transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 90);
                        mDir = Vector2.left * unitsPerFrame;

                        upDir = false;
                        downDir = false;
                        rightDir = false;
                        leftDir = true;
                        setMovement();
                    }
                
                }

                else if(PlayerPrefs.GetString("Controls").Equals("DPad"))
                {
                    upBtn.onClick.AddListener(Up);
                    downBtn.onClick.AddListener(Down);
                    leftBtn.onClick.AddListener(Left);
                    rightBtn.onClick.AddListener(Right);
                }
            }  
        }     

#endif

        if (pointsHistory.Count == 0)
        {
            pointCreate();
        }
        if (pointsHistory != null && GameObject.FindGameObjectWithTag("Player").transform.position != pointsHistory[pointsHistory.Count - 1])
        {
            pointCreate();
        }
        
    }

    void Up()
    {
        if (downDir == false) 
        {
            transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
            mDir = Vector2.up * unitsPerFrame;

            upDir = true;
            downDir = false;
            rightDir = false;
            leftDir = false;
            setMovement();
        }
    }

    void Down()
    {
        if (upDir == false)
        {
            transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 180);
            mDir = Vector2.down * unitsPerFrame;

            upDir = false;
            downDir = true;
            rightDir = false;
            leftDir = false;
            setMovement();
        }
    }

    void Left()
    {
        if (rightDir == false)
        {

            transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 90);
            mDir = Vector2.left * unitsPerFrame;

            upDir = false;
            downDir = false;
            rightDir = false;
            leftDir = true;
            setMovement();
        }
    }

    void Right()
    {
        if (leftDir == false)
        {
            transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, -90);
            mDir = Vector2.right * unitsPerFrame;

            upDir = false;
            downDir = false;
            rightDir = true;
            leftDir = false;
            setMovement();
        }
    }

    void pointCreate()
    {
        Vector4 newPoint = transform.position;
        newPoint.w = Time.time;
        pointsHistory.Add(newPoint);
    }

    void Move ()
    {
        transform.Translate(mDir);
    }
    void setMovement()
    {
        moving = true;
    }
}
