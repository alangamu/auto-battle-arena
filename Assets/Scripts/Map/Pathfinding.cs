using System;
using System.Collections.Generic;
using System.Linq;

public class Pathfinding
{
    private int[][] directions = new int[][]
    {
        new int[] { 1, 0 },   // Derecha
        new int[] { 1, -1 },  // Abajo-derecha
        new int[] { 0, -1 },  // Abajo-izquierda
        new int[] { -1, 0 },  // Izquierda
        new int[] { -1, 1 },  // Arriba-izquierda
        new int[] { 0, 1 }    // Arriba-derecha
    };

    private int gridSizeX;
    private int gridSizeY;
    private bool[,] walkableGrid;

    public Pathfinding(int sizeX, int sizeY)
    {
        gridSizeX = sizeX;
        gridSizeY = sizeY;
        walkableGrid = new bool[sizeX, sizeY];

        // Establecer todos los nodos como transitables de forma predeterminada
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                walkableGrid[x, y] = true;
            }
        }
    }

    public void SetWalkable(int x, int y, bool walkable)
    {
        walkableGrid[x, y] = walkable;
    }

    public List<HexNode> FindPath(HexNode startNode, HexNode endNode)
    {
        if (!walkableGrid[startNode.GridX, startNode.GridY] || !walkableGrid[endNode.GridX, endNode.GridY])
        {
            // Si el nodo de inicio o el nodo de destino no son transitables, retorna un camino vacío
            return new List<HexNode>();
        }

        List<HexNode> openSet = new List<HexNode>();
        HashSet<HexNode> closedSet = new HashSet<HexNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            HexNode currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost ||
                    openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach (HexNode neighbor in GetNeighbors(currentNode))
            {
                if (!walkableGrid[neighbor.GridX, neighbor.GridY] || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = newMovementCostToNeighbor;
                    neighbor.HCost = GetDistance(neighbor, endNode);
                    neighbor.Parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                    else
                    {
                        // Si el vecino ya está en openSet, actualizamos sus valores de costo y padre
                        int index = openSet.IndexOf(neighbor);
                        openSet[index] = neighbor;
                    }
                }
            }
        }

        // No se encontró un camino válido
        return new List<HexNode>();
    }

    private List<HexNode> RetracePath(HexNode startNode, HexNode endNode)
    {
        List<HexNode> path = new List<HexNode>();
        HexNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }

    private List<HexNode> GetNeighbors(HexNode node)
    {
        List<HexNode> neighbors = new List<HexNode>();

        foreach (int[] direction in directions)
        {
            int neighborX = node.GridX + direction[0];
            int neighborY = node.GridY + direction[1];

            if (neighborX >= 0 && neighborX < gridSizeX && neighborY >= 0 && neighborY < gridSizeY)
            {
                neighbors.Add(new HexNode(neighborX, neighborY));
            }
        }

        return neighbors;
    }

    private int GetDistance(HexNode nodeA, HexNode nodeB)
    {
        int distanceX = Math.Abs(nodeA.GridX - nodeB.GridX);
        int distanceY = Math.Abs(nodeA.GridY - nodeB.GridY);

        return distanceX + Math.Max(0, (distanceY - distanceX) / 2);
    }
}

public class HexNode
{
    public int GridX { get; }
    public int GridY { get; }
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get { return GCost + HCost; } }
    public HexNode Parent { get; set; }

    public HexNode(int gridX, int gridY)
    {
        GridX = gridX;
        GridY = gridY;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        HexNode otherNode = (HexNode)obj;
        return GridX == otherNode.GridX && GridY == otherNode.GridY;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GridX, GridY);
    }
}

