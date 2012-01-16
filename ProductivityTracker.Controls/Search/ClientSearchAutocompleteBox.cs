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
    public class ClientSearchAutocompleteBox : CustomAutocompleteBox
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;

        public ClientSearchAutocompleteBox()
        {
            Resources.MergedDictionaries.Add(new ResourceDictionary
                                                 {
                                                     Source = new Uri("/ProductivityTracker.Controls;component/Themes/SearchTemplate.xaml", UriKind.Relative)
                                                 });
            _asyncRequestDispatcherFactory = ServiceLocator.Current.GetInstance<IAsyncRequestDispatcherFactory>();
            ItemTemplate = (DataTemplate) Resources["ClientTemplate"];
        }

        public override void Search(string query)
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetClientsCoveredRequest {Query = query});
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      var clients = Mapper.Map<IEnumerable<ClientDto>, IEnumerable<ClientSearch>>(r.Get<GetClientsCoveredResponse>().Clients);
                                                      Dispatcher.BeginInvoke(new Action(() =>
                                                                                            {
                                                                                                SearchResults = clients;
                                                                                                PopulateComplete();
                                                                                            })); 
                                                  }, e => { });
        }
    }

    public class ClientSearch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IndustryName { get; set; }
        public IEnumerable<ClientPosition> Positions { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ClientPosition
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}