using _Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Scripts.Infrastructure.AssetManagment
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}