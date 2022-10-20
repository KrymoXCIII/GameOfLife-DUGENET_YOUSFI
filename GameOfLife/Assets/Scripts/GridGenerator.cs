using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private Entity[,] tab = new Entity[20,20];

    private Entity[,] tempTab = new Entity[20, 20];
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < tab.GetLength(1); j++)
            {
               // tab[i][j] =  
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class Entity
{
    private bool isAlive;
    
}

class Wolf
{
    
}

class Sheep
{
    
}

class Grass
{
    private bool isAlive;
    private int timeBeforeRespawn = Random.Range(1,5);
    
    
}


