using UnityEngine;
using UnityEngine.UI;

namespace UI.Dashboard
{
    public class Dashboard : MonoBehaviour
    {
        private Player _player = Player.GetInstance();
        public Text company_textbox;
        
        public void UpdatePage()
        {
            company_textbox.text = _player.company;
        }

    }
}
