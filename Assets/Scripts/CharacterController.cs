using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    
    int BoardIndex = 4;
    int BodyIndex = 2;
    int HeadIndex = 4;
    //SWIM
    private float FRAMESWIM = 0.150f;
    private float PASSEDSWIM;
    private int SwimIndex = 0;
    private Sprite[] SwimHead;
    private Sprite[] SwimBody;
    private Sprite[] SwimBoard;
    //

    //SURFING
    private Sprite[] SpritesHead;
    private Sprite[] SpritesBody;
    private Sprite[] SpritesBoard;
    private static float DEFAULTTIMETOWAITINDEX = 0.055f;
    private float TIMETOWAITINDEX = DEFAULTTIMETOWAITINDEX;

    private static float DEFAULTTIMETOWAITINDEXDOWN = 0.055f;
    private float TIMETOWAITINDEXDOWN = DEFAULTTIMETOWAITINDEXDOWN;
    private float Force = 300f;
    public const float STARTSPEED = 1;
    public float Speed = STARTSPEED;
    public float OldSpeed;
    public float MaxSpeed;
    public float SpeedGet;
    public float Gravity;
    //

    //States
    private StateController GameState;
    public bool HasStarted;
    bool HasTouched;
    public int Index = 6;
    public bool inSky;
    int Flip = 1;
    public bool OnTube;
    //

    //others
    Animator thisAnim;
    public SpriteRenderer Head;
    public SpriteRenderer Body;
    public SpriteRenderer Board;
    public Rigidbody2D Rigid;
    public Camera thisCamera;
    
    //Trick
   public int RotationTrick;
   public bool dieing;
   public bool dead;
    

    void Start()
    {
        thisAnim = this.GetComponent<Animator>();
        GameState = (StateController)FindObjectOfType(typeof(StateController));
        SwimHead = new Sprite[4];
        SwimBody = new Sprite[4];
        SwimBoard = new Sprite[4];

        for (int i = 0; i < SwimHead.Length; i++)
        {
            string PathBoard = "Characters/board_idlestart_" + BoardIndex + i.ToString();
            string PathHead = "Characters/head_idlestart_" + HeadIndex + i.ToString();
            string PathBody = "Characters/body_idlestart_" + BodyIndex + i.ToString();

            SwimBoard[i] = (Sprite)Resources.Load(PathBoard, typeof(Sprite));
            SwimHead[i] = (Sprite)Resources.Load(PathHead, typeof(Sprite));
            SwimBody[i] = (Sprite)Resources.Load(PathBody, typeof(Sprite));
        }

        SpritesHead = new Sprite[9];
        SpritesBody = new Sprite[9];
        SpritesBoard = new Sprite[9];
        for (int i = 0; i < SpritesHead.Length; i++)
        {
            string PathBoard = "Characters/board" + BoardIndex + i.ToString();
            string PathHead = "Characters/head" + HeadIndex + i.ToString();
            string PathBody = "Characters/body" + BodyIndex + i.ToString();

            SpritesBoard[i] = (Sprite)Resources.Load(PathBoard, typeof(Sprite));
            SpritesHead[i] = (Sprite)Resources.Load(PathHead, typeof(Sprite));
            SpritesBody[i] = (Sprite)Resources.Load(PathBody, typeof(Sprite));
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.isGamePaused || dead)
            return;

        if (Input.GetMouseButton(0) && !HasTouched)
        {
            HasTouched = true;
        }


        if (!HasStarted)
            AnimateSwim();

        if (HasStarted)
        {
            if (!inSky)
            {
                CheckDirection();
                GetSpeed();
            }
            else
            {

                CheckDirectionTrick();
            }
        }

        if(Input.GetMouseButton(1))
        {
            thisAnim.SetTrigger("Trick");
        }
    }
    private void CheckDirectionTrick()
    {
        if (Input.GetMouseButton(0))
        {
            if (TIMETOWAITINDEX > 0)
                TIMETOWAITINDEX -= Time.deltaTime;


            if (TIMETOWAITINDEX <= 0)
            {

                Vector2 MousePos = thisCamera.ScreenToWorldPoint(Input.mousePosition);

                if (MousePos.x < 0)
                {
                    if (RotationTrick > -8)
                    {
                        RotationTrick--;
                    }
                    else
                    {
                        RotationTrick = 8;
                    }

                    if (RotationTrick < 0)
                        Flip = -1;
                    else
                        Flip = 1;
                }
                else
                {

                    if (RotationTrick < 8)
                    {
                        RotationTrick++;
                    }
                    else
                    {
                        RotationTrick = -8;
                    }

                    if (RotationTrick < 0)
                        Flip = -1;
                    else
                        Flip = 1;

                }

                foreach (Transform child in transform)
                    child.localScale = new Vector3(Flip, 1, 1);


                Index = Mathf.Abs(RotationTrick);

                TIMETOWAITINDEX = DEFAULTTIMETOWAITINDEX;
            }

        }
        else
        {

            //if (TIMETOWAITINDEXDOWN > 0)
            //    TIMETOWAITINDEXDOWN -= Time.deltaTime;

            //if (TIMETOWAITINDEXDOWN <= 0)
            //{
            //    if (Index < 8)
            //    {
            //        RotationTrick++;
            //        Index++;
            //    }
            //    TIMETOWAITINDEXDOWN = DEFAULTTIMETOWAITINDEXDOWN;
            //}

        }

        Head.sprite = SpritesHead[Index];
        Body.sprite = SpritesBody[Index];
        Board.sprite = SpritesBoard[Index];
    }

    private void GetSpeed()
    {
      
            if (Speed < MaxSpeed)
            {
                Speed += SpeedGet * Time.deltaTime;

            }
        
    }


    private void AnimateSwim()
    {
        PASSEDSWIM += Time.deltaTime;

        if (PASSEDSWIM >= FRAMESWIM)
        {

            switch (SwimIndex)
            {
                case 0: SwimIndex++; break;
                case 1: SwimIndex++; break;
                case 2:
                    {

                        if (!HasTouched)
                            SwimIndex = 0;
                        else
                            SwimIndex++;

                         
                    };break;
                case 3:
                    {
                        HasStarted = true;
                        ForceDown();
                        

                    }break;
            }
            PASSEDSWIM = 0;

            Head.sprite = SwimHead[SwimIndex];
            Body.sprite = SwimBody[SwimIndex];
            Board.sprite = SwimBoard[SwimIndex];
        }
       

    }

    private void CheckDirection()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 MousePos = thisCamera.ScreenToWorldPoint(Input.mousePosition);

            if (TIMETOWAITINDEX > 0)
                TIMETOWAITINDEX -= Time.deltaTime;


            if (TIMETOWAITINDEX <= 0)
            {

                if (MousePos.x < 0)
                {


                    if (Index > 0)
                    {
                        Index--;
                    }
                    if (!inSky)
                    {
                        if(Index <= 4)
                        ForceUp(0);
                    }
                }
                else
                {
                    if (Index < 8)
                    {
                        Index++;
                    }

                    if (!inSky)
                    {
                        if (Index  >= 4)
                        ForceDown(); ;
                    }

                }
                TIMETOWAITINDEX = DEFAULTTIMETOWAITINDEX;
            }
        }

        //if (!Input.GetMouseButton(0))
        //{
        //    if (TIMETOWAITINDEXDOWN > 0)
        //        TIMETOWAITINDEXDOWN -= Time.deltaTime;

        //    if (TIMETOWAITINDEXDOWN <= 0)
        //    {
        //        if (Index < 8)
        //        {
        //            Index++;
        //        }
        //        TIMETOWAITINDEXDOWN = DEFAULTTIMETOWAITINDEXDOWN;
        //    }


        //}
        Head.sprite = SpritesHead[Index];
        Body.sprite = SpritesBody[Index];
        Board.sprite = SpritesBoard[Index];
    }

    private void ForceDown()
    {
        Rigid.AddForce(new Vector2(0, Time.deltaTime * -Force));
    }

    private void ForceUp(int Qtd)
    {
        Rigid.AddForce(new Vector2(0, Time.deltaTime * Force + Qtd));
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        CheckTrickEdgeCollision(c);

        if (c.tag == "Edge" && !dieing && !dead)
            NormalDie();

        if (c.tag == "Coin")
            c.gameObject.SetActive(false);

    }

    private void CheckTrickEdgeCollision(Collider2D c)
    {
        if (c.tag == "TrickEdgeStart" && !inSky)
        {
            Rigid.gravityScale = Gravity;
            inSky = true;
            ForceUp(0);
            RotationTrick = Index;
            OldSpeed = Speed;
            Speed = 0;

        }
        if (c.tag == "TrickEdgeEnd" && inSky)
        {
            Rigid.gravityScale = 0f;
            ForceUp(10);
            inSky = false;
            CheckLanding();
        }

      

    }

    private void CheckLanding()
    {

        Speed = OldSpeed;

        if (Index > 0 && Index <= 4)
            NormalDie();

        if ((Index != 8 && Index != 0) && Flip == -1) //morre se tiver virado menos caindo de costas e frente virado
            NormalDie();
        else
            Index = 8;

        //if (Index == 7)
        //{
        //    //if (OldSpeed - 1f >= STARTSPEED)
        //        //Speed = OldSpeed - 1f;
        //}
        //else if (Index == 6)
        //{
        //    //if (OldSpeed - 3f >= STARTSPEED)
        //       // Speed = OldSpeed - 3f;
        //}
        //else if (Index == 5)
        //{
        //    //Speed = STARTSPEED;
        //}
        //else if( Index == 0)
        //{
        //    Index = 8;
        //  //  Speed = OldSpeed;
        //}
        //else if (Index == 8)
        //{
        //    //Speed = OldSpeed;
        //}
        //else
        //{
           
        //    Speed = STARTSPEED;
        //    Index = 8;
        //    NormalDie();
        //    //return;

        //}
        Flip = 1;
        foreach (Transform child in transform)
            child.localScale = new Vector3(Flip, 1, 1);
    }

    private void NormalDie()
    {
        dieing = true;
        thisAnim.SetTrigger("TrgNormalDeath");
        Rigid.isKinematic = true;
    }


}
