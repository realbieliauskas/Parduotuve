# Summoners Wardrobe
Summoners Wardrobe is an e-shop powered by Blazor and written in C#, intended to facilitate the sale of digital goods (skins) from the video game "League of Legends" made by Riot Games. As we do not hold any rights to these skins, the skins themselves are not sold in any way - it is a mere imitation, currently, we only provide a certificate of ownership in the form of a text file, following a "purchase".
The project uses Interactive Server rendering in order to provide a smooth user experience, as well as a simple way to implement complex interactive components for the developers.
In order to store data, a SQLite database is used, with the help of MS Entity Framework.
The Bootstrap CSS library is used in order to allow the creation of consistently styled and scalable elements.

# First launch
In order to initialize the project, an empty SQLite database file must be placed within the following path:
> /Parduotuve/Data/Store.db

Afterwards, in order to initialize the database, the following CLI command must be executed within the project path:
> update-database

If the command executes without errors, the project is ready to be built and run!

# Further details and considerations
At its current stage, the website contains leftovers from the Visual Studio Blazor template in addition to insecurely kept credentials and incomplete authentication systems, therefore, while it is functional, it should not be used in any non-development environment.
The location and name of the SQLite database file may be changed within the appsettings.json file within the root directory of the project.
