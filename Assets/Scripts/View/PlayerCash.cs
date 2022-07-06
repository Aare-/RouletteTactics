using TMPro;
using UnityEngine;

namespace View {
    public class PlayerCash : MonoBehaviour {

        [SerializeField] 
        protected GameState _State;
        
        [SerializeField] 
        protected TextMeshProUGUI _Cash;

        protected void Update() {
            _Cash.text = $"<size=50%>My Cash:</size>\n{_State.Cash}$";
        }
    }
}