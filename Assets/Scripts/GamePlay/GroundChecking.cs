using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GroundChecking : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider col)
    {
        

        if (col.gameObject.tag == "Sky")
        {
            transform.parent.gameObject.transform.DOKill();
          transform.parent.GetComponent<Rigidbody>().useGravity = true;
           GamePlayAction.instance.hanumanAnim.SetTrigger("ForceIdle" );

        }

        

    }
    private void OnTriggerStay(Collider col)
    {

//        Debug.Log("Collide object tag " + col.gameObject.tag);
        if (col.gameObject.tag == "Ground")
        {           

            GamePlayAction.instance.currentState = GamePlayAction.HunaumanState.Ground;          

            GamePlayAction.instance.isGrounded = true;
            GamePlayAction.instance.hanumanAnim.SetBool("IsGrounded",true);
            GamePlayAction.instance.hanumanAnim.SetBool("GoIdle", false);

            if ((GamePlayAction.instance.leftClicked || GamePlayAction.instance.rightClicked))
            {
                if (!GloabalAudioSournce.instance.sm.isPlaying) { 
                    GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.run;
                GloabalAudioSournce.instance.sm.Play();
            }
            }
        }
        if (col.gameObject.tag == "Sky")
        {
            transform.parent.gameObject.transform.DOKill();
            transform.parent.GetComponent<Rigidbody>().useGravity = true;
            GamePlayAction.instance.hanumanAnim.SetTrigger("ForceIdle");

        }
        if (col.gameObject.tag == "Box" && GamePlayAction.instance.isGrounded &&
            !GamePlayAction.instance.hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")
            && GamePlayAction.instance.box==null )
        {


            if (!GamePlayAction.instance.isBoxLift && GamePlayAction.instance.hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
            {
                Camera.main.GetComponent<UnityEngine.Animations.PositionConstraint>().enabled = false;
                GamePlayAction.instance.Hanuman.GetComponent<HanumanMovement>().enabled = false;
            }
            else {
                Camera.main.GetComponent<UnityEngine.Animations.PositionConstraint>().enabled = true;
                GamePlayAction.instance.Hanuman.GetComponent<HanumanMovement>().enabled = true;
            }


            GamePlayAction.instance.attackButton.SetActive(false);
            GamePlayAction.instance.liftBoxButton.SetActive(true);
            GamePlayAction.instance.box = col.gameObject;

            Debug.Log("Collide cube " +col.gameObject.name);



        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GamePlayAction.instance.currentState = GamePlayAction.HunaumanState.Jump;
           // Debug.Log("GamePlayAction.instance.currentState " + GamePlayAction.instance.currentState);
            GamePlayAction.instance.hanumanAnim.SetBool("IsGrounded", false);
            GamePlayAction.instance.isGrounded = false;

        }
        if (col.gameObject.tag == "Box")
        {
            Camera.main.GetComponent<UnityEngine.Animations.PositionConstraint>().enabled = true;
            GamePlayAction.instance.Hanuman.GetComponent<HanumanMovement>().enabled = true;
          //  GamePlayAction.instance.isBoxLift = false;

            if (!GamePlayAction.instance.isBoxLift)
            {
                col.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                col.gameObject.GetComponent<Rigidbody>().useGravity = true;

                GamePlayAction.instance.attackButton.SetActive(true);
                GamePlayAction.instance.liftBoxButton.SetActive(false);
                GamePlayAction.instance.box = null;
            }
           
        }

    }
   

}
