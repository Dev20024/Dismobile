
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [Header("Biome Settings")]
    public Biome[] ActiveBiomes;
    Dictionary<Biome, float> weightedBiomes = new Dictionary<Biome, float>();
    [Header("Road Settings")]
    public float roadOffset;
    public int startingPieces = 4;
    public Transform generationStartPoint;
    [Header("Current Road")]
    [SerializeField] List<GameObject> generatedRoadPrefabs = new List<GameObject>();
    [SerializeField] List<Chunk> generatedChunks = new List<Chunk>();
    public Chunk lastLoadedChunk = null;
    [Header("Organization")]
    public Transform roadHolder;
    [Header("Current Biomes")]
    public Biome currentBiome;
    public int biomeTokens;
    
    // singleton Set Up
    public static RoadManager instance;
    void Awake()
    {
        if (instance != this) {
            instance = null;
        }
        instance = this;
    }

    // Set Up Generation Trigger Events
    private void OnEnable() {
        GenerationTrigger.onGenerationEvent += GenerateChunk;
    }

    private void OnDisable() {
        GenerationTrigger.onGenerationEvent -= GenerateChunk;
    }
   
    // Road Initialization
    private void Start() {
        // set up
        WalkingNPCManager.AddWalkingLanes(4);
        addBiomeWeights();
        // starting generation
        SelectBiome();
        GenerateInitialChunk(generationStartPoint.position);
        for (int i = 0; i < startingPieces; i++) {
            GenerateChunk();
        }
    }


    /*
        BIOME SET UP AND GENERATION
    */


    // initializes Biomes and their chances of occuring
    private void addBiomeWeights() {
        foreach (Biome biome in ActiveBiomes) {
            weightedBiomes.Add(biome, biome.rarity);
        }
    }

    // subtracts biome tokens and determines the next biome to be loaded
    private void AdjustBiomeTokens() {
        biomeTokens--;
        if (biomeTokens <= 0) {
            SelectBiome();
        }
    }

    // selects the next biome
    private void SelectBiome() {
        // picking next biome
        float totalWeight = 0;
        foreach (float weight in weightedBiomes.Values) {
            totalWeight += weight;
        }
        
        float randomNum = Random.Range(0f, totalWeight);
        foreach (KeyValuePair<Biome, float> biomeChance in weightedBiomes) {
            if (!(randomNum <= biomeChance.Value)) {continue;}
            if (currentBiome == biomeChance.Key) {continue;}
            currentBiome = biomeChance.Key;
            // assign biome tokens (max variation of 30%)
            biomeTokens = currentBiome.length + Mathf.RoundToInt( Random.Range(-currentBiome.length * .3f, currentBiome.length * .3f));
            return;
        }
        SelectBiome();
    }


    /*
        ROAD CHUNK GENERATION
    */

    
    // Creates a new chunk which is added to the end of the road
    public void GenerateChunk() {
        if (lastLoadedChunk == null) {Debug.LogWarning("No previous loaded chunk to calculate new pos"); return;}
        // calculate new Position
        Vector3 newPos = new Vector3(lastLoadedChunk.position.x, lastLoadedChunk.position.y, lastLoadedChunk.position.z + roadOffset);
        // create new chunk
        Chunk newChunk = new Chunk();
        generatedChunks.Add(newChunk);
        newChunk.position = newPos;
        newChunk.road = LoadRoad(newChunk);
        // handle tokens
        AdjustBiomeTokens();
        // clean up previous chunks
        CleanUpChunks();
        // sets last loaded chunk
        lastLoadedChunk = newChunk;
    } 
    // Creates a new chunk which is added at a custom position
    public void GenerateInitialChunk(Vector3 startingPos) {
        // create new chunk
        Chunk newChunk = new Chunk();
        generatedChunks.Add(newChunk);
        newChunk.position = startingPos;
        newChunk.road = LoadRoad(newChunk);
        // handle tokens
        AdjustBiomeTokens();
        // clean up previous chunks
        CleanUpChunks();
        // sets last loaded chunk
        lastLoadedChunk = newChunk;
    } 

    // Creates the Road and all decorated structures, returns the road prefab
    private GameObject LoadRoad(Chunk newChunk) {
        GameObject roadPrefab = Instantiate(currentBiome.roads[Random.Range(0,currentBiome.roads.Length)], newChunk.position, Quaternion.identity, roadHolder);
        RoadInfo roadInfo = roadPrefab.GetComponent<RoadInfo>();
        // Add Buildings
        loadBuildings(roadInfo.BuildingNodes, roadPrefab);
        // Add walking nodes
        WalkingNPCManager.loadWalkableChunk(roadInfo, newChunk);
        // Add etc...
        Debug.Log("adding whatever the fuck.");
        // add road prefab to logs
        generatedRoadPrefabs.Add(roadPrefab);
        return roadPrefab;
    }

    // Creates buildings between the building nodes of a Road Object
    private void loadBuildings(List<Transform> BuildingNodes, GameObject roadPrefab) {
       // Vector3 currentPos;
        
        // loop through each building node
        foreach (Transform buildingNode  in BuildingNodes){
            // loop through connected building points

            for (int i = 0; i < buildingNode.childCount; i++) {
                // check to make sure not last point
                if (i == buildingNode.childCount - 1) { break;}
                // calculate path
                
                Transform currentPoint = buildingNode.GetChild(i);
                Transform nextPoint = buildingNode.GetChild(i + 1);
                float distance = (nextPoint.position - currentPoint.position).magnitude;
                Vector3 direction = (nextPoint.position - currentPoint.position).normalized;
                float currentDistance = 0;
                
                while (currentDistance < distance) { 
                    // building blueprints
                    Transform  randomTR = currentBiome.buildings[Random.Range(0, currentBiome.buildings.Length)].transform;
                    float heightOffset = randomTR.GetChild(0).transform.localScale.y / 2;
                    float lengthOffset = randomTR.GetChild(0).transform.localScale.z / 2;
                    // create building
                    currentDistance += lengthOffset;
                    GameObject newBuilding = Instantiate(randomTR.gameObject, currentPoint.position, Quaternion.identity, roadPrefab.transform);
                    //adjust location and angle
                    newBuilding.transform.Translate(direction * currentDistance); 
                    newBuilding.transform.rotation = Quaternion.LookRotation(direction);
                    newBuilding.transform.position += newBuilding.transform.up * heightOffset;
                    // check if placement is valid
                    //Debug.DrawRay(new Vector3(newBuilding.transform.position.x, 2.5f, newBuilding.transform.position.z) ,-transform.forward,Color.green, 100);
                    bool detectionOne = Physics.Raycast(new Vector3(newBuilding.transform.position.x, 2.5f, newBuilding.transform.position.z) ,transform.right,3f);
                    bool detectionTwo = Physics.Raycast(new Vector3(newBuilding.transform.position.x, 2.5f, newBuilding.transform.position.z) ,-transform.forward,3f);;
                    if (detectionOne || detectionTwo) { 
                        //if (hit.transform != newBuilding.transform) {
                            Debug.Log(transform.position);
                             Destroy(newBuilding);
                            
                            Debug.Log("destroyed clipped building");
                       // }   
                    }
                    // adjust next building location
                    currentDistance += lengthOffset;
                    
                }
                
            }
        }
    }

    // Cleans up chunks behind the player
    void CleanUpChunks() {
        
        if (generatedChunks.Count > 10) {
            generatedChunks[0].UnLoad();
            generatedChunks[0] = null;
            generatedChunks.Remove(generatedChunks[0]);
        }
        /*
        if (generatedRoadPrefabs.Count > 10) {
            Destroy(generatedRoadPrefabs[0]);
            generatedRoadPrefabs.Remove(generatedRoadPrefabs[0]);
        }
        */
    }

}
