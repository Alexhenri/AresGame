using System;
using System.Collections;
using System.Collections.Generic;
using System.Net; 
using System.Net.Sockets; 
using System.Text;
using System.Threading;
using UnityEngine;

public class Game : MonoBehaviour 
{
    public GameObject targetPrefab;
    public GameObject environmentGame;
    public int numberTargetPrefabs;
	public bool lock_start_game;

	#region private members 	 	
	private TcpListener tcpListener; // TCPListener to listen for incomming TCP connection requests
		
	private Thread tcpListenerThread; // Background thread for TcpServer workload	 	
	private TcpClient connectedTcpClient; // Create handle to connected tcp client		
	#endregion 	


    void Start() {

		tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests)); 		
		tcpListenerThread.IsBackground = true; 		
		tcpListenerThread.Start(); 

		lock_start_game = false;	
    }
	void Update() {

		if(lock_start_game == false && Input.GetKeyDown("enter")) {
			StartGame();
			lock_start_game = true;
		}
        if (Input.GetKey("escape")){
            Application.Quit();
        }
	}

    private void ListenForIncommingRequests() { 		
		try { 			
			// Create listener on localhost port 8037. 			
			tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8037); 			
			tcpListener.Start();              
			Debug.Log("Server is listening");              
			Byte[] bytes = new Byte[1024];  

			while (true) { 				
				using (connectedTcpClient = tcpListener.AcceptTcpClient()) { 					
					// Get a stream object for reading 					
					using (NetworkStream stream = connectedTcpClient.GetStream()) { 						
						int length; 						
						// Read incomming stream into byte array. 						
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 							
							var incommingData = new byte[length]; 							
							Array.Copy(bytes, 0, incommingData, 0, length);  							
							// Convert byte array to string message. 							
							string clientMessage = Encoding.ASCII.GetString(incommingData); 							
							MessageTreatment(clientMessage);
						} 					
					} 				
				} 			
			} 		
		} 		
		catch (SocketException socketException) { 			
			Debug.Log("SocketException " + socketException.ToString()); 		
		}     
	}  	

    private void MessageTreatment(string msg) {
		Debug.Log(msg);
        switch (msg) {
			case "Fire":
				// function fire():
				//		number_of_fired_bullets += 1;
				//		if bullet hits target
				//			number = how many target are there yet
				//			if no more targets
				//				function that end game
				//			else 
				//				msg returning number
				//		else
				//			msg returning fired and missed.
                break;
            case "Start":
				// function start():
				// 		starts the enviroment 
				//		check how many how many prefab target were generated
				//		msg returning "Game Started" and the number of prefab targets created
                break;
            case "Forward":
            case "Backward":
            case "Left":
            case "Right":
            case "Down":
            case "Up":
            case "SpinR":
            case "SpinL":
				// sendMessage("Player tried and succeeded")
            default:
				// sendMessage("What are you trying to do?")
                break;
        }
    }

    public void SendMessage() { 		
		if (connectedTcpClient == null) {             
			return;         
		}  		
		try { 			
			// Get a stream object for writing 			
			NetworkStream stream = connectedTcpClient.GetStream(); 			
			if (stream.CanWrite) {                 
				string serverMessage = "This is a message from your server."; 			
				// Convert string message to byte array                 
				byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage); 				
				// Write byte array to socketConnection stream               
				stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);               
				Debug.Log("Server sent his message - should be received by client");           
			}       
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		} 	
	} 

    private IEnumerator SpawnSingleTarget() {
         
        var x = UnityEngine.Random.Range(-20, 30);
        var y = UnityEngine.Random.Range(3, 5);
        var z = UnityEngine.Random.Range(-20, 30);
        Instantiate(targetPrefab, new Vector3(x,y,z), targetPrefab.transform.rotation);
        
        yield return null;
    }

    private IEnumerator SpawnTargets() {
        numberTargetPrefabs = UnityEngine.Random.Range(5, 15);
        for (int i = 0; i < numberTargetPrefabs; i++) {
            yield return SpawnSingleTarget();
        }
        yield return null;
    }

    private IEnumerator SpawnEnviromentGame() {
        var enviroment = Instantiate(environmentGame, environmentGame.transform.position, environmentGame.transform.rotation);

        yield return null;
    }

    private void StartGame() {
        if(lock_start_game == false){
            
            StartCoroutine(SpawnEnviromentGame());
            StartCoroutine(SpawnTargets());
            lock_start_game = true;
        }
		
    }
}
