﻿using System;
using Entity.DynamicEntity;
using UnityEngine;

namespace UI_Audio
{
    public class InventoryManager : MonoBehaviour
    {
        [NonSerialized] public static InventoryManager Instance;
        
        public Inventory playerInventory;
        public SmithInventory smithInventory;
        //public BuyerInventory buyerInventory;
        //public SellerInventory sellerInventory;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(this);
        }

        public bool Initialize()
        {
            NPC.InventoryManager = this;
            CloseAllInventories();
            return true;
        }

        private void CloseAllInventories()
        {
            playerInventory.Close();
            smithInventory.Close();
        }
    }
}