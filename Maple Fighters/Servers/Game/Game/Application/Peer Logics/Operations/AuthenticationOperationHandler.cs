﻿using System;
using CommonCommunicationInterfaces;
using CommonTools.Log;
using Database.Common.AccessToken;
using ServerApplication.Common.ApplicationBase;
using ServerCommunicationHelper;
using Shared.Game.Common;

namespace Game.Application.PeerLogic.Operations
{
    internal class AuthenticationOperationHandler : IOperationRequestHandler<AuthenticateRequestParameters, AuthenticateResponseParameters>
    {
        private readonly int peerId;
        private readonly Action<int> onAuthenticated;
        private readonly LocalDatabaseAccessTokens databaseAccessTokens;
        private readonly DatabaseAccessTokenExistence databaseAccessTokenExistence;
        private readonly DatabaseAccessTokenProvider databaseAccessTokenProvider;
        private readonly DatabaseUserIdViaAccessTokenProvider databaseUserIdViaAccessTokenProvider;

        public AuthenticationOperationHandler(int peerId, Action<int> onAuthenticated)
        {
            this.peerId = peerId;
            this.onAuthenticated = onAuthenticated;

            databaseAccessTokens = Server.Entity.Container.GetComponent<LocalDatabaseAccessTokens>().AssertNotNull();
            databaseAccessTokenExistence = Server.Entity.Container.GetComponent<DatabaseAccessTokenExistence>().AssertNotNull();
            databaseAccessTokenProvider = Server.Entity.Container.GetComponent<DatabaseAccessTokenProvider>().AssertNotNull();
            databaseUserIdViaAccessTokenProvider = Server.Entity.Container.GetComponent<DatabaseUserIdViaAccessTokenProvider>().AssertNotNull();
        }

        public AuthenticateResponseParameters? Handle(MessageData<AuthenticateRequestParameters> messageData, ref MessageSendOptions sendOptions)
        {
            var accessToken = messageData.Parameters.AccessToken;

            if (databaseAccessTokens.Exists(accessToken))
            {
                return new AuthenticateResponseParameters(AuthenticationStatus.Failed);
            }

            if (!databaseAccessTokenExistence.Exists(accessToken))
            {
                return new AuthenticateResponseParameters(AuthenticationStatus.Failed);
            }

            var userId = databaseUserIdViaAccessTokenProvider.GetUserId(accessToken);

            if (databaseAccessTokenProvider.GetAccessToken(userId) != accessToken)
            {
                return new AuthenticateResponseParameters(AuthenticationStatus.Failed);
            }

            databaseAccessTokens.Add(peerId, accessToken);

            onAuthenticated.Invoke(userId);
            return new AuthenticateResponseParameters(AuthenticationStatus.Succeed);
        }
    }
}