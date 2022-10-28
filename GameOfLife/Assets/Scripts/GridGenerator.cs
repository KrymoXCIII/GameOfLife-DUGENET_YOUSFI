using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    public Vector3 gridOrigin = Vector3.zero;
    public float gridSpacingOffset = 1f;

    public GameObject GRASS, SHEEP, WOLF;

    private int currentFrame;

    private Entity[,] tab = new Entity[20, 20];

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
                tab[i, j] = new Entity(randomSelector);




            }
        }

        currentFrame = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Suppression précédent
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("GRID"))
            {
                Destroy(o);
            }

            currentFrame++;
            // Parcours du tableau
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
                                            if (x + i < tab.GetLength(0) && x + i > 0 && y + j < tab.GetLength(1) &&
                                                y + j > 0)
                                            {
                                                if (tab[x + i, y + j].entityType == 2)
                                                {
                                                    entityListSheep.Add(tab[x + i, y + j]);

                                                }

                                                if (tab[x + i, y + j].entityType == 1)
                                                {
                                                    for (int x2 = -1; x2 < 2; x2++)
                                                    {
                                                        for (int y2 = -1; y2 < 2; y2++)
                                                        {
                                                            if (x2 != 0 || y2 != 0)
                                                            {
                                                                if (x2 + x + i < tab.GetLength(0) && x2 + x + i > 0 &&
                                                                    y2 + y + j < tab.GetLength(1) && y2 + y + j > 0)
                                                                {

                                                                    if (tab[x2 + x + i, y2 + y + j].entityType == 2 &&
                                                                        tab[x2 + x + i, y2 + y + j].isAlive == true)
                                                                    {
                                                                        break;


                                                                    }



                                                                }
                                                            }
                                                        }
                                                    }

                                                    entityListWolf.Add(tab[x + i, y + j]);
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
                                            if (x + i < tab.GetLength(0) && x + i > 0 && y + j < tab.GetLength(1) &&
                                                y + j > 0)
                                            {
                                                if (tab[x + i, y + j].entityType == 2)
                                                {
                                                    entityListG.Add(tab[x + i, y + j]);

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

                        case 1: // Wolf

                            if (tab[i, j].isAlive)
                            {
                                List<Entity> entityListW = new List<Entity>();
                                if (tab[i, j].timeBeforeStarving <= 0 || tab[i, j].lifeDuration <= 0)
                                {
                                    tab[i, j].isAlive = false;

                                }

                                for (int x = -1; x < 2; x++)
                                {
                                    for (int y = -1; y < 2; y++)
                                    {
                                        if (x != 0 || y != 0)
                                        {
                                            if (x + i < tab.GetLength(0) && x + i > 0 && y + j < tab.GetLength(1) &&
                                                y + j > 0)
                                            {
                                                if (tab[x + i, y + j].entityType == 2 &&
                                                    tab[x + i, y + j].isAlive == true)
                                                {
                                                    entityListW.Add(tab[x + i, y + j]);

                                                }
                                            }
                                        }
                                    }
                                }

                                if (entityListW.Count != 0)
                                {
                                    Entity eated = entityListW[Random.Range(0, entityListW.Count)];

                                    eated.isAlive = false;

                                    tab[i, j].timeBeforeStarving = 5;

                                }

                            }
                            else
                            {
                                tab[i, j] = new Entity(0);
                            }

                            break;


                        case 2: // SHEEP
                            if (tab[i, j].isAlive)
                            {
                                if (tab[i, j].lifeDuration <= 0 || tab[i, j].timeBeforeStarving <= 0)
                                {
                                    tab[i, j].isAlive = false;

                                }

                            }
                            else
                            {
                                tab[i, j] = new Entity(0);
                            }

                            break;
                    }

                }
            }
            // tempTab = tab;




            // AFFICHAGE DU JEU 

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    Vector3 spawnPosition = new Vector3(i * gridSpacingOffset, 0, j * gridSpacingOffset) + gridOrigin;
                    GameObject clone;

                    switch (tab[i, j].entityType)
                    {
                        case 0: //GRASS
                            clone = Instantiate(GRASS, spawnPosition, Quaternion.identity);
                            clone.tag = "GRID";
                            break;

                        case 1: //WOLF
                            clone = Instantiate(WOLF, spawnPosition, Quaternion.identity);
                            clone.tag = "GRID";
                            break;

                        case 2: //SHEEP
                            clone = Instantiate(SHEEP, spawnPosition, Quaternion.identity);
                            clone.tag = "GRID";
                            break;
                    }




                }

            }

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



