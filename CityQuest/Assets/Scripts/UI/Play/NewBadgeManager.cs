﻿using UnityEngine;
using UnityEngine.UI;

class NewBadgeManager : MonoBehaviour
{
    public Text BadgeName;
    public RawImage BadgeImage;

    
    public void GetBadge(Badge badge)
    {
        BadgeName.text = badge.Name;
        // TODO Afficher l'image du badge sur BadgeImage
    }

}

