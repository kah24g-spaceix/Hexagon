using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public List<Node> nodes;

    public Tree()
    {
        nodes = new List<Node>();
    }

    public Node CreateNode(int id)
    {
        Node node = new Node(id);
        nodes.Add(node);
        return node;
    }

    public Node FindNode(int id)
    {
        return nodes.Find(node => node.id == id);
    }
}