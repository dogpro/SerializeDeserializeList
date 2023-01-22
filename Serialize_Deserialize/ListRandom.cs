using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class ListRandom
{
    public ListNode Head;
    public ListNode Tail;
    public int Count;

    public void Serialize(FileStream s)
    {
        Dictionary<string, int> listNodesData = new Dictionary<string, int>();
        ListNode current = Head;

        List<string> data = new List<string>();
        int randCount = 0;
        listNodesData.Add(current.Data, 0);
        data.Add(Count.ToString());
        data.Add(current.Data);
        
        for (int i = 1; i < Count; i++)
        {
            if (current.Random != null)
            {
                randCount++;
            }
            current = current.Next;
            data.Add(current.Data);
            listNodesData.Add(current.Data, i);
        }
        
        current = Head;
        
        data.Add(randCount.ToString());
        
        for (int i = 0; i < Count; i++)
        {
            if (current.Random != null)
            {
                data.Add($"{i},{listNodesData[current.Random.Data]}");
            }

            current = current.Next;
        }
        
        byte[] bytes = Encoding.ASCII.GetBytes(string.Join("\n", data));
        
        if (File.Exists(s.Name)) 
            File.WriteAllText(s.Name,string.Empty);
        
        s.Write(bytes, 0, bytes.Length);
    }

    public void Deserialize(FileStream s)
    {
        byte[] bytes = new byte[s.Length];
        s.Read(bytes, 0, bytes.Length);
        
        string textFromFile = Encoding.Default.GetString(bytes);
        
        List<string> lines = textFromFile.Split('\n').ToList();

        List<ListNode> nodes = new List<ListNode>();
        Count = int.Parse(lines[0]);
        lines.RemoveAt(0);
        
        ListNode head = new ListNode();
        ListNode prev = head;
        head.Data = lines[0];
        lines.RemoveAt(0);
        
        nodes.Add(head);
        
        for (int i = 1; i < Count; i++)
        {
            ListNode node = new ListNode();
            nodes.Add(node);
            node.Data = lines[0];

            prev.Next = node;
            node.Previous = prev;
            lines.RemoveAt(0);

            prev = node;
        }

        Tail = prev;
        Head = head;

        int countRandom = int.Parse(lines[0]);
        lines.RemoveAt(0);
        
        for (int i = 0; i < countRandom; i++)
        {
            var res = lines[0].Split(',');
            int nodeIndex = int.Parse(res[0]);
            int randomIndex = int.Parse(res[1]);
            
            lines.RemoveAt(0);

            nodes[nodeIndex].Random = nodes[randomIndex];
        }
    }

    public void Print()
    {
        var node = Head;
        while (true)
        {
            if (node == null)
            {
                break;
            }

            if (node.Random == null)
            {
                Console.WriteLine(node.Data);
            }
            else
            {
                Console.WriteLine($"{node.Data} <rand>  {node.Random.Data}");
            }
            
            Console.WriteLine("->");
            node = node.Next;
        }

        Console.WriteLine("null");
    }
}