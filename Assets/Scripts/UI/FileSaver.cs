using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class FileSaver : MonoBehaviour
{
    //Path to excel file
    private string path = "Assets\\Save Files\\saveFile.csv";

    //Using TMP Pro
    //Gender
    public TMPro.TMP_Dropdown gender;
    //Age
    public TMPro.TMP_Dropdown age;
    //Method of transport
    public TMPro.TMP_Dropdown metTrans;
    public TMPro.TMP_InputField metTransMore;
    //Childh. neigh. desc.
    public TMPro.TMP_Dropdown chiNeiDesc;
    //Curr. neigh. desc.
    public TMPro.TMP_Dropdown currNeiDesc;
    //Time walking in street
    public TMPro.TMP_Dropdown timeInStreet;
    //Number of times walking more than 10 min
    public TMPro.TMP_Dropdown timeOf10Min;
    //Primary purpose to walk
    public TMPro.TMP_Dropdown walkingPurpose;
    public TMPro.TMP_InputField walkingPurposeMore;

    /*//Greenery Rating
    public TMPro.TMP_Dropdown greenery;
    //Building Diversity Rating
    public TMPro.TMP_Dropdown buildingDiversity;
    //Setback Rating
    public TMPro.TMP_Dropdown setback;
    //Block length Rating
    public TMPro.TMP_Dropdown blockLength;
    //Enclosure Rating
    public TMPro.TMP_Dropdown enclosure;
    //Public space Rating
    public TMPro.TMP_Dropdown publicSpace;
    //Building density Rating
    public TMPro.TMP_Dropdown buildingDensity;




    //Using Legacy
    //Gender
    public Dropdown gender;
    //Age
    public Dropdown age;
    //Method of transport
    public Dropdown metTrans;
    public InputField metTransMore;
    //Childh. neigh. desc.
    public Dropdown chiNeiDesc;
    //Curr. neigh. desc.
    public Dropdown currNeiDesc;
    //Time walking in street
    public Dropdown timeInStreet;
    //Number of times walking more than 10 min
    public Dropdown timeOf10Min;
    //Primary purpose to walk
    public Dropdown walkingPurpose;
    public InputField walkingPurposeMore;*/


    //Building Diversity Rating
    public Dropdown buildingDiversity;
    //Building density Rating
    public Dropdown buildingDensity;
    //Block length Rating
    public Dropdown blockLength;
    //Greenery Rating
    public Dropdown greenery;
    //Enclosure & Setback Rating
    public Dropdown enclosure_Setback;
    //Public space Rating
    public Dropdown publicSpace;
    

    //user ID auto-increment
    int id = 1;

    public bool writeTofile()
    {
        Debug.Log("Saving to csv file!");
        try
        {

            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }


            bool fileExists = File.Exists(path);

            
            if (!fileExists)    // Create CSV header if the file doesn't exist
            {
                string header = "User ID," +
                    "Gender," +
                    "Age," +
                    "Method of transport," +
                    "Childhood Neighborhood description," +
                    "Current Neighborhood description," +
                    "Time Spent Walking per day," +
                    "Times walking more than 10," +
                    "Primary purpose for walking," +
                    "Greenery Rating," +
                    "Building diversity Rating," +
                    "Enclosure & SetBack Rating," +
                    "Block Length Rating," +
                    "Public Space Rating," +
                    "Building Density Rating";
                File.AppendAllText(path, header + "\n", Encoding.UTF8);
            }


            string newRow = string.Join(",", new string[]
            {
                id.ToString(), // Auto-incremented User ID
                gender.options[gender.value].text,
                age.options[age.value].text,
                metTrans.options[metTrans.value].text + (metTrans.value == metTrans.options.Count - 1 ? " - " + metTransMore.text : ""),
                chiNeiDesc.options[chiNeiDesc.value].text,
                currNeiDesc.options[currNeiDesc.value].text,
                timeInStreet.options[timeInStreet.value].text,
                timeOf10Min.options[timeOf10Min.value].text,
                walkingPurpose.options[walkingPurpose.value].text + (walkingPurpose.value == walkingPurpose.options.Count - 1 ? " - " + walkingPurposeMore.text : ""),
                greenery.options[greenery.value].text,
                buildingDiversity.options[buildingDiversity.value].text,
                enclosure_Setback.options[enclosure_Setback.value].text,
                blockLength.options[blockLength.value].text,
                publicSpace.options[publicSpace.value].text,
                buildingDensity.options[buildingDensity.value].text,
            });

            File.AppendAllText(path, newRow + "\n", Encoding.UTF8);

            //Incrementing Id
            this.id++;

            return true;
        }
        catch (System.Exception e) // Failure in writing
        {
            Debug.LogError("Error writing to CSV: " + e.Message);
            return false; 
        }
    }
}
