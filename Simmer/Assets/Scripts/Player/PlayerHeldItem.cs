using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Inventory;

namespace Simmer.Player
{
    public class PlayerHeldItem : MonoBehaviour
    {
        private PlayerInventory _playerInventory;
        private SpriteRenderer _spriteRenderer;

        public void Construct(PlayerManager playerManager)
        {
            _playerInventory = playerManager.playerInventory;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}