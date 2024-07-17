using UnityEngine;

public class NodeRenderer : MonoBehaviour
{
    public Node node;
    public LineRenderer lineRenderer;
    public Transform parentTransform;

    void Update()
    {
        if (lineRenderer != null && parentTransform != null)
        {
            lineRenderer.SetPosition(0, parentTransform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
