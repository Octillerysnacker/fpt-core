﻿using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.IO;
namespace FPT.Core.Tests.IO
{
    public class MockLevelInitializer : ILevelInitializer
    {
        public string SuppliedLevelId { get; private set; }
        public string SuppliedUser { get; private set; }
        public void InitializeIfNecessary(string levelId, string user)
        {
            SuppliedLevelId = levelId;
            SuppliedUser = user;
        }
    }
}
