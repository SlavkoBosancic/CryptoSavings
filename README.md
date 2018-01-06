# CryptoSavings
Portfolio app for crypto currencies

Backend:
In general a .NET Core (Standard) app using a HTTP client to retrieve market info, and repositories to persist purchase data.
- HTTP Client via RestSharp (exchangeable to others)
- HTTP data source https://www.cryptocompare.com (exchangeable to others, coinmarketcap.com in the plans)
- Persistance via LiteDB, a NoSQL single-file (serverless) database engine (exchangeable to others)
- MediatR for in-process notifications
- SignalR for data delivery

Frontend:
- HTML/JS web client (probably Angular) talking to restfull WebAPI (ASP.NET Core)
- WPF desktop client (still in planing)
