using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DIPS.Xamarin.Forms.IssuesRepro
{
    public class IssueService
    {
        private const string m_baseUrl = "https://api.github.com/repos/DIPSAS/DIPS.Xamarin.UI/issues/";
        private static HttpClient m_client = new HttpClient();
        public async Task<IssueModel> GetIssueAsync(int id)
        {
            var result = await m_client.GetStringAsync(m_baseUrl + id);
            return await Task.Run(() => JsonConvert.DeserializeObject<IssueModel>(result));
        }
    }

    public class User
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class IssueModel
    {
        public string url { get; set; }
        public string repository_url { get; set; }
        public string labels_url { get; set; }
        public string comments_url { get; set; }
        public string events_url { get; set; }
        public string html_url { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public int number { get; set; }
        public string title { get; set; }
        public User user { get; set; }
        public string state { get; set; }
        public bool locked { get; set; }
        public object milestone { get; set; }
        public int comments { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? closed_at { get; set; }
        public string author_association { get; set; }
        public object active_lock_reason { get; set; }
        public string body { get; set; }
        public object closed_by { get; set; }
        public object performed_via_github_app { get; set; }
    }

}
