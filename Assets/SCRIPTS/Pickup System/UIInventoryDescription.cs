using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        
        
            [SerializeField]
            private Image itemImage;
            [SerializeField]
            private TMP_Text title;
            [SerializeField]
            private TMP_Text description;


            public void Awake()
            {
                ResetDescription();
            }

            public void ResetDescription()
            {
                itemImage.gameObject.SetActive(false);
                
                description.text = "";
            }

            public void SetDescription(Sprite sprite, 
                string itemDescription)
            {
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = sprite;
                
                description.text = itemDescription;
            }
        }

    }
