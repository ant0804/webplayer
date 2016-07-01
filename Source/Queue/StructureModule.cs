﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Infrastructure.Models;
using Webplayer.Modules.Structure.ViewModels;
using Webplayer.Modules.Structure.Views;

namespace Queue
{
    public class StructureModule : IModule
    {
        private IRegionManager _rm;
        private IUnityContainer _container;

        public StructureModule(IRegionManager rm, IUnityContainer container)
        {
            _rm = rm;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterInstance<IPlaylist>(SharedResourcesNames.QueuePlaylist, new SimplePlaylist());
            _container.RegisterType<IQueueView, QueueView>().
                RegisterType<IQueueViewModel, QueueViewModel>();

            _rm.RegisterViewWithRegion(RegionNames.QueueRegion, typeof(IQueueView));
        }
    }
}