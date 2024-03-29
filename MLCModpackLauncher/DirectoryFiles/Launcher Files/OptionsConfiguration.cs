﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MLCModpackLauncher
{
    public class OptionsConfiguration
    {
        /// <summary>
        /// Live Game Installation Folder.  Default is C:\USER\AppData\Roaming\.minecraft\
        /// </summary>
        public string MinecraftDirectory { get; private set; }
        /// <summary>
        /// Download Directory for PTR files. Default is C:\USER\Desktop
        /// </summary>
        public string PTRDirectory { get; private set; }

        public OptionsConfiguration()
        {
            MinecraftDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft\\");
            PTRDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        public OptionsConfiguration(string mcDirectory, string ptrDirectory)
        {
            MinecraftDirectory = mcDirectory;
            PTRDirectory = ptrDirectory;
        }

        public void SetMCDirectory(string directory)
        {
            char[] dirChars = directory.ToCharArray();
            if(dirChars.Last() == '\\')
            {
                MinecraftDirectory = directory;
            }
            else
            {
                MinecraftDirectory = directory + "\\";
            }
        }

        public void SetPTRDirectory(string directory)
        {
            char[] dirChars = directory.ToCharArray();
            if (dirChars.Last() == '\\')
            {
                PTRDirectory = directory;
            }
            else
            {
                PTRDirectory = directory + "\\";
            }
        }
    }
}
