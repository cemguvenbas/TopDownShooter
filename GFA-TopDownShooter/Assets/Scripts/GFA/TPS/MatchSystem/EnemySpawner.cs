using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GFA.TPS.MatchSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        private Camera _camera;

        private Plane _plane = new Plane(Vector3.up, Vector3.zero);

        [SerializeField]
        private float _offset;

        private void Awake()
        {
            _camera = Camera.main;
        }
        private void Start()
        {
            StartCoroutine(CreateEnemy());
        }
        private Vector3 GetSpawnOffSetByViewportPosition(Vector3 viewport, float sign)
        {

            return Vector3.up * _offset;
        }

        private IEnumerator CreateEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                var viewportPoint = Vector3.zero;
                if (Random.value > 0.5)
                {
                    viewportPoint = new Vector3(Mathf.Round(Random.value), Random.value);                    
                }
                else
                {
                    viewportPoint = new Vector3(Random.value, Mathf.Round(Random.value));
                }
                var offset = GetSpawnOffSetByViewportPosition(viewportPoint);
                var ray = _camera.ViewportPointToRay(viewportPoint);

                if (_plane.Raycast(ray,out float enter))
                {
                    var worldPosition = ray.GetPoint(enter) - offset;
                    var inst = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    inst.transform.position = worldPosition;
                }

            }
        }
    }
}

