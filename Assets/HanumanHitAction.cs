using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HanumanHitAction : MonoBehaviour
{
    float backMove;
    bool isDestroy = false;
    GameObject hitObject;

    private void OnTriggerEnter(Collider other)
    {
       
        Debug.Log("hitobject " + other.gameObject.name);
        if (other.gameObject.tag == "EnemySpider")
        {
            GetHitObject(other.gameObject);
            IncrementHitCount(other.gameObject);            
            if (other.gameObject.name == "SpiderKing")
            {
                backMove = 1f;
                PlayParticleAndSFX(other.gameObject, GetDestroyTime(other.gameObject), GloabalAudioSournce.instance.spiderDamage);
            }
            else {
                backMove = 0.5f;
                PlayParticleAndSFX(other.gameObject, GetDestroyTime(other.gameObject), GloabalAudioSournce.instance.spiderDamage);
            }            
           
            MoveEnemyPosition(other.gameObject, backMove);

        }
       
        else if (GamePlayAction.instance.hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")
            && other.name == "MushroomDanger" )
        {
            GetHitObject(other.transform.parent.gameObject);
            IncrementHitCount(other.transform.parent.gameObject);
            

            other.gameObject.GetComponent<Animator>().SetTrigger("Damage");
            backMove = 0.2f;

            PlayParticleAndSFX(other.transform.parent.gameObject, GetDestroyTime(other.transform.parent.gameObject), GloabalAudioSournce.instance.mashroomDamage);
            MoveEnemyPosition(other.transform.parent.gameObject, backMove);   
        }

        else if (GamePlayAction.instance.hanumanAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")
           && other.name == "Zombie Plant" )
        {
            GetHitObject(other.transform.parent.gameObject);
            IncrementHitCount(other.transform.parent.gameObject);
            backMove = 0.2f;
            other.gameObject.GetComponent<Animator>().SetTrigger("Damage");        
            PlayParticleAndSFX(other.transform.parent.gameObject, GetDestroyTime(other.transform.parent.gameObject), GloabalAudioSournce.instance.mashroomDamage);
            MoveEnemyPosition(other.transform.parent.gameObject, backMove);

        }


    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject.tag == "EnemySpider")
        {

        }
        else if (other.transform.parent.name == "EnemyMushroom")
        {
            other.gameObject.transform.GetChild(2).gameObject.SetActive(false);

        }

    }

    void MoveEnemyPosition(GameObject go,float backMovePos) {

        if (System.Math.Abs(GamePlayAction.instance.Hanuman.transform.localScale.z + -1f) <= 0)
        {
            go.transform.DOMoveX(go.transform.position.x + backMovePos, 0.5f);
        }
        else
        {
            go.transform.DOMoveX(go.transform.position.x - backMovePos, 0.5f);
        }

    }
    void PlayParticleAndSFX(GameObject go, float destroyTime,AudioClip sfx)
    {

        if (CheckDestroyCondition(go))
        {
            GloabalAudioSournce.instance.sm.clip = sfx;
            GloabalAudioSournce.instance.sm.Play();
            hitObject.GetComponent<EnemyHitCounter>().destroyParticle.SetActive(false);
            hitObject.GetComponent<EnemyHitCounter>().destroyParticle.SetActive(true);
            hitObject.GetComponent<EnemyHitCounter>().destroyParticle.GetComponent<ParticleSystem>().Play();
            Destroy(hitObject, destroyTime);
        }
        else
        {
            GloabalAudioSournce.instance.sm.clip = sfx;
            GloabalAudioSournce.instance.sm.Play();
            hitObject.GetComponent<EnemyHitCounter>().hitParticle.SetActive(false);
            hitObject.GetComponent<EnemyHitCounter>().hitParticle.SetActive(true);
            hitObject.GetComponent<EnemyHitCounter>().hitParticle.GetComponent<ParticleSystem>().Play();

        }

    }

    bool CheckDestroyCondition(GameObject go)
    {

        if (go.gameObject.GetComponent<EnemyHitCounter>().hitCounter >=
             go.gameObject.GetComponent<EnemyHitCounter>().destroyCounter)
        {
            isDestroy = true;
        }
        else {
            isDestroy = false;
        }

        return isDestroy;

    }

    int IncrementHitCount(GameObject go)
    {

        go.GetComponent<EnemyHitCounter>().hitCounter++;

        return go.GetComponent<EnemyHitCounter>().hitCounter;

    }
    float GetDestroyTime(GameObject go)
    {
        return go.GetComponent<EnemyHitCounter>().destroyTime;
    }
    void GetHitObject(GameObject go) {
        hitObject = go;
    }
}
