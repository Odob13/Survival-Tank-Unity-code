using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Instance in the canvas a prefab indicator offscreen that points to each remaining enemy, which indicates the location of this and is seen on the sides of the camera, this will rotate to the enemy position
public class RemainingEnemysGui : MonoBehaviour
{
    public Canvas canvas;
    public Camera camera;
    public GameObject[] enemies;
    public GameObject[] pointers;
    public GameObject pointerPrefab;
    public bool wavecount = false;
    public GameObject player;

    private Pointers pointer;

    int i= 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(wavecount);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        pointers = new GameObject[enemies.Length];
        if(wavecount)
        {
            i= 0;
            foreach (GameObject enemy in enemies)
            {
                if (pointers[i] == null)
                {
                    pointers[i] = Instantiate(pointerPrefab, player.transform.position, Quaternion.identity, camera.transform);
                    pointers[i].GetComponent<Pointers>().target = enemy;
                }
                i++;
            }
            wavecount = false;
        }
        else
        {
            pointers = GameObject.FindGameObjectsWithTag("Pointer");
            //set active the pointer[0] only
            if (pointers.Length > 0)
            {
                pointers[0].GetComponent<SpriteRenderer>().enabled = true;
                for (int i = 1; i < pointers.Length; i++)
                {
                    pointers[i].GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            
        }

    }


    private void FixedUpdate() {
        //pointers to the nearest position of their target with out left the screen

    }
}
