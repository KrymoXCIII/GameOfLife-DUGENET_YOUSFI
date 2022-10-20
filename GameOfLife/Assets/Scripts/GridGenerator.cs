using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private Entity[,] tab = new Entity[20,20];

    private Entity[,] tempTab = new Entity[20, 20];

    private int randomSelector; 
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < tab.GetLength(1); j++)
            {
                randomSelector = Random.Range(0, 4);

                switch (randomSelector)
                {
                    case 0 :
                        tab[i,j] = null;
                        break;
                    case 1 :
                        tab[i, j] = new Wolf();
                        break;
                    case 2 :
                        tab[i, j] = new Sheep();
                        break;
                    case 3 :
                        tab[i, j] = new Grass();
                        break;
                } 
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

class Animal : Entity
{
    private int timeBeforeStarving = 5;
    private int lifeDuration = 20;
    private bool isFed;

}
class Wolf : Animal
{
    
}

class Sheep : Animal
{
    
}

class Grass : Entity
{
    
    private int timeBeforeRespawn = Random.Range(1,5);
    
    
}


