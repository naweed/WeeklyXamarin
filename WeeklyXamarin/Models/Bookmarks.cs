using System;
using System.Collections.Generic;
using System.Linq;

namespace WeeklyXamarin.Models
{
    public class Bookmarks
    {
        public List<Article> SavedArticles { get; set; } = new List<Article>();

        public void Add(Article atricle)
        {
            if(!SavedArticles.Any(_artcile => _artcile.Id == atricle.Id))
            {
                SavedArticles.Add(atricle);
            }
        }

        public void Remove(Article atricle)
        {
            if (SavedArticles.Any(_artcile => _artcile.Id == atricle.Id))
            {
                SavedArticles.Remove(atricle);
            }
        }

    }
}
