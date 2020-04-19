using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HanumanCollision : MonoBehaviour
{
    float backMove;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Wall" && GamePlayAction.instance.hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
        {

            Camera.main.GetComponent<UnityEngine.Animations.PositionConstraint>().enabled = false;
            GamePlayAction.instance.doNotMove = true;
                       
        }

      

        if ( (other.gameObject.tag == "EnemySpider" || other.gameObject.name == "MushroomDanger"
             || other.gameObject.name == "Zombie Plant")
            && (!GamePlayAction.instance.hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")
            || GamePlayAction.instance.hanumanAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump Hit")))
        {
           

            if (other.gameObject.name == "SpiderKing")
                backMove = 1f;
            else
                backMove = 0.5f;

            if (other.gameObject.name == "MushroomDanger")
            other.gameObject.GetComponent<Animator>().SetTrigger("Attack");


            GamePlayAction.instance.Hanuman.transform.GetChild(3).gameObject.SetActive(false);
            GamePlayAction.instance.Hanuman.transform.GetChild(3).gameObject.SetActive(true);
            //GloabalAudioSournce.instance.sm.Stop();
            GloabalAudioSournce.instance.sm.clip = GloabalAudioSournce.instance.hanumanDamage;
            GloabalAudioSournce.instance.sm.Play();
           // GloabalAudioSournce.instance.sm.PlayOneShot(GloabalAudioSournce.instance.hanumanDamage);


            if (other.gameObject.transform.position.x< GamePlayAction.instance.Hanuman.transform.position.x)
            {
                GamePlayAction.instance.Hanuman.transform.DOMoveX(GamePlayAction.instance.Hanuman.transform.position.x + backMove, 0.25f);
                GamePlayAction.instance.Hanuman.transform.DOMoveY(GamePlayAction.instance.Hanuman.transform.position.y + 0.1f, 0.1f);

            }
            else
            {
                GamePlayAction.instance.Hanuman.transform.DOMoveX(GamePlayAction.instance.Hanuman.transform.position.x - backMove, 0.25f);
                GamePlayAction.instance.Hanuman.transform.DOMoveY(GamePlayAction.instance.Hanuman.transform.position.y + 0.1f, 0.1f);


            }

        }
        
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "EnemyMushroom" )
        {

            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
         if (other.gameObject.tag == "EnemyZombie")
        {
            other.transform.GetChild(0).GetComponent<Animator>().SetBool("Grow", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Camera.main.GetComponent<UnityEngine.Animations.PositionConstraint>().enabled = true;
            GamePlayAction.instance.doNotMove = false;
        }
        if (other.gameObject.tag == "EnemyMushroom")
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (other.gameObject.tag == "EnemyZombie" && !GamePlayAction.instance.hanumanAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            other.transform.GetChild(0).GetComponent<Animator>().SetBool("Grow", false);
        }
    }


}
