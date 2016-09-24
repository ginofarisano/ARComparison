using UnityEngine;
using System.Collections;
using Vuforia;

///This class activate and disactivete object target at runtime.
///Every target have a DynamicDataSetLoader script to handle this function
/// Load dataset is a very expensive operarion. So I load all dataset the first time and
/// after i stop/active the tracker. Note it is possible riuse a scene, see <see cref="Magnet"/> class.
/// So handle the activation/disactivation here 
public class DataSetHandler : MonoBehaviour
{

	private Magnet magnet;
	private MyUI ui;
	private DynamicDataSetLoader SchedaMadreTop1;
	private DynamicDataSetLoader SchedaMadreCMOSSide1;
	private DynamicDataSetLoader SchedaMadreRamSide1;
	private DynamicDataSetLoader SchedaMadreCPUSide1;
	private DynamicDataSetLoader SchedaMadreInputComponentSide1;
	private DynamicDataSetLoader RamFirstHalf;
	private DynamicDataSetLoader RamSecondHalf;
	private DynamicDataSetLoader CPU;
	private DynamicDataSetLoader Ethernet;
	private DynamicDataSetLoader VGA;
	private bool[] flagIsNotTheFirst;

	void loadAllDataset ()
	{
		SchedaMadreTop1.LoadDataSet (false);
		SchedaMadreCMOSSide1.LoadDataSet (false);
		SchedaMadreRamSide1.LoadDataSet (false);
		SchedaMadreCPUSide1.LoadDataSet (false);
		SchedaMadreInputComponentSide1.LoadDataSet (false);
		RamFirstHalf.LoadDataSet (true);
		RamSecondHalf.LoadDataSet (true);
		Ethernet.LoadDataSet (true);
		CPU.LoadDataSet (true);
		VGA.LoadDataSet (true);
	}

	public void stopLastTracker ()
	{

		RamFirstHalf.stopTracker ();
		RamSecondHalf.stopTracker ();
		Ethernet.stopTracker ();
		CPU.stopTracker ();
		VGA.stopTracker ();
	}
	
	// Use this for initialization
	void Start ()
	{
		//start initialization
		magnet = (Magnet)GameObject.FindWithTag ("Magnet").GetComponent<Magnet> ();

		ui = (MyUI)GameObject.FindWithTag ("UserInterface").GetComponent<MyUI> ();
		
		SchedaMadreTop1 = GameObject.FindWithTag ("SchedaMadreTop1Target").GetComponent<DynamicDataSetLoader> ();

		SchedaMadreCMOSSide1 = GameObject.FindWithTag ("SchedaMadreCMOSSide1Target").GetComponent<DynamicDataSetLoader> ();

		SchedaMadreRamSide1 = GameObject.FindWithTag ("SchedaMadreRamSide1Target").GetComponent<DynamicDataSetLoader> ();

		SchedaMadreInputComponentSide1 = GameObject.FindWithTag ("SchedaMadreInputComponentSide1Target").GetComponent<DynamicDataSetLoader> ();

		SchedaMadreCPUSide1 = GameObject.FindWithTag ("SchedaMadreCPUSide1Target").GetComponent<DynamicDataSetLoader> ();

		RamFirstHalf = GameObject.FindWithTag ("RamFirstHalfTarget").GetComponent<DynamicDataSetLoader> ();
		
		RamSecondHalf = GameObject.FindWithTag ("RamSecondHalfTarget").GetComponent<DynamicDataSetLoader> ();

		CPU = GameObject.FindWithTag ("CPUTarget").GetComponent<DynamicDataSetLoader> ();

		Ethernet = GameObject.FindGameObjectWithTag ("EthernetTarget").GetComponent<DynamicDataSetLoader> ();
		
		VGA = GameObject.FindWithTag ("VGATarget").GetComponent<DynamicDataSetLoader> ();

		flagIsNotTheFirst = new bool[ui.LABEL.Length];

		loadAllDataset ();

		//end initialization

	}
	
	// Update is called once per frame
	void Update ()
	{

		switch (magnet.GetCount ()) {

		case 4:

			if (!flagIsNotTheFirst [4]) {

				SchedaMadreTop1.stopTracker ();
				SchedaMadreCMOSSide1.stopTracker ();
				SchedaMadreRamSide1.stopTracker ();
				SchedaMadreCPUSide1.stopTracker ();
				SchedaMadreInputComponentSide1.stopTracker ();

				RamFirstHalf.startTracker ();
				RamSecondHalf.startTracker ();

				flagIsNotTheFirst [4] = true;

			}
		
			break;



		case 5:
		
			if (!flagIsNotTheFirst [5]) {
  
				RamFirstHalf.stopTracker ();
				RamSecondHalf.stopTracker ();
			
				SchedaMadreTop1.startTracker ();
				SchedaMadreCMOSSide1.startTracker ();
				SchedaMadreRamSide1.startTracker ();
				SchedaMadreCPUSide1.startTracker ();
				SchedaMadreInputComponentSide1.startTracker ();

				flagIsNotTheFirst [5] = true;

			}
		
			break;

		case 6:

			if (!flagIsNotTheFirst [6]) {

				RamFirstHalf.stopTracker ();
				RamSecondHalf.stopTracker ();

				SchedaMadreTop1.startTracker ();
				SchedaMadreCMOSSide1.startTracker ();
				SchedaMadreRamSide1.startTracker ();
				SchedaMadreCPUSide1.startTracker ();
				SchedaMadreInputComponentSide1.startTracker ();

				flagIsNotTheFirst [6] = true;

			}

			break;

		case 15:
			
			if (!flagIsNotTheFirst [15]) {

				SchedaMadreTop1.stopTracker ();
				SchedaMadreCMOSSide1.stopTracker ();
				SchedaMadreRamSide1.stopTracker ();
				SchedaMadreCPUSide1.stopTracker ();
				SchedaMadreInputComponentSide1.stopTracker ();
				
				CPU.startTracker ();
				
				flagIsNotTheFirst [15] = true;

			}
			
			break;
		
	

		case 16:
		
			if (!flagIsNotTheFirst [16]) {

				CPU.stopTracker ();

				SchedaMadreTop1.startTracker ();
				SchedaMadreCMOSSide1.startTracker ();
				SchedaMadreRamSide1.startTracker ();
				SchedaMadreCPUSide1.startTracker ();
				SchedaMadreInputComponentSide1.startTracker ();
			
				flagIsNotTheFirst [16] = true;

			}
		
			break;

		case 17:

			if (!flagIsNotTheFirst [17]) {

				CPU.stopTracker ();

				SchedaMadreTop1.startTracker ();
				SchedaMadreCMOSSide1.startTracker ();
				SchedaMadreRamSide1.startTracker ();
				SchedaMadreCPUSide1.startTracker ();
				SchedaMadreInputComponentSide1.startTracker ();

				flagIsNotTheFirst [17] = true;

			}

			break;

		case 23:
			
			if (!flagIsNotTheFirst [23]) {
			
				SchedaMadreCMOSSide1.stopTracker ();
				SchedaMadreRamSide1.stopTracker ();
				SchedaMadreCPUSide1.stopTracker ();
				SchedaMadreInputComponentSide1.stopTracker ();
				
				flagIsNotTheFirst [23] = true;
				
			}
			
			break;

		case 24:

			if (!flagIsNotTheFirst [24]) {

				SchedaMadreCMOSSide1.stopTracker ();
				SchedaMadreRamSide1.stopTracker ();
				SchedaMadreCPUSide1.stopTracker ();
				SchedaMadreInputComponentSide1.stopTracker ();

				flagIsNotTheFirst [24] = true;

			}

			break;

		case 25:
			
			if (!flagIsNotTheFirst [25]) {

				Ethernet.startTracker ();

				SchedaMadreTop1.stopTracker ();
				SchedaMadreCMOSSide1.stopTracker ();
				SchedaMadreRamSide1.stopTracker ();
				SchedaMadreCPUSide1.stopTracker ();
				SchedaMadreInputComponentSide1.stopTracker ();

				flagIsNotTheFirst [25] = true;

			}

			break;

		case 26:

			if (!flagIsNotTheFirst [26]) {

				Ethernet.stopTracker ();

				SchedaMadreTop1.startTracker ();
				SchedaMadreCMOSSide1.startTracker ();
				SchedaMadreRamSide1.startTracker ();
				SchedaMadreCPUSide1.startTracker ();
				SchedaMadreInputComponentSide1.startTracker ();

				flagIsNotTheFirst [26] = true;

			}
			
			break;

		case 27:
			
			if (!flagIsNotTheFirst [27]) {
 
				SchedaMadreTop1.stopTracker ();

				VGA.startTracker ();
				
				flagIsNotTheFirst [27] = true;
			
			}
			
			break;

		case 28:
			
			if (!flagIsNotTheFirst [28]) {

				VGA.stopTracker ();

				SchedaMadreTop1.startTracker ();
				SchedaMadreCMOSSide1.startTracker ();
				SchedaMadreRamSide1.startTracker ();
				SchedaMadreCPUSide1.startTracker ();
				SchedaMadreInputComponentSide1.startTracker ();

				flagIsNotTheFirst [28] = true;

			}
			
			break;

		case 29:

			if (!flagIsNotTheFirst [29]) {

				SchedaMadreTop1.startTracker ();
				SchedaMadreCMOSSide1.startTracker ();
				SchedaMadreRamSide1.startTracker ();
				SchedaMadreCPUSide1.startTracker ();
				SchedaMadreInputComponentSide1.startTracker ();

				flagIsNotTheFirst [29] = true;

			}

			break;


		case 30:
		
			if (!flagIsNotTheFirst [30]) {
			
				Ethernet.stopTracker ();
			
				SchedaMadreTop1.startTracker ();
				SchedaMadreCMOSSide1.startTracker ();
				SchedaMadreRamSide1.startTracker ();
				SchedaMadreCPUSide1.startTracker ();
				SchedaMadreInputComponentSide1.startTracker ();
			
				flagIsNotTheFirst [30] = true;
			
			}

			break;

		case 31:
		
			if (!flagIsNotTheFirst [31]) {
			
				VGA.stopTracker ();
			
				SchedaMadreTop1.startTracker ();
				SchedaMadreCMOSSide1.startTracker ();
				SchedaMadreRamSide1.startTracker ();
				SchedaMadreCPUSide1.startTracker ();
				SchedaMadreInputComponentSide1.startTracker ();
			
				flagIsNotTheFirst [31] = true;
			
			}
			break;

		}

	}
}
