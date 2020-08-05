using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.Forms.IssuesRepro
{
    public class MainViewModel
    {
        private static readonly Dictionary<string, Func<Page>> s_issues = new Dictionary<string, Func<Page>>
        {
            { "64", () => new Github64.Github64() },
            { "86", () => new Github86.Github86() },
            { "112", () => new Github112.Github112() },
            { "120", () => new Github120.Github120Page() },
            { "123", () => new Github123.Github123() },
            { "137", () => new NewIssue.Github137() },
            { "142", () => new Github142.Github142() },
            { "150", () => new Github150.Github150() },
            { "159", () => new Github159.Github159() },
            { "179", () => new Github179.Github179() },
            { "185", () => new Github185.Github185Page() },
            { "203", () => new Github203.Github203() },
            { "211", () => new Github211.Github211() },
        };

        public List<IssueViewModel> Issues { get; } = s_issues.Keys.Select(l => new IssueViewModel(l)).ToList();

        public class IssueViewModel
        {
            public IssueViewModel(string value)
            {
                Value = value;
                OnTapCommand = new Command(() => Application.Current.MainPage.Navigation.PushAsync(s_issues[value]()));
            }

            public ICommand OnTapCommand { get; }
            public string Value { get; }
            public string PresentedValue => "Github issue " + Value;
        }
    }
}