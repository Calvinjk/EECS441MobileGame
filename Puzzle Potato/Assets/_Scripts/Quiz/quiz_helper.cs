using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
//data container class, which acts as an intermediate for data persistence
// (for writing to our persistent file).
[Serializable]
class QuizData{

	public List<string> questions;
}

//A few things to note:  We store pre-programmed quiz questions in "quizData.dat",
//	and user-created questions in "userQuiz.dat"
namespace quiz_helper{

	//EFFECTS: Checks for the existence of our pre-programmed questions in the file system. 
	//         If the expected file doesn't exist (or no longer exists) then it
	//         will create it on the fly. 
	//MODIFIES: the persistentDataPath directory
	//NOTE:  We need this check because, for example, if our code saves this
	//       data to an SD card, then the quiz game will try to access it and
	//       end up displaying empty text, which isn't good for business :[
	//FIXME: Once this static array gets hefty, we should probably run this check 
	//       before the actual game starts.. 
	class Helper{

		public void check_and_create() {
			
			if( File.Exists( Path.Combine(Application.persistentDataPath, "quizData.dat") )){
				return;
			}
			
			//File has been lost or never existed in the first place, let's create it
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create( Path.Combine(Application.persistentDataPath, "quizData.dat") );
			//grab the question list and write it to the file
			QuizData q_data = new QuizData();
			q_data.questions = new List<string>();
			int q_size = preprogrammed._questions.Length;
			for(int i = 0; i < q_size; i++){
				q_data.questions.Add(preprogrammed._questions[i]);
			}
			bf.Serialize(file, q_data);
			file.Close();
			
		}//END check_and_create
	}//END Helper


}
//WARNING: Only look past this point if you're okay with long lines of code ugliness

//preprogrammed questions, unfortunately this is the easiest way to accomplish persistence so that the quiz game can always work

public struct preprogrammed{

	public static string[] _questions = {"# Which of the following people played with The Beatles? #C# John Lennon,Paul McCartney,George Harrison,Ringo Starr,Pete Best,Stuart Sutcliffe,Billy Preston,Eric Clapton,Donovan,Mick Jagger,Linda McCartney,Keith Moon,Keith Richards, #W# Neil Young,Edward Sharpe,Yoke Ono,David Bowie, John Coltrane, Miles Davis;", 
								  "# Which of the following albums did Nirvana release? #C# Nevermind,Bleach,In Utero,MTV Unplugged in New York,Nirvana, #W# Smells Like Teen Spirit,Come as You Are,Live at Red Rocks,Spermicide,Degreaser,Lithium,;",
								  "# Which of the following songs were originals, written by members of The Beatles? #C# I Saw Her Standing There,I Want to Hold Your Hand,She Loves You,If I Fell,A Hard Day's Night,If I Needed Someone,Michelle,She Said She Said,Doctor Robert,Taxman, #W# Roll Over Beethoven,You Really Got a Hold on Me,Devil in Her Heart,Money (That's What I Want),Long Tall Sally,Please Mister Postman,Twist and Shout,Boys,;",
								  "# Which of the following albums are studio albums by The Beatles? #C# Meet the Beatles!,Please Please Me,A Hard Day's Night,Beatles for Sale,Help!,Rubber Soul,Sgt. Pepper's Lonely Hearts Club Band,Magical Mystery Tour,The Beatles (The White Album),Yellow Submarine,Abbey Road,Let It Be, #W# Live at the BBC,The Beatles' Story,The Early Beatles,The Beatles' Greatest,The Beatles in Italy,Los Beatles,Love Songs,Hey Jude,;",
								  "# Which of the following songs are Chuck Berry singles? #C# Maybellene,Roll Over Beethoven,Rock and Roll Music,Sweet Little Sixteen,Johnny B. Goode,Chuck's Beat,Reelin' and Rockin', #W# Johnny Guitar,Johnny Thunder,Rock and Roll,Crossroads,Death Letter,;"
								  };

};
