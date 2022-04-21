using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Collections;
using Dummiesman;

public class RunINGP : MonoBehaviour
{
  public string pathToINGPScripts = "C:/Users/User/Desktop/Master/MasterThesisProjects/instant-ngp/scripts";//C:\\Users\\User\\Desktop\\Master\\MasterThesisProjects\\instant-ngp\\scripts";
  public string pythonScript = "py -3.10 runCrop.py ";
  public string INGPmode = "\"nerf\" ";
  public string INGPscene = "C:/Users/User/Desktop/Master/MasterThesisProjects/instant-ngp/data/hmdPics/";
  public string transformsName = "transforms.json ";
  public string INGPsteps = "100 ";
  public string ObjectName = "testCropFromUnity.obj";
  public string command;
  public bool GUI;
  public GameObject loadedObject;

  //py -3.10 runCrop.py --mode "nerf" --scene C:\Users\User\Desktop\Master\MasterThesisProjects\instant-ngp\data\hmdPics\transforms.json --n_steps 1000
  //--save_mesh C:\Users\User\Desktop\Master\MasterThesisProjects\instant-ngp\data\hmdPics\testCrop.ply
  public void Run()
  {
    command = "cd " + pathToINGPScripts + "&" + pythonScript + "--mode " +  INGPmode + "--scene " + INGPscene + transformsName + "--n_steps " +  INGPsteps + "--save_mesh " + INGPscene + ObjectName;
    if (GUI)
      command += " --gui";
    UnityEngine.Debug.Log(command);
    //command = "/C cd C:/Users/User/Desktop/Master/MasterThesisProjects/instant-ngp/scripts&py test.py";
    System.Diagnostics.Process.Start("CMD.exe", "/C " + command); //Start cmd process
  }

  public void LoadObj()
  {
    //file path
    if (!File.Exists(INGPscene + ObjectName))
    {
      UnityEngine.Debug.LogWarning("File doesn't exist.");
    }
    else
    {
      if (loadedObject != null)
        Destroy(loadedObject);
      loadedObject = new OBJLoader().Load(INGPscene + ObjectName);
    }

    //Mesh holderMesh = new Mesh();
    //ObjImporter newMesh = new ObjImporter();
    //holderMesh = newMesh.ImportFile("C:/Users/cvpa2/Desktop/ng/output.obj");

    //MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
    //MeshFilter filter = gameObject.AddComponent<MeshFilter>();
    //filter.mesh = holderMesh;
  }

  //public void run_cmd(string cmd, string args)
  //{
  //  ProcessStartInfo start = new ProcessStartInfo();
  //  start.FileName = path;
  //  start.Arguments = string.Format("{0} {1}", cmd, args);
  //  start.UseShellExecute = false;
  //  start.RedirectStandardOutput = true;
  //  using (Process process = Process.Start(start))
  //  {
  //    using (StreamReader reader = process.StandardOutput)
  //    {
  //      string result = reader.ReadToEnd();
  //      UnityEngine.Debug.Log(result);
  //    }
  //  }
  //}
}
