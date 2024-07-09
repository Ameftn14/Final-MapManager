using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;




public class searchroad : MonoBehaviour
{
    public List<int> shortestPath = new List<int>();

    private float moveSpeed = 10.0f;

    private int currentPathIndex = 0; // 当前路径索引
    private bool isMoving = false;    //when true, start to move
    private Vector3 targetPosition;   // 目标位置
    void Awake()
    {
        Debug.Log("egg awaked");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("egg started");
        Dictionary<int, Dictionary<int, float>> graph = new Dictionary<int, Dictionary<int, float>>
        {
            {1, new Dictionary<int, float>{{2, 6.0f}, {3, 3.0f}}},
            {2, new Dictionary<int, float>{{1, 6.0f}, {3, 2.0f}, {4, 5.0f}}},
            {3, new Dictionary<int, float>{{1, 3.0f}, {2, 2.0f}, {4, 3.0f}, {5, 4.0f}}},
            {4, new Dictionary<int, float>{{2, 5.0f}, {3, 3.0f}, {5, 2.0f}, {6, 3.0f}}},
            {5, new Dictionary<int, float>{{3, 4.0f}, {4, 2.0f}, {6, 5.0f}}},
            {6, new Dictionary<int, float>{{4, 3.0f}, {5, 5.0f}}}
        };


        //Debug.Assert(graph.ContainsKey(val1));
        DijkstraAlgorithm algo = new DijkstraAlgorithm(graph);
        shortestPath = algo.ShortestPath(1, 4);
        Debug.Log($"Shortest path from 1 to 4 is: {string.Join(" -> ", shortestPath)}");

        // 初始化目标位置为第一个路径点的位置
        if (shortestPath.Any())
            targetPosition = GameObject.Find("Vertices" + shortestPath[currentPathIndex]).transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // 检测按键输入
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S pressed");
            // 开始移动
            if (!isMoving)
            {
                isMoving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C pressed");
            // 停止移动
            isMoving = false;
        }

        if (isMoving)
        {
            // 如果还没有到达目标位置
            if ((gameObject.transform.position - targetPosition).sqrMagnitude > 0.1f)
            {
                // 向目标位置移动
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                // 到达目标位置后更新路径索引和目标位置
                currentPathIndex++;
                if (currentPathIndex < shortestPath.Count)
                {
                    targetPosition = GameObject.Find("Vertices" + shortestPath[currentPathIndex]).transform.position;
                }
                else
                {
                    // 到达路径末尾后停止移动
                    isMoving = false;
                }
            }
        }
    }

    public class SimplePriorityQueue<T, TPrior> where TPrior : IComparable<TPrior>
    {
        private List<T> items = new List<T>();
        private List<TPrior> priorities = new List<TPrior>();

        public void Enqueue(T item, TPrior priority)
        {
            items.Add(item);
            priorities.Add(priority);
        }

        public T Dequeue()
        {
            int index = 0;
            for (int i = 1; i < priorities.Count; i++)
            {
                if (priorities[i].CompareTo(priorities[index]) < 0)
                {
                    index = i;
                }
            }

            T result = items[index];
            items.RemoveAt(index);
            priorities.RemoveAt(index);

            return result;
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }
    }

    public class DijkstraAlgorithm
    {
        Dictionary<int, Dictionary<int, float>> graph;
        Dictionary<int, float> distances;
        Dictionary<int, int> predecessors;
        Dictionary<int, bool> visited;

        public DijkstraAlgorithm(Dictionary<int, Dictionary<int, float>> graph)
        {
            this.graph = graph;
            distances = new Dictionary<int, float>();
            predecessors = new Dictionary<int, int>();
            visited = new Dictionary<int, bool>();
        }

        public List<int> ShortestPath(int start, int end)
        {
            foreach (var vertex in graph.Keys)
            {
                distances[vertex] = float.MaxValue;
                visited[vertex] = false;
                predecessors[vertex] = -1; // -1 表示没有前驱
            }

            distances[start] = 0;

            SimplePriorityQueue<int, float> pq = new SimplePriorityQueue<int, float>();
            pq.Enqueue(start, 0);

            while (!pq.IsEmpty())
            {
                int current = pq.Dequeue();

                if (visited[current])
                    continue;
                visited[current] = true;

                foreach (var neighbor in graph[current])
                {
                    int neighborVertex = neighbor.Key;
                    float edgeWeight = neighbor.Value;

                    float newDistance = distances[current] + edgeWeight;

                    if (newDistance < distances[neighborVertex])
                    {
                        distances[neighborVertex] = newDistance;
                        predecessors[neighborVertex] = current; // 记录前驱
                        pq.Enqueue(neighborVertex, newDistance);
                    }
                }
            }

            List<int> path = new List<int>();
            int now = end;
            while (now != -1)
            {
                path.Insert(0, now); // 插入到列表开头，保持顺序
                now = predecessors[now];
            }

            return path;
        }
    }
}
