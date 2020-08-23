# identityserver4.test

Simple proof of concept to check policies and scopes for identity server 4 using;
- **.NET Core Identity Server 4**, 
- **.NET Core Web API** and 
- **.NET Core Console Client**. 

The identity server has two registered clients, with 3 scopes;
- My Api Client 
-- `api1scope`
-- `api2scope`
- Groot Client
-- `grootscope`

The API service has 3 configuired policies, that reference the created scopes;
- Api1Policy - `api1scope`
- Api2Policy - `api2scope`
- GrootScope - `grootscope`

and 5 services;
- Identity - must be authorised
- WeatherForecast - not authorised
- Api1 - authorised with Api1Policy
- Api2 - authorised with Api2Policy
- Groot - authorised with GrootPolicy

The console application creats bearer tokens and then accesses the services with the tokens to check access is correctly allowed or rejected. The output ios as follows;
```
Calling: https://localhost:6001/identity as  with : Unauthorized
Calling: https://localhost:6001/api1 as  with : Unauthorized
Calling: https://localhost:6001/api2 as  with : Unauthorized
Calling: https://localhost:6001/groot as  with : Unauthorized
Calling: https://localhost:6001/weatherforecast as  with : OK
=====================
Calling: https://localhost:6001/identity as MyApiApplication with api1scope: OK
Calling: https://localhost:6001/api1 as MyApiApplication with api1scope: OK
Calling: https://localhost:6001/api2 as MyApiApplication with api1scope: Forbidden
Calling: https://localhost:6001/groot as MyApiApplication with api1scope: Forbidden
Calling: https://localhost:6001/weatherforecast as MyApiApplication with api1scope: OK
=====================
Calling: https://localhost:6001/identity as MyApiApplication with api2scope: OK
Calling: https://localhost:6001/api1 as MyApiApplication with api2scope: Forbidden
Calling: https://localhost:6001/api2 as MyApiApplication with api2scope: OK
Calling: https://localhost:6001/groot as MyApiApplication with api2scope: Forbidden
Calling: https://localhost:6001/weatherforecast as MyApiApplication with api2scope: OK
=====================
Calling: https://localhost:6001/identity as MyGrootApp with grootscope: OK
Calling: https://localhost:6001/api1 as MyGrootApp with grootscope: Forbidden
Calling: https://localhost:6001/api2 as MyGrootApp with grootscope: Forbidden
Calling: https://localhost:6001/groot as MyGrootApp with grootscope: OK
Calling: https://localhost:6001/weatherforecast as MyGrootApp with grootscope: OK
```
