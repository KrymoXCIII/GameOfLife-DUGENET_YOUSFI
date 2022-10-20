using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    private int currentFrame;
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
                randomSelector = Random.Range(0, 3);
                tab[i,j] = new Entity(randomSelector);
                
                      
                   
                
            }
        }

        currentFrame = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentFrame++;

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    // Type d'entité
                    switch (tab[i, j].entityType)
                    {
                        case 0: // Grass
                            
                            // Reproduction 
                            if (tab[i, j].isAlive)
                            {
                                List<Entity> entityListSheep = new List<Entity>();
                                List<Entity> entityListWolf = new List<Entity>();
                                // Verif cases 
                                for (int x = -1; x < 2; x++)
                                {
                                    for (int y = -1; y < 2; y++)
                                    {
                                        if (x != 0 || y != 0)
                                        {
                                            if (x + i < tab.GetLength(0) && x + i > 0 && y + j < tab.GetLength(1) && y + j > 0)
                                            {
                                                if (tab[x + i,y + j].entityType == 2)
                                                {
                                                    entityListSheep.Add(tab[x + i,y + j]);
                                                    
                                                }

                                                if (tab[x + i, y + j].entityType == 1)
                                                {
                                                    entityListWolf.Add(tab[x + i,y + j]);
                                                }
                                            }
                                        }
                                    }
                                }

                                if (entityListSheep.Count >= 2)
                                {
                                    tempTab[i, j] = new Entity(2);
                                    break;

                                }

                                if (entityListWolf.Count >= 2)
                                {
                                    tempTab[i, j] = new Entity(1);
                                    break;
                                }

                            }
                            // Mort
                            if (tab[i, j].isAlive == false)
                            {
                                tempTab[i, j] = new Entity(tab[i, j]);
                                tempTab[i, j].timeBeforeRespawn--;

                                if (tempTab[i, j].timeBeforeRespawn == 0)
                                {
                                    tempTab[i, j].isAlive = true;
                                    tempTab[i, j].timeBeforeRespawn = 5;
                                }



                            }
                            // Mangé ?
                            if (tab[i, j].isAlive)
                            {
                                List<Entity> entityListG = new List<Entity>();
                                // Verif cases 
                                for (int x = -1; x < 2; x++)
                                {
                                    for (int y = -1; y < 2; y++)
                                    {
                                        if (x != 0 || y != 0)
                                        {
                                            if (x + i < tab.GetLength(0) && x + i > 0 && y + j < tab.GetLength(1) && y + j > 0)
                                            {
                                                if (tab[x + i,y + j].entityType == 2)
                                                {
                                                    entityListG.Add(tab[x + i,y + j]);
                                                    
                                                }
                                            }
                                        }
                                    }
                                }

                                if (entityListG.Count != 0)
                                {
                                    Entity eater = entityListG[Random.Range(0, entityListG.Count)];
                                    
                                    eater.timeBeforeStarving = 5;

                                    tab[i, j].timeBeforeRespawn = 5;
                                    tab[i, j].isAlive = false;
                                }

                            }
                            break;
                        
                        case 1 : // Wolf
                            List<Entity> entityListW = new List<Entity>();
                            



                            break;
                    }

                }
            }






            tab = tempTab;
        }




    }

    
}

class Entity
{

    public Entity(int entityType)
    {
        isAlive = true;
        this.entityType = entityType;
    }
    
    public Entity(Entity entity)
    { 
        isAlive = entity.isAlive ;
        entityType = entity.entityType; // 0 : Grass ; 1 : Wolf ; 2 : Sheep
        timeBeforeStarving = entity.timeBeforeStarving;
        lifeDuration = entity.lifeDuration;
       
        timeBeforeRespawn = entity.timeBeforeRespawn;
    
    }
    public bool isAlive;
    public int entityType; // 0 : Grass ; 1 : Wolf ; 2 : Sheep
    public int timeBeforeStarving = 5;
    public int lifeDuration = 20;
    
    public int timeBeforeRespawn = 5;
    
    

}



