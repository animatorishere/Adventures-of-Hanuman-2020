  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GamePlayAction : MonoBehaviour
{
    public static GamePlayAction instance;
    public GameObject Hanuman;
    [SerializeField]
    internal Animator hanumanAnim;
    public int speed;
    int changeSpeedForDirection = 4;
    int jumpSpeed = 8;
    int currentSpeed;
    internal bool leftClicked = false;
    internal bool rightClicked = false;
    bool doubleJumpEnable = false;
    internal bool isGrounded = true;
    internal bool doNotMove = false;
    bool alternateAttack = false;
    float tempTime;
    bool isAttacked = false;



    float jumpTime;
    internal HunaumanState currentState;
    internal JumpState jumpState;

    public GameObject hanumanHand,box_parent,box;
    public Transform boxSpaun;
    public GameObject attackButton, liftBoxButton;
    public bool isBoxLift;

  //  public Transform Masharoom, zoombeePlant;


    internal enum HunaumanState
    {
        Ground,
        Jump,
        Attack
    }
    internal enum JumpState
    {
        onGround,
        Single,
        Double
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
       // hanumanAnim = Hanuman.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Attack_Down();

        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Attack_Up();

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftWalkOnDown();

        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            LeftWalkUp();

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightWalkOnDown();

        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            RightWalkUp();

        }

        //if (hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
        //{
        //    GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.run;
        //    GloabalAudioSournce.instance.sm.Play();
        //}
        if (leftClicked)
        {
           
            if (hanumanAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                speed = -jumpSpeed;

            }
            else if (hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                speed = 0;

            }
            else
            {
                Invoke("WaitForLeftWalk", 0f);
            }

           

        }
        if (rightClicked)
        {
           
            if (hanumanAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {

                speed = jumpSpeed;
            }
            else if (hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                speed = 0;

            }
            else
            {
                Invoke("WaitForRightWalk", 0f);
            }

        
        }


    }

  
    void WaitForLeftWalk()
    {
        speed = -changeSpeedForDirection;

    }
    void WaitForRightWalk()
    {
        speed = changeSpeedForDirection;

    }

    #region UI Action
    public void OnClick() {
        Debug.Log("kkkkjkkkkkkkkkkk");
    }
    public void LeftWalkOnDown()
    {
        if (currentState == GamePlayAction.HunaumanState.Ground)
        {
            GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.run;
            GloabalAudioSournce.instance.sm.Play();
        }
        GamePlayAction.instance.hanumanAnim.SetBool("GoIdle", true);
        leftClicked = true;
        Hanuman.transform.localScale = new Vector3(1, 1, -1);

        
        if (!isBoxLift)
            hanumanAnim.SetBool("LeftWalk", true);
        else
            hanumanAnim.SetBool("BoxWalkLeft", true);

        if (hanumanAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            speed = -jumpSpeed;

        }
        else
        {
            speed = -changeSpeedForDirection;
           

        }
    }
    public void LeftWalkUp()
    {
        GloabalAudioSournce.instance.sm.Stop();
        GamePlayAction.instance.hanumanAnim.SetBool("GoIdle", false);
       
        leftClicked = false;

      
        hanumanAnim.SetBool("LeftWalk", false);
        hanumanAnim.SetBool("BoxWalkLeft", false);


        speed = 0;
    }


    public void RightWalkOnDown()
    {
        if (currentState == GamePlayAction.HunaumanState.Ground)
        {
            GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.run;
            GloabalAudioSournce.instance.sm.Play();
        }

        GamePlayAction.instance.hanumanAnim.SetBool("GoIdle", true);

        rightClicked = true;

        Hanuman.transform.localScale = new Vector3(1, 1, 1);
        if(!isBoxLift)
        hanumanAnim.SetBool("RightWalk", true);
        else 
        hanumanAnim.SetBool("BoxWalkRight", true);

        if (hanumanAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            speed = jumpSpeed;
        }
        else
        {
            speed = changeSpeedForDirection;
        }
    }
    public void RightWalkUp()
    {
        GloabalAudioSournce.instance.sm.Stop();
        GamePlayAction.instance.hanumanAnim.SetBool("GoIdle", false);

        rightClicked = false;

       

        hanumanAnim.SetBool("BoxWalkRight", false);
        hanumanAnim.SetBool("RightWalk", false);


        speed = 0;

    }

    public void Jump()
    {

        if (!isAttacked && !hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Debug.Log("AP1000 - JumpState before" + jumpState);
            if (currentState == GamePlayAction.HunaumanState.Ground)
            {
                jumpState = JumpState.Single;
                Hanuman.GetComponent<Rigidbody>().useGravity = false;
                if (!isBoxLift)
                    hanumanAnim.SetTrigger("Jump");
                else
                {
                    hanumanAnim.SetTrigger("BoxJump");
                    Destroy(box.GetComponent<Rigidbody>());
                }
                Hanuman.transform.DOMoveY(Hanuman.transform.position.y + 0.8f, 0.5f).OnComplete(EnableGravity);
            }
            else if (jumpState == JumpState.Single && !isBoxLift )
            {
                jumpState = JumpState.Double;
                Hanuman.GetComponent<Rigidbody>().useGravity = false;
                hanumanAnim.SetTrigger("Jump_2");
                Hanuman.transform.DOMoveY(Hanuman.transform.position.y + 1.5f, 0.5f).OnComplete(EnableGravity);

            }
            Debug.Log("AP1000 - JumpState After" + jumpState);
            GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.jump;
            GloabalAudioSournce.instance.sm.Play();

        }
    }

    void EnableGravity()
    {
        Hanuman.GetComponent<Rigidbody>().useGravity = true;
        if (jumpState == JumpState.Double)
            jumpState = JumpState.onGround;
      
    }

    public void Attack_Down()
    {
        isAttacked = true;
        hanumanAnim.SetBool("GoIdle", true);

        if (hanumanAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            hanumanAnim.SetTrigger("JumpAttack");
            GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.jumpAttack;
            GloabalAudioSournce.instance.sm.Play();

        }
        else if (hanumanAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump_2"))
        {
            hanumanAnim.SetTrigger("JumpAttack");
            GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.jumpAttack;
            GloabalAudioSournce.instance.sm.Play();
        }
        else
        {
            if (!alternateAttack)
            {
                hanumanAnim.SetTrigger("Attack_1");
            }
            else
            {
                hanumanAnim.SetTrigger("Attack_2");
            }
            alternateAttack = !alternateAttack;
            GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.attack;
            GloabalAudioSournce.instance.sm.Play();
        }
    }
    public void Attack_Up()
    {
        isAttacked = false;
        hanumanAnim.SetBool("GoIdle", false);
       
    }

    public void Combo_Down()
    {
        if (currentState == GamePlayAction.HunaumanState.Ground)
        {
            isAttacked = true;
            hanumanAnim.SetTrigger("Combo");
            GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.ComboAttack;
            GloabalAudioSournce.instance.sm.Play();

        }       

    }
    public void Combo_Up()
    {
            isAttacked = false;        
        
    }


    public void LiftBox() {

        isBoxLift = !isBoxLift;
        if (isBoxLift && !hanumanAnim.GetBool("LiftBox"))
        { 
            hanumanAnim.SetBool("LiftBox", true);        
            Destroy(box.GetComponent<Rigidbody>());
            GamePlayAction.instance.hanumanAnim.SetBool("BoxIdle", true);
            box.transform.parent = hanumanHand.transform;
            box.transform.localEulerAngles = boxSpaun.localEulerAngles;
            box.transform.localPosition = boxSpaun.localPosition;
            Camera.main.GetComponent<UnityEngine.Animations.PositionConstraint>().enabled = true;
            GamePlayAction.instance.Hanuman.GetComponent<HanumanMovement>().enabled = true;

        }
        else {
            hanumanAnim.SetBool("LiftBox", false);
            GamePlayAction.instance.hanumanAnim.SetBool("BoxIdle", false);
            box.transform.parent = box_parent.transform;
            box.AddComponent<Rigidbody>();
            box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            if (Hanuman.transform.localScale == new Vector3(1, 1, 1))
            {
                box.GetComponent<Rigidbody>().AddForce(Vector3.right * 2.7f, ForceMode.Impulse);
            }
            else {
                box.GetComponent<Rigidbody>().AddForce(Vector3.left * 2.7f, ForceMode.Impulse);
            }
            GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.attack;
            GloabalAudioSournce.instance.sm.Play();

            Invoke("DoBoxEmpty", 2f);

        }


    }
    void DoBoxEmpty() {
        if(!isBoxLift)
        box = null;
      


    }
    #endregion


}
