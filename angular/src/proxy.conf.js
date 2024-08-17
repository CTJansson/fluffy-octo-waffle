const { env } = require('process');

const target = "https://localhost:5002"
// const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
//     env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:5001';

    console.log(target);

const PROXY_CONFIG = [
  {
    context: [
      // BFF Management Routes
      "/bff",

      // OIDC Handler Routes
      "/signin-oidc",
      "signout-callback-oidc",

      // API Routes
      "/todos",
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;