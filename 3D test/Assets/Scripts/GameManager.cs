using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class GameManager : MonoBehaviour
{
    int sum;
    bool pause = false, finish = false;
    float x, z;
    //public Text Toto;
    private List<GameObject> Apple;
    public GameObject snake1, snake2, apple;
    // Use this for initialization
    void Start()
    {
        sum = 0;

        Apple = new List<GameObject>();
        //Apple.Add(Instantiate(apple) as GameObject);
        //Toto.gameObject.SetActive(false);
        //Apple[0].SetActive(false);

        snake1.transform.position = new Vector3(Random.Range(-1, 1), 0.05f, Random.Range(-1, 1));
        //do
        //{
        //    snake2.transform.position = new Vector3(Random.Range(-1, 1), 0.05f, Random.Range(-1, 1));
        //} while (snake1.transform.position == snake2.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        sum = snake1.GetComponent<PlayerMovement>().count;// + snake2.GetComponent<Snake>().count;
        if ((int)Mathf.Sqrt(sum) + 1 > Apple.Count)
            Apple.Add(Instantiate(apple) as GameObject);
        if (Input.GetKey(KeyCode.R))
        {
            //Toto.gameObject.SetActive(true);
            //Toto.text = "Reseting...";
            SceneManager.LoadScene("Game");

        }
        if (Input.GetKeyUp(KeyCode.P) && !finish)
        {
            //Toto.gameObject.SetActive(!Toto.gameObject.activeSelf);
            //Toto.text = "Paused";
            snake1.GetComponent<Snake>().enabled = !snake1.GetComponent<Snake>().enabled;
            snake2.GetComponent<Snake>().enabled = !snake2.GetComponent<Snake>().enabled;
            pause = !pause;

        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        for (int i = 0; i < Apple.Count; i++)
        {
            if (!Apple[i].gameObject.activeSelf)
            {
                x = Random.Range(-1.9f, 1.9f);
                z = Random.Range(-1.9f, 1.9f);
                while (inPos(x, z))// || inApplePos(Apple[i]))
                {
                    x = Random.Range(-1.9f, 1.9f);
                    z = Random.Range(-1.9f, 1.9f);
                }

                Apple[i].SetActive(true);
                Apple[i].transform.position = new Vector3(x, 0.06f, z);
            }
        }
        bool SM1 = snake1.GetComponent<PlayerMovement>().enabled;//, SM2 = snake2.GetComponent<Snake>().enabled;

        if (!SM1)// && !SM2 )//&& !Toto.gameObject.activeSelf)
        {
            finish = true;
            //Toto.text = "DRAWWWWWW!!!";
            //Toto.gameObject.SetActive(true);
        }
        if (SM1)// && !SM2 && !pause)
        {
            finish = true;
            snake1.GetComponent<PlayerMovement>().enabled = false;
            //Toto.gameObject.SetActive(true);
            //Toto.text = snake1.GetComponent<Renderer>().material.name.Split(' ')[0] + " Snake Won!";
            //Toto.text.Replace("(Instance)", "");
        }
        if (!SM1)// && SM2 && !pause)
        {
            finish = true;
            //***snake2.GetComponent<PlayerMovement>().enabled = false;
            //Toto.gameObject.SetActive(true);
            //Toto.text = snake2.GetComponent<Renderer>().material.name.Split(' ')[0] + " Snake Won!";

        }
    }

    public bool inPos(float x, float z)
    {
        if (snake1.activeSelf && snake1.transform.position.x == x && snake1.transform.position.z == z)
            return true;


        //if (snake2.activeSelf && snake2.transform.position.x == x && snake2.transform.position.z == z)
        //    return true;
        foreach (GameObject ob in GameObject.FindGameObjectsWithTag("Body1"))
        {
            if (ob.transform.position.x == x && ob.transform.position.z == z)
                return true;
        }
        //foreach (GameObject ob in GameObject.FindGameObjectsWithTag("Body2"))
        //{
        //    if (ob.transform.position.x == x && ob.transform.position.z == z)
        //        return true;
        //}

        return false;
    }

    /*private bool inApplePos(GameObject ap)
    {
        for (int i = 0; i < Apple.Count; i++)
        {
            if (Apple[i] != ap && Apple[i].transform.position.x == x && Apple[i].transform.position.z == z)
                return true;
        }
        return false;
    }*/
}
