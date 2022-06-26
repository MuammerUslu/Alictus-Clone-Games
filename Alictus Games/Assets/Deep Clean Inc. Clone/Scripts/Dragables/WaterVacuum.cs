using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepCleanInc
{
    public class WaterVacuum : DragableObject
    {
        [SerializeField] GameObject lineRendererBrush;

        private Vector3 lastLineRendererPointPos = Vector3.one * Mathf.Infinity;
        LineRenderer lineRenderer;

        private void Start()
        {
            lineRenderer = Instantiate(lineRendererBrush, GetComponent<SphereCollider>().transform.position, Quaternion.identity).GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, new Vector3(transform.position.x, 0.69f, transform.position.y));
            lineRenderer.SetPosition(1, new Vector3(transform.position.x, 0.69f, transform.position.y));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "DirtScoreCollider")
            {
                GameManager.Instance.CheckLevelCompleted();
                other.enabled = false;
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.transform.tag == "VacuumDirt")
            {
                if (Vector3.Distance(lastLineRendererPointPos, collision.contacts[0].point) >= 0.05f)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, collision.contacts[0].point);
                    lastLineRendererPointPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                }
            }
        }

    }
}
