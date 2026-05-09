using System.Collections.Generic;
using _Project.Core.Infrastructure.Config;
using UnityEngine;


namespace _Project.Features.Gameplay.Background
{
    [CreateAssetMenu(fileName="Background", menuName="Backgrounds/Background")]
    public class BackgroundConfig : ScriptableObject, IConfig
    {
        public int id;
        public int price;
        public List<BackgroundLayer> backgroundLayers;
    }
}