using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            Members = new ObservableCollection<MemberInfo>();
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
        
        public string MemberSearch
        {
            get => _memberSearch;
            set
            {
                _memberSearch = value;
                Members.Clear();
                Members.AddRange(_service.MemberInfos(_memberSearch));
            }
        }

        public ICommand Update =>
            new DefaultCommand((obj) =>
            {
                MemberSearch = string.Empty;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MemberSearch)));
            });
        
    }
}