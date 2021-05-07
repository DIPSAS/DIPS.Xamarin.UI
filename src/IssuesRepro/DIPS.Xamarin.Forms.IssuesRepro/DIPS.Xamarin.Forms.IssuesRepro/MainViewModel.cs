using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.Forms.IssuesRepro
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<IssueViewModel> m_allIssues;
        private string m_searchText;
        private List<IssueViewModel> m_issues;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            Issues = GetIssues();
            m_allIssues = Issues.ToList();
        }

        public string SearchText
        {
            get => m_searchText;
            set
            {
                PropertyChanged.RaiseWhenSet(ref m_searchText, value);
                OnSearch(value);
            }
        }


        private async void OnSearch(string text)
        {
            await Task.Delay(300);
            if (text != SearchText)
            {
                return;
            }

            Issues = m_allIssues.Where(a => a.Value.ToString().Contains(text.ToLower()) || a.PresentedValue.ToLower().Contains(text.ToLower())).ToList();
        }

        private List<IssueViewModel> GetIssues()
        {
            var issues = new List<IssueViewModel>();
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(Issue), true).Length > 0)
                {
                    var issue = type.GetCustomAttributes(typeof(Issue), true).First() as Issue;
                    issues.Add(new IssueViewModel(issue.Id, () =>
                    {
                        var page = Activator.CreateInstance(type) as Page;
                        page.Title = issue.Id.ToString();
                        Application.Current.MainPage.Navigation.PushAsync(page);
                    }));
                }
            }

            return issues.OrderByDescending(i => i.Value).ToList();
        }

        public List<IssueViewModel> Issues { get => m_issues; private set =>  PropertyChanged.RaiseWhenSet(ref  m_issues, value); }

        public class IssueViewModel : INotifyPropertyChanged
        {
            private IssueService m_service = new IssueService();
            private string m_presentedValue;
            private bool m_isBug;

            public IssueViewModel(int value, Action onTap)
            {
                //Application.Current.MainPage.Navigation.PushAsync(s_issues[value]())
                PresentedValue = "Github issue " + value;
                OnTapCommand = new Command(onTap);
                Value = value;
                Initialize();
            }


            private async void Initialize()
            {
                try
                {
                    var model = await m_service.GetIssueAsync(Value);
                    PresentedValue = model.title;
                    IsBug = model.title.ToLower().Contains("bug");
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            public bool IsBug { get => m_isBug; private set => PropertyChanged.RaiseWhenSet(ref m_isBug, value); }
            public int Value { get; }
            public ICommand OnTapCommand { get; }
            public string PresentedValue { get => m_presentedValue; private set => PropertyChanged.RaiseWhenSet(ref m_presentedValue, value); }
            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}