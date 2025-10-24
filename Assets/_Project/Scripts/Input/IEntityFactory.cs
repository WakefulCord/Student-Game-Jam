using UnityEngine;

namespace StudentGameJam
{
    public interface IEntityFactory<T> where T : Entity
    {
        T Create(Transform spawnPoint);
    }
}

