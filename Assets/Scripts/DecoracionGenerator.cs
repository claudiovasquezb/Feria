using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoracionGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] christmasObjects = new GameObject[6];
    [SerializeField] GameObject[] halloweenObjects = new GameObject[11];
    [SerializeField] GameObject[] defaultObjects = new GameObject[1];
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] halloweenInstance = new GameObject[50];
        GameObject[] christmasInstance = new GameObject[50];
        GameObject[] defaultInstance = new GameObject[50];
        // Obtener numero random, si sale 0, decoraremos con tematica navidad
        // si sale 1 decoraremos con tematica halloween
        // si sale 2 decoraremos por defecto
        int n = Random.Range(0,3);
        
        if (n == 0)
        {

            // Decoracion inicio izquierda
            christmasInstance[0] = Instantiate(christmasObjects[4], new Vector3(66.79f, 0, 42), Quaternion.Euler(-90,-90,0));
            christmasInstance[0].transform.localScale = new Vector3(10, 10, 10);
            // Decoracion inicio derecha
            christmasInstance[1] = Instantiate(christmasObjects[4], new Vector3(98.3f, 0, 42), Quaternion.Euler(-90,90,0));
            christmasInstance[1].transform.localScale = new Vector3(10, 10, 10);
            // Decoracion inicio centro
            christmasInstance[2] = Instantiate(christmasObjects[5], new Vector3(96.7f, -4, 40.5f), Quaternion.identity);
            christmasInstance[2].transform.localScale = new Vector3(9.5f, 10, 10);
            christmasInstance[3] = Instantiate(christmasObjects[1], new Vector3(91.2f, 3, 40.5f), Quaternion.Euler(-90,0,0));
            christmasInstance[3].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            christmasInstance[4] = Instantiate(christmasObjects[1], new Vector3(75.5f, 3, 40.5f), Quaternion.Euler(-90,0,0));
            christmasInstance[4].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            christmasInstance[5] = Instantiate(christmasObjects[3], new Vector3(82.39f, 0, 42.85f), Quaternion.Euler(-90,0,0));
            christmasInstance[5].transform.localScale = new Vector3(300, 300, 300);
           // christmasInstance[6] = Instantiate(christmasObjects[0], new Vector3(80.7f, 0, 43.5f), Quaternion.Euler(-90,190,0));
            //christmasInstance[6].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // Decoracion largo de la feria (en las paredes)
            // Pared izquierda
            christmasInstance[7] = Instantiate(christmasObjects[5], new Vector3(65.3f, -4, 47), Quaternion.Euler(0,90,0));
            christmasInstance[7].transform.localScale = new Vector3(22, 10, 10);
            // Pared derecha
            christmasInstance[8] = Instantiate(christmasObjects[5], new Vector3(99.8f, -4, 47), Quaternion.Euler(0,90,0));
            christmasInstance[8].transform.localScale = new Vector3(22, 10, 10);

        } else if (n == 1) {
            float zParedIzquierda = 50.3f;
            float zParedDerecha = 50.3f;
            // Decoracion inicio izquierda
            halloweenInstance[0] = Instantiate(halloweenObjects[8], new Vector3(66, 0, 41), Quaternion.Euler(-90,30,0));
            halloweenInstance[0].transform.localScale = new Vector3(2, 2, 2);
            halloweenInstance[1] = Instantiate(halloweenObjects[6], new Vector3(68, 0, 42), Quaternion.Euler(-90,30,0));
            halloweenInstance[1].transform.localScale = new Vector3(2, 2, 2);
            halloweenInstance[2] = Instantiate(halloweenObjects[6], new Vector3(66, 0, 43), Quaternion.Euler(-90,30,0));
            halloweenInstance[2].transform.localScale = new Vector3(2, 2, 2);

            // Decoracion inicio derecha
            halloweenInstance[3] = Instantiate(halloweenObjects[8], new Vector3(98.5f, 0, 41), Quaternion.Euler(-90,-30,0));
            halloweenInstance[3].transform.localScale = new Vector3(2, 2, 2);
            halloweenInstance[4] = Instantiate(halloweenObjects[6], new Vector3(98.5f, 0, 43), Quaternion.Euler(-90,-30,0));
            halloweenInstance[4].transform.localScale = new Vector3(2, 2, 2);
            halloweenInstance[5] = Instantiate(halloweenObjects[6], new Vector3(96.5f, 0, 42), Quaternion.Euler(-90,-30,0));
            halloweenInstance[5].transform.localScale = new Vector3(2, 2, 2);

            // Decoracion inicio centro
            halloweenInstance[6] = Instantiate(halloweenObjects[0], new Vector3(84.6f, 0, 42), Quaternion.Euler(-90,0,0));
            halloweenInstance[6].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            halloweenInstance[7] = Instantiate(halloweenObjects[1], new Vector3(83, 0, 42), Quaternion.Euler(-90,0,0));
            halloweenInstance[7].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            halloweenInstance[8] = Instantiate(halloweenObjects[5], new Vector3(86.37f, 0, 42.5f), Quaternion.Euler(-90,0,0));
            halloweenInstance[8].transform.localScale = new Vector3(1, 1, 1);
            halloweenInstance[9] = Instantiate(halloweenObjects[5], new Vector3(80.7f, 0, 41.16f), Quaternion.Euler(-90,0,0));
            halloweenInstance[9].transform.localScale = new Vector3(1, 1, 1);
            halloweenInstance[10] = Instantiate(halloweenObjects[5], new Vector3(81.2f, 0, 42.7f), Quaternion.Euler(-90,0,0));
            halloweenInstance[10].transform.localScale = new Vector3(1, 1, 1);
            halloweenInstance[11] = Instantiate(halloweenObjects[2], new Vector3(83.47f, 0, 43.7f), Quaternion.Euler(-90,0,0));
            halloweenInstance[11].transform.localScale = new Vector3(5, 3, 3);
            halloweenInstance[12] = Instantiate(halloweenObjects[2], new Vector3(87, 0, 42), Quaternion.Euler(-90,90,0));
            halloweenInstance[12].transform.localScale = new Vector3(2.5f, 3, 3);
            halloweenInstance[13] = Instantiate(halloweenObjects[2], new Vector3(80, 0, 42), Quaternion.Euler(-90,90,0));
            halloweenInstance[13].transform.localScale = new Vector3(2.5f, 3, 3);
            halloweenInstance[14] = Instantiate(halloweenObjects[4], new Vector3(81.6f, 0, 41.3f), Quaternion.Euler(-90,-40,0));
            halloweenInstance[14].transform.localScale = new Vector3(2, 2, 2);
            halloweenInstance[15] = Instantiate(halloweenObjects[4], new Vector3(86.2f, 0, 41.3f), Quaternion.Euler(-90,-125,0));
            halloweenInstance[15].transform.localScale = new Vector3(2, 2, 2);
            halloweenInstance[16] = Instantiate(halloweenObjects[10], new Vector3(84.8f, 1, 43.9f), Quaternion.Euler(-90,0,0));
            halloweenInstance[16].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            halloweenInstance[17] = Instantiate(halloweenObjects[10], new Vector3(82.1f, 1, 43.9f), Quaternion.Euler(-90,0,0));
            halloweenInstance[17].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            // Decoracion largo de la feria (en las paredes)
            // Pared izquierda
            for (int i = 0; i < 8; i++)
            {
                halloweenInstance[18 + i] = Instantiate(halloweenObjects[10], new Vector3(65.3f, 6.3f, zParedIzquierda), Quaternion.Euler(-90,0,0));
                halloweenInstance[18 + i].transform.localScale = new Vector3(3, 3, 3);
                zParedIzquierda +=9;
            }
            // Pared derecha
            for (int i = 0; i < 8; i++)
            {
                halloweenInstance[26 + i] = Instantiate(halloweenObjects[10], new Vector3(99.5f, 6.3f, zParedDerecha), Quaternion.Euler(-90,0,0));
                halloweenInstance[26 + i].transform.localScale = new Vector3(3, 3, 3);
                zParedDerecha +=9;
            }
        }else if (n == 2) {
            defaultInstance[0] = Instantiate(defaultObjects[0], new Vector3(70, 0, 44), Quaternion.Euler(0,90,0));
            defaultInstance[0].transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
            defaultInstance[1] = Instantiate(defaultObjects[0], new Vector3(95.6f, 0, 42.64f), Quaternion.Euler(0,90,0));
            defaultInstance[1].transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
