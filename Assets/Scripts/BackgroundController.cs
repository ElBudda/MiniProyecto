using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCode : MonoBehaviour
{
    private float startingPosition;
    private float length;//posicion inicial del fondo
    public GameObject cam; //objeto publico para seleccionar la camara
    public float parralaxEffect; // velocidada la que se mueve el fondo, relativo a la camara
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // distancia del fondo relativo al movimiento de la camara
        float distance = cam.transform.position.x * parralaxEffect; // mas = menos rapido
        float movement = cam.transform.position.x * (1 - parralaxEffect);

        transform.position = new Vector3(startingPosition + distance, transform.position.y, transform.position.z);


        //si el fondo llega a su limite en x (length) ajusta su posicion
        if (movement > startingPosition + length)
        {
            startingPosition += length;
        }
        else if (movement < startingPosition - length)
        {
            startingPosition -= length;
        }

    }
}
