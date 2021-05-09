using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace question_4
{
    class ViewModel : BindableBase
    {
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand ChooseFolderCommand { get; private set; }


        private ObservableCollection<File> _files;
        //private ICommand _getFiles;

        public ObservableCollection<File> Files
        {
            get { return _files; }
            set
            {
                SetProperty(ref _files, value);
            }
        }
        private int _searchProgress;
        public int SearchProgress
        {
            get { return _searchProgress; }
            set
            {
                SetProperty<int>(ref _searchProgress, value);


            }
        }


        private string _folder;
        public string Folder {
            get
            {
                return _folder;
            }
            set
            {
                SetProperty(ref _folder, value);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }


        private string _nameToSearch;
        public string NameToSearch
        {
            get
            {
                return _nameToSearch;
            }
            set
            {
                SetProperty(ref _nameToSearch, value);
                SearchCommand.RaiseCanExecuteChanged();

            }
        }
        public ViewModel()
        {

            ChooseFolderCommand = new DelegateCommand(ChooseFolder);
            SearchCommand = new DelegateCommand(Search, CanSearch).ObservesProperty<int>(() => SearchProgress);
            Files = new ObservableCollection<File>();
        }

        private bool CanSearch()
        {
            if (string.IsNullOrEmpty(Folder) || string.IsNullOrEmpty(NameToSearch))
                return false;
            return true;
        }

        private void Search()
        {
            Task.Run(() => { FindFiles(Folder, 100); });
        }

        private void ChooseFolder()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Folder;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Folder = dialog.FileName;
            }


        }
        
        void FindFiles(string path, int progress)
        {
        
    
            string[] files = Directory.GetFiles(path);
            string[] folders = Directory.GetDirectories(path);
            if (folders.Length + files.Length != 0)
            {
                object[] pram = new object[] {
                 files,
                 progress/(folders.Length+files.Length)
            };

                Application.Current.Dispatcher.Invoke(new addFileDelegate(addFile), pram);
            } 
            if (folders.Length > 0)
            {

                foreach (var item in folders)
                {
                    FindFiles(item, progress / folders.Length + files.Length);
                }
            }
        }
        public delegate void addFileDelegate(string[] files, int progress);
        public void addFile(string[] files,int progress)
        {
            //cheack if the inti folder is empty
            if (files.Length==0)
                SearchProgress += progress;

            else
            {
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Contains(NameToSearch))
                        Files.Add(new File(files[i]));
                    SearchProgress += progress;
                }
            }
        }

     

    }
}
