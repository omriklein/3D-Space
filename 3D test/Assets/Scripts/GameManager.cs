using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class GameManager : MonoBehaviour
{
    int sum;
    bool pause = false, finish = false;
    float x, y, z;
    //public Text Toto;
    private List<GameObject> Apple;
    public GameObject snake1, snake2, apple;
    // Use this for initialization
    void Start()
    {
        sum = 0;

        Apple = new List<GameObject>();
        foreach (GameObject app in GameObject.FindGameObjectsWithTag("Apple"))
        {
            Apple.Add(app);
        }

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
            SceneManager.LoadScene("Scene");

        }
        if (Input.GetKeyUp(KeyCode.P) && !finish)
        {
            //Toto.gameObject.SetActive(!Toto.gameObject.activeSelf);
            //Toto.text = "Paused";
            snake1.GetComponent<Snake>().enabled = !snake1.GetComponent<Snake>().enabled;
            //snake2.GetComponent<Snake>().enabled = !snake2.GetComponent<Snake>().enabled;
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
                x = Random.Range(-150f, 150f);
                y = Random.Range(-150f, 150f);
                z = Random.Range(-150f, 150f);
                while (inPos(x, y, z))// || inApplePos(Apple[i]))
                {
                    x = Random.Range(-150f, 150f);
                    y = Random.Range(-150f, 150f);
                    z = Random.Range(-150f, 150f);
                }

                Apple[i].SetActive(true);
                Apple[i].transform.position = new Vector3(x, y, z);
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

    public bool inPos(float x, float y, float z)
    {
        if (snake1.activeSelf && snake1.transform.position.x == x && snake1.transform.position.z == z && snake1.transform.position.y == y)
            return true;


        //if (snake2.activeSelf && snake2.transform.position.x == x && snake2.transform.position.z == z)
        //    return true;
        foreach (GameObject ob in GameObject.FindGameObjectsWithTag("Body1"))
        {
            if (ob.transform.position.x == x && ob.transform.position.y == y && ob.transform.position.z == z)
                return true;
        }
        //foreach (GameObject ob in GameObject.FindGameObjectsWithTag("Body2"))
        //{
        //    if (ob.transform.position.x == x && ob.transform.position.z == z)
        //        return true;
        //}

        return false;
    }

    private bool inApplePos(GameObject ap)
    {
        for (int i = 0; i < Apple.Count; i++)
        {
            if (Apple[i] != ap && Apple[i].transform.position.x == x && Apple[i].transform.position.z == z)
                return true;
        }
        return false;
    }
}
