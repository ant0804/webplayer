﻿using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Regions;
using SpotifyAPI.Web;
using Webplayer.Modules.Spotify.Services;
using Webplayer.Modules.Spotify.ViewModels;
using Webplayer.Modules.Spotify.Views;
using Webplayer.Services.Spotify;

namespace Webplayer.Modules.Spotify
{
    public class SpotifyModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public SpotifyModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            var spotifyWebApi = new SpotifyWebAPI();
            spotifyWebApi.UseAuth = false;
            var provider = new SpotifyWebAPIProvider();
            provider.Api = spotifyWebApi;
            _container.RegisterInstance(provider);

            _container.RegisterType<ISpotifySongSearch, NewSpotifySongService>();

            _container.RegisterType<ISpoifyFindView, SpotifyFindView>();
            _container.RegisterType<ISpotifyFindViewModel, SpotifyFindViewModel>();
            _container.RegisterType<ISpotifyLocalPlayerView, SpotifyLocalPlayerView>();
            _container.RegisterType<ISpotifyLocalPlayerViewModel, SpotifyLocalPlayerViewModel>();
            _container.RegisterType<ISpotifyFindSingleView, SpotifyFindSingleView>();
            _container.RegisterType<ISpotifyFindSingleViewModel, SpotifyFindSingleViewModel>();
            _container.RegisterType<ISpotifyAcountView, SpotifyAcountView>();
            _container.RegisterType<ISpotifyAcountViewModel, SpotifyAcountViewModel>();
            _container.RegisterType<ISpotifyUserService, SpotifyUserService>();

            _regionManager.RegisterViewWithRegion(RegionNames.FindRegion, typeof (TransitionsView));
            _regionManager.RegisterViewWithRegion("SpotTrans", typeof (ISpoifyFindView));
            _regionManager.RegisterViewWithRegion("SpotTrans", typeof (ISpotifyAcountView));
            _regionManager.RegisterViewWithRegion(RegionNames.InfoRegion, typeof(ISpotifyLocalPlayerView));
        }
    }
}
