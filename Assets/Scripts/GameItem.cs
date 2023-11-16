using UnityEngine;
using System.Collections.Generic;


public class GameItem : MonoBehaviour
{
    public enum ItemType { DragonEgg, Dragon, Tree }
    public enum ItemLevel { Level1, Level2, Level3 }

    public ItemType itemType;
    public ItemLevel itemLevel;
    public BoardGenerator boardGenerator; // ��������������

    // �����屻����ʱ����
    void OnMouseUp()
    {
        CheckAndMerge();
    }

    // ��鲢ִ�кϳ�
    private void CheckAndMerge()
    {
        if (transform == null)
            return;
        // ��ȡ���嵱ǰλ��
        Vector3 position = transform.position;
        int x = Mathf.RoundToInt(position.x);
        int z = Mathf.RoundToInt(position.z);

        // �����Χ���岢���Ժϳ�
        if (CanMerge(this, x, z))
        {
            PerformMerge(x, z);
        }
    }

    // ����Ƿ���Ժϳ�
    private bool CanMerge(GameItem item, int x, int z)
    {
        HashSet<GameItem> visited = new HashSet<GameItem>(); // ���ڴ洢�Ѽ�������
        Queue<GameItem> queue = new Queue<GameItem>();       // BFS����
        queue.Enqueue(item);

        List<string> mergeableItemNames = new List<string>(); // �洢�ɺϳ����������
        int count = 0; // ��ͬ��������ļ���

        while (queue.Count > 0)
        {
            GameItem currentItem = queue.Dequeue();
            if (visited.Contains(currentItem))
                continue;

            visited.Add(currentItem);
            count++;
            mergeableItemNames.Add(currentItem.gameObject.name); // �����������

            List<GameItem> adjacentItems = GetAdjacentItems(currentItem, Mathf.RoundToInt(currentItem.transform.position.x), Mathf.RoundToInt(currentItem.transform.position.z));
            foreach (var adjacentItem in adjacentItems)
            {
                if (!visited.Contains(adjacentItem))
                {
                    queue.Enqueue(adjacentItem);
                }
            }
        }

        // ������Ժϳɣ����ӡ���пɺϳ����������
        if (count >= 3)
        {
            Debug.Log("�ɺϳ�����: " + string.Join(", ", mergeableItemNames));
            return true;
        }

        return false;
    }

    // ִ�кϳ�
    private void PerformMerge(int x, int z)
    {

    }

    // ����������
    private GameItem CreateNewObjectAt(int x, int z)
    {
        // �������������Ϸ�߼��������ͷ���������
        // ʾ�����룬��Ҫ���ݾ�����Ϸ����ʵ��
        return new GameItem(); // �����´���������
    }

    // ��ȡ��Χ����
    private List<GameItem> GetAdjacentItems(GameItem item, int x, int z)
    {
        List<GameItem> adjacentItems = new List<GameItem>();

        // ֻ���ˮƽ�ʹ�ֱ����
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
