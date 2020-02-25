using UnityEngine;
using UnityEngine.UI;

namespace UI.Dashboard
{
    public class Dashboard : MonoBehaviour
    {
        private Player _player;
        public Text company_textbox;
        
        void Start()
        {
            _player = Player.GetInstance();
            company_textbox.text = _player.company;
        }

    }
}
