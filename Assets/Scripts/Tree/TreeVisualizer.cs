using System.Collections.Generic;
using UnityEngine;

public class TreeVisualizer : MonoBehaviour
{
    public GameObject nodePrefab;  // ��带 ǥ���� ������
    public Tree tree;
    public float initialRadius = 2f; // �ʱ� ������
    public float radiusIncrement = 2f; // �� �������� ������ ������

    void Start()
    {
        tree = new Tree();

        // ���� Ʈ�� ����
        Node node1 = tree.CreateNode(1);
        Node node2 = tree.CreateNode(2);
        Node node3 = tree.CreateNode(3);
        Node node4 = tree.CreateNode(4);
        Node node5 = tree.CreateNode(5);

        node1.AddChild(node2);
        node1.AddChild(node3);
        node2.AddChild(node4);
        node2.AddChild(node5);

        // Ʈ�� �ð�ȭ ����
        VisualizeTree(node1, Vector3.zero, initialRadius, 0, null);
    }

    // Ʈ�� �ð�ȭ �޼���
    void VisualizeTree(Node node, Vector3 position, float radius, float angle, Transform parentTransform)
    {
        if (node == null)
            return;

        // ��� ���� �� ��ġ ����
        GameObject nodeObject = Instantiate(nodePrefab, position, Quaternion.identity);
        nodeObject.name = "Node " + node.id;

        // ��� ������ ������Ʈ �߰�
        NodeRenderer nodeRenderer = nodeObject.AddComponent<NodeRenderer>();
        nodeRenderer.node = node;
        nodeRenderer.parentTransform = parentTransform;

        // �θ�-�ڽ� ������ �ð������� ǥ���ϴ� ���� ����
        if (parentTransform != null)
        {
            GameObject lineObject = new GameObject("Line");
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;

            nodeRenderer.lineRenderer = lineRenderer;
        }

        // �ڽ� ������ ����� �迭�� ��ġ
        int childCount = node.children.Count;
        if (childCount == 0)
            return;

        float angleStep = 360f / childCount;

        for (int i = 0; i < childCount; i++)
        {
            Node child = node.children[i];
            float childAngle = angle + i * angleStep;
            Vector3 childPosition = position + new Vector3(Mathf.Cos(childAngle * Mathf.Deg2Rad) * radius, 0, Mathf.Sin(childAngle * Mathf.Deg2Rad) * radius);

            // ��������� �ڽ� ����� ��ġ ��� �� �ð�ȭ
            VisualizeTree(child, childPosition, radius + radiusIncrement, childAngle, nodeObject.transform);
        }
    }
}
