# cart-service

A WebApi application for manage your cart

## Swagger

Swagger has no authentication
Url: `https://localhost:5001/swagger`

## Hangfire

Hangfire use dashboard
Url: `https://localhost:5001/hangfire`

Jobs:
* `CartCloserJob` - for close expired carts, starts every hour
* `ReportJob` - for create reports, starts in 00:00 every day, reports folder: `\reports`
