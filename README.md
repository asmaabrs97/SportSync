# SportSync - Application de Gestion Sportive

SportSync est une application web moderne construite avec ASP.NET Core qui aide à gérer les activités sportives, les inscriptions et les sessions utilisateurs. Elle offre une expérience fluide tant pour les athlètes que pour les administrateurs.

## Fonctionnalités

- **Authentification Utilisateur**
  - Système sécurisé de connexion et d'inscription
  - Gestion des profils utilisateurs
  - Contrôle d'accès basé sur les rôles (Administrateur, Entraîneur, Utilisateur)

- **Gestion des Sports**
  - Parcourir différentes disciplines sportives
  - Voir les informations détaillées de chaque sport
  - Filtrer les sports par type, niveau et exigences techniques

- **Gestion des Sessions**
  - Planifier et gérer les séances d'entraînment
  - Suivre les présences
  - Gérer les inscriptions

- **Système de Paiement**
  - Traitement sécurisé des paiements
  - Support de plusieurs méthodes de paiement
  - Suivi de l'historique des paiements

- **Gestion des Documents**
  - Télécharger et gérer les certificats médicaux
  - Stocker les documents importants
  - Suivre le statut des documents

# SportSync ⛳️

## Prérequis ⚙️
- **Visual Studio 2022** ou plus récent
- **.NET 8.0 SDK** ([Télécharger](https://dotnet.microsoft.com/download/dotnet/8.0))
- **SQL Server Express** ou supérieur ([Télécharger](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads))

---

## Installation 🚀

### 1. Cloner le projet
```bash
# Cloner le dépôt
git clone https://github.com/asmaabrs97/SportSync.git
cd SportSync
```

### 2. Résolution des erreurs courantes
En cas d'erreurs lors du build, exécutez les commandes suivantes dans l'ordre :
```bash
dotnet clean
rm -r bin/ obj/
```

### 3. Installation des packages
Ajoutez les dépendances nécessaires avec les commandes suivantes :
```bash
dotnet add package Microsoft.EntityFrameworkCore

# ORM Entity Framework Core pour SQL Server
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet add package Microsoft.EntityFrameworkCore.Design

# Authentification
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore

dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

dotnet add package Microsoft.AspNetCore.Identity.UI

# Documentation API
dotnet add package Swashbuckle.AspNetCore

# Paiements et autres outils
dotnet add package Stripe.net
dotnet add package SendGrid
dotnet add package AutoMapper
dotnet add package Newtonsoft.Json
```

### 4. Lancer le projet
Restaurez, compilez et lancez l'application :
```bash
dotnet restore
dotnet build
dotnet run
```
L'application sera disponible à l'adresse **http://localhost:5000** (par défaut).

---

## Fonctionnalités principales 📋
- ✅ **Authentification des utilisateurs**
- ✅ **Gestion des activités sportives**
- ✅ **Système de réservation**
- ✅ **Gestion des paiements**
- ✅ **Interface administrateur**

---

## Structure du projet 📁
```plaintext
SportSync/
├── Controllers/       # Contrôleurs pour l'application
├── Models/            # Modèles de données
├── Views/             # Vues Razor pour le frontend
├── Services/          # Logique métier et services
├── Data/              # Configuration de la base de données
└── wwwroot/           # Contenus statiques (CSS, JS, images)
```

---

## Stack technique 💻
- **Backend** : ASP.NET Core 8.0
- **ORM** : Entity Framework Core
- **BDD** : SQL Server
- **Frontend** : Razor + Bootstrap 5

---

## En développement 🛠️
- Notifications en temps réel
- Application mobile
- Intégration d'API externes
- Dashboard analytique

---

## Contact 📧
Pour toute question ou assistance :
-   [https://github.com/asmaabrs97]

---

## Licence 📄
Ce projet est sous licence **MIT License**. Voir [LICENSE](./LICENSE) pour plus d'informations.
