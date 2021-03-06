using Entity.DynamicEntity.LivingEntity.Player;
using Mirror;
using UnityEngine;

namespace Entity.StaticEntity {
	public class Portal: Entity, IInteractiveEntity {
		private Animator _animator;
		private int _nbOfPlayers;
		private static readonly int IsPlayerHere = Animator.StringToHash("IsPlayerHere");

		private void Start() {
			Instantiate();
			_nbOfPlayers = 0;
			_animator = GetComponent<Animator>();
		}
		
		protected new void OnTriggerEnter2D(Collider2D other) {
			if (_nbOfPlayers <= 0) {
				_animator.Play("Opening");
				_animator.SetBool(IsPlayerHere, true);
			}
			_nbOfPlayers++;
			base.OnTriggerEnter2D(other);
		}

		protected new void OnTriggerExit2D(Collider2D other) {
			_nbOfPlayers--;
			if (_nbOfPlayers <= 0) 
				_animator.SetBool(IsPlayerHere, false);
			base.OnTriggerExit2D(other);
		}

		[Server] public void Interact(Player player) {
			Debug.Log("salut les salopes");
		}
	}
}
