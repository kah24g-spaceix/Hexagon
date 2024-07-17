using System.Collections.Generic;
using UnityEngine;

public class TreeVisualizer : MonoBehaviour
{
    public GameObject nodePrefab;  // 노드를 표현할 프리팹
    public Tree tree;
    public float initialRadius = 2f; // 초기 반지름
    public float radiusIncrement = 2f; // 각 레벨마다 반지름 증가량

    void Start()
    {
        tree = new Tree();

        // 예시 트리 생성
        Node node1 = tree.CreateNode(1);
        Node node2 = tree.CreateNode(2);
        Node node3 = tree.CreateNode(3);
        Node node4 = tree.CreateNode(4);
        Node node5 = tree.CreateNode(5);

        node1.AddChild(node2);
        node1.AddChild(node3);
        node2.AddChild(node4);
        node2.AddChild(node5);

        // 트리 시각화 시작
        VisualizeTree(node1, Vector3.zero, initialRadius, 0, null);
    }

    // 트리 시각화 메서드
    void VisualizeTree(Node node, Vector3 position, float radius, float angle, Transform parentTransform)
    {
        if (node == null)
            return;

        // 노드 생성 및 위치 설정
        GameObject nodeObject = Instantiate(nodePrefab, position, Quaternion.identity);
        nodeObject.name = "Node " + node.id;

        // 노드 렌더러 컴포넌트 추가
        NodeRenderer nodeRenderer = nodeObject.AddComponent<NodeRenderer>();
        nodeRenderer.node = node;
        nodeRenderer.parentTransform = parentTransform;

        // 부모-자식 연결을 시각적으로 표현하는 라인 설정
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

        // 자식 노드들을 방사형 배열로 배치
        int childCount = node.children.Count;
        if (childCount == 0)
            return;

        float angleStep = 360f / childCount;

        for (int i = 0; i < childCount; i++)
        {
            Node child = node.children[i];
            float childAngle = angle + i * angleStep;
            Vector3 childPosition = position + new Vector3(Mathf.Cos(childAngle * Mathf.Deg2Rad) * radius, 0, Mathf.Sin(childAngle * Mathf.Deg2Rad) * radius);

            // 재귀적으로 자식 노드의 위치 계산 및 시각화
            VisualizeTree(child, childPosition, radius + radiusIncrement, childAngle, nodeObject.transform);
        }
    }
}
