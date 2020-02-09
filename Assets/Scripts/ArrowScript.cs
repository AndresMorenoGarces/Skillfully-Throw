using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    Rigidbody2D arrowRigibody;
    GameObject oldArrow;
    GameObject cloneArrow;
    GameObject currentArrow;

    bool movingArrow;
    bool isFailure_;

    int currentLevel_;
    int arrowLimits_;
    Transform launchedArrows_;
    Transform[] arrowArray_;

    Transform arrowPosition_;

    void ThrowObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isFailure_ == false)
        {
            arrowRigibody.constraints = RigidbodyConstraints2D.None;
            movingArrow = true;
        }
        if (movingArrow)
            arrowRigibody.velocity = Vector3.up * 35;
        else
            arrowRigibody.velocity = Vector3.zero;
    }

    void InstantiateOtherArrow()
    {
        oldArrow = currentArrow;
        oldArrow.transform.SetParent(launchedArrows_);
        arrowRigibody.constraints = RigidbodyConstraints2D.FreezeAll;

        if (launchedArrows_.childCount < arrowLimits_)
            ClonePrefab();
        currentArrow = cloneArrow;
        DestroyOldArrow();

        void ClonePrefab()
        {
            cloneArrow = Instantiate(arrowArray_[currentLevel_], arrowPosition_.position, Quaternion.Euler(Vector3.zero)).gameObject;
            cloneArrow.transform.SetParent(GameManager.instance.levels[currentLevel_]);
            cloneArrow.name = arrowArray_[currentLevel_].name;
        }

        void DestroyOldArrow()
        {
            Destroy(oldArrow.transform.GetComponent<Rigidbody2D>());
            Destroy(oldArrow.transform.GetComponent<ArrowScript>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movingArrow = false;
        if (collision.transform.tag == "Diane" && isFailure_ == false)
            InstantiateOtherArrow();
        else if (collision.transform.tag == "Arrow")
            GameManager.instance.isFailure = true;
    }

    void Start()
    {
        currentArrow = gameObject;
        arrowRigibody = GetComponent<Rigidbody2D>();
        launchedArrows_ = GameManager.instance.launchedArrows;
        arrowLimits_ = GameManager.instance.arrowLimit;
        arrowArray_ = GameManager.instance.arrowArray;
        arrowPosition_ = GameManager.instance.arrowPosition;
    }
    
    void Update()
    {
        ThrowObject();
        arrowRigibody = currentArrow.GetComponent<Rigidbody2D>();
        currentLevel_ = GameManager.instance.currentLevel;
        isFailure_ = GameManager.instance.isFailure;
    }
}
