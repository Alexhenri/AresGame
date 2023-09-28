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
	public bool lock_start_game;
	
	// --- Target Manager --- //
	public int startedNumberTargetPrefabs;
	private int currentNumberTargetPrefabs;
	private int numberOfTargetEffects;

	// --- Fire Manager --- //
	private int numberOfBullets;

	// --- TCP VARS --- //	
	private TcpListener tcpListener; // TCPListener to listen for incomming TCP connection requests
	private TcpClient connectedTcpClient; // Create handle to connected tcp client
	private NetworkStream stream;

	
	private Tank tankPlayer;
	private BulletSpawn bulletSpawn;
	private SpinTankTower tankTower;
	private UpDownCanon tankCanon;

	private async void Start() {

		numberOfBullets = 0;
		lock_start_game = false;
		ListenForIncommingRequests();

	}

	void Update() {

		if(lock_start_game == false && (Input.GetKeyDown("return") || Input.GetKeyDown("enter"))) {
			StartGame();
			lock_start_game = true;
		}
        if (Input.GetKey("escape")){
            Application.Quit();
        }
	}

    private async void ListenForIncommingRequests() { 		
		try { 			
			// Create listener on localhost port 8037. 			
			tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8037); 			
			tcpListener.Start();              
			Debug.Log("Server is listening");              
			Byte[] bytes = new Byte[1024];  

			while (true) { 				
				using (connectedTcpClient = await tcpListener.AcceptTcpClientAsync()) { 			
					// Get a stream object for reading 					
					using (NetworkStream stream = connectedTcpClient.GetStream()) { 						
						int length; 						
						// Read incomming stream into byte array. 						
						while ((length = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0) { 							
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
				fireBullet();
                break;
            case "Start":
				if(lock_start_game == false){
                	StartGame();
					SendMessage("Game started with " + startedNumberTargetPrefabs + " targets.");;
				} else {
					SendMessage("Game already started with " + startedNumberTargetPrefabs + " targets.");;
				}
                break;
            case "Forward":
				moveForward();
				SendMessage("Player tried and succeeded");
				break;
            case "Backward":
				moveBackward();
				SendMessage("Player tried and succeeded");
				break;
            case "Left":
				moveLeft();
				SendMessage("Player tried and succeeded");
				break;
            case "Right":
				moveRight();
				SendMessage("Player tried and succeeded");
				break;
            case "Down":
				downCanon();
				SendMessage("Player tried and succeeded");
				break;
            case "Up":
				upCanon();
				SendMessage("Player tried and succeeded");
				break;
            case "SpinR":
				spinRight();
				SendMessage("Player tried and succeeded");
				break;
            case "SpinL":
				spinLeft();
				SendMessage("Player tried and succeeded");
				break;
            default:
				SendMessage("What are you trying to do?");
                break;
        }
    }

    private void SendMessage(string msg) { 		
		if (connectedTcpClient == null) {             
			return;         
		}  		
		try { 			
			// Get a stream object for writing 			
			stream = connectedTcpClient.GetStream(); 			
			if (stream.CanWrite) {                 
				string serverMessage = msg; 			
				// Convert string message to byte array                 
				byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(serverMessage); 				
				// Write byte array to socketConnection stream               
				stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);               
				//Debug.Log("Server sent his message - should be received by client");           
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
        startedNumberTargetPrefabs = UnityEngine.Random.Range(5, 15);
        for (int i = 0; i < startedNumberTargetPrefabs; i++) {
            yield return SpawnSingleTarget();
        }
        yield return null;
    }

    private IEnumerator SpawnEnviromentGame() {
        var enviroment = Instantiate(environmentGame, environmentGame.transform.position, environmentGame.transform.rotation);

        yield return null;
    }

    private void StartGame() {      
        StartCoroutine(SpawnEnviromentGame());
        StartCoroutine(SpawnTargets());
        lock_start_game = true;
    }

	private void moveForward() {
		// // Find the tankplayer
		tankPlayer = FindObjectOfType<Tank>();
        if (tankPlayer != null){
			tankPlayer.MoveTank(1);
			tankPlayer.RotateWheels(1, 0);
        }
	}
	private void moveBackward() {
		// // Find the tankplayer
		tankPlayer = FindObjectOfType<Tank>();
        if (tankPlayer != null){
			tankPlayer.MoveTank(-1);
			tankPlayer.RotateWheels(-1, 0);
        }
	}
	private void moveLeft() {
		// // Find the tankplayer
		tankPlayer = FindObjectOfType<Tank>();
        if (tankPlayer != null){
			tankPlayer.RotateTank(-1);
			tankPlayer.RotateWheels(0, -1);
        }
	}
	private void moveRight() {
		// // Find the tankplayer
		tankPlayer = FindObjectOfType<Tank>();
        if (tankPlayer != null){
			tankPlayer.RotateTank(1);
			tankPlayer.RotateWheels(0, 1);
        }
	}
	private void spinRight() {
		// // Find the tank tower
		tankTower = FindObjectOfType<SpinTankTower>();
        if (tankTower != null){
			tankTower.SpinTank(1);
        }
	}
	private void spinLeft() {
		// // Find the tankplayer
		tankTower = FindObjectOfType<SpinTankTower>();
        if (tankTower != null){
			tankTower.SpinTank(-1);
        }
	}
	private void upCanon() {
		// // Find the tankplayer
		tankCanon = FindObjectOfType<UpDownCanon>();
        if (tankCanon != null){
			tankCanon.MoveVertically(1);
        }
	}
	private void downCanon() {
		// // Find the tankplayer
		tankCanon = FindObjectOfType<UpDownCanon>();
        if (tankCanon != null){
			tankCanon.MoveVertically(-1);
        }
	}

	private void fireBullet() {
		// Find all effects in scene
        GameObject[] destroyEffects = GameObject.FindGameObjectsWithTag("DestroyEffect");
		numberOfTargetEffects = destroyEffects.Length;
		
		bulletSpawn = FindObjectOfType<BulletSpawn>();
		if (bulletSpawn != null){
			bulletSpawn.SpawnBullet();
			numberOfBullets += 1;

        } else {
			SendMessage("Error while trying to fire!!");
		}

		// Find all effects in scene
		destroyEffects = GameObject.FindGameObjectsWithTag("DestroyEffect");

		// Check if hit
		if (numberOfTargetEffects < destroyEffects.Length) {
			currentNumberTargetPrefabs -= 1;
			// Check if there is not more targets
			if (currentNumberTargetPrefabs == 0) {
				// end game function()
				SendMessage("You hit the target!! No more targets left.");
			} else {
				SendMessage("You hit the target!! " + currentNumberTargetPrefabs + "targets left.");
			}
		} else {
			SendMessage("You missed the target!!");
		}
	}

	public void hitTarget() {

	}
}
