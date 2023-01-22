using System;
using System.Collections.Generic;
using System.IO;

namespace Serialize_Deserialize
{
    internal class Program
    {
        private static int _listLenght = 10;
        private static string _filePath = @"file.txt";

        public static void Main(string[] args)
        {
            var randList = CreateStructure(_listLenght);

            FileStream stream = File.OpenWrite(_filePath);

            randList.Print();
            randList.Serialize(stream);
            Console.WriteLine("--------------------------------------------------------------------------------------");
            ListRandom desRandList = new ListRandom();

            stream.Close();

            FileStream fs = File.OpenRead(_filePath);
            desRandList.Deserialize(fs);
            desRandList.Print();

            fs.Close();
        }

        private static ListRandom CreateStructure(int count)
        {
            List<ListNode> listNodes = new List<ListNode>();
            ListRandom rand = new ListRandom();
            ListNode head = new ListNode();
            ListNode prev = new ListNode();

            rand.Count = count;
            head.Data = "data_0";
            prev = head;

            listNodes.Add(head);

            for (int i = 1; i < count; i++)
            {
                var current = new ListNode();
                listNodes.Add(current);
                current.Data = "data_" + i;

                prev.Next = current;

                prev = current;

                if (i != 0)
                {
                    current.Previous = prev;
                }
            }

            rand.Head = head;
            rand.Tail = prev;
            var curNode = head;
            var rndGen = new Random();
            for (int i = 0; i < count / 2; i++)
            {
                curNode.Random = listNodes[rndGen.Next(0, count)];
                curNode = curNode.Next;
            }

            curNode.Random = curNode;

            return rand;
        }
    }
}