using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeolabPortfolio.Database;
using GeolabPortfolio.Models;

namespace GeolabPortfolio.Extensions
{
    public class ProjectFilter
    {
        GeolabPortfolioDBContext context = new GeolabPortfolioDBContext();

        public List<ProjectList> ProjectFilterByTextAndTags(string words, string tags)
        {
            string filterWords = FilterProjectKeywords(words);
            List<ProjectList> items = new List<ProjectList>();

            tags = tags.Replace(' ', ',');

            if (filterWords != string.Empty)
            {

                items = context.Database
                            .SqlQuery<ProjectList>("exec FilterProjectByProjectIdAndTags '" + filterWords + "','" + tags + "'").ToList();
            }

            return items;
        }

        public List<ProjectList> ProjectFilterByText(string words)
        {
            string ids = FilterProjectKeywords(words);

            if (ids == string.Empty)
            {
                return new List<ProjectList>();
            }

            string command = "FilterProjectByProjectId '" + ids + "'";
            var items = context.Database.SqlQuery<ProjectList>(command).ToList();
            return items;
        }

        private string FilterProjectKeywords(string words)
        {
            List<string> splitWords = WordSplitter.SplitWords(words, ' ');
            HashSet<int> hash = new HashSet<int>();

            foreach (var project in context.Projects.ToList())
            {
                if (ContainStringTheListItem(project.Name, splitWords) || ContainStringTheListItem(project.Description, splitWords))
                {
                    hash.Add(project.Id);
                }
            }

            foreach (var tag in context.Tags.ToList())
            {
                if (ContainStringTheListItem(tag.Name, splitWords))
                {
                    foreach (var projectTag in context.ProjectTags.Where(x => x.TagId == tag.Id).ToList())
                    {
                        hash.Add(projectTag.ProjectId);
                    }
                }
            }

            foreach (var author in context.Authors.ToList())
            {
                if (ContainStringTheListItem(author.LastName.ToLower(), splitWords) || ContainStringTheListItem(author.FirstName.ToLower(), splitWords))
                {
                    foreach (var project in context.Projects.Where(x => x.AuthorId == author.Id).ToList())
                    {
                        hash.Add(project.Id);
                    }
                }
            }

            return GetHashString(hash);
        }

        private string GetHashString(HashSet<int> hash)
        {
            StringBuilder builder = new StringBuilder();

            if (hash.Count > 0)
            {
                int[] array = hash.ToArray();

                for (int i = 0; i < array.Length; i++)
                {
                    builder.Append(array[i].ToString());

                    if (i != array.Length - 1)
                    {
                        builder.Append(",");
                    }
                }

            }

            return builder.ToString();
        }

        public bool ContainStringTheListItem(string name, List<string> words)
        {
            name.ToLower();
            foreach (string word in words)
            {
                if (name.Contains(word))
                {
                    return true;
                }
            }
            return false;
        }
    }
}