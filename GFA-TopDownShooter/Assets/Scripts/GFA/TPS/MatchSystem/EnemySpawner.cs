using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.MatchSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        private Camera _camera;

        private Plane _plane = new Plane(Vector3.up, Vector3.zero);

        private void Awake()
        {
            _camera = Camera.main;
        }
        private void Start()
        {
            StartCoroutine(CreateEnemy());
        }

        private IEnumerator CreateEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var viewportPoint = new Vector3(-0.1f, 0, Mathf.Abs(_camera.transform.position.z));
                var ray = _camera.ViewportPointToRay(viewportPoint);

                if (_plane.Raycast(ray,out float enter))
                {
                    var worldPosition = ray.GetPoint(enter);
                    var inst = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    inst.transform.position = worldPosition;
                }

            }
        }
    }
}

