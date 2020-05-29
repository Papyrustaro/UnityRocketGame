using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameObject : MonoBehaviour
{
    [SerializeField] private E_MenuType menuType;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MenuUIManager.Instance.OpenAndClosePanel(this.menuType, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MenuUIManager.Instance.OpenAndClosePanel(this.menuType, false);
        }
    }
}
