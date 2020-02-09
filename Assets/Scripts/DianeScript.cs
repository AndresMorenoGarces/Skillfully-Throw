using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianeScript : MonoBehaviour
{
    GameObject dianeObject;

    void RotateDiane()
    {
        dianeObject.transform.Rotate(Vector3.forward, (5 + (GameManager.instance.currentLevel * (GameManager.instance.currentLevel / 2))));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
            GameManager.instance.launchedArrows.SetParent(dianeObject.transform);
    }

    private void Start()
    {
        dianeObject = gameObject;
    }

    private void Update()
    {
        RotateDiane();
    }
}
