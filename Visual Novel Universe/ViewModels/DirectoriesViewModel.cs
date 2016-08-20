using System.Collections.ObjectModel;
using Caliburn.Micro;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed class DirectoriesViewModel : Screen
    {
        public ObservableCollection<string> ActiveDirectories { get; set; } = new ObservableCollection<string>();

        private string _SelectedItem;
        public string SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        public DirectoriesViewModel()
        {
            DisplayName = "Root Directories";
        }

        public void Add()
        {
            var Dialog = new CommonOpenFileDialog
            {
                Title = "Add Root Directory",
                IsFolderPicker = true,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = @"C:\",
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (Dialog.ShowDialog() != CommonFileDialogResult.Ok) return;

            ActiveDirectories.Add(Dialog.FileName);
        }

        public void Remove()
        {
            ActiveDirectories.Remove(SelectedItem);
        }

        public void Apply()
        {
            TryClose();
        }
    }
}
