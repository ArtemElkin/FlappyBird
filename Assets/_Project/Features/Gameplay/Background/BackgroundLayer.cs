using System;
using UnityEngine;


namespace _Project.Features.Gameplay.Background
{
    [Serializable]
    public struct BackgroundLayer
    {
        public Sprite sprite;
        public int order;
        public int count;
        public float speed;
    }
}