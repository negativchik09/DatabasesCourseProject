using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        #region Shared Information

        private readonly IMemberService _service;
        private MemberInfo _selectedMember;

        public MembersTabViewModel()
        {
            _service = new MemberService();
            Members = new ObservableCollection<MemberInfo>(_service.MemberInfos(
                new MemberSearchFilter(DateTime.MinValue, DateTime.MaxValue, "")));
            Stats = new ObservableCollection<RecordModel>();
            _periodStart = new DateTime(2010, 1, 1);
            _periodEnd = DateTime.Today;
            Visibility = Visibility.Hidden;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged(string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public ObservableCollection<MemberInfo> Members { get; set; }

        public MemberInfo SelectedMember
        {
            get => _selectedMember;
            set
            {
                _selectedMember = value;
                Stats.Clear();
                if (value == null)
                {
                    FirePropertyChanged(nameof(SelectedMember));
                    return;
                }
                Stats.AddRange(_service.MemberStats(
                    new MemberStatsFilter(SelectedMember.Id))
                );
                if (Stats.Count > 0)
                {
                    MaximalPossibleDate = Stats.Max(x => x.Date);
                    MinimalPossibleDate = Stats.Min(x => x.Date);
                    StartFilterDate = MinimalPossibleDate;
                    EndFilterDate = MaximalPossibleDate;
                }
                Visibility = Visibility.Visible;
                FirePropertyChanged(nameof(SelectedMember));
            }
        }

        private static bool IsMemberSelected(object? obj) => obj is MemberInfo;
        
        #endregion

        #region Members CRUD

        private string _memberSearch;
        public string MemberSearch
        {
            get => _memberSearch;
            set
            {
                _memberSearch = value;
                UpdateDataFunction();
                FirePropertyChanged(nameof(MemberSearch));
            }
        }

        private DateTime _periodStart;

        public DateTime PeriodStart
        {
            get => _periodStart;
            set
            {
                _periodStart = value;
                UpdateDataFunction();
                FirePropertyChanged(nameof(PeriodStart));
            }
        }
        
        private DateTime _periodEnd;

        public DateTime PeriodEnd
        {
            get => _periodEnd;
            set
            {
                _periodEnd = value;
                UpdateDataFunction();
                FirePropertyChanged(nameof(PeriodEnd));
            }
        }

        public ICommand UpdateData =>
            new DefaultCommand((obj) => UpdateDataFunction());

        private void UpdateDataFunction()
        {
            Members.Clear();
            Members.AddRange(_service.MemberInfos(
                new MemberSearchFilter(PeriodStart, PeriodEnd, MemberSearch)));
            Visibility = Visibility.Hidden;
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

            if (window.ShowDialog() != true) return;
            data.Id = window.Id;
            data.FirstName = window.FirstName ?? string.Empty;
            data.LastName = window.LastName ?? string.Empty;
            data.DateOfBirth = window.DateOfBirth;
            data.Role = window.Role;
            action(data);
        }

        public ICommand Delete => new DefaultCommand(
            DeleteCommandExecute);

        private void DeleteCommandExecute(object? obj)
        {
            if (!IsMemberSelected(obj)) return;
            _service.RemoveMember(obj as MemberData);
            UpdateDataFunction();
        }
        
        #endregion

        #region Additional Info

        private Visibility _visibility;

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                FirePropertyChanged(nameof(Visibility));
            }
        }

        public ObservableCollection<RecordModel> Stats { get; set; }

        private string _statsSearchBar;

        public string StatsSearchBar
        {
            get => _statsSearchBar;
            set
            {
                _statsSearchBar = value;
                UpdateStats();
                FirePropertyChanged(nameof(StatsSearchBar));
            }
        }
        
        private DateTime _startFilterDate;

        public DateTime StartFilterDate
        {
            get => _startFilterDate;
            set
            {
                _startFilterDate = value;
                UpdateStats();
                FirePropertyChanged(nameof(StartFilterDate));
            }
        }

        private DateTime _endFilterDate;

        public DateTime EndFilterDate
        {
            get => _endFilterDate;
            set
            {
                _endFilterDate = value;
                UpdateStats();
                FirePropertyChanged(nameof(EndFilterDate));
            }
        }

        private DateTime _minimalPossibleDate;

        public DateTime MinimalPossibleDate
        {
            get => _minimalPossibleDate;
            set
            {
                _minimalPossibleDate = value;
                FirePropertyChanged(nameof(MinimalPossibleDate));
            }
        }
        
        private DateTime _maximalPossibleDate;
        public DateTime MaximalPossibleDate
        {
            get => _maximalPossibleDate;
            set
            {
                _maximalPossibleDate = value;
                FirePropertyChanged(nameof(MaximalPossibleDate));
            }
        }

        private void UpdateStats()
        {
            Stats.Clear();
            Stats.AddRange(
                _service.MemberStats(
                    new MemberStatsFilter(SelectedMember.Id, 
                        StartFilterDate > MinimalPossibleDate ? StartFilterDate : MinimalPossibleDate, 
                        EndFilterDate < MaximalPossibleDate && EndFilterDate > MinimalPossibleDate ? EndFilterDate : MaximalPossibleDate, 
                        StatsSearchBar ?? string.Empty))
                    .OrderBy(r => r.Date)
                );
        }

        #endregion
    }
}