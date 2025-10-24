using UnityEngine;

namespace StudentGameJam
{
    public class LinearSpawnPointStrategy : ISpawnPointStrategy
    {
        private Transform[] spawnPoints;
        private int index = 0;
        public LinearSpawnPointStrategy(Transform[] spawnPoints)
        {
            this.spawnPoints = spawnPoints;
        }
        public Transform NextSpawnPoint()
        {
            Transform result = spawnPoints[index];
            index = (index + 1) % spawnPoints.Length;
            return result;
        }
    }
}

