using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    [SerializeField] int initAmount = 2;
    private float plotSize = 100f;
    private float xPosRight = 50f;
    private float xPosLeft = -50f;
    private float lastZPos = -50f;
    private int maxNumOfPlots = 10;

    [SerializeField] List<GameObject> plots;
    private List<GameObject> plotsArray_L = new List<GameObject>();
    private List<GameObject> plotsArray_R = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for ( int i = 0; i < initAmount ; i++)
        {
            PlotInstantiation();
        }
    }

    public void SpawnPlot()
    {
        if (plotsArray_L.Count < maxNumOfPlots)
        {
            PlotInstantiation();
            return;
        }

        GameObject movedPlot_L = plotsArray_L[0];
        plotsArray_L.Remove(movedPlot_L);
        movedPlot_L.transform.position = new Vector3(xPosLeft, 0, lastZPos);
        plotsArray_L.Add(movedPlot_L);
        

        GameObject movedPlot_R = plotsArray_R[0];
        plotsArray_R.Remove(movedPlot_R);
        movedPlot_R.transform.position = new Vector3(xPosRight, 0, lastZPos);
        plotsArray_R.Add(movedPlot_R);

        lastZPos += plotSize;
    }

    private void PlotInstantiation()
    {
        GameObject plotLeft = (GameObject)Instantiate(GetRandomPlot(), new Vector3(xPosLeft, 0, lastZPos), Quaternion.identity);
        plotsArray_L.Add(plotLeft);

        GameObject plotRight = (GameObject)Instantiate(GetRandomPlot(), new Vector3(xPosRight, 0, lastZPos), new Quaternion(0, 180, 0, 0));
        plotsArray_R.Add(plotRight);

        lastZPos += plotSize;
    }

    private GameObject GetRandomPlot()
    {
        return plots[Random.Range(0, plots.Count)];
    }
}
