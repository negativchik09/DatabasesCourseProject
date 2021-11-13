using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using BD_CourseProject.UI.Commands;
using BD_CourseProject.BL.Entities;
using BD_CourseProject.BL.Interfaces;
using BD_CourseProject.BL.Services;

namespace BD_CourseProject.UI.ViewModels
{
    public class MembersTabViewModel  : INotifyPropertyChanged
    {
        private readonly IMemberService _service;
        private MemberInfo _selectedMember;
        private string _memberSearch;

        public MembersTabViewModel()
        {
            _service = new MemberService();
            Members = new ObservableCollection<MemberInfo>(_service.MemberInfos(_memberSearch));
            Stats = new ObservableCollection<RecordModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MemberInfo> Members { get; set; }

        public ObservableCollection<RecordModel> Stats { get; set; }

        public MemberInfo SelectedMember
        {
            get => _selectedMember;
            set
            {
                _selectedMember = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMember)));
            }
        }
        
        private static bool IsMemberSelected(object? obj) => obj is MemberInfo;
        
        public string MemberSearch
        {
            get => _memberSearch;
            set => UpdateDataFunction(value);
        }

        public ICommand UpdateData =>
            new DefaultCommand((obj) => UpdateDataFunction());

        private void UpdateDataFunction(string search = "")
        {
            _memberSearch = search;
            Members.Clear();
            Members.AddRange(_service.MemberInfos(_memberSearch));
        }
        
        private ICommand _addCommand;
        public ICommand Add => _addCommand ??=
            new DefaultCommand(CreateExecute);

        private void CreateExecute(object? obj)
        {
            ShowMemberEditDialog(new MemberData(), _service.AddMember);
            UpdateDataFunction();
        }

        private ICommand _updateCommand;
        public ICommand Update => _updateCommand ??=
            new DefaultCommand(UpdateExecute);

        private void UpdateExecute(object? obj)
        {
            if (!IsMemberSelected(obj)) return;
            ShowMemberEditDialog(obj as MemberData, _service.UpdateMember);
            UpdateDataFunction();
        }

        private void ShowMemberEditDialog(MemberData data, Action<MemberData> action)
        {
            if (data.DateOfBirth == DateTime.MinValue) data.DateOfBirth = DateTime.Today;
            var window = new MemberView()
            {
                Owner = Application.Current.MainWindow,
                Id = data.Id,
                DateOfBirth = data.DateOfBirth,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Role = data.Role
            };

            if (window.ShowDialog() == true)
            {
                data.Id = window.Id;
                data.FirstName = window.FirstName;
                data.LastName = window.LastName;
                data.DateOfBirth = window.DateOfBirth;
                data.Role = window.Role;
                action(data);
            }
        }

        public ICommand Delete => new DefaultCommand(
            DeleteCommandExecute);

        private void DeleteCommandExecute(object? obj)
        {
            if (!IsMemberSelected(obj)) return;
            _service.RemoveMember(obj as MemberData);
            UpdateDataFunction();
        }
    }
}