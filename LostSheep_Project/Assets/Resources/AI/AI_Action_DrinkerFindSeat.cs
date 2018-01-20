using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("酒客寻找座位")]
	public class AI_Action_DrinkerFindSeat : Action {

		//
		public override void OnStart(){

			L_Actor_Seat[] seats = L_Actor_Seat.GetAllAeats ();

			for(int i = 0 ;i<seats.Length;i++){
				L_Actor_Seat seat = seats[i];
				if(seat.IsEmpty){
					L_Character_Drinker drinker = GetComponent<L_Character_Drinker> ();
					drinker.SeatID = seat.ID;
					GameEventMachine.SendEvent (GameEventID.Event_Drinker_FindSeat,seat.ID,drinker.ID);
					return;
				}
			}
		}
	}
}