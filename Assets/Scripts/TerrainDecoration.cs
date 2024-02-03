using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainDecoration : MonoBehaviour
{
    [SerializeField] private GameObject tree;
    public TreeInstance[] originalTrees;
    private Vector3 worldPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "FeriaArtesanal")
        {
            /*CreateTreeInstance(95f, 0f, 44f, 0.8f, 0.8f);
            CreateTreeInstance(71f, 0f, 44f, 0.8f, 0.8f);*/
            CreateTreeInstance(0.071f, 0, 0.044f, 0.6f, 0.6f, 0);
            CreateTreeInstance(0.094f,0,0.044f, 0.6f, 0.6f, 0);
            CreateTreeInstance(0.096f, 0, 0.117f, 0.5f, 0.5f, 1);
            CreateTreeInstance(0.068f, 0, 0.117f, 0.5f, 0.5f, 1);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        Terrain terrain = Terrain.activeTerrain;
        terrain.terrainData.treeInstances = new TreeInstance[0];
    }

    private void CreateTreeInstance(float x, float y, float z, float widthScale, float heightScale, int prototypeIndex)
    {
        Terrain terrain = Terrain.activeTerrain;
        TreeInstance treeTemp = new TreeInstance();
        treeTemp.position = new Vector3(x, y, z);
        treeTemp.prototypeIndex = prototypeIndex;
        treeTemp.widthScale = widthScale;
        treeTemp.heightScale = heightScale;
        treeTemp.color = Color.white;
        treeTemp.lightmapColor = Color.white;
        terrain.AddTreeInstance(treeTemp);
        terrain.Flush();
    }
}
