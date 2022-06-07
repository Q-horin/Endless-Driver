using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    [SerializeField] int initAmount = 4;
    private float plotSize = 100f;
    private float xPosRight = 50f;
    private float xPosLeft = -50f;
    private float lastZPos = -50f;
    int initCount = 0;

    [SerializeField] List<GameObject> plots;

    // Start is called before the first frame update
    void Start()
    {
        for ( int i = 0; i < initAmount ; i++)
        {
            SpawnPlot();
        }
    }

    public void SpawnPlot()
    {
        GameObject plotLeft = plots[Random.Range(0, plots.Count)];
        GameObject plotRight = plots[Random.Range(0, plots.Count)];

        Instantiate(plotLeft, new Vector3(xPosLeft, 0, lastZPos), plotLeft.transform.rotation);
        Instantiate(plotRight, new Vector3(xPosRight, 0, lastZPos), new Quaternion(0, 180, 0, 0));
        lastZPos += plotSize;
    }
}
