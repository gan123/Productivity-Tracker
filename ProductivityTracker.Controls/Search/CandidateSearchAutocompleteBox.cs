using System;
using System.Collections.Generic;
using System.Windows;
using Agatha.Common;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;

namespace ProductivityTracker.Controls.Search
{
    public class CandidateSearchAutocompleteBox : CustomAutocompleteBox
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;

        public CandidateSearchAutocompleteBox()
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary
                                                 {
                                                     Source =
                                                         new Uri(
                                                         "/ProductivityTracker.Controls;component/Themes/SearchTemplate.xaml",
                                                         UriKind.Relative)
                                                 });
            _asyncRequestDispatcherFactory = ServiceLocator.Current.GetInstance<IAsyncRequestDispatcherFactory>();
            ItemTemplate = (DataTemplate)Resources["CandidateTemplate"];
        }

        public override void Search(string query)
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetCandidatesRequest { Query = query });
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      var candidates = Mapper.Map<IEnumerable<CandidateDto>, IEnumerable<CandidateSearch>>(r.Get<GetCandidatesResponse>().Candidates);
                                                      Dispatcher.BeginInvoke(new Action(() =>
                                                                                            {
                                                                                                SearchResults = candidates;
                                                                                                PopulateComplete();
                                                                                            }));
                                                  }, e => { });
        }
    }

    public class CandidateSearch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}