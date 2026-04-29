using System.Collections.Generic;
using UnityEngine;

namespace _Project.Features.Gameplay.Chunk
{
    public class ChunkTeleporter : MonoBehaviour
    {

        [SerializeField] private ChunkComponent[] _chunks;
        [SerializeField] private float _speed;
        private float _startPosX = 69.12f;
        private float _endPosX = -34.56f;
        private LinkedList<ChunkComponent> _linkedList;
        private Transform _firstChunkTransform;


        private void Awake()
        {
            _linkedList = new LinkedList<ChunkComponent>();
            _linkedList.AddLast(_chunks[0]);
            _linkedList.AddLast(_chunks[1]);
            _linkedList.AddLast(_chunks[2]);
            _firstChunkTransform = _linkedList.First.Value.transform;
        }

        private void FixedUpdate()
        {
            CheckFirstChunk();
        }
        
        // TODO: Переписать под новую логику, основанную на конфигах
        private void CheckFirstChunk()
        {
            var pos =  _firstChunkTransform.localPosition;
            if (pos.x < _endPosX)
            {
                var tmp = _linkedList.First.Value;
                pos.x = _linkedList.Last.Value.transform.localPosition.x + 34.56f;
                _firstChunkTransform.localPosition = pos;
                _linkedList.RemoveFirst();
                _linkedList.AddLast(tmp);
                tmp.RespawnPipes();
                _firstChunkTransform =  _linkedList.First.Value.transform;
            }
        }
    
    }
}
