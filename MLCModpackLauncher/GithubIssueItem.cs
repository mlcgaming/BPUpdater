using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MLCModpackLauncher
{
    public class GithubIssueItem
    {
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string Assignee { get; private set; }
        public int Milestone { get; private set; }
        public List<string> Labels { get; private set; }

        public GithubIssueItem(string title, string body, params string[] labels)
        {
            Title = title;
            Body = body;
            Assignee = "mlcgaming";
            Milestone = 1;
            Labels = new List<string>();
            if(labels.Count() > 0)
            {
                foreach(string label in labels)
                {
                    Labels.Add(label);
                }
            }
        }

        public static void SendTicket(string issueJson, Exception ex = null)
        {
            WebRequest request = WebRequest.Create("https://api.github.com/repos/mlcgaming/BPUpdater/issues");
            request.Method = "POST";
            string postData = issueJson;
            byte[] byteArray = null;
            if (ex == null)
            {
                byteArray = Encoding.UTF8.GetBytes(postData);
            }
            else
            {
                byteArray = Encoding.UTF8.GetBytes(string.Format(postData, ex));
            }
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
        }
    }
}
