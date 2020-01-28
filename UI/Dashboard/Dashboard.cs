using UnityEngine;
using UnityEngine.UI;

namespace UI.Dashboard
{
    public class Dashboard : MonoBehaviour
    {
    

        public Text companyName;
        // Start is called before the first frame update
        void Start()
        {
        }

        public void UpdatePage()
        {
            companyName.text = DataBridge.GetName();
        }
        // Update is called once per frame
        void Update()
        {
        
        }


    }
}
