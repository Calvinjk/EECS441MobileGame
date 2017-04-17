using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Reflection;
using System;
using System.Runtime.Serialization.Formatters.Binary;

using quiz_helper;

namespace com.aaronandco.puzzlepotato {
	public class QuizController : Puzzle {

		private Color ORIGINAL_COLOR = new Color32(0x1F, 0xB2, 0x99, 0xFF);
		private Color SELECTED_COLOR = new Color32(0xF3, 0x2C, 0x2C, 0xFF);
		private int TOTAL_POSSIBLE_ANSWERS = 6;
		public bool debugLogs = true;

		private GameObject Question;

		public GameObject buttonPrefab;
		public GameObject questionPrefab;

		public List<UnityEngine.UI.Button> answers;

		//private Button pressedButton;
		private string selected_line;

		private bool[] answer_key = new bool[6];
		private GameObject[] buttons = new GameObject[6];
		private int num_correct_answers;
		private int players_correct_selections = 0;

		private bool first_question = true;

		void reset(){
			//reset colors of buttons
			buttons[0].GetComponent<Button>().image.color = ORIGINAL_COLOR;
			buttons[1].GetComponent<Button>().image.color = ORIGINAL_COLOR;
			buttons[2].GetComponent<Button>().image.color = ORIGINAL_COLOR;
			buttons[3].GetComponent<Button>().image.color = ORIGINAL_COLOR;
			buttons[4].GetComponent<Button>().image.color = ORIGINAL_COLOR;
			buttons[5].GetComponent<Button>().image.color = ORIGINAL_COLOR;

			//reset the answer key
			for(int i = 0; i < TOTAL_POSSIBLE_ANSWERS; i++){
				answer_key[i] = false;
			}
			//reset the number of answers the player got correct
			players_correct_selections = 0;

		}
		void player_won_maybe(int num_right){

			if(num_right >= num_correct_answers){

				//player won! perhaps we should make a congratulatory message?
				GameCompleted();
			}
			else{ return; }
			
		}

		//helper for the quiz game
		bool is_seen(int selection, List<int> list){

			for(int i = 0; i < list.Count; i++){

				if(list[i] == selection){
					return true;
				} 
			}

			return false;
		}
		// Use this for initialization

		void Start(){
			//the following call makes sure our file already exists
			Helper help = new quiz_helper.Helper();
			help.check_and_create();
			Initialize();
		}

		public override void StartGame() {

			if(first_question){
				//BEGIN instantiating question/answer GameObjects
				Question = Instantiate(questionPrefab, GameObject.Find("Canvas").transform, false);
				Vector3 q_pos = new Vector3(0f, 134f, 0f);
				Question.GetComponent<RectTransform>().localPosition = q_pos;

				buttons[0] = Instantiate(buttonPrefab, GameObject.Find("Canvas").transform, false);
				UnityEngine.UI.Button button_temp = buttons[0].GetComponent<UnityEngine.UI.Button>();
				button_temp.onClick.AddListener(Selection);
				Vector3 position = new Vector3(181f, 47f, 3f);
				buttons[0].GetComponent<RectTransform>().localPosition = position;

				buttons[1] = Instantiate(buttonPrefab, GameObject.Find("Canvas").transform, false) ;
				button_temp = buttons[1].GetComponent<UnityEngine.UI.Button>();
				button_temp.onClick.AddListener(Selection1);
				position = new Vector3(-181f, 47f, 3f);
				buttons[1].GetComponent<RectTransform>().localPosition = position;

				buttons[2] = Instantiate(buttonPrefab, GameObject.Find("Canvas").transform, false);
				button_temp = buttons[2].GetComponent<UnityEngine.UI.Button>();
				button_temp.onClick.AddListener(Selection2);
				position = new Vector3(181f, -99f, 3f);
				buttons[2].GetComponent<RectTransform>().localPosition = position;

				buttons[3] = Instantiate(buttonPrefab, GameObject.Find("Canvas").transform, false);
				button_temp = buttons[3].GetComponent<UnityEngine.UI.Button>();
				button_temp.onClick.AddListener(Selection3);
				position = new Vector3(181f, -26f, 3f);
				buttons[3].GetComponent<RectTransform>().localPosition = position;

				buttons[4] = Instantiate(buttonPrefab, GameObject.Find("Canvas").transform, false);
				button_temp = buttons[4].GetComponent<UnityEngine.UI.Button>();
				button_temp.onClick.AddListener(Selection4);
				position = new Vector3(-181f, -99f, 3f);
				buttons[4].GetComponent<RectTransform>().localPosition = position;

				buttons[5] = Instantiate(buttonPrefab, GameObject.Find("Canvas").transform, false);
				button_temp = buttons[5].GetComponent<UnityEngine.UI.Button>();
				button_temp.onClick.AddListener(Selection5);
				position = new Vector3(-181f, -26f, 3f);
				buttons[5].GetComponent<RectTransform>().localPosition = position;

				first_question = false;
			}

			//END object instantiations ----------------- ----------------------

			//step 1: fill an Array with all available filenames to choose between
			//		  choose a file at random after getting these filenames 
			//		  (maybe give them a special extension?)
			//        FOR NOW, just going to open the file

			string current_dir = Path.Combine(Application.persistentDataPath, "quizData.dat");
			if(debugLogs){ Debug.Log(current_dir); }


			if(!File.Exists(Path.Combine(Application.persistentDataPath, "quizData.dat"))){
				GameCompleted();
			}

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = new FileStream(Path.Combine(Application.persistentDataPath, "quizData.dat"), FileMode.Open, FileAccess.Read, FileShare.None);
			QuizData data = (QuizData)bf.Deserialize(file);
			file.Close();
			//step 2: Choose a question from the array at random
			//        must make use of a line counter for files to do this
			System.Random rand = new System.Random();

			int upper_limit = data.questions.Count;

			int line = rand.Next(0, upper_limit);

			selected_line = data.questions[line];

			if(debugLogs){ Debug.Log(selected_line); }

			//step 3: display the question text
			string[] parsed_text = selected_line.Split('#');
			if(debugLogs){ Debug.Log(parsed_text[1]); }
			Question.GetComponentInChildren<Text>().text = parsed_text[1];
			string correct_answers = parsed_text[3];
			string wrong_answers = parsed_text[5];

			//step 4: Fill a list of right and wrong answers
			string[] correct_array = correct_answers.Split(',');
			string[] wrong_array = wrong_answers.Split(',');

			//these limits are size-1 because both the correct/incorrect answer
			// pools end with a comma, so the last position will be empty
			int correct_limit = correct_array.Length-1;
			int wrong_limit = wrong_array.Length-1;

			//choose the number of correct answers to be diplayed
			int num_correct = rand.Next(1, 4);
			//set the global win condition variable
			num_correct_answers = num_correct;

            if (debugLogs) { Debug.Log("Number of correct answers: " + num_correct);}
			//the rest will be incorrect answers
			int num_wrong = TOTAL_POSSIBLE_ANSWERS - num_correct;

			List<string> correct_selections = new List<string>();
			List<string> wrong_selections = new List<string>();

			int get_answer_at_pos;
			//we can't allow duplicates, so keep track of answers we've already selected
			List<int> already_selected = new List<int>();

			for(int i = 0; i < num_correct;){
				//gather random correct answers
				get_answer_at_pos = rand.Next(0, correct_limit);
				if(is_seen(get_answer_at_pos, already_selected)){ 
					//answer here would be a duplicate, go back to loop
					continue;
				}
				else{
					already_selected.Add(get_answer_at_pos);
					correct_selections.Add(correct_array[get_answer_at_pos]);
					i++;
				}
				
			}

			//get list ready for wrong answer selection

			already_selected.Clear();

			for(int i = 0; i < num_wrong;){
				//gather random wrong answers
				get_answer_at_pos = rand.Next(0, wrong_limit);
				if(is_seen(get_answer_at_pos, already_selected)){ 
					//answer here would be a duplicate, go back to loop
					continue;
				}
				else{
					already_selected.Add(get_answer_at_pos);
					wrong_selections.Add(wrong_array[get_answer_at_pos]);
					i++;
				}
					
			}

			int temp;
			int right_or_wrong = -1;
			//put the answer text on buttons!
			for(int i = 0; i < TOTAL_POSSIBLE_ANSWERS; i++){

				//first, choose a right or wrong answer to give to a button
				if(correct_selections.Count > 0 && wrong_selections.Count > 0){
					//we don't want to put right/wrong answers in a deterministic ordering,
					// so we'll but them in a random order
					right_or_wrong = rand.Next(0, 2);
				}
				if(right_or_wrong == 1){

					temp = rand.Next(0, correct_selections.Count);
					//Add a correct answer to the button
					buttons[i].GetComponentInChildren<Text>().text = correct_selections[temp];
					answer_key[i] = true;
					correct_selections.RemoveAt(temp);

					if(correct_selections.Count <= 0){
						//if we've ran out of correct answers, 
						// only select from the wrong list from here on out
						right_or_wrong = 0;
					}
				}
				else if(right_or_wrong == 0){

					temp = rand.Next(0, wrong_selections.Count);
					//Add a correct answer to the button
					buttons[i].GetComponentInChildren<Text>().text = wrong_selections[temp];
					answer_key[i] = false;
					wrong_selections.RemoveAt(temp);

					if(wrong_selections.Count <= 0){
						//if we've ran out of wrong answers, 
						// only select from the correct list from here on out
						right_or_wrong = 1;
					}

				}
				else{
                    if (debugLogs) { Debug.Log("ERROR IN QUIZ CONTROLLER line 272"); }
				}
			}//END FOR LOOP ---------------------------------------------------

		}
		

		public void Selection(){
			buttons[0].GetComponent<Button>().image.color = SELECTED_COLOR;
			if(answer_key[0]){

				players_correct_selections += 1;
				player_won_maybe(players_correct_selections);
			}
			else{
				//restart game, player chose incorrectly
				reset();
				StartGame();
			}
		}

		public void Selection1(){
			buttons[1].GetComponent<Button>().image.color = SELECTED_COLOR;
			if(answer_key[1]){

				players_correct_selections += 1;
				player_won_maybe(players_correct_selections);
			}
			else{
				//restart game, player chose incorrectly
				reset();
				StartGame();
			}
		}

		public void Selection2(){
			buttons[2].GetComponent<Button>().image.color = SELECTED_COLOR;
			if(answer_key[2]){

				players_correct_selections += 1;
				player_won_maybe(players_correct_selections);
			}
			else{
				//restart game, player chose incorrectly
				reset();
				StartGame();
			}
		}

		public void Selection3(){
			buttons[3].GetComponent<Button>().image.color = SELECTED_COLOR;
			if(answer_key[3]){

				players_correct_selections += 1;
				player_won_maybe(players_correct_selections);
			}
			else{
				//restart game, player chose incorrectly
				reset();
				StartGame();
			}
		}

		public void Selection4(){
			buttons[4].GetComponent<Button>().image.color = SELECTED_COLOR;
			if(answer_key[4]){

				players_correct_selections += 1;
				player_won_maybe(players_correct_selections);
			}
			else{
				//restart game, player chose incorrectly
				reset();
				StartGame();
			}
		}

		public void Selection5(){
			buttons[5].GetComponent<Button>().image.color = SELECTED_COLOR;
			if(answer_key[5]){

				players_correct_selections += 1;
				player_won_maybe(players_correct_selections);
			}
			else{
				//restart game, player chose incorrectly
				reset();
				StartGame();
			}
		}

	}
}//END namespace puzzepotato


//data container class

//APPENDIX

		//string cur_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		//string cur_path = Directory.GetCurrentDirectory();
		//cur_path = cur_path + "\\Assets\\Scripts\\";
		//string [] filenames = Directory.GetFiles(cur_path,  "*.txt");
		//int size = filenames.Length;
		//Debug.Log();

		/*
		if(size > 0){

			for(int i = 0; i < size; i++){
				if(debugLogs){ Debug.Log(filenames[i]);}
			}
		}
		*/
