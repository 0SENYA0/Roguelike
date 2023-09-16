using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Infrastructure;
using Assets.Inventory;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.SoundSystem;
using Assets.Scripts.UI.Widgets;
using Lean.Localization;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.DefendItems;
using Assets.Interface;

namespace DefaultNamespace
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            InventoryModel inventory = new InventoryModel(10);
            inventory.AddItem(new Armor(null, null, null));

        }
    }
}
