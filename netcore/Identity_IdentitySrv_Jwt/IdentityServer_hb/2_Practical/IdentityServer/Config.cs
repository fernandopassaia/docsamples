// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                //aqui eu adiciono um Research igual o nome do meu AllowedScopes lá embaixo (notafiscal)
                new ApiResource("notafiscal", "My API of Tests for Fiscal Parts")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                //aqui eu vou adicionar meus CLIENTS... essa informação precisa estar no meu App...
                //Meu Client poderia estar no banco de dados, claro... Note que isso retorna um Array...
                
                new Client
                {
                    ClientId = "mvc.implicit",
                    ClientName = "Minha aplicação Web MVC",
                    ClientSecrets = { new Secret("@1234Fd@".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    
                    //nota: eu preciso especificar qual é a minha URL de retorno... veja a imagem 12
                    RedirectUris = { "http://localhost:5001/signin-oidc" }, //lista de URL - pode ser HTTPs, DNS, IP Válido, localhost
                    //nota: Eu poderia colocar várias URLs, URLS de dominio próprio, Whathever...

                    AllowedScopes = {
                        "notafiscal", //isso aponta para uma API minha ali em cima no GetApis()
                        "openid",
                        "profile",
                        //IdentityServerConstants.StandardScopes.OpenId,
                        //IdentityServerConstants.StandardScopes.Profile
                    }
                },

                #region Credentials comentadas (estavam conflitando)
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // MVC client using hybrid flow
                //new Client
                //{
                //    ClientId = "mvc",
                //    ClientName = "MVC Client",

                //    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                //    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                //    RedirectUris = { "http://localhost:5001/signin-oidc" },
                //    FrontChannelLogoutUri = "http://localhost:5001/signout-oidc",
                //    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

                //    AllowOfflineAccess = true,
                //    AllowedScopes = { "openid", "profile", "api1" }
                //},

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes = { "openid", "profile", "api1" }
                }
                #endregion
            };
        }
    }
}