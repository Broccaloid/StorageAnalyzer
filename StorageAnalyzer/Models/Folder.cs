﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StorageAnalyzer.Models
{
    public class Folder : BaseItem, IExpandable
    {
        public Folder(string fullPath) : base(fullPath)
        {
            SetSizeAsync();
        }

        private async Task SetSizeAsync()
        {
            Size = await GetSizeAsync();
        }
        private async Task<long> GetSizeAsync()
        {
            long size = 0;
            foreach (var file in await GetAllFilesAsync(FullPath))
            {
                size += file.Length;
            }
            return size;
        }

        public List<IItem> GetChildrenItems()
        {
            var childrenItems = new List<IItem>();
            try
            {
                var drive = new DirectoryInfo(FullPath);
                foreach (var dir in drive.GetDirectories())
                {
                    childrenItems.Add(new Folder(dir.FullName));
                }
                foreach (var file in drive.GetFiles())
                {
                    childrenItems.Add(new File(file.FullName));
                }
            }
            catch (UnauthorizedAccessException e)
            {
                itemLogger.Error($"Can't get an access to a folder. Exception message: {e.Message}");
            };
            return childrenItems;
        }

        private async Task<List<DirectoryInfo>> GetAllDirectoriesAsync(string path)
        {
            var mainFolder = new DirectoryInfo(path);
            var allFolders = new List<DirectoryInfo>() { mainFolder };
            var tasks = new List<Task<List<DirectoryInfo>>>();
            try
            {
                foreach (var directory in mainFolder.GetDirectories())
                {
                    try
                    {
                        if (directory.GetDirectories().Length != 0)
                        {
                            tasks.Add(GetAllDirectoriesAsync(directory.FullName));
                        }
                        else
                        {
                            allFolders.Add(directory);
                        }
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        itemLogger.Error($"Can't get an access to a folder. Exception message: {e.Message}");
                    }
                }
                var result = await Task.WhenAll(tasks);
                foreach (var item in result)
                {
                    allFolders.AddRange(item);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                itemLogger.Error($"Can't get an access to a folder. Exception message: {e.Message}");
            }
            return allFolders;
        }

        private async Task<List<FileInfo>> GetAllFilesAsync(string path)
        {
            var allFiles = new List<FileInfo>();
            foreach (var folder in await GetAllDirectoriesAsync(path))
            {
                try
                {
                    foreach (var file in folder.GetFiles())
                    {
                        allFiles.Add(file);
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    itemLogger.Error($"Can't get an access to a folder. Exception message: {e.Message}");
                }
            }
            return allFiles;
        }
    }
}
