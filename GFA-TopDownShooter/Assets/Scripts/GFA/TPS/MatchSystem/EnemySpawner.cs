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
        private MatchInstance _matchInstance;

        [SerializeField]
        private EnemySpawnData _enemySpawnData;

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
        private Vector3 GetSpawnOffSetByViewportPosition(Vector3 vector, float sign)
        {

            return vector * sign * _offset;
        }

        private GameObject GetSpawnObject()
        {
            var time = _matchInstance.Time;

            if (_enemySpawnData.TryGetEntryByTime(time,out SpawnEntry entry))
            {
                return entry.Prefabs[Random.Range(0, entry.Prefabs.Length - 1)];
            }
            return null;
        }

        private IEnumerator CreateEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                var viewportPoint = Vector3.zero;

                var offset = Vector3.zero;

                if (Random.value > 0.5)
                {
                    var dir = Mathf.Round(Random.value);
                    viewportPoint = new Vector3(dir, Random.value);

                    offset = GetSpawnOffSetByViewportPosition(Vector3.right, dir < 0.001f ? -1f : 1f);
                }
                else
                {
                    var dir = Mathf.Round(Random.value);
                    viewportPoint = new Vector3(Random.value,dir);

                    offset = GetSpawnOffSetByViewportPosition(Vector3.forward, dir < 0.001f ? -1f : 1f);
                }

                var ray = _camera.ViewportPointToRay(viewportPoint);

                if (_plane.Raycast(ray,out float enter))
                {
                    var worldPosition = ray.GetPoint(enter) + offset;
                    var inst = Instantiate(GetSpawnObject(), worldPosition, Quaternion.identity);
                    inst.transform.position = worldPosition;
                }

            }
        }
    }
}

