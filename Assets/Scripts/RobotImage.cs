/* 
 * @brief ARSEA Project
 * @author Miquel Massot Campos
 * @author System, Robotics and Vision
 * @author University of the Balearic Islands
 */

using ROSBridgeLib;
using ROSBridgeLib.sensor_msgs;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;


public class RobotImage : ROSBridgeSubscriber
{
    public new static string GetMessageTopic()
    {
        return "/stereo_down/scaled_x4/left/image_rect_color";
    }

    public new static string GetMessageType()
    {
        return "sensor_msgs/Image";
    }

    public new static ROSBridgeMsg ParseMessage(JSONNode msg)
    {
        return new ImageMsg(msg);
    }

    public new static void CallBack(ROSBridgeMsg msg)
    {
        ImageMsg image = (ImageMsg)msg;
        byte[] color_data = image.GetImage();
        int height = (int)image.GetHeight();
        int width = (int)image.GetWidth();
        int step = (int)image.GetRowStep();
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        Color[] colors = new Color[width * height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                byte B = color_data[j * step + i * 3];
                byte G = color_data[j * step + i * 3 + 1];
                byte R = color_data[j * step + i * 3 + 2];
                Color c = new Color((float)R / 255.0f, (float)G / 255.0f, (float)B / 255.0f);
                colors[j * width + i] = c;
                //tex.SetPixel(i, height - j, c);
            }
        }
        tex.SetPixels(colors);
        tex.Apply();
        GameObject cam_image = GameObject.Find("CameraImage");
        cam_image.GetComponent<RawImage>().texture = tex;
    }
}

