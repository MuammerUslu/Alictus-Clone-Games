using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ASMRStudio
{
    public class SoapController : MonoBehaviour
    {
        public List<Transform> knifePosition;

        [SerializeField] List<MeshRenderer> layerMeshRenderers;

        [SerializeField] float yDistbetweenLayers;
        private void Start()
        {
            ArrangeMeshRenderers();
        }
        private void ArrangeMeshRenderers()
        {
            for (int i = 0; i < layerMeshRenderers.Count; i++)
            {
                if (i < GameManager.currentLayerIndex)
                    layerMeshRenderers[i].enabled = false;
                else if ((i == GameManager.currentLayerIndex))
                    layerMeshRenderers[i].material.renderQueue = 3002;
            }
        }

        public void DecreasPointPositions()
        {
            foreach (Transform t in knifePosition)
                t.transform.position -= Vector3.up * yDistbetweenLayers;
        }

        private void OnEnable()
        {
            GameManager.OnLayerChanged += ArrangeMeshRenderers;
        }

        private void OnDisable()
        {
            GameManager.OnLayerChanged -= ArrangeMeshRenderers;
            for (int i = 0; i < layerMeshRenderers.Count; i++)
            {
                layerMeshRenderers[i].material.renderQueue = 3000;
            }
        }
    }
}
