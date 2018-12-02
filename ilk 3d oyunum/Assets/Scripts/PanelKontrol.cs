using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelKontrol : MonoBehaviour {

    float speed = 40;
    public Text heightText;
    Vector3 fitVeUpAradakiMesafe;
    GameObject fitPoint;
    GameObject upPoint;

    void Start()
    {
        fitPoint = GameObject.FindGameObjectWithTag("fitPointTag");
        upPoint = GameObject.FindGameObjectWithTag("upPointTag");

        fitVeUpAradakiMesafe = fitPoint.transform.position - upPoint.transform.position;
    }

    void Update ()
    {
        heightText.text = transform.localScale.y + " M";
        panelHareket();
    }

    void LateUpdate()
    {
        fitPoint.transform.position = upPoint.transform.position + fitVeUpAradakiMesafe; //fitpoint ile uppoint arasındaki mesafe
    }

    void panelHareket()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0); //sağa veya sola döndür
    }

    void OnCollisionEnter(Collision col) //panele carpan sekillerin hangi acidan carptigini saptama
    {
        if (col.gameObject.tag.Equals("squareTag"))
        {
            Vector3 hit = col.contacts[0].normal;
            //Debug.Log(hit);
            float angle = Vector3.Angle(hit, Vector3.up);

            if (Mathf.Approximately(angle, 0))
            {
                //Down
                //Debug.Log("Down");
            }
            if (Mathf.Approximately(angle, 180))
            {
                //Up
                //Debug.Log("Up");
            }
            if (Mathf.Approximately(angle, 90))
            {
                // Sides
                Vector3 cross = Vector3.Cross(Vector3.forward, hit);
                if (cross.y > 0)
                { // left side of the player
                    //Debug.Log("Left");
                }
                else
                { // right side of the player
                    //Debug.Log("Right");
                }
            }
        }
    }
}
