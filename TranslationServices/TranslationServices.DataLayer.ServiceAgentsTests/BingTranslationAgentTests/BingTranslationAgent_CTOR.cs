﻿using Moq;
using OnlineServices.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using TranslationServices.DataLayer.ServiceAgents;
using Xunit;
using Serilog;
using TranslationServices.DataLayer.ServiceAgents.TranslationAgents;

namespace TranslationServices.DataLayer.ServiceAgentsTests
{
    public class BingTranslationAgent_CTOR
    {
        [Fact]
        public void CTOR_ThrowsIsNullOrWhiteSpaceException_WhenAPIKeyIsNull()
        {
            //ARRANGE
            var mockILogger = TestHelper.MockILogger();
            string APIKey = null;
            
            //ACT & ASSERT
            Assert.Throws<IsNullOrWhiteSpaceException>(() => new BingTranslationAgent(mockILogger.Object, APIKey));
            mockILogger.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CTOR_ThrowsIsNullOrWhiteSpaceException_WhenAPIKeyIsEmpty()
        {
            //ARRANGE
            var mockILogger = TestHelper.MockILogger();
            string APIKey = "";

            //ACT & ASSERT
            Assert.Throws<IsNullOrWhiteSpaceException>(() => new BingTranslationAgent(mockILogger.Object, APIKey));
            mockILogger.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CTOR_ThrowsIsNullOrWhiteSpaceException_WhenAPIKeyIsWhitSpace()
        {
            //ARRANGE
            var mockILogger = TestHelper.MockILogger();
            string APIKey = "  ";

            //ACT & ASSERT
            Assert.Throws<IsNullOrWhiteSpaceException>(() => new BingTranslationAgent(mockILogger.Object, APIKey));
            mockILogger.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CTOR_ThrowsArgumentNullException_WhenIloggerIsNull()
        {
            //ARRANGE
            ILogger iLogger = null;
            string APIKey = "Hello, World!";

            //ACT & ASSERT
            Assert.Throws<ArgumentNullException>(() => new BingTranslationAgent(iLogger, APIKey));
        }
    }
}
