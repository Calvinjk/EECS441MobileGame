using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace com.aaronandco.puzzlepotato {
	public class PuzzleSelectionScroll : Puzzle {

        private string[] puzzle_names = {"Pop the bubbles", "Squish the bugs", 
                                 		 "Cross the river", "Tic-tac-toe", 
                                 		 "Rapid Touch", "Matching", "Timing Bar", 
                                 		 "Quiz Challenge", "Tilt maze", "Darts"};

		public GameObject toggle_template;

		public GameObject selectAll;

		protected GameManager gameManagerScript;

		public UnityEngine.UI.Text duration_text;

		//fill the with all of the toggles for the "select all" feature,
		// and for making sure that at least one puzzle is selected at all times.
		private List<GameObject> toggles = new List<GameObject>();

		int count_selected(List<bool> selections){

			int count = 0;
			for(int i = 0; i < selections.Count; i++){
				if(selections[i]){ count++; }
			}
			return count;
		}

		void Awake(){

			gameManagerScript = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
			if(gameManagerScript.duration[0]){

				duration_text.text = "Current: Short";
			}
			else if(gameManagerScript.duration[1]){

				duration_text.text = "Current: Medium";
			}
			else{

				duration_text.text = "Current: Long";
			}
		}
		// Use this for initialization
		void Start () {

			//sets the first name, because there is already one button created 
			// and I want to use it to easy set the parent of the rest of the buttons
			// created below
			toggle_template.SetActive(false);

			selectAll.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener(toggled);
			//first_ts.SetName(puzzle_names[0]);
			Vector3 scale = new Vector3(1f, 1f, 1f);

			for(int i = 0; i < puzzle_names.Length; i++){

				GameObject game_obj = Instantiate(toggle_template) as GameObject;
				game_obj.SetActive(true);

				//instantiate the toggles from the pre-existing instance
				game_obj.transform.SetParent(toggle_template.transform.parent);
				game_obj.GetComponent<RectTransform>().localScale = scale;
				game_obj.GetComponentInChildren<Text>().text = puzzle_names[i];
				//Give it the values we currently have stored in the gameManager bool array
				// that way the selections will persist between scenes
				game_obj.GetComponent<UnityEngine.UI.Toggle>().isOn = gameManagerScript.user_puzzle_selections[i];

				game_obj.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener(toggled);
				game_obj.name = puzzle_names[i];

				toggles.Add(game_obj);
			}
		}

		void toggled(bool selected){
			
			//Get the name of the button that triggered this function call
			string clicked_name = EventSystem.current.currentSelectedGameObject.name;
			//find the index of the clicked button
			if(clicked_name == "SelectAll"){
				selectAll.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
				//activate all puzzles
				for(int i = 0; i < toggles.Count; i++){
					toggles[i].GetComponent<UnityEngine.UI.Toggle>().isOn = true;
					gameManagerScript.user_puzzle_selections[i] = true;
				}
			}else{
				//set the selected puzzle if it is allowable
				int clicked_index = -1;
				for(int i = 0; i < puzzle_names.Length; i++){

					if(puzzle_names[i] == clicked_name){

						clicked_index = i;
						break;
					}
				}

				int num_selected_puzzles = count_selected(gameManagerScript.user_puzzle_selections);
				if(num_selected_puzzles == 1){
					if(gameManagerScript.user_puzzle_selections[clicked_index]){
						//in this case, the user is trying to deactivate the 
						//  last active puzzle, and we need at least one to play! 
						toggles[clicked_index].GetComponent<UnityEngine.UI.Toggle>().isOn = true;
						return; //we should probably print a message but it's okay for now
					}
				}
				//set the value of the external array in gamemanager
				gameManagerScript.user_puzzle_selections[clicked_index] = selected;
				//mess with the select all button info
				num_selected_puzzles = count_selected(gameManagerScript.user_puzzle_selections);
				if(num_selected_puzzles == toggles.Count){
					//All are selected
					selectAll.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
				}
				else{
					selectAll.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
				}
			}	
		}//END toggled --------------------------------------------------------
		
		public void short_selected(){
			duration_text.text = "Current: Short";

			if(gameManagerScript.duration[0]){
				return;
			}
			else{

				for(int i= 0; i < gameManagerScript.duration.Count; i++){
					gameManagerScript.duration[i] = false;
				}
				gameManagerScript.duration[0] = true;
			}
		}

		public void med_selected(){

			duration_text.text = "Current: Medium";

			if(gameManagerScript.duration[1]){
				return;
			}
			else{

				for(int i= 0; i < gameManagerScript.duration.Count; i++){
					gameManagerScript.duration[i] = false;
				}
				gameManagerScript.duration[1] = true;
			}
		}

		public void long_selected(){
			duration_text.text = "Current: Long";
			//set long true, and the rest false
			if(gameManagerScript.duration[2]){
				return;
			}
			else{

				for(int i= 0; i < gameManagerScript.duration.Count; i++){
					gameManagerScript.duration[i] = false;
				}
				gameManagerScript.duration[2] = true;
			}
		}

	}
}