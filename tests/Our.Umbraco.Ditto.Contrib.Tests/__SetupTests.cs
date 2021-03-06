﻿using NUnit.Framework;
using Our.Umbraco.Ditto.Mocks;
using Umbraco.Core.Logging;
using Umbraco.Core.ObjectResolution;

namespace Our.Umbraco.Ditto.Contrib.Tests
{
    [SetUpFixture]
    public class __SetupTests
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            // Create a mock logger
            var mockLogger = new MockLogger();
            if (ResolverBase<LoggerResolver>.HasCurrent)
            {
                ResolverBase<LoggerResolver>.Current.SetLogger(mockLogger);
            }
            else
            {
                ResolverBase<LoggerResolver>.Current = new LoggerResolver(mockLogger) { CanResolveBeforeFrozen = true };
            }
        }
    }
}