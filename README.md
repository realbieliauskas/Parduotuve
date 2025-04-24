# Summoners Wardrobe

Summoners Wardrobe is an e-shop powered by Blazor and written in C#, designed to facilitate the sale of digital goods (skins) inspired by the video game "League of Legends" by Riot Games. **Note:** This project does not sell or distribute actual skins. Instead, it provides a certificate of ownership in the form of a text file as a novelty item following a "purchase." 

The project leverages **Interactive Server Rendering** to deliver a smooth user experience and simplify the implementation of complex interactive components. Data is stored using a **SQLite database** with the help of **Entity Framework Core**, and the **MudBlazor Component library** is used to create consistently styled and scalable UI elements.

---

## Features
- **Blazor Interactive Server Rendering** for responsive and dynamic user interfaces.
- **SQLite Database** for lightweight and efficient data storage.
- **Entity Framework Core** for database management and migrations.
- **Bootstrap CSS** for consistent and scalable styling.
- A simple e-commerce flow for purchasing digital certificates.

---

## First Launch & Configuration

The project currently contains placeholder credentials and an incomplete authentication system. **Do not use this project in a production environment.**

To set up and run the project, follow these steps:

1. In order to initialize the project, an empty SQLite database file must be placed within the following path:

/Parduotuve/Data/Store.db

2. Afterwards, in order to initialize the database, the following CLI command must be executed within the project path:

update-database

3. Lastly, create an appsettings.json file in the root of the project with the following structure:
```json
{
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "StoreDB": "Data Source=Data\\Store.db"
  },
  "StripeAPIKey": "your-stripe-api-key-here",
  "StripeWebHookSecret": "your-stripe-webhook-secret-here",
  "SmtpServerUrl": "your-smtp-server-url-here",
  "SmtpUsername": "your-smtp-username-here",
  "SmtpPassword": "your-smtp-password-here",
  "SmtpPort": "your-smtp-port-here"
}
```
Note: Never commit your appsettings.json with real credentials or secrets to git.

If all previous commands executed without errors, the project is ready to be built and run!

---

## Known Limitations
- Authentication and security features are incomplete and should not be relied upon.
- This project is intended for development and demonstration purposes only.

---

## Future Improvements
- Implement a secure and complete authentication system.
- Improve the certificate generation process.

---

## License
This project is for educational and demonstration purposes only. It is not affiliated with or endorsed by Riot Games.

---

## Acknowledgments
- **Riot Games** for inspiring the concept with "League of Legends."
- **Microsoft Blazor** for enabling interactive web development.
- **MudBlazor** for providing a robust Razor component library.
