using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
  [SerializeField] private GameObject _shop;
  [SerializeField] private GameObject _canvasRecord; 
  
  public void Shop()
  {
    _shop.gameObject.SetActive(true);
  }

  public void CloseShop()
  {
    _shop.gameObject.SetActive(false);
  }

  public void Play()
  {
    SceneManager.LoadScene("Level");
  }

  public void Record()
  {
    _canvasRecord.SetActive(!_canvasRecord.activeSelf);
  }
}
