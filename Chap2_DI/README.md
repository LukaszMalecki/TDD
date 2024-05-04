# Dependency Injection using API example - Chapter 2

The goal of this chapter was to teach about DI and how to transform an existing project into one using DI.

The task was make an API app which can return random and real weather data and then to change implementation to support Dependency Injection.

The solution consists of a .net web API app.

Some of the subjects discussed in the chapter:
- API
- dependency
- abstraction and concrete types
- good OOP practices
- DI
- seams
- service lifetime
- stubbed response

## Additional Notes:
- Added ClientThreeOne class because OpenWeatherMap doesn't support OneCall 2.5 anymore
- To use Real API you need an API key from OpenWeatherMap with a OneCall 3.0 service activated (key should then be added to appsettings)
- You can change LoadTest in appsettings to true, to have RealAPI not use OpenWeatherMap and just give a random response
