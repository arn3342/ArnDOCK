using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace dock
{
    public class ExecManager
    {
        public void GetExe(string filePath, string destPath)
        {
            string filename = Path.GetFileName(filePath);
            GenIcon(filePath, destPath);
            string[] lines = new string[] { filename, filePath };
            File.WriteAllLines(destPath + filename + ".data" , lines);
        }

     
            
    public void GenIcon(string filePath , string destPath)
    {
            Icon TheIcon = IconFromFilePath(filePath);
            string filename = Path.GetFileNameWithoutExtension(filePath);
            if (TheIcon != null)
            {
                Bitmap bi = TheIcon.ToBitmap();
                const float limit = 1f;
                for (int i = 0; i < bi.Width; i++)
                {
                    for (int j = 0; j < bi.Height; j++)
                    {
                        System.Drawing.Color c = bi.GetPixel(i, j);
                        if (c.GetBrightness() > limit)
                        {
                            bi.SetPixel(i, j, System.Drawing.Color.Transparent);
                        }
                    }
                }
                bi.Save(destPath + filename + ".imgdata");
            }
        }

        public static Icon IconFromFilePath(string filePath)
        {
            Icon result = null/* TODO Change to default(_) if this is not a reference type */;
            try
            {
                result = Icon.ExtractAssociatedIcon(filePath);
            }
            catch
            {
            }// '# swallow and return nothing. You could supply a default Icon here as well
            return result;
        }
        public void RunProcess(string ProcessName, string RunAs, string additionalPath)
        {
            List<string> file = new List<string>();
            if (additionalPath.Length <= 0)
            {
                file = new List<string>(File.ReadLines(ProcessName));
            }
            else
            {
                file = new List<string>(File.ReadLines(additionalPath + ProcessName));
            }
            if (RunAs == "")
            {
                Process.Start(file[1]);
            }
        }
        public void ReplaceIcon(string ProcessName, string IconPath)
        {
            string filename = Path.GetFileNameWithoutExtension(ProcessName);
            filename = filename.Replace(".exe", "");
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ArnDESK\\ArnDOCK\\" + filename + ".imgdata");
            File.Copy(IconPath, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ArnDESK\\ArnDOCK\\" + filename + ".imgdata");
        }
        public void RemoveApp(string filename, string additionalPath)
        {
          string MainFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +"\\ArnDESK\\SliderWidget\\" + additionalPath + filename;
          string fname = Path.GetFileNameWithoutExtension(MainFile);
          File.Delete(MainFile.Replace(filename, fname) + ".imgdata");
          File.Delete(MainFile + ".data");
        }
    }
}
