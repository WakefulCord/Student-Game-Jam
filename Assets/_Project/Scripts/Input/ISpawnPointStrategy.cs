using NUnit.Framework;
using UnityEngine;

namespace StudentGameJam
{
    public interface ISpawnPointStrategy
    {
        Transform NextSpawnPoint();
    }
}

