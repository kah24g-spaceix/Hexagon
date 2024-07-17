using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int id;
    public List<Node> children;

    public Node(int id)
    {
        this.id = id;
        this.children = new List<Node>();
    }

    public void AddChild(Node child)
    {
        children.Add(child);
    }
}