using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
  [SerializeField] private GameObject _shop;
  
  
  public void Shop()
  {
    _shop.gameObject.SetActive(true);
  }

  public void CloseShop()
  {
    _shop.gameObject.SetActive(false);
  }
}
