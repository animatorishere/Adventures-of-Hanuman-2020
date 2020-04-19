using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpiderKingMove : MonoBehaviour

{
    public Transform hanuman;
    public float currentSpeed;
    float backMove;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, hanuman.position.x, Time.deltaTime * currentSpeed),
           transform.position.y, transform.position.z);

        if (transform.position.x <= hanuman.position.x)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90f, transform.eulerAngles.z);
        }

        else if (transform.position.x >= hanuman.position.x)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90f, transform.eulerAngles.z);

        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Wall")
    //    {
    //        if (other.gameObject.name == "SpiderKing")
    //        {
    //            backMove = 1f;
    //            //other.gameObject.transform.GetChild(18).gameObject.SetActive(false);
    //            //other.gameObject.transform.GetChild(18).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            backMove = 0.5f;
    //            //other.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    //            //other.gameObject.transform.GetChild(2).gameObject.SetActive(true);

    //        }




    //        if (System.Math.Abs(GamePlayAction.instance.Hanuman.transform.localScale.z + -1f) <= 0)
    //        {
    //            other.gameObject.transform.DOMoveX(other.gameObject.transform.position.x + backMove, 0.5f);
    //        }
    //        else
    //        {
    //            other.gameObject.transform.DOMoveX(other.gameObject.transform.position.x - backMove, 0.5f);
    //        }




    //    }

  //  }
}
