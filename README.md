# Redmango API

Redmango API est une API Web ASP.NET Core qui permet la gestion des données d'un restaurant, y compris la gestion de photos stockées dans Azure Blob Storage et une base de données Azure SQL Server. L'API prend en charge l'authentification et la gestion des rôles à l'aide de tokens JWT pour les clients React, ainsi que la personnalisation de Swagger pour une meilleure expérience de développement.

## Fonctionnalités

- Gestion des plats, menus et informations sur le restaurant.
- Stockage et récupération de photos associées aux plats.
- Stockage des données dans une base de données Azure SQL Server.
- Authentification des utilisateurs avec des tokens JWT.
- Gestion des rôles pour les utilisateurs.
- Personnalisation de Swagger pour la documentation de l'API.
- API RESTful pour l'accès aux données.

## Technologies Utilisées

- ASP.NET Core : Framework de développement web pour les applications web modernes.
- Entity Framework Core : Outil de mappage objet-relationnel pour interagir avec la base de données.
- Azure Blob Storage : Service de stockage d'objets dans le cloud Azure pour la gestion des photos.
- Azure SQL Server : Base de données cloud pour le stockage des données du restaurant.
- C# : Langage de programmation principal pour le développement de l'API.
- React : Bibliothèque JavaScript pour la création de l'application cliente.
- JWT (JSON Web Tokens) : Méthode d'authentification pour les utilisateurs.
- Swagger : Outil de documentation d'API personnalisable.

## Configuration

Pour configurer l'API, assurez-vous d'avoir les éléments suivants :

- Un compte Azure Storage avec des informations d'authentification pour le stockage des photos.
- Une base de données Azure SQL Server avec des informations de connexion.
- La configuration de l'authentification et de la gestion des rôles pour les tokens JWT.
- La personnalisation de Swagger pour l'authentification.

## Installation

1. Clonez ce référentiel sur votre machine locale.
2. Exécutez `dotnet build` pour compiler le projet.
3. Configurez les informations d'authentification Azure pour le stockage des photos, la base de données Azure SQL Server et la configuration JWT.
4. Exécutez l'API avec `dotnet run`.

Assurez-vous de consulter la documentation complète de l'API pour plus de détails sur les endpoints.

## Authentification et Gestion des Rôles

L'authentification des utilisateurs est gérée à l'aide de tokens JWT. Les rôles peuvent être définis pour contrôler les autorisations des utilisateurs.

## Personnalisation de Swagger

Swagger est configuré pour documenter les endpoints API et permettre l'authentification à l'aide de tokens JWT. Assurez-vous de consulter la documentation Swagger pour tester les endpoints protégés.
