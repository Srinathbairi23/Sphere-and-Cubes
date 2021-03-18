using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cubePrefab;
    [SerializeField] float spawnradius;
    [SerializeField] float timeLimit;
    public bool freezetimer;
    public bool gameEnd;
    public int timeleft;
    [SerializeField] Text Timer;
    [SerializeField] [Range(0, 0.4f)] float checkCollisoionRadius;

    void Start()
    {
        StartCoroutine(SpawnCube());
    }

    private void Update()
    {
        //timer 
        if (timeLimit > 0 && !freezetimer)
        {
            timeLimit -= Time.deltaTime;
            timeleft = (int)timeLimit;
            Timer.text = "Timer : " + timeleft.ToString();
        }
        else if(timeLimit<0)
        {
            gameEnd = true;
        }
    }

    IEnumerator SpawnCube()
    {
        int iteration=0;
        for(int i = 0; i<10;i++)
        {
            //random position
            Vector3 cubespos = new Vector3(Random.Range(-spawnradius, spawnradius), 0.5f, Random.Range(-spawnradius, spawnradius));
            if (!Physics.CheckSphere(cubespos, checkCollisoionRadius))
            {
                //condition to not spawn cube on the same position.
                GameObject cubes = Instantiate(cubePrefab, cubespos, Quaternion.identity,this.transform);
                yield return new WaitForSeconds(0);
                continue;
            }

            //to run the loop once more if the cube is colliding with the other cube.
            i--;

            //to limit the loop from running infinite times.
            iteration++;

            //stopping after 20 iterations.
            //loop will run maximum of 20 times independent of number of cubes spawned.
            if (iteration > 40)
            {
                yield break;
            }
        }
    }
}
