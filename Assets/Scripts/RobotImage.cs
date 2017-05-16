/* 
 * @brief ARSEA Project
 * @author Francisco Bonin Font, Miquel Massot Campos
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

 using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using ROSBridgeLib.geometry_msgs;
using ROSBridgeLib.sensor_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;


public class RobotImage : ROSBridgeSubscriber
{
    

  public new static string GetMessageTopic()
  {
    return "/stereo_down/scaled_x2/left/image_color";
  }

  public new static string GetMessageType()
  {
    return "sensor_msgs/Image";
  }

  public new static ROSBridgeMsg ParseMessage(JSONNode msg)
  {
        Debug.Log("<color=green>INFO:</color> RobotImage parser!");
        return new ImageMsg(msg);
    }

    public new static void CallBack(ROSBridgeMsg msg)
  {
    Debug.Log("<color=green>INFO:</color> RobotImage CallBack!");
    ImageMsg image = (ImageMsg)msg;
    byte[] color_data = image.GetImage();
    int n_rows = (int)image.GetHeight();
    int n_columns = (int)image.GetWidth();
    int step = (int)image.GetRowStep();
    int data_size = n_rows * step;

        //byte[] bImportedTexture = System.BitConverter.GetBytes(color_data);

        // Create a texture. Texture size does not matter, since
        // LoadImage will replace with with incoming image size.
        Texture2D tex = new Texture2D(n_rows, n_columns, TextureFormat.RGB24, false);

        int count = 0;
        for (int i = 0; i < n_rows; i++)
        {
            for (int j = 0; j < n_columns; j++)
            {
                byte B = color_data[i * step + j];
                byte G = color_data[i * step + j + 1];
                byte R = color_data[i * step + j + 2];
                Color c = new Color((float)R / 255.0f, (float)G / 255.0f, (float)B / 255.0f);
                tex.SetPixel(i, j, c);
            }
        }

        //tex.LoadRawTextureData(color_data);
        tex.Apply();
        //tex.LoadImage(bImportedTexture);  // No funciona

        GameObject cam_image = GameObject.Find("CameraImage");
        cam_image.GetComponent<RawImage>().texture = tex;

        //ArseaViewer viewer = (ArseaViewer)robot.GetComponent(typeof(ArseaViewer));
        //viewer.Paint_image(color_data, data_size,n_rows, n_columns);


    }
}

