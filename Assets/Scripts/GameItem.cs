using UnityEngine;
using System.Collections.Generic;


public class GameItem : MonoBehaviour
{
    public enum ItemType { DragonEgg, Dragon, Tree }
    public enum ItemLevel { Level1, Level2, Level3 }

    public ItemType itemType;
    public ItemLevel itemLevel;
    public BoardGenerator boardGenerator; // 引用棋盘生成器

    // 当物体被放置时调用
    void OnMouseUp()
    {
        CheckAndMerge();
    }

    // 检查并执行合成
    private void CheckAndMerge()
    {
        if (transform == null)
            return;
        // 获取物体当前位置
        Vector3 position = transform.position;
        int x = Mathf.RoundToInt(position.x);
        int z = Mathf.RoundToInt(position.z);

        // 检查周围物体并尝试合成
        if (CanMerge(this, x, z))
        {
            PerformMerge(x, z);
        }
    }

    // 检查是否可以合成
    private bool CanMerge(GameItem item, int x, int z)
    {
        HashSet<GameItem> visited = new HashSet<GameItem>(); // 用于存储已检查的物体
        Queue<GameItem> queue = new Queue<GameItem>();       // BFS队列
        queue.Enqueue(item);

        List<string> mergeableItemNames = new List<string>(); // 存储可合成物体的名称
        int count = 0; // 相同类型物体的计数

        while (queue.Count > 0)
        {
            GameItem currentItem = queue.Dequeue();
            if (visited.Contains(currentItem))
                continue;

            visited.Add(currentItem);
            count++;
            mergeableItemNames.Add(currentItem.gameObject.name); // 添加物体名称

            List<GameItem> adjacentItems = GetAdjacentItems(currentItem, Mathf.RoundToInt(currentItem.transform.position.x), Mathf.RoundToInt(currentItem.transform.position.z));
            foreach (var adjacentItem in adjacentItems)
            {
                if (!visited.Contains(adjacentItem))
                {
                    queue.Enqueue(adjacentItem);
                }
            }
        }

        // 如果可以合成，则打印所有可合成物体的名称
        if (count >= 3)
        {
            Debug.Log("可合成物体: " + string.Join(", ", mergeableItemNames));
            return true;
        }

        return false;
    }

    // 执行合成
    private void PerformMerge(int x, int z)
    {

    }

    // 创建新物体
    private GameItem CreateNewObjectAt(int x, int z)
    {
        // 这里根据您的游戏逻辑来创建和放置新物体
        // 示例代码，需要根据具体游戏规则实现
        return new GameItem(); // 返回新创建的物体
    }

    // 获取周围物体
    private List<GameItem> GetAdjacentItems(GameItem item, int x, int z)
    {
        List<GameItem> adjacentItems = new List<GameItem>();

        // 只检查水平和垂直方向
        int[] dx = { 0, 0, 1, -1 };
        int[] dz = { 1, -1, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            GameItem neighbour = boardGenerator.GetObjectAt(x + dx[i], z + dz[i]);
            if (neighbour != null && neighbour.itemType == item.itemType && neighbour.itemLevel == item.itemLevel)
            {
                adjacentItems.Add(neighbour);
            }
        }

        return adjacentItems;
    }
}
