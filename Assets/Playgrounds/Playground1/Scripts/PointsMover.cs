using AnimFlex.Tweener;
using UnityEngine;

public class PointsMover : MonoBehaviour
{
    public Transform[] points;

    [Header("Tweener settings")] 
    public float duration = 1;
    public float delay = 0.2f;
    public Ease ease = Ease.InOutSine;

    private int currentIndx = 0;
    private bool canMove = true;

    private void Start()
    {
        transform.position = points[0].position;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawLine(points[i].transform.position, points[(i + 1) % points.Length].transform.position);
        }
    }

    public void Next()
    {
        if (!canMove) return;

        canMove = false;
        var tweener = transform.AnimPositionTo(points[(currentIndx + 1) % points.Length].position, ease, duration, delay);
        tweener.onComplete += () =>canMove = true;
        currentIndx = (currentIndx + 1) % points.Length;
    }

    public void Previous()
    {
        if(!canMove) return;
        
        canMove = false;
        
        int nextIndex = currentIndx - 1;
        if (nextIndex < 0) nextIndex += points.Length;
        var tweener = transform.AnimPositionTo(points[nextIndex].position, ease, duration, delay);
        tweener.onComplete += () => canMove = true;
        currentIndx = nextIndex;
    }
}
