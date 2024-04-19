using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public GameObject[] cubePrefabs1;
    public GameObject[] cubePrefabs2;
    public GameObject targetObject; // Target objeyi buraya ata
    public int mazeWidth = 10;
    public int mazeHeight = 10;
    public int mazeDepth = 10;

    public int removeRandomObjects = 10;

    private GameObject[] selectedCubePrefabs;
    private int[,,] maze;
    private List<GameObject> mazeObjects = new List<GameObject>(); // Tüm objeleri tutmak için bir dizi

    void Start()
    {
        // Rastgele bir dizi seç
        if (Random.Range(0, 2) == 0)
        {
            selectedCubePrefabs = cubePrefabs1;
        }
        else
        {
            selectedCubePrefabs = cubePrefabs2;
        }

        maze = new int[mazeWidth, mazeHeight, mazeDepth];
        GenerateMaze();
        BuildMaze();

        GetObjectCount();

        // Sahneye eklenen objelerden 5 tanesini rastgele sil
        RemoveRandomObjects(removeRandomObjects);
    }

    void GenerateMaze()
    {
        // Use a (modified) recursive backtracker algorithm for maze generation
        Stack<Vector3Int> cellStack = new Stack<Vector3Int>();
        Vector3Int currentCell = new Vector3Int(Random.Range(0, mazeWidth), Random.Range(0, mazeHeight), Random.Range(0, mazeDepth));

        while (true)
        {
            maze[currentCell.x, currentCell.y, currentCell.z] = 1;  // Mark as visited
            List<Vector3Int> unvisitedNeighbors = GetUnvisitedNeighbors(currentCell);

            if (unvisitedNeighbors.Count > 0)
            {
                Vector3Int nextCell = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
                RemoveWallBetween(currentCell, nextCell);
                cellStack.Push(currentCell);
                currentCell = nextCell;
            }
            else if (cellStack.Count > 0)
            {
                currentCell = cellStack.Pop();
            }
            else
            {
                break; // Maze is complete
            }
        }
    }

    List<Vector3Int> GetUnvisitedNeighbors(Vector3Int cell)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        // Check all potential neighbors in each direction
        if (cell.x > 0 && maze[cell.x - 1, cell.y, cell.z] == 0) neighbors.Add(new Vector3Int(cell.x - 1, cell.y, cell.z));
        if (cell.x < mazeWidth - 1 && maze[cell.x + 1, cell.y, cell.z] == 0) neighbors.Add(new Vector3Int(cell.x + 1, cell.y, cell.z));
        if (cell.y > 0 && maze[cell.x, cell.y - 1, cell.z] == 0) neighbors.Add(new Vector3Int(cell.x, cell.y - 1, cell.z));
        if (cell.y < mazeHeight - 1 && maze[cell.x, cell.y + 1, cell.z] == 0) neighbors.Add(new Vector3Int(cell.x, cell.y + 1, cell.z));
        if (cell.z > 0 && maze[cell.x, cell.y, cell.z - 1] == 0) neighbors.Add(new Vector3Int(cell.x, cell.y, cell.z - 1));
        if (cell.z < mazeDepth - 1 && maze[cell.x, cell.y, cell.z + 1] == 0) neighbors.Add(new Vector3Int(cell.x, cell.y, cell.z + 1));

        return neighbors;
    }

    void RemoveWallBetween(Vector3Int cell1, Vector3Int cell2)
    {
        // (Assume walls exist between all neighbor cells)
        // You'll need to adjust based on your wall placement logic
    }

    void BuildMaze()
    {
        GameObject target = Instantiate(targetObject, Vector3.zero, Quaternion.identity);

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                for (int z = 0; z < mazeDepth; z++)
                {
                    if (maze[x, y, z] == 1)
                    {
                        // Rastgele bir küp prefabı seçmek için:
                        int randomIndex = Random.Range(0, selectedCubePrefabs.Length);
                        GameObject cube = Instantiate(selectedCubePrefabs[randomIndex], new Vector3(x, y, z), Quaternion.identity);
                        cube.transform.parent = target.transform;
                        mazeObjects.Add(cube); // Oluşturulan her objeyi listeye ekle
                    }
                }
            }
        }
    }

    public int GetObjectCount()
    {
        // Dizinin eleman sayısını Debug.Log ile yazdırma
        Debug.Log("Maze Object Count: " + mazeObjects.Count);
        return mazeObjects.Count;
    }

    public void RemoveRandomObjects(int count)
    {
        // Silinecek obje sayısını kontrol et
        if (count <= 0)
        {
            Debug.LogWarning("Count should be greater than 0.");
            return;
        }

        // Silinecek obje sayısı, mevcut obje sayısından fazla olamaz
        if (count > mazeObjects.Count)
        {
            Debug.LogWarning("Count should be less than or equal to the total number of objects.");
            return;
        }

        // Belirtilen sayıda rastgele objeyi sil
        for (int i = 0; i < count; i++)
        {
            // Silinecek objeyi rastgele seç
            int randomIndex = Random.Range(0, mazeObjects.Count);
            GameObject objectToRemove = mazeObjects[randomIndex];

            // Seçilen objeyi listeden ve sahneden kaldır
            mazeObjects.RemoveAt(randomIndex);
            Destroy(objectToRemove);
        }

        Debug.Log(count + " random objects removed.");
    }

    // Ek olarak, bu yöntemi çağırarak tüm objeleri elde edebilirsiniz
    public GameObject[] GetAllObjectsInMaze()
    {
        return mazeObjects.ToArray();
    }
}
